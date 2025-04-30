using AutoMapper;
using LibraryManagement.DTOs;
using LibraryManagement.Models;
using Microsoft.Identity.Client;

namespace LibraryManagement.Mappers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<Member, AddMemberDto>().ReverseMap();
            CreateMap<Member, UpdateMemberDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
            CreateMap<Borrowing, BorrowingDto>().ReverseMap();
            CreateMap<Borrowing, AddBorrowingDto>().ReverseMap();
        }
    }
}
