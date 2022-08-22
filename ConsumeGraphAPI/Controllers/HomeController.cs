using ConsumeGraphAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeGraphAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMainServices _mainServices;

        public HomeController(IMainServices mainServices)
        {
            _mainServices = mainServices;
        }

        [HttpGet]
        public async Task<string> GetDataFromGraphAPI()
        {
            var responseToken = _mainServices.GetDataByAuthToken();
            return responseToken.Result;
        }
    }
}
