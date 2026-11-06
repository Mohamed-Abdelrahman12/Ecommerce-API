

using AutoMapper;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Infastructure.Data;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace Ecom.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        private readonly UserManager<AppUser> _userManager;
       private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IGenerateToken _generateToken;

        public ICategoryRepository CategoryRepository { get; }
        public IBrandRepository BrandRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public ICustomerCartRepository CustomerCartRepository { get; }
        public IConnectionMultiplexer Redis { get; }
        public IRatingRepository RatingRepository
        {
            get;
        }
        public IAuthRepository AuthRepository { get; }
        public IUsersRepository usersRepository { get; }

        public IWishListRepository wishListRepository { get; }
        public IContactRepository   ContactRepository { get; }

        public UnitOfWork(AppDbContext context, IMapper mapper,
            IImageManagementService imageManagementService,
            IConnectionMultiplexer redis, UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken, IWishListRepository _wishListRepository, RoleManager<IdentityRole> roleManager )

        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            Redis = redis;
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _generateToken = generateToken;
            _roleManager = roleManager;
            CategoryRepository = new CategoryRepository(_context);
            BrandRepository = new BrandRepository(_context);
            ProductRepository = new ProductRepository(_context, _imageManagementService);
            PhotoRepository = new PhotoRepository(_context);
            CustomerCartRepository = new CustomerCartRepository(redis);
            AuthRepository = new AuthRepository(_userManager, _signInManager, _roleManager, _emailService, _generateToken, _context);
            wishListRepository = new WishListRepository(_context);
            usersRepository = new UsersRepository(_userManager, _roleManager);
            RatingRepository = new RatingRepository(_context, _userManager);
            ContactRepository = new ContactRepository(_context);
        }
    }
}
