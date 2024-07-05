using AutoMapper;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;

namespace Bags_Wallets.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<PageImageViewModel, PageImage>().ReverseMap();
            CreateMap<ContactUsViewModel, ContactUs>().ReverseMap();
            CreateMap<AboutUsViewModel, AboutUs>().ReverseMap();
            CreateMap<ContactInfoViewModel, ContactInfo>().ReverseMap();
            CreateMap<CommentViewModel, Comment>().ReverseMap();
            CreateMap<SellerViewModel, Seller>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartViewModel>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            CreateMap<Wishlist, WishlistViewModel>().ReverseMap();
            CreateMap<WishlistItem, WishlistItemViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            //CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, EditUserViewModel>().ReverseMap();

            //CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>()
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            //CreateMap<OrderItem, OrderItemViewModel>()
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}
