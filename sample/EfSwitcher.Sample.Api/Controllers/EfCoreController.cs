using System.Threading.Tasks;
using EfSwitcher.Sample.Data.Contexts;
using EfSwitcher.Sample.Data.Entities;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EfSwitcher.Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class EfCoreController : Controller
    {
        private readonly IUnitOfWorkAsync<AdventureWorksContextEfCore> _unitOfWork;

        public EfCoreController(IUnitOfWorkAsync<AdventureWorksContextEfCore> unitOfWork)
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
