

Create PROCEDURE [dbo].[uspGetCustomers] 
--@SortOrder varchar(50),
--@SearchValue varchar(50)
AS
Begin
-- Above parameters can be added here
-- So data will directly from this stored procedure
	SELECT *
	FROM Customers
	
End

