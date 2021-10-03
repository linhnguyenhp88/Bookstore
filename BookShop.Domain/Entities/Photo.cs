using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Entities
{
    public class Photo : AbstractEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }    
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
