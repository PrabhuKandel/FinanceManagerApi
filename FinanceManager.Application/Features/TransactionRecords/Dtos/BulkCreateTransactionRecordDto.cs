using System;
using System.Collections.Generic;
using FinanceManager.Application.Dtos.TransactionPayment;

namespace FinanceManager.Application.Features.TransactionRecords.Dtos
{


    // Represents one transaction row
    public class BulkCreateTransactionRecordDto
    {
        public DateTime TransactionDate { get; set; }
        public Guid TransactionCategoryId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public List<TransactionPaymentDto> Payments { get; set; } = new();
    }

}
