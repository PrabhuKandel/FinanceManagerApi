using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Mapping;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord;
using FinanceManager.Application.Interfaces.Services;
using MediatR;



    namespace FinanceManager.Application.Features.TransactionRecords.Queries.ExportToExcel
    {
        public class ExportTransactionRecordsHandler(
         IExcelBuilder<IEnumerable<TransactionRecordExportDto>> _excelBuilder,
        IExcelService _excelService,
            IMediator mediator
            ) : IRequestHandler<ExportTransactionRecordsQuery, byte[]>
        {
        public async Task<byte[]> Handle(ExportTransactionRecordsQuery request, CancellationToken cancellationToken)
        {

            var getAllQuery = new GetAllTransactionRecordsDapperQuery
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
            var workbook = _excelBuilder.Build(exportRecords);

            return _excelService.SaveWorkbook(workbook);

        }
        }


    

   
   
    }
