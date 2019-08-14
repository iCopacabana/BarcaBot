using System;
using Barcabot.Common.DataModels;
using Barcabot.HangfireService.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Barcabot.HangfireService.Controllers
{
    // ReSharper disable once StringLiteralTypo
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerJobs : ControllerBase
    {
        private readonly PlayerUpdaterService _service;

        public PlayerJobs (PlayerUpdaterService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ApiStatus> Get()
        {
            try
            {
                RecurringJob.AddOrUpdate("matches-update-job", () => _service.UpdatePlayers(), Cron.Daily);

                return new ApiStatus
                {
                    StatusCode = 200,
                    Message = "Added/updated all the Players jobs successfully."
                };
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                
                return new ApiStatus
                {
                    StatusCode  = 500,
                    Message = e.Message
                };
            }
            
        }
    }
}