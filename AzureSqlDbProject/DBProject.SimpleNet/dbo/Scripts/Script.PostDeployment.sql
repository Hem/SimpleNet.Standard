-- This file contains SQL statements that will be executed after the build script.

SET IDENTITY_INSERT USERS ON;
GO

WITH Records AS 
(
    SELECT 1 as Id, 'Admin' as FirstName, 'Admin' as LastName
    UNION
    SELECT 2 as Id,  'Hem' as FirstName,  'Talreja' as LastName
)
MERGE INTO USERS u
USING Records as r ON (r.Id = u.Id)
    WHEN MATCHED 
        THEN UPDATE 
            SET FirstName = r.FirstName,
                LastName = r.LastName
    WHEN NOT MATCHED
        THEN INSERT (Id, FirstName, LastName)
            VALUES (r.Id, r.FirstName, r.LastName)
;
GO
SET IDENTITY_INSERT USERS OFF;
GO


SET IDENTITY_INSERT GROUPS ON;
GO


WITH Records AS 
(
    SELECT 1 as Id, 'Admin Users' as [Name], 1 as OwnerId
    UNION
    SELECT 2 as Id,  'Standard Users' as [Name],  1 as OwnerId
)
MERGE INTO dbo.GROUPS g
USING Records as r ON (r.Id = g.Id)
    WHEN MATCHED 
        THEN UPDATE 
            SET [Name] = r.[Name],
                OwnerId = r.OwnerId
    WHEN NOT MATCHED
        THEN INSERT (Id, [Name], OwnerId)
            VALUES (r.Id, r.Name, r.OwnerId);

GO
SET IDENTITY_INSERT GROUPS OFF;
GO