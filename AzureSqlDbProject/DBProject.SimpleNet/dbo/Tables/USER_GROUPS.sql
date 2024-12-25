CREATE TABLE [dbo].[USER_GROUPS] (
    [UserId]      INT      NOT NULL,
    [GroupId]     INT      NOT NULL,
    [DateCreated] DATETIME NULL,
    CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_USER_GROUPS_GROUPS] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[GROUPS] ([Id]),
    CONSTRAINT [FK_USER_GROUPS_USERS] FOREIGN KEY ([UserId]) REFERENCES [dbo].[USERS] ([Id])
);
GO

ALTER TABLE [dbo].[USER_GROUPS]
    ADD CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC);
GO


ALTER TABLE [dbo].[USER_GROUPS]
    ADD CONSTRAINT [FK_USER_GROUPS_GROUPS] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[GROUPS] ([Id]);
GO


ALTER TABLE [dbo].[USER_GROUPS]
    ADD CONSTRAINT [FK_USER_GROUPS_USERS] FOREIGN KEY ([UserId]) REFERENCES [dbo].[USERS] ([Id]);
GO
