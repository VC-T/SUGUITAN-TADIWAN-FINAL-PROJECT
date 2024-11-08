CREATE TABLE [dbo].[BillTbl] (
    [BillId]     INT          IDENTITY (1000, 1) NOT NULL,
    [UName]      VARCHAR (50) NOT NULL,
    [ClientName] VARCHAR (50) NOT NULL,
    [Amount]     INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([BillId] ASC)
);
