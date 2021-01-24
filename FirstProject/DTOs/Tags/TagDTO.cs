using FirstProject.DTOs.Vendors;
using FirstProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.DTOs.Tags
{
    public class TagDTO
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        public string Value { get; set; }
    }
}
