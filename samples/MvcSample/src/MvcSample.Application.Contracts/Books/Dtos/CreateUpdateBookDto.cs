using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace MvcSample.Books.Dtos
{
    [Serializable]
    public class CreateUpdateBookDto
    {
        [DisplayName("BookName")]
        public string Name { get; set; }

        [DisplayName("BookIsbn")]
        public string Isbn { get; set; }

        [DisplayName("BookLength")]
        public int Length { get; set; }

        [DisplayName("BookDetail")]
        public CreateUpdateBookDetailDto Detail { get; set; }

        [DisplayName("BookTags")]
        public List<CreateUpdateBookTagDto> Tags { get; set; }
    }
}