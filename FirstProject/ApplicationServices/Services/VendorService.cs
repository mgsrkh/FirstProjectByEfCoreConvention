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

        public VendorDTO GetAll(int id)
        {
            //var vendors = _repository.GetAll();
            //var vendorTags = _tagRepository.GetAll();

            //var tags = vendorTags.Select(x => new TagDTO
            //{
            //    //Id = x.Id,
            //    Name = x.Name,
            //    Value = x.Value,
            //    //VendorId = x.Vendor.Id // or x.VendorId
            //}).ToList();

            //var tags = new TagDTO()
            //{
            //    Name = vendorTags.Name,
            //    Value = vendorTags.Value
            //};

            //var vendor = vendors.Select(x => new VendorDTO
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Title = x.Title,
            //    Date = x.Date,
            //    //Tags = tags
            //    Tags = tags.Select(x=>x.Name).ToList()
            //}).ToList();

            //foreach (var item in vendors)
            //{
            //    var vendorWithTags = new VendorDTO()
            //    {
            //        Name = item.Name,
            //        Title = item.Title,
            //        Date = item.Date,
            //        Tags = item.Tags.Select(x => x.Name).ToList()
            //    };
            //}

            //return new VendorGridResultDTO(vendors);
            var vendor = _repository.GetAll(id);

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

        public bool Insert(VendorInsertDTO dto)
        {
            bool result = false;

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
                result = true;
            }
            return result;
        }

        public bool Update(VendorUpdateDTO dto)
        {
            bool result = false;

            var vendor = _repository.GetById(dto.Id);

            var getTagVendorIdById = new Tag()
            {
                VendorId = dto.Id
            };

            _tagRepository.DeleteById(getTagVendorIdById.VendorId);

            var tagList = new List<Tag>();

            var tags = new Tag()
            {
                Name = dto.Tags.Name,
                Value = dto.Tags.Value
            };
            tagList.Add(tags);

            //Second Approach to Update
            //if (dto.Tags != null && dto.Tags.Count > 0)
            //{
            //    foreach (var item in dto.Tags)
            //    {
            //        var tags = new Tag
            //        {
            //            Name = item.Name,
            //            Value = item.Value
            //        };
            //        tagList.Add(tags);
            //    }
            //}

            vendor.Name = dto.Name;
            vendor.Title = dto.Title;
            vendor.Date = dto.Date;
            vendor.Tags = tagList;

            int inserted = _repository.Update(vendor);
            if (inserted > 0)
            {
                result = true;
            }
            return result;
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

        public bool Patch(int id)
        {
            bool result = false;

            var isPatched = _repository.Patch(id);
            if (isPatched > 0)
            {
                result = true;
            }
            return result;
        }

        public VendorDTO GetByIdForPatch(int id)
        {
            var Mapdto = _repository.GetById(id);

            var result = new VendorDTO()
            {
                Name = Mapdto.Name,
                //Title = Mapdto.Title,
                //Date = Mapdto.Date
            };

            return result;
        }
    }
}
