using FinanceManager.Application.Features.TransactionRecords.Dtos;

namespace FinanceManager.Application.Features.TransactionRecords.Mapping
{
    public static class TransactionRecordExportMappingProfile
    {
        public static TransactionRecordExportDto ToExportDto(TransactionRecordResponseDto record)
        {
            return new TransactionRecordExportDto
            {
                TransactionDate = record.TransactionDate,
                Category = record.TransactionCategory?.Name ?? "-",
                Amount = record.Amount,
                Description = record.Description,
                ApprovalStatus = record.ApprovalStatus,
                CreatedBy = record.CreatedBy?.Email,
                UpdatedBy = record.UpdatedBy?.Email,
                ActionedBy = record.ActionedBy?.Email,
                CreatedByApplicationUserId = record.CreatedBy?.Id.ToString() ?? "",
                Payments = record.TransactionPayments?.Select(p => new PaymentExportDto
                {
                    PaymentName = p.Name ?? "-",
                    Amount = p.Amount
                }).ToList() ?? new List<PaymentExportDto>()
            };
        }

        public static List<TransactionRecordExportDto> ToExportDtoList(IEnumerable<TransactionRecordResponseDto> records)
        {
            return records.Select(r => ToExportDto(r)).ToList();
        }
    }
}
