-- Write your own SQL object definition here, and it'll be included in your package.

-- SET IDENTITY_INSERT USERS ON;

-- WITH Records AS 
-- (
--     SELECT 1 as Id, 'Admin' as FirstName, 'Admin' as LastName
--     UNION
--     SELECT 2 as Id,  'Hem' as FirstName,  'Talreja' as LastName
-- )

-- MERGE INTO dbo.users
-- USING Records as r ON (r.Id = users.Id)
--     WHEN MATCHED 
--         THEN UPDATE 
--             SET FirstName = r.FirstName,
--                 LastName = r.LastName
--     WHEN NOT MATCHED
--         THEN INSERT (Id, FirstName, LastName)
--             VALUES (r.Id, r.FirstName, r.LastName)
-- ;

-- SET IDENTITY_INSERT USERS OFF;

-- GO;