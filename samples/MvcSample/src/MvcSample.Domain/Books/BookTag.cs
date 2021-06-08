using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MvcSample.Books
{
    public class BookTag : Entity
    {
        public virtual Guid BookId { get; protected set; }
        
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