using FinanceManager.Application.Features.TransactionRecords.Commands.Create;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinanceManager.Api.ModelBinder
{
    public class TransactionRecordCommandModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.HttpContext.Request.HasFormContentType)
            {
                bindingContext.ModelState.AddModelError("", "The request content type must be multipart/form-data.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var form = await bindingContext.HttpContext.Request.ReadFormAsync();

       
            var commandJson = form["transactionRecord"].FirstOrDefault();
            var files = form.Files.GetFiles("transactionAttachments").ToArray();

            if (string.IsNullOrWhiteSpace(commandJson))
            {
                bindingContext.ModelState.AddModelError("transactionRecord", "Transaction data is required.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

         
            var command = JsonSerializer.Deserialize<CreateTransactionRecordCommand>(
                commandJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (command == null)
            {
                bindingContext.ModelState.AddModelError("transactionRecord", "Invalid transaction data.");
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

     
            var commandWithFiles = command with { TransactionAttachments = files };

            bindingContext.Result = ModelBindingResult.Success(commandWithFiles);
        }
    }
}
