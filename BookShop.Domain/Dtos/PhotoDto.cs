using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Dtos
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }      
        public int BookId { get; set; }
    }
}
