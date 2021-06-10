using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class PropertyShowInValueObject : ValueObject, IPropertyShowIn
    {
        public virtual bool List { get; private set; }
        
        public virtual bool Detail { get; private set; }
        
        public virtual bool Creation { get; private set; }
        
        public virtual bool Edit { get; private set; }

        protected PropertyShowInValueObject()
        {
        }
        
        public PropertyShowInValueObject(bool list, bool detail, bool creation, bool edit)
        {
            List = list;
            Detail = detail;
            Creation = creation;
            Edit = edit;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return List;
            yield return Detail;
            yield return Creation;
            yield return Edit;
        }
    }
}