using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class BookDto : EntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Isbn { get; set; }

        public int Length { get; set; }

        public BookDetailDto Detail { get; set; }

        public List<BookTagDto> Tags { get; set; }
    }
}