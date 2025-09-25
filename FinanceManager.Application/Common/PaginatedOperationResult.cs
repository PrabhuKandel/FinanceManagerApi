
namespace FinanceManager.Application.Common
{
    public class PaginatedOperationResult<T> : OperationResult<T>
    {
        public int? TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? TotalPages 
        => (TotalCount.HasValue && PageSize.HasValue && PageSize != 0)
            ? (int)Math.Ceiling((double)TotalCount.Value / PageSize.Value)
            : null;
    }

}
