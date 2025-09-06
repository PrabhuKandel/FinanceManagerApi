using Newtonsoft.Json;

namespace FinanceManager.Application.Dtos.TransactionRecord
{

        public class EntitySummaryDto
        {
            public Guid Id { get; set; }
            public required string Name { get; set; }
        }

        public class ApplicationUserSummaryDto
        {
            public required string Id { get; set; }
            public required string FirstName { get; set; }

        }
        public class TransactionRecordResponseDto
        {

            public Guid Id { get; set; }
            public EntitySummaryDto? TransactionCategory { get; set; }
            public EntitySummaryDto? PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public string? Description { get; set; }
            public DateTime TransactionDate { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ApplicationUserSummaryDto? CreatedBy { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]    
            public ApplicationUserSummaryDto? UpdatedBy { get; set; }

        }

    //public class AdminTransactionRecordResponseDto
    //{
    //    public string CreatedBy { get; set; }
    //    public string UpdatedBy { get; set; }
    //}
}
