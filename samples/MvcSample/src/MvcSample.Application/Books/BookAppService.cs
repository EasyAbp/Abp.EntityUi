using System;
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
    }
}
