﻿CREATE PROCEDURE [dbo].[spInsertSupplierLogin](
	@userName varchar(50),
	@supplierID int
	)
AS
BEGIN
	INSERT INTO [SupplierLogin] (UserName, SupplierID)
	VALUES (@userName, @supplierID)
	
	RETURN @@ROWCOUNT
END