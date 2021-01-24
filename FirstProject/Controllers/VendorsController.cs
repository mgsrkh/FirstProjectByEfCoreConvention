﻿using Amazon.EC2.Model;
using FirstProject.ApplicationServices.IServices;
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
        public IActionResult InsertVendor([FromBody] VendorResponseDTO dto)
        {
            var vendorInsertResponse = _vendorService.Insert(dto);

            return Created(new Uri($"/api/Vendors/{dto.Id}", UriKind.Relative), vendorInsertResponse);
        }
        [HttpPut]
        public IActionResult UpdateVendor(VendorUpdateDTO dto)
        {
            var result = _vendorService.Update(dto);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPatch("{id}")]
        public StatusCodeResult PatchVendor([FromBody] JsonPatchDocument<VendorJsonPatchDTO> patch, [FromRoute] int id)
        {
            var res = _vendorService.GetByIdForJsonPatch(patch, id);
            if (res != null)
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
