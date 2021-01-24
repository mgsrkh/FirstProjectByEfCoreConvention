using FirstProject.ApplicationServices.IServices;
using FirstProject.DTOs.Tags;
using FirstProject.DTOs.Vendors;
using FirstProject.InferaStructure.IRepositories;
using FirstProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;

namespace FirstProject.ApplicationServices.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _repository;
        private readonly ITagRepository _tagRepository;

        public VendorService(IVendorRepository repository, ITagRepository tagRepository)
        {
            _repository = repository;
            _tagRepository = tagRepository;
        }

        public VendorDTO GetVendorsById(int id)
        {
            var vendor = _repository.GetVendorById(id);

            var tags = vendor.Tags.Select(x => new TagDTO
            {
                Name = x.Name,
                Value = x.Value

            }).ToList();


            var vendorMapDto = new VendorDTO()
            {
                Name = vendor.Name,
                Title = vendor.Title,
                Date = vendor.Date,
                Tags = tags
            };

            return vendorMapDto;
        }

        public Vendor Insert(VendorInsertResponseDTO dto)
        {
            var vendorTagList = new List<Tag>();

            if (dto.Tags != null && dto.Tags.Count > 0)
            {
                foreach (var item in dto.Tags)
                {
                    var tag = new Tag
                    {
                        Name = item.Name,
                        Value = item.Value
                    };
                    vendorTagList.Add(tag);
                }
            }

            var vendor = new Vendor()
            {
                Name = dto.Name,
                Title = dto.Title,
                Date = dto.Date,
                Tags = vendorTagList
            };

            int inserted = _repository.Insert(vendor);
            if (inserted > 0)
            {
                return vendor;
            }
            return null;
        }

        public Vendor Update(VendorUpdateDTO dto)
        {
            var vendor = _repository.GetById(dto.Id);

            foreach (var item in dto.Tags)
            {
                var getTagVendorIdById = new Tag()
                {
                    VendorId = dto.Id
                };
                _tagRepository.DeleteById(getTagVendorIdById.VendorId);
            }

            var tagList = new List<Tag>();

            foreach (var item in dto.Tags)
            {
                var tags = new Tag()
                {
                    Name = item.Name,
                    Value = item.Value
                };
                tagList.Add(tags);
            }

            vendor.Name = dto.Name;
            vendor.Title = dto.Title;
            vendor.Date = dto.Date;
            vendor.Tags = tagList;

            int inserted = _repository.Update(vendor);
            if (inserted > 0)
            {
                return vendor;
            }
            return null;
        }
        public bool Delete(int id)
        {
            bool result = false;

            var deleteVendor = _repository.DeleteById(id);

            if (deleteVendor > 0)
            {
                result = true;
            }

            return result;
        }

        public Vendor GetByIdForJsonPatch(JsonPatchDocument<VendorJsonPatchDTO> vendorPatch, int id)
        {
            var vendor = _repository.GetByIdForPatch(id);

            var tags = vendor.Tags.Select(t => new TagDTO
            {
                Name = t.Name,
                Value = t.Value,
            }).ToList();

            var firstMap = new VendorJsonPatchDTO()
            {
                Name = vendor.Name,
                Title = vendor.Title,
                Date = vendor.Date,
                Tags = tags
            };

            vendorPatch.ApplyTo(firstMap);

            var SecondMap = new Vendor()
            {
                Id = id,
                Name = firstMap.Name,
                Title = firstMap.Title,
                Date = firstMap.Date,
                Tags = vendor.Tags
            };

            var Patched = _repository.Patch(SecondMap);

            return Patched;
        }

        public Vendor GetVendorByIdForJsonPatchDoc(int id)
        {
            return _repository.GetByIdForPatch(id);
        }

        public int SavePatchChanges(Vendor vendor)
        {
            return _repository.SavePatchChanges(vendor);
        }
    }
}
