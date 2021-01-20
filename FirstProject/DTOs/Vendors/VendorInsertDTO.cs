using FirstProject.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.DTOs.Vendors
{
    public class VendorInsertDTO
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public List<TagDTO> Tags { get; set; }
    }
}
