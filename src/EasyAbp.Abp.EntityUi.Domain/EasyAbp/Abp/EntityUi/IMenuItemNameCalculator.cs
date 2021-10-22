using System.Threading.Tasks;

namespace EasyAbp.Abp.EntityUi
{
    public interface IMenuItemNameCalculator
    {
        string GetName(string moduleName);

        string GetName(string moduleName, string aggregateRootName);
        
        string GetDisplayName(string moduleName);

        string GetDisplayName(string moduleName, string aggregateRootName);
    }
}