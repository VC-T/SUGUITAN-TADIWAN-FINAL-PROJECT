CREATE TABLE [dbo].[BookTbl] (
    [BId]     INT             IDENTITY (100, 1) NOT NULL,
    [BTitle]  VARCHAR (100)   NOT NULL,
    [BAuthor] VARCHAR (50)    NOT NULL,
    [BCat]    VARCHAR (50)    NOT NULL,
    [BQty]    INT             NOT NULL,
    [BPrice]  DECIMAL(18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([BId] ASC)
);
