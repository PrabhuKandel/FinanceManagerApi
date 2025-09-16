CREATE OR ALTER PROCEDURE usp_DeleteTransactionRecord
	@Id UNIQUEIDENTIFIER
	AS
  BEGIN
  DELETE FROM TransactionRecords
	WHERE Id = @Id;
  END