using Bogus;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public static class TransactionRecordFaker
    {
        public static Faker<TransactionRecord> Create
            (
               
                List<Guid> transactionCategoryIds,
                List<Guid> paymentMethodIds,
                List<string> applicationUserIds,
                string adminId
            )
        {

            return new Faker<TransactionRecord>()
                .RuleFor(tr => tr.Id, f => Guid.NewGuid())
                .RuleFor(tr => tr.TransactionCategoryId, f => f.PickRandom(transactionCategoryIds))
                .RuleFor(tr => tr.Description, f => f.Lorem.Sentence(2))
                .RuleFor(tr => tr.TransactionDate, f => f.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow))
                .RuleFor(tr => tr.ApprovalStatus, f => f.PickRandom<TransactionRecordApprovalStatus>())
                .RuleFor(tr => tr.CreatedByApplicationUserId, f => f.PickRandom(applicationUserIds))
                .RuleFor(tr => tr.CreatedAt, f => f.Date.Recent(30))
                .RuleFor(tr => tr.UpdatedAt, f => DateTime.UtcNow)
                .RuleFor(tr => tr.UpdatedByApplicationUserId, f => null)
                .RuleFor(tr => tr.TransactionPayments, (f, tr) =>
                 {
                     // generate between 1–3 payments
                     var paymentCount = f.Random.Int(1, 3);

                     var payments = TransactionPaymentFaker
                         .Create(tr.Id, paymentMethodIds)
                         .Generate(paymentCount);

                     // calculate total amount = sum of payments
                     tr.Amount = payments.Sum(p => p.Amount);

                     return payments;
                 })
                  .FinishWith((f, tr) =>
                  {
                      // Simulate update
                      if (f.Random.Bool(0.3f)) // 30% chance record was updated
                      {
                          // Choose updater: admin or user
                          var canBeUpdatedByAdmin = f.Random.Bool(0.5f); // 50% chance admin updated
                          if (canBeUpdatedByAdmin)
                          {
                              tr.UpdatedByApplicationUserId = adminId;
                          }
                          else
                          {
                              // User can only update their own records
                              tr.UpdatedByApplicationUserId = tr.CreatedByApplicationUserId;
                          }

                          tr.UpdatedAt = tr.CreatedAt.AddDays(f.Random.Int(1, 10));
                      }

                      // Approval logic
                      if (tr.CreatedByApplicationUserId == adminId)
                      {
                          tr.ApprovalStatus = TransactionRecordApprovalStatus.Approved;
                          tr.ActionedByApplicationUserId = adminId;
                          tr.ActionedAt = tr.CreatedAt;
                      }
                      else if (tr.ApprovalStatus != TransactionRecordApprovalStatus.Pending)
                      {
                          // Admin approves/cancels
                          tr.ActionedByApplicationUserId = adminId;
                          tr.ActionedAt = tr.CreatedAt.AddDays(f.Random.Int(1, 5));
                      }
                  });

        }
    }
}
