using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWorkPhysicon.Logic.Models;
using TestWorkPhysicon.Logic.Services;
using TestWorkPhysicon.Models;

namespace TestWorkPhysicon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryService _service;

        public HomeController(ILogger<HomeController> logger, ILibraryService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("TestWorkPhysicon/Courses")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses([FromBody] CourseFilterModel filters, CancellationToken token = default)
        {
            try
            {
                var couses = await _service.GetModules(filters.Subject, filters.Grade, filters.Genre);
                return Ok(couses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
