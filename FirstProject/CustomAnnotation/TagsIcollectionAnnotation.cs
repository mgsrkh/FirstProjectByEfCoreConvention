using FirstProject.DTOs.Tags;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.CustomAnnotation
{
    public class TagsIcollectionAnnotation : Attribute
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        public string Value { get; set; }
    }
}
