//using AutoMapper;
//using Ecom.Application.DTOs;
//using Ecom.Application.Interfaces;
//using Ecom.Core.Services;
//using Ecom.Domain.Entities.Order;
//using Ecom.Infastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Ecom.infrastructure.Repositories.service
//{
//    public class OrderService : IOrderService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly AppDbContext _context;
//        private readonly IMapper _mapper;
//        private readonly IPaymentService _paymentService;

//        public OrderService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper, IPaymentService paymentService)
//        {
//            _unitOfWork = unitOfWork;
//            _context = context;
//            _mapper = mapper;
//            _paymentService = paymentService;
//        }

//        public async Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string BuyerEmail)
//        {
//            var cart = await _unitOfWork.CustomerCartRepository.GetCartAsync(orderDTO.cartId);

//            List<OrderItem> orderItems = new List<OrderItem>();

//            foreach (var item in cart.cartItems) {
//                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);

//                OrderItem orderitem = new(product.Id, item.Image, product.Name, item.Price, item.Quantity);

//                orderItems.Add(orderitem);
//            }
//            var deliveryMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);
//            var subTotal = orderItems.Sum(m => m.Price * m.Quantity);
//            var ship = _mapper.Map<ShippingAddress>(orderDTO.ShippingAddress);
//            var ExsistOrders = await _context.Orders.Where(m=>m.PaymentIntentId == cart.PaymentIntentId).FirstOrDefaultAsync();
//            if (ExsistOrders is not null)
//            {
//                _context.Orders.Remove(ExsistOrders);
//              await  _paymentService.CreateOrUpdateAsync(cart.PaymentIntentId, deliveryMethod.Id);
//            }
//            var order = new Orders(BuyerEmail, subTotal, ship, deliveryMethod,orderItems,cart.PaymentIntentId);

//            await _context.Orders.AddAsync(order);
//            await _context.SaveChangesAsync();
//           await _unitOfWork.CustomerCartRepository.DeleteCartAsync(orderDTO.cartId);
//            return order;
//        }

//        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
//        {
//          return  await _context.DeliveryMethods.AsNoTracking().ToListAsync();
//        }

//        public async Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync(string BuyerEmail)
//        {
//           var orders = await _context.Orders.Where(m=>m.BuyerEmail ==  BuyerEmail)
//                 .Include(m => m.orderItems).Include(m => m.deliveryMethod)
//                .ToListAsync();
//            var result =  _mapper.Map<IReadOnlyList<OrderResponse>>(orders);
//            return result;
//        }

//        public async Task<OrderResponse> GetOrdersById(int id, string BuyerEmail)
//        {
//            var order = await _context.Orders.Where(m => m.Id == id && m.BuyerEmail == BuyerEmail)
//                .Include(m=>m.orderItems).Include(m => m.deliveryMethod)
//                .FirstOrDefaultAsync();
//            var result = _mapper.Map<OrderResponse>(order);
//            return result;

//        }
//    }
//}
using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Order;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories.service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(
            IUnitOfWork unitOfWork,
            AppDbContext context,
            IMapper mapper,
            IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        // ── Create Order ────────────────────────────────────────────────────────
        public async Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string buyerEmail)
        {
            // 1. Load cart
            var cart = await _unitOfWork.CustomerCartRepository.GetCartAsync(orderDTO.cartId);
            if (cart is null)
                throw new Exception("Cart not found.");

            // 2. Build order items — prices always come from DB, never from the cart/client
            var orderItems = new List<OrderItem>();
            foreach (var item in cart.cartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                if (product is null)
                    throw new Exception($"Product with id {item.Id} not found.");

                orderItems.Add(new OrderItem(
                    product.Id,
                    item.Image,
                    product.Name,
                    product.NewPrice,   // always trust DB price
                    item.Quantity
                ));
            }

            // 3. Load delivery method
            var deliveryMethod = await _context.DeliveryMethods
                .FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery method not found.");

            // 4. If an order already exists for this PaymentIntent, remove it first
            //    then refresh the PaymentIntent amount in Stripe before re-creating
            if (!string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var existingOrder = await _context.Orders
                    .FirstOrDefaultAsync(o => o.PaymentIntentId == cart.PaymentIntentId);

                if (existingOrder is not null)
                {
                    _context.Orders.Remove(existingOrder);
                    await _context.SaveChangesAsync();

                    // Refresh Stripe PaymentIntent with latest totals
                    await _paymentService.CreateOrUpdateAsync(orderDTO.cartId, deliveryMethod.Id);

                    // Reload cart — CreateOrUpdateAsync updates the cart in Redis
                    cart = await _unitOfWork.CustomerCartRepository.GetCartAsync(orderDTO.cartId);
                }
            }

            // 5. Build and persist the order
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);
            var shippingAddress = _mapper.Map<ShippingAddress>(orderDTO.ShippingAddress);

            var order = new Orders(
                buyerEmail,
                subTotal,
                shippingAddress,
                deliveryMethod,
                orderItems,
                cart.PaymentIntentId    // links order to Stripe — webhook uses this to find the order
            );

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // 6. Delete cart — order is now the source of truth
            await _unitOfWork.CustomerCartRepository.DeleteCartAsync(orderDTO.cartId);

            return order;
        }

        // ── Queries ─────────────────────────────────────────────────────────────
        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _context.Orders
                .Where(o => o.BuyerEmail == buyerEmail)
                .Include(o => o.orderItems)
                .Include(o => o.deliveryMethod)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IReadOnlyList<OrderResponse>>(orders);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id && o.BuyerEmail == buyerEmail)
                .Include(o => o.orderItems)
                .Include(o => o.deliveryMethod)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order is null)
                return null;

            return _mapper.Map<OrderResponse>(order);
        }
    }
}