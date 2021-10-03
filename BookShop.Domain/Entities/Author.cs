using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookShop.Domain.Entities
{
    public class Author : AbstractEntity
    {
       
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }   
    }
}
