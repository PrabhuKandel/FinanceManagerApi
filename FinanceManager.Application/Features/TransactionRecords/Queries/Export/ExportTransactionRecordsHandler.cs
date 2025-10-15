
using ClosedXML.Excel;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Mapping;
using FinanceManager.Application.Features.TransactionRecords.Queries.GetAll;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;



    namespace FinanceManager.Application.Features.TransactionRecords.Queries.Export
    {
        public class ExportTransactionRecordsHandler(
            ITransactionRecordExportService exportService,
            IMediator mediator
            ) : IRequestHandler<ExportTransactionRecordsQuery, byte[]>
        {
        public async Task<byte[]> Handle(ExportTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            //var isAdmin = userContext.IsAdmin();

            //var query = context.TransactionRecords
            // .AsNoTracking()
            // .Select(tr => new TransactionRecordExportDto
            // {
            //     TransactionDate = tr.TransactionDate,
            //     Category = tr.TransactionCategory != null ? tr.TransactionCategory.Name : "-",
            //     Amount = tr.Amount,
            //     Description = tr.Description,
            //     ApprovalStatus = tr.ApprovalStatus.ToString(),
            //     CreatedBy = tr.CreatedByApplicationUser != null ? tr.CreatedByApplicationUser.Email : "-",
            //     UpdatedBy = tr.UpdatedByApplicationUser != null ? tr.UpdatedByApplicationUser.Email : "-",
            //     ActionedBy = tr.ActionedByApplicationUser != null ? tr.ActionedByApplicationUser.Email : "-",
            //     Payments = tr.TransactionPayments.Select(tp => new PaymentExportDto
            //     {
            //         PaymentName = tp.PaymentMethod != null ? tp.PaymentMethod.Name : "-",
            //         Amount = tp.Amount
            //     }).ToList(),
            //     CreatedByApplicationUserId = tr.CreatedByApplicationUserId
            // });


            //if (!isAdmin)
            //    query = query.Where(tr => tr.CreatedByApplicationUserId == userContext.UserId);

            //    var records = await query.ToListAsync(cancellationToken);
            // Fetch current page using existing Dapper handler
            var getAllQuery = new GetAllTransactionRecordsQuery
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                CreatedBy = request.CreatedBy,
                UpdatedBy = request.UpdatedBy,
                ApprovalStatus = request.ApprovalStatus,
                Search = request.Search,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending
            };

            var result = await mediator.Send(getAllQuery, cancellationToken);

            // Map Dapper response DTOs to Export DTOs

            var exportRecords = TransactionRecordExportMappingProfile.ToExportDtoList(result.Data);
            return exportService.GenerateExcel(exportRecords);

        }
        }


    

   
   
    }
