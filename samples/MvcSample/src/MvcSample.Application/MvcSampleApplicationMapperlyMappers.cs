using MvcSample.Books;
using MvcSample.Books.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace MvcSample
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class BookToBookDtoMapper : MapperBase<Book, BookDto>
    {
        public override partial BookDto Map(Book source);

        public override partial void Map(Book source, BookDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class BookDetailToBookDetailDtoMapper : MapperBase<BookDetail, BookDetailDto>
    {
        public override partial BookDetailDto Map(BookDetail source);

        public override partial void Map(BookDetail source, BookDetailDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class BookTagToBookTagDtoMapper : MapperBase<BookTag, BookTagDto>
    {
        public override partial BookTagDto Map(BookTag source);

        public override partial void Map(BookTag source, BookTagDto destination);
    }
}
