namespace EasySystems.PeriodOverlapChecker.Web.SharedKernel
{
    public interface IResult
    {
        bool IsFailure { get; }
        bool IsSuccess { get; }
    }
}