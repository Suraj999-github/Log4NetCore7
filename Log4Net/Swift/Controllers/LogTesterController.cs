using Log4Net.LogUtility;
using Microsoft.AspNetCore.Mvc;
namespace Swift.Controllers
{
    public class LogTesterController : Controller
    {
        private readonly ILog4Logger _logger;
        
        public LogTesterController(ILog4Logger logger) {  
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Debug("Suraj test");          
            return View();
        }
    }
}
