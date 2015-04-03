﻿CREATE TABLE [dbo].[Supplier] (
    [SupplierID]    INT           IDENTITY (100, 1) NOT NULL,
    [CompanyName]   VARCHAR (255) NOT NULL,
    [FirstName]     VARCHAR (50)  NOT NULL,
    [LastName]      VARCHAR (50)  NOT NULL,
    [Address1]      VARCHAR (255) NOT NULL,
    [Address2]      VARCHAR (255) NULL,
    [Zip]           VARCHAR (10)  NOT NULL,
    [PhoneNumber]   VARCHAR (15)  NOT NULL,
    [EmailAddress]  VARCHAR (100) NOT NULL,
    [ApplicationID] INT           NOT NULL,
    [UserID]        INT           NOT NULL,
    [Active]        INT           NOT NULL,
	[SupplyCost]	DECIMAL(3,2)		  NOT NULL DEFAULT(.70), 
    CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([SupplierID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [SupplierIndex]
    ON [dbo].[Supplier]([SupplierID] ASC);