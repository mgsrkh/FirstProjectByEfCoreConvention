using FirstProject.DTOs.Vendors;
using FirstProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FirstProject.ApplicationServices.IServices
{
    public interface IVendorService
    {
        VendorDTO GetVendorsById(int id);
        Vendor Insert(VendorResponseDTO dto);
        bool Update(VendorUpdateDTO dto);
        bool Delete(int id);
        Vendor GetByIdForJsonPatch(JsonPatchDocument<VendorJsonPatchDTO> vendorPatch, int id);
    }
}
