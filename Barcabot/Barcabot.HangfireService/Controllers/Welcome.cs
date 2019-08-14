using System;
using Barcabot.Common.DataModels;
using Barcabot.HangfireService.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Barcabot.HangfireService.Controllers
{
    // ReSharper disable once StringLiteralTypo
    [Route("/")]
    [ApiController]
    public class Welcome : ControllerBase
    {
        [HttpGet]
        public ActionResult<ApiStatus> Get()
        {
            return new ApiStatus
            {
                StatusCode = 200,
                Message = "API working."
            };
        }
    }
}