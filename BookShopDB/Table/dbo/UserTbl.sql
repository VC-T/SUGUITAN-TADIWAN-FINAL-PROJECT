CREATE TABLE [dbo].[UserTbl] (
    [UId]    INT          IDENTITY (500, 1) NOT NULL,
    [UName]  VARCHAR (50) NOT NULL,
    [UPhone] VARCHAR (50) NOT NULL,
    [UAdd]   VARCHAR (50) NOT NULL,
    [UPass]  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UId] ASC)
);