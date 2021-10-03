using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.Dtos
{
    public class BookForDetailedDto
    {
        public int Id { get; set; }     
        public string Title { get; set; }   
        public string Subtitle { get; set; }    
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime? publishedDate { get; set; }
        public string publisher { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
