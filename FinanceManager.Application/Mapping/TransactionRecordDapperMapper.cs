using FinanceManager.Application.Dtos.TransactionRecord;
using static FinanceManager.Application.Dtos.Shared.SummaryDtos;

namespace FinanceManager.Application.Mapping
{
    public static class TransactionRecordDapperMapper
    {
        public static List<TransactionRecordResponseDto> MapTransactionRecordResults(IEnumerable<dynamic> rows)
        {
            var lookup = new Dictionary<Guid, TransactionRecordResponseDto>();

            foreach (var row in rows)
            {
                if (!lookup.TryGetValue(row.TransactionRecordId, out TransactionRecordResponseDto tr))
                {
                    tr = new TransactionRecordResponseDto
                    {
                        Id = row.TransactionRecordId,
                        Amount = row.TransactionAmount,
                        Description = row.Description,
                        TransactionDate = row.TransactionDate,
                        CreatedAt = row.CreatedAt,
                        UpdatedAt = row.UpdatedAt,
                        TransactionCategory = row.TransactionCategoryId == null ? null : new TransactionCategorySummaryDto
                        {
                            Id = row.TransactionCategoryId,
                            Name = row.TransactionCategoryName
                        },
                        CreatedBy = row.CreatedByUserId == null ? null : new ApplicationUserSummaryDto
                        {
                            Id = row.CreatedByUserId,
                            Email = row.CreatedByEmail
                        },
                        UpdatedBy = row.UpdatedByUserId == null ? null : new ApplicationUserSummaryDto
                        {
                            Id = row.UpdatedByUserId,
                            Email = row.UpdatedByEmail
                        },
                        TransactionPayments = new List<TransactionPaymentSummaryDto>()
                    };

                    lookup.Add(tr.Id, tr);
                }

                // Add payment if exists
                if (row.PaymentMethodId != null)
                {
                    tr.TransactionPayments.Add(new TransactionPaymentSummaryDto
                    {
                        PaymentMethodId = row.PaymentMethodId,
                        Name = row.PaymentMethodName,
                        Amount = row.PaymentAmount
                    });
                }
            }

            return lookup.Values.ToList();
        }

    }
}
