-- Write your own SQL object definition here, and it'll be included in your package.

-- WITH Records AS 
-- (
--     SELECT 1 as Id, 'Admin Users' as [Name], 1 as OwnerId
--     UNION
--     SELECT 2 as Id,  'Standard Users' as [Name],  1 as OwnerId
-- )
-- MERGE INTO dbo.GROUPS g
-- USING Records as r ON (r.Id = g.Id)
--     WHEN MATCHED 
--         THEN UPDATE 
--             SET [Name] = r.[Name],
--                 OwnerId = r.OwnerId
--     WHEN NOT MATCHED
--         THEN INSERT (Id, [Name], OwnerId)
--             VALUES (r.Id, r.Name, r.OwnerId);

-- GO

