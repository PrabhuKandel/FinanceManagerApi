CREATE OR ALTER procedure [dbo].[usp_GetPaymentMethodById]
 @id uniqueidentifier
as
begin
	 SET NOCOUNT ON;  -- Prevents extra "rows affected" messages
	select *
	from dbo.PaymentMethods
	where Id = @id;
end