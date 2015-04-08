﻿CREATE PROCEDURE [dbo].[spSupplierLoginArchive](
	@active bit,
	
	@original_userID int,
	@original_userPassword varchar(50),
	@original_userName varchar(50)
	)
AS
BEGIN
UPDATE [SupplierLogin]
	SET	[Active] = @active
	WHERE [UserID] = @original_userID
		AND [UserPassword] = @original_userPassword
		AND [UserName] = @original_userName

	RETURN @@ROWCOUNT
END