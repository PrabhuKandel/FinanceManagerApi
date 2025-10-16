using Newtonsoft.Json;
using static FinanceManager.Application.Dtos.Shared.SummaryDtos;

namespace FinanceManager.Application.Features.TransactionRecords.Dtos
{


        public class TransactionRecordResponseDto
        {

            public Guid Id { get; set; }
            public TransactionCategorySummaryDto? TransactionCategory { get; set; }
            public List<TransactionPaymentSummaryDto>? TransactionPayments { get; set; }
            public decimal Amount { get; set; }
            public string? Description { get; set; }

            public string ApprovalStatus { get; set; } = string.Empty;
            public DateTime TransactionDate { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ApplicationUserSummaryDto? CreatedBy { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]    
            public ApplicationUserSummaryDto? UpdatedBy { get; set; }

            public ApplicationUserSummaryDto? ActionedBy { get; set; }

        }

}
