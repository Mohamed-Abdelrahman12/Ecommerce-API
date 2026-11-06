

namespace Ecom.Application.Sharing
{
    public class ProductParams
    {

        public string Sort { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? Rating { get; set; }
        public string Search { get; set; } = string.Empty;
        public int MaxPageSize { get; set; } = 6;

        private int _pageSize = 3;
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value ; }
        }

        public int PageNumber { get; set; } = 1;

    }
}
