using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MvcSample.Books
{
    public class BookDetail : Entity
    {
        [Key]
        public virtual Guid BookId { get; protected set; }
        
        [CanBeNull]
        public virtual string Outline { get; protected set; }

        protected BookDetail()
        {
        }
        
        public BookDetail(Guid bookId, [CanBeNull] string outline)
        {
            BookId = bookId;
            Outline = outline;
        }
        
        public override object[] GetKeys()
        {
            return new object[] {BookId};
        }
    }
}