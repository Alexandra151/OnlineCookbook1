﻿using System.ComponentModel.DataAnnotations;

namespace OnlineCookbook.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }

}
