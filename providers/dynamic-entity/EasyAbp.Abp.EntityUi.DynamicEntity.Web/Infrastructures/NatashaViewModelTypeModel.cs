using System;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public class NatashaViewModelTypeModel
    {
        public Type CreationViewModelType { get; set; }
        
        public Type EditViewModelType { get; set; }
        
        public DateTime? EntityLastModificationTime { get; set; }
    }
}