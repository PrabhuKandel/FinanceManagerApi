CREATE OR ALTER procedure [dbo].[usp_GetAllPaymentMethods]
as
begin
	 SET NOCOUNT ON;  -- Prevents extra "rows affected" messages
	select *
	from dbo.PaymentMethods;
end