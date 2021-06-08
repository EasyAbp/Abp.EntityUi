using MvcSample.Books;
using MvcSample.Books.Dtos;
using AutoMapper;

namespace MvcSample
{
    public class MvcSampleApplicationAutoMapperProfile : Profile
    {
        public MvcSampleApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>(MemberList.Source);
        }
    }
}
