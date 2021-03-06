﻿using FirstProject.CustomAnnotation;
using FirstProject.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.DTOs.Vendors
{
    public class VendorDTO
    {
        public VendorDTO()
        {
            Tags = new HashSet<TagDTO>();
        }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [TagsIcollectionAnnotation]
        public ICollection<TagDTO> Tags { get; set; }

    }
}
