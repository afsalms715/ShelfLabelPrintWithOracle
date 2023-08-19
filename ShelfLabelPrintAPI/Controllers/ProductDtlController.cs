﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShelfLabelPrintAPI.Services;
using ShelfLabelPrintAPI.Models;

namespace ShelfLabelPrintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDtlController : ControllerBase
    {
        private readonly ProductDtlService _productService;
        public ProductDtlController(ProductDtlService productService)
        {
            _productService = productService;
        }
        [Route("product")]
        [HttpGet]
        public ProductDtl Get(string barcode,string loc)
        {
            return _productService.GetProduct(barcode,loc);
        }
    }
}