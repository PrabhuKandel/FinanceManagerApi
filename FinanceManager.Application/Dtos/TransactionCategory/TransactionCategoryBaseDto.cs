using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Dtos.TransactionCategory
{
    public class TransactionCategoryBaseDto 
    {
        public required string Name { get; set; }


        public string? Description { get; set; }


        public CategoryType Type { get; set; }
    }
}
