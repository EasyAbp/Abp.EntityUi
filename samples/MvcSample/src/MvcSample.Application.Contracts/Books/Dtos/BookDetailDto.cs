using System;
using Volo.Abp.Application.Dtos;

namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class BookDetailDto : EntityDto
    {
        public string Outline { get; set; }
    }
}