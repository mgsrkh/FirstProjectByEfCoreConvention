using FirstProject.Contexts;
using FirstProject.InferaStructure.IRepositories;
using FirstProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstProject.InferaStructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ProjectContext _db;

        public VendorRepository(ProjectContext db)
        {
            _db = db;
        }
        public Vendor GetVendorById(int id)
        {
            return _db.Vendor.Where(x => x.Id == id).Include(x => x.Tags).FirstOrDefault();
        }
        public Vendor GetById(int id)
        {
            return _db.Vendor.Find(id);
        }
        public int Insert(Vendor vendor)
        {
            _db.Vendor.Add(vendor);
            return _db.SaveChanges();
        }
        public int Update(Vendor vendor)
        {
            _db.Vendor.Update(vendor);
            return _db.SaveChanges();
        }
        public int Delete(Vendor vendor)
        {
            _db.Vendor.Remove(vendor);
            return _db.SaveChanges();
        }

        public int DeleteById(int id)
        {
            var result = _db.Vendor.Where(x => x.Id == id).FirstOrDefault();
            _db.Vendor.Remove(result);
            return _db.SaveChanges();
        }
        public Vendor Patch(Vendor vendor)
        {
            _db.Vendor.Update(vendor);
            _db.SaveChanges();
            return vendor;
        }

        public int SavePatchChanges(Vendor vendor)
        {
            _db.Vendor.Update(vendor);
            return _db.SaveChanges();
        }

        public Vendor GetByIdForPatch(int id)
        {
            var vendor = _db.Set<Vendor>().Find(id);
            _db.Vendor.AsNoTracking();
            _db.Entry(vendor).State = EntityState.Detached;
            return vendor;
        }
    }
}
