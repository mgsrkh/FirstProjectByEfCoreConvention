using FirstProject.DTOs.Vendors;
using FirstProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FirstProject.ApplicationServices.IServices
{
    public interface IVendorService
    {
        Vendor GetVendorByIdForJsonPatchDoc(int id);
        VendorDTO GetVendorsById(int id);
        Vendor Insert(VendorInsertResponseDTO dto);
        Vendor Update(VendorUpdateDTO dto);
        bool Delete(int id);
        Vendor GetByIdForJsonPatch(JsonPatchDocument<VendorJsonPatchDTO> vendorPatch, int id);
        int SavePatchChanges(Vendor vendor);
    }
}
