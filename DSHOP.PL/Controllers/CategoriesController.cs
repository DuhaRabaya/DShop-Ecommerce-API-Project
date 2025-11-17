using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSHOP.DAL.Data;

namespace DSHOP.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
