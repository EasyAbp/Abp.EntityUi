using System;
using System.Linq;
using System.Threading.Tasks;
using MvcSample.Permissions;
using MvcSample.Books.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MvcSample.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto, CreateUpdateBookDto>,
        IBookAppService
    {
        protected override string GetPolicyName { get; set; } = MvcSamplePermissions.Book.Default;
        protected override string GetListPolicyName { get; set; } = MvcSamplePermissions.Book.Default;
        protected override string CreatePolicyName { get; set; } = MvcSamplePermissions.Book.Create;
        protected override string UpdatePolicyName { get; set; } = MvcSamplePermissions.Book.Update;
        protected override string DeletePolicyName { get; set; } = MvcSamplePermissions.Book.Delete;

        private readonly IBookRepository _repository;
        
        public BookAppService(IBookRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override Task<Book> MapToEntityAsync(CreateUpdateBookDto createInput)
        {
            var id = GuidGenerator.Create();

            var book = new Book(
                id,
                CurrentTenant.Id,
                createInput.Name,
                createInput.Isbn,
                createInput.Length,
                new BookDetail(id, createInput.Detail.Outline),
                createInput.Tags.Select(t => new BookTag(id, t.Tag)).ToList());

            return Task.FromResult(book);
        }

        protected override Task MapToEntityAsync(CreateUpdateBookDto updateInput, Book entity)
        {
            entity.Update(
                updateInput.Name,
                updateInput.Isbn,
                updateInput.Length,
                new BookDetail(entity.Id, updateInput.Detail.Outline),
                updateInput.Tags.Select(t => new BookTag(entity.Id, t.Tag)).ToList());

            return Task.CompletedTask;
        }
    }
}
