using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MvcSample.Books
{
    public class BookTag : Entity
    {
        [Key, Column(Order = 0)]
        public virtual Guid BookId { get; protected set; }
        
        [Key, Column(Order = 1)]
        [NotNull]
        public virtual string Tag { get; protected set; }

        protected BookTag()
        {
        }
        
        public BookTag(Guid bookId, [NotNull] string tag)
        {
            BookId = bookId;
            Tag = tag;
        }
        
        public override object[] GetKeys()
        {
            return new object[] {BookId, Tag};
        }
    }
}