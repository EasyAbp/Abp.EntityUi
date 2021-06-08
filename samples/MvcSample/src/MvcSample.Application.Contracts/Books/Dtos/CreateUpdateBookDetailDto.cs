using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class CreateUpdateBookDetailDto
    {
        [DisplayName("BookDetailOutline")]
        public string Outline { get; set; }
    }
}