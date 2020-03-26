using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebPulsaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController: ControllerBase
    {
          // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Gets()
        {
            return new string[]{"Test1", "Test2", "Test3"};
        }
    }
}