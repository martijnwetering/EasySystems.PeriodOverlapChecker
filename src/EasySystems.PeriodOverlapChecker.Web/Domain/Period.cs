using System;
using EasySystems.PeriodOverlapChecker.Web.SharedKernel;

namespace EasySystems.PeriodOverlapChecker.Web.Domain
{
    public class Period
    {
        private DateTime Start { get; }
        private DateTime End { get; }

        private Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static Result<Period> Create(DateTime start, DateTime end)
        {
            if (start >= end) return Result.Fail<Period>("De startdatum moet altijd kleiner zijn dan de begindatum");

            var period = new Period(start, end);
            return Result.Ok(period);
        }

        // There are only two possible scenarios in which two data ranges do not overlap.
        // (EndA <= StartB or StartA >= EndB)
        public bool OverlapsWith(Period period) => !(End <= period.Start || Start >= period.End);
    }
}