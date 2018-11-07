using System;
using EasySystems.PeriodOverlapChecker.Web.Domain;
using FluentAssertions;
using Xunit;

namespace EasySystems.PeriodOverlapChecker.Specs.Domain
{
    public class PeriodSpec
    {
        private static readonly DateTime DEFAULT_START_DATE = new DateTime(2000, 01, 01);
        private static readonly DateTime DEFAULT_END_DATE = new DateTime(2000, 01, 02);

        private static readonly (Period period1, Period period2) PERIODS_OVERLAPPING = (
            Period.Create(DEFAULT_START_DATE, DEFAULT_END_DATE).Value,
            Period.Create(DEFAULT_END_DATE.AddDays(-1), DEFAULT_END_DATE.AddDays(1)).Value);

        private static readonly (Period period1, Period period2) PERIODS_NONE_OVERLAPPING = (
            Period.Create(DEFAULT_START_DATE, DEFAULT_END_DATE).Value,
            Period.Create(DEFAULT_END_DATE.AddDays(1), DEFAULT_END_DATE.AddDays(2)).Value);

        private static readonly (Period period1, Period period2) PERIODS_OVERLAP_ONE_DAY_START = (
            Period.Create(DEFAULT_START_DATE, DEFAULT_END_DATE).Value,
            Period.Create(DEFAULT_START_DATE.AddDays(-1), DEFAULT_START_DATE.AddDays(1)).Value);

        private static readonly (Period period1, Period period2) PERIODS_OVERLAP_ONE_DAY_END = (
            Period.Create(DEFAULT_START_DATE, DEFAULT_END_DATE).Value,
            Period.Create(DEFAULT_END_DATE.AddDays(-1), DEFAULT_END_DATE.AddDays(1)).Value);

        [Fact]
        public void When_given_correct_dates_should_create_valid_period()
        {
            // Arrange
            var start = DEFAULT_START_DATE;
            var end = DEFAULT_END_DATE;

            // Act
            var result = Period.Create(start, end);


            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Start_date_should_not_be_greater_then_end_date()
        {
            // Arrange
            var start = DEFAULT_END_DATE.AddDays(10);
            var end = DEFAULT_END_DATE;

            // Act
            var result = Period.Create(start, end);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("The end date should always be greater then start date");
        }

        [Fact]
        public void When_given_two_overlapping_periods_should_return_true()
        {
            // Arrange
            var periods = PERIODS_OVERLAPPING;

            // Act
            var period1 = periods.period1;
            var period2 = periods.period2;
            var result = period1.OverlapsWith(period2);

            // Assert
            result.Should().BeTrue();

        }

        [Fact]
        public void When_given_two_none_overlapping_date_ranges_should_return_false()
        {
            // Arrange
            var periods = PERIODS_NONE_OVERLAPPING;

            // Act
            var period1 = periods.period1;
            var period2 = periods.period2;
            var result = period1.OverlapsWith(period2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Should_return_false_when_overlap_is_one_day_at_start()
        {
            // Arrange
            var periods = PERIODS_OVERLAP_ONE_DAY_START;

            // Act
            var period1 = periods.period1;
            var period2 = periods.period2;
            var result = period1.OverlapsWith(period2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Should_return_true_when_overlap_is_one_day_at_end()
        {
            // Arrange
            var periods = PERIODS_OVERLAP_ONE_DAY_END;

            // Act
            var period1 = periods.period1;
            var period2 = periods.period2;
            var result = period1.OverlapsWith(period2);

            // Assert
            result.Should().BeTrue();
        }
    }
}
