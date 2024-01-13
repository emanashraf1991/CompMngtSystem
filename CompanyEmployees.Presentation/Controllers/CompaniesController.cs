using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared;
namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/[controller]")]
    //  [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service) => _service = service;
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);

            return Ok(companies);
        }
        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null"); ;
            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }


        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            var result = _service.CompanyService.CreateCompanyCollection(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companyDtos);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetByIds([ModelBinder(binderType: typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(companies);
        }
        [HttpDelete("{id:guid}")]
        //employee entities ondelete deletebehavior.cascade
        public IActionResult DeleteCompany(Guid id)
        {
            _service.CompanyService.DeleteCompany(id, trackChanges: false);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
            _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee,
            compTrackChanges: false, empTrackChanges: true);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");
            _service.CompanyService.UpdateCompany(id, company, trackChanges: true);
            return NoContent();
        }
    }
}