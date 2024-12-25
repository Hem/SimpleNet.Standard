CREATE TABLE [dbo].[GROUPS] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50) NULL,
    [OwnerId] INT           NULL,
    CONSTRAINT [PK_GROUPS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

