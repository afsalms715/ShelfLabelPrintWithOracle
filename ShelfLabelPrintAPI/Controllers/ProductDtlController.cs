using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShelfLabelPrintAPI.Services;
using ShelfLabelPrintAPI.Models;
using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Text.Json;


namespace ShelfLabelPrintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorePolicy")]
    public class ProductDtlController : ControllerBase
    {
        private readonly ProductDtlService _productService;

        public IWebHostEnvironment WebHostEnvironment;
        public ProductDtlController(ProductDtlService productService, IWebHostEnvironment WebHostEnvironment)
        {
            _productService = productService;
            this.WebHostEnvironment = WebHostEnvironment;
        }
        [Route("product")]       
        [HttpGet]
        public ActionResult<ProductDtl> Get(string barcode,string loc)
        {
            if (barcode == "" ||barcode.Length<4 || loc == "")
            {
                return BadRequest();
            }
                var result = _productService.GetProduct(barcode, loc);
                if (result.Su_desc == null)
                {
                    return NotFound();
                }
            
            return result;
        }

        [Route("productmaster")]
        [HttpGet]
        public IEnumerable<ProductDtl> GetProducts()
        {
            var results = _productService.GetProducts();
            return results;
        }

        }
}
