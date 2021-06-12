using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace MvcSample.Books
{
    public class Book : AggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string Isbn { get; protected set; }
        
        public virtual int Length { get; protected set; }
        
        [ForeignKey(nameof(Id))]
        public virtual BookDetail Detail { get; protected set; }
        
        public virtual List<BookTag> Tags { get; protected set; }

        protected Book()
        {
        }

        public Book(
            Guid id,
            Guid? tenantId,
            string name,
            string isbn,
            int length,
            BookDetail detail,
            List<BookTag> tags
        ) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            Isbn = isbn;
            Length = length;
            Detail = detail;
            Tags = tags;
        }
    }
}
