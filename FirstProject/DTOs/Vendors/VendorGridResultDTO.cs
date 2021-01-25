using FirstProject.CustomAnnotation;
using FirstProject.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.DTOs.Vendors
{
    public class VendorGridResultDTO
    {
        public VendorGridResultDTO(ICollection<VendorDTO> vendorDTOs)
        {
            VendorDTOs = vendorDTOs;
        }
        [TagsIcollectionAnnotation]
        public ICollection<VendorDTO> VendorDTOs { get; set; }
    }
}
