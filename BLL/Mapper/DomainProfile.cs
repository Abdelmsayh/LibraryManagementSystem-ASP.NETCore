using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()) 
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<BorrowingTransaction, BorrowingTransactionDTO>().ReverseMap();

            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<ReservationTransaction, ReservationTransactionDTO>().ReverseMap();

            //CreateMap<Role, RoleDTO>().ReverseMap();
            //CreateMap<UserInRole, UserInRoleDTO>().ReverseMap();
            //CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
