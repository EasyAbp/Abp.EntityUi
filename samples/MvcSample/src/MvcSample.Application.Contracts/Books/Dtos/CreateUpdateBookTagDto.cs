using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class CreateUpdateBookTagDto
    {
        [DisplayName("BookTagsTag")]
        public string Tag { get; set; }
    }
}