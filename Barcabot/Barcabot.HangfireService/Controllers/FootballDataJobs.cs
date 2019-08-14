using System;
using Barcabot.Common.DataModels;
using Barcabot.HangfireService.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Barcabot.HangfireService.Controllers
{
    // ReSharper disable once StringLiteralTypo
    [Route("/api/[controller]")]
    [ApiController]
    public class FootballDataJobs : ControllerBase
    {
        private readonly FootballDataUpdaterService _service;

        public FootballDataJobs(FootballDataUpdaterService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ApiStatus> Get()
        {
            try
            {
                RecurringJob.AddOrUpdate("matches-update-job", () => _service.UpdateMatches(), "0/2 * * * *");
                RecurringJob.AddOrUpdate("ucl-scorers-update-job", () => _service.UpdateUclScorers(), "0/2 * * * *");
                RecurringJob.AddOrUpdate("laliga-scorers-update-job", () => _service.UpdateLaLigaScorers(),
                    "0/2 * * * *");

                return new ApiStatus
                {
                    StatusCode = 200,
                    Message = "Added/updated all the FootballData jobs successfully."
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