using AutoMapper;
using Persistence.Entities;
using Services.Models;

namespace Services
{
    public static class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<BookListEntity, BookList>();
            Mapper.CreateMap<BookEntity, Book>();
            
            Mapper.AssertConfigurationIsValid();
        }
    }
}
