using System.Threading.Tasks;
using EfSwitcher.Sample.Data.Contexts;
using EfSwitcher.Sample.Data.Entities;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EfSwitcher.Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class Ef6Controller : Controller
    {
        private readonly IUnitOfWorkAsync<AdventureWorksContextEf6> _unitOfWork;

        public Ef6Controller(IUnitOfWorkAsync<AdventureWorksContextEf6> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.RepositoryAsync<Product>().SelectAsync());
        }
    }
}
