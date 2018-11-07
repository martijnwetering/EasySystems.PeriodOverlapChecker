using System;
using System.Linq;
using EasySystems.PeriodOverlapChecker.Web.Domain;
using EasySystems.PeriodOverlapChecker.Web.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace EasySystems.PeriodOverlapChecker.Web.Controllers
{
    public class PeriodCheckModel
    {
        public DateTime[] Period1 { get; set; }
        public DateTime[] Period2 { get; set; }
    }

    [Route("api/periodcheck")]
    public class PeriodCheckController : Controller
    {
        [HttpPost]
        public IActionResult CheckPeriod([FromBody]PeriodCheckModel periods)
        {

            var firstPeriodResult = Period.Create(periods.Period1[0], periods.Period1[1]);
            var secondPeriodResult = Period.Create(periods.Period2[0], periods.Period2[1]);

            var combinedResult = Result.Combine(firstPeriodResult, secondPeriodResult);
            if (combinedResult.IsFailure)
            {
                return BadRequest(combinedResult.Error.FirstOrDefault());
            }

            var periodsOverlap = firstPeriodResult.Value.OverlapsWith(secondPeriodResult.Value);

            return Ok(new { overlap = periodsOverlap });
        }
    }
}