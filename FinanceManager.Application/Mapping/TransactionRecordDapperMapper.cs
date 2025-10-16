using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Domain.Enums;
using static FinanceManager.Application.Dtos.Shared.SummaryDtos;

namespace FinanceManager.Application.Mapping
{
    public static class TransactionRecordDapperMapper
    {
        public static List<TransactionRecordResponseDto> MapTransactionRecordResults(IEnumerable<dynamic> rows,bool isAdmin = false)
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
                        ApprovalStatus = ((TransactionRecordApprovalStatus)row.ApprovalStatus).ToString(),
                        TransactionCategory = row.TransactionCategoryId == null ? null : new TransactionCategorySummaryDto
                        {
                            Id = row.TransactionCategoryId,
                            Name = row.TransactionCategoryName
                        },
              

                        TransactionPayments = new List<TransactionPaymentSummaryDto>()
                    };
                    //only populate for admins
                    if (isAdmin)
                    {
                        tr.CreatedBy = row.CreatedByUserId == null ? null : new ApplicationUserSummaryDto
                        {
                            Id = row.CreatedByUserId,
                            Email = row.CreatedByEmail
                        };
                        tr.UpdatedBy = row.UpdatedByUserId == null ? null : new ApplicationUserSummaryDto
                        {
                            Id = row.UpdatedByUserId,
                            Email = row.UpdatedByEmail
                        };
                        tr.ActionedBy = row.ActionedByUserId == null ? null : new ApplicationUserSummaryDto
                        {
                            Id = row.ActionedByUserId,
                            Email = row.ActionedByEmail
                        };
                    }

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
