-- Person
WITH CTE AS
(
	SELECT 1 AS Id
		, CONCAT('FirstName', 1) AS FirstName
		, CONCAT('Lastname', 1) AS LastName
		, NEWID() AS SSN
		, GETUTCDATE() AS CreatedDateUtc
	UNION ALL
	SELECT Id + 1
		, CONCAT('FirstName', Id + 1)
		, CONCAT('Lastname', Id + 1)
		, NEWID()
		, GETUTCDATE()
	FROM CTE
	WHERE Id < 10000
)
INSERT INTO Person
(
	FirstName,
	LastName,
	SSN,
	CreatedDateUtc
)
SELECT FirstName
	, LastName
	, SSN
	, CreatedDateUtc
FROM CTE
ORDER BY Id ASC
OPTION (MAXRECURSION 10000);
GO

-- Event
WITH CTE AS
(
	SELECT 1 AS Id
		, 1 AS [Type]
		, CONCAT('Value', 1) AS [Value]
		, GETUTCDATE() AS CreatedDateUtc
	UNION ALL
	SELECT Id + 1
		, (Id + 1) % 9
		, CONCAT('Value', (Id + 1)) AS [Value]
		, GETUTCDATE()
	FROM CTE
	WHERE Id < 10000
)
INSERT INTO [Event]
(
	[Type]
	, [Value]
	, CreatedDateUtc
)
SELECT [Type]
	, [Value]
	, CreatedDateUtc
FROM CTE
ORDER BY Id ASC
OPTION (MAXRECURSION 10000);

-- Signal
WITH CTE AS
(
	SELECT 1 AS Id
		, CONCAT('Value', 1) AS [Value]
		, GETUTCDATE() AS CreatedDateUtc
	UNION ALL
	SELECT Id + 1
		, CONCAT('Value', (Id + 1)) AS [Value]
		, GETUTCDATE()
	FROM CTE
	WHERE Id < 10000
)
INSERT INTO [Signal]
(
	[Value]
	, CreatedDateUtc
)
SELECT [Value]
	, CreatedDateUtc
FROM CTE
ORDER BY Id ASC
OPTION (MAXRECURSION 10000);