using AutoMapper;
using BookstoreAPI.DTOs;
using BookstoreAPI.Models;

namespace BookstoreAPI.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TblBook, BookListDTO>();
            CreateMap<LoginDTO, TblUser>();
            CreateMap<RegisterDTO, TblUser>();
        }
    }
}