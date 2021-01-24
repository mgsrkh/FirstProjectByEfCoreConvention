using FirstProject.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.DTOs.Vendors
{
    public class VendorResponseDTO : VendorInsertDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
