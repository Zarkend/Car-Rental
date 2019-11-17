using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;
using TestCompany.CarRental.WebAPI.Services;
using TestCompany.CarRental.Domain.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CompanyRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CompaniesController(ILogger<CompaniesController> logger, ICompanyService companyService, IMapper mapper, IUriService uriService)
        {
            _logger = logger;
            _companyService = companyService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Companies.Get)]
        public async Task<IActionResult> GetAsync([FromRoute]int companyId)
        {
            IEnumerable<Company> companies = await _companyService.GetAsync();

            Company company = companies.FirstOrDefault(x => x.Id == companyId);

            if(company == null)
            {
                return NotFound($"There is no Company with Id {companyId}. Please try with another companyId");
            }

            return Ok(new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(company)));
        }

        [HttpPost(ApiRoutes.Companies.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyRequest request)
        {
            if(string.IsNullOrEmpty(request.Name) || request.Name.Length < 5)
                return BadRequest(new BadRequestObjectResult($"Name must have atleast 5 characters."));

            Company createdCompany = await _companyService.CreateAsync(_mapper.Map<Company>(request));

            if (createdCompany == null)
                return NotFound(new NotFoundObjectResult($"Company was not created."));

            var locationUri = _uriService.GetCompanyUri(Request.Path.Value.Substring(1) + "/" + createdCompany.Id.ToString());

            return Created(locationUri, new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(createdCompany)));
        }

        [HttpPut(ApiRoutes.Companies.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute]int companyId, [FromBody] UpdateCompanyRequest request)
        {
            Company company = (await _companyService.GetAsync(x => x.Id == companyId)).FirstOrDefault();

            if (company == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no Company with Id {companyId}. Please try with another CompanyId"));
            }

            company.Name = request.Name;

            var updated = await _companyService.UpdateAsync(company);

            if (!updated)
                return NotFound(new NotFoundObjectResult($"Company was not updated."));

            return Ok(new Response<CompanyResponse>(_mapper.Map<CompanyResponse>(company)));
        }

        [HttpDelete(ApiRoutes.Companies.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute]int companyId)
        {
            Company company = (await _companyService.GetAsync(x => x.Id == companyId)).FirstOrDefault();

            if (company == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no Company with Id {companyId}. Please try with another CompanyId"));
            }

            var deleted = await _companyService.DeleteAsync(company);

            if (!deleted)
                return NotFound(new NotFoundObjectResult($"Company was not deleted."));


            return NoContent();
        }

        /// <summary>
        /// Get Companies matching the parameters provided.
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Companies.GetAll)]
        public async Task<ActionResult<IEnumerable<Company>>> GetAsync([FromQuery]int? id, [FromQuery] string? name)
        {
            IEnumerable<Company> companies = await _companyService.GetAsync();

            if (id.HasValue)
                companies = companies.Where(x => x.Id == id);

            if (!string.IsNullOrEmpty(name))
                companies = companies.Where(x => x.Name == name);

            return Ok(new Response<List<CompanyResponse>>(_mapper.Map<List<CompanyResponse>>(companies)));
        }

    }
}
