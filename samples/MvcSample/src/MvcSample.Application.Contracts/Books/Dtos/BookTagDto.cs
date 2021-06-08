using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class BookTagDto : EntityDto
    {
        [Required]
        public string Tag { get; set; }
    }
}