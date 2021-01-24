﻿using Amazon.EC2.Model;
using FirstProject.ApplicationServices.IServices;
using FirstProject.DTOs.Tags;
using FirstProject.DTOs.Vendors;
using FirstProject.InferaStructure.IRepositories;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpGet("{id}")]
        public IActionResult GetVendors([FromRoute] int id)
        {
            var result = _vendorService.GetVendorsById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertVendor([FromBody] VendorInsertResponseDTO dto)
        {
            var vendorInsertResponse = _vendorService.Insert(dto);

            return Created(new Uri($"/api/Vendors/{vendorInsertResponse.Id}", UriKind.Relative),
                new VendorInsertResponseDTO()
                {
                    Id = vendorInsertResponse.Id,
                    Name = vendorInsertResponse.Name,
                    Title = vendorInsertResponse.Title,
                    Date = vendorInsertResponse.Date,
                    Tags = vendorInsertResponse.Tags.Select(x => new TagDTO
                    {
                        Name = x.Name,
                        Value = x.Value
                    }).ToList()
                }); ; ;
        }

        [HttpPut]
        public IActionResult UpdateVendor(VendorUpdateDTO dto)
        {
            var vendorUpdateResponse = _vendorService.Update(dto);

            return Created(new Uri($"/api/Vendors/{vendorUpdateResponse.Id}", UriKind.Relative),
                new VendorUpdateDTO()
                {
                    Id = vendorUpdateResponse.Id,
                    Name = vendorUpdateResponse.Name,
                    Title = vendorUpdateResponse.Title,
                    Date = vendorUpdateResponse.Date,
                    Tags = vendorUpdateResponse.Tags.Select(x => new TagDTO
                    {
                        Name = x.Name,
                        Value = x.Value
                    }).ToList()
                }); ; ;
        }
        [HttpPatch("{id}")]
        public StatusCodeResult PatchVendor([FromBody] JsonPatchDocument<Vendor> patch, [FromRoute] int id)
        {
            var jsonPatchApply = _vendorService.GetVendorByIdForJsonPatchDoc(id);

            patch.ApplyTo(jsonPatchApply);

            var savePatched = _vendorService.SavePatchChanges(jsonPatchApply);
            if (savePatched > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVendor([FromRoute] int id)
        {
            var result = _vendorService.Delete(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
