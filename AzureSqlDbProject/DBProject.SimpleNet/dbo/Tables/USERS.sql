CREATE TABLE [dbo].[USERS] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NULL,
    [LastName]  NVARCHAR (50) NULL,
    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

