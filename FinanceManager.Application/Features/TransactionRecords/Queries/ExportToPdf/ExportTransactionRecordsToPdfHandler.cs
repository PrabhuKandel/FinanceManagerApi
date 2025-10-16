

using DocumentFormat.OpenXml.InkML;
using FinanceManager.Application.Features.TransactionRecords.Queries.GetAll;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf
{
    public class ExportTransactionRecordsToPdfHandler(IUserContext userContext, IMediator mediator, ITemplateRenderer _templateRenderer, IPdfGenerator _pdfGenerator) : IRequestHandler<ExportTransactionRecordsToPdfQuery, byte[]>
    {
        public async Task<byte[]> Handle(ExportTransactionRecordsToPdfQuery request, CancellationToken cancellationToken)
        {
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

            var transactionDtos = await mediator.Send(getAllQuery, cancellationToken);
      
            var html = await _templateRenderer.RenderTemplateAsync(
                "transactions-template.hbs",
                new { Transactions = transactionDtos.Data, IsAdmin = userContext.IsAdmin() }
            );

            // Generate PDF
            return await _pdfGenerator.GeneratePdfAsync(html);

            throw new NotImplementedException();
        }
    }
}
