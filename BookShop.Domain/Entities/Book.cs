using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookShop.Domain.Entities
{
    public class Book : AbstractEntity
    {       
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        [MaxLength(150)]
        public string Subtitle { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime? publishedDate { get; set; }
        public string publisher { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
