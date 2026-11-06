

namespace Ecom.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IRatingRepository RatingRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public ICustomerCartRepository CustomerCartRepository { get; }
        public IAuthRepository AuthRepository { get; }
        public IUsersRepository usersRepository { get; }
        public IWishListRepository wishListRepository { get; }
        public IContactRepository ContactRepository { get; }
    }
}
