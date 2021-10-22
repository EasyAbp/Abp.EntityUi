using System.Linq;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi
{
    public class MenuItemNameCalculator : IMenuItemNameCalculator, ITransientDependency
    {
        public virtual string GetName(string moduleName)
        {
            return $"{moduleName}";
        }

        public virtual string GetName(string moduleName, string aggregateRootName)
        {
            return $"{moduleName}.{aggregateRootName}";
        }

        public virtual string GetDisplayName(string moduleName)
        {
            return $"Menu:{moduleName.Split('.').Last()}";
        }

        public virtual string GetDisplayName(string moduleName, string aggregateRootName)
        {
            return $"Menu:{aggregateRootName}";
        }
    }
}