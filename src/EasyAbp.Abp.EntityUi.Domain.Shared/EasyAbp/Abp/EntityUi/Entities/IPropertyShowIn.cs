using System.Collections.Generic;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public interface IPropertyShowIn
    {
        bool List { get; }
        
        bool Detail { get; }
        
        bool Creation { get; }
        
        bool Edit { get; }
    }
}