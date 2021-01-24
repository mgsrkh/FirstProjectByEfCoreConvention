using FirstProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace FirstProject.InferaStructure.IRepositories
{
    public interface IVendorRepository
    {
        Vendor GetVendorById(int id);
        Vendor GetById(int id);
        int Insert(Vendor vendor);
        int Update(Vendor vendor);
        int Delete(Vendor vendor);
        int DeleteById(int id);
        Vendor GetByIdForPatch(int id);
        Vendor Patch(Vendor vendor);
        int SavePatchChanges(Vendor vendor);

    }
}
