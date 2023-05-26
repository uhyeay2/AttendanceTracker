DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Student] 
GO

CREATE PROCEDURE [dbo].[SeedTable_Student] AS
BEGIN

	PRINT 'Populating Students in [dbo].[Student]';

	IF OBJECT_ID('tempdb.dbo.#dbo_Student') IS NOT NULL DROP TABLE #dbo_Student;

	SELECT 
		[Id], [StudentCode], [FirstName], [LastName], [DateOfBirth] 
	INTO #dbo_Student 
	FROM [dbo].[Student] 
	WHERE 0 = 1;

	SET IDENTITY_INSERT #dbo_Student ON;

	INSERT INTO #dbo_Student 
		( [Id], [StudentCode], [FirstName], [LastName], [DateOfBirth] )
	SELECT 
		  [Id], [StudentCode], [FirstName], [LastName], [DateOfBirth] 
	FROM 
	(  VALUES

		  (1, N'CGR6243', N'Megan', N'Henderson', CAST(N'1920-05-22T22:47:31.000' AS DateTime))
		, (2, N'BTT5500', N'Cody', N'Washington', CAST(N'1903-10-02T13:50:02.000' AS DateTime))
		, (3, N'POM0744', N'Justin', N'Ratzlaff', CAST(N'2009-07-12T09:41:46.000' AS DateTime))
		, (4, N'OXD3730', N'Madison', N'Murphy', CAST(N'1934-07-06T21:14:40.000' AS DateTime))
		, (5, N'OLX8120', N'Lindsey', N'Bailey', CAST(N'1961-07-11T13:23:26.000' AS DateTime))
		, (6, N'SKT7694', N'Zachary', N'Mc Vicar', CAST(N'1947-12-03T17:13:57.000' AS DateTime))
		, (7, N'NYY9449', N'Alexandria', N'Olsen', CAST(N'1949-08-18T14:29:16.000' AS DateTime))
		, (8, N'TDK3078', N'Isabella', N'Young', CAST(N'1919-07-13T17:38:33.000' AS DateTime))
		, (9, N'VRB6847', N'Ian', N'Gomes', CAST(N'1921-08-09T08:19:23.000' AS DateTime))
		, (10, N'OGA6352', N'Steven', N'Wilson', CAST(N'1911-08-16T22:18:12.000' AS DateTime))
		, (11, N'ARC2612', N'Jacqueline', N'Lopez', CAST(N'2012-05-12T17:56:01.000' AS DateTime))
		, (12, N'IUH3438', N'Cody', N'Carter', CAST(N'1925-04-12T02:16:46.000' AS DateTime))
		, (13, N'LAH7548', N'Andrea', N'Robinson', CAST(N'1990-06-17T05:10:50.000' AS DateTime))
		, (14, N'RBS8139', N'Jennifer', N'Richardson', CAST(N'1972-05-06T15:27:00.000' AS DateTime))
		, (15, N'QRF2296', N'Raymundo', N'Pearson', CAST(N'2011-10-08T23:30:08.000' AS DateTime))
		, (16, N'BHT0326', N'Jenna', N'Evans', CAST(N'1906-05-11T13:17:38.000' AS DateTime))
		, (17, N'GDB4346', N'Brooke', N'Green', CAST(N'1967-12-11T00:51:29.000' AS DateTime))
		, (18, N'GWM1652', N'Aaliyah', N'Gomes', CAST(N'1958-08-21T07:10:00.000' AS DateTime))
		, (19, N'VDJ4527', N'Daniel', N'Collins', CAST(N'1904-01-15T20:20:14.000' AS DateTime))
		, (20, N'VVY5702', N'Seth', N'Rodriguez', CAST(N'1964-01-20T22:05:45.000' AS DateTime))
		, (21, N'GBO4518', N'Michelle', N'Adams', CAST(N'1996-05-30T12:36:27.000' AS DateTime))
		, (22, N'ZBF2372', N'Colby', N'Flores', CAST(N'1988-07-25T09:04:44.000' AS DateTime))
		, (23, N'WYM6410', N'Sophia', N'Washington', CAST(N'1983-01-09T08:20:37.000' AS DateTime))
		, (24, N'KDW3623', N'Steven', N'Rivera', CAST(N'1948-03-01T22:42:34.000' AS DateTime))
		, (25, N'BAZ2052', N'Cole', N'White', CAST(N'1925-04-11T10:09:52.000' AS DateTime))
		, (26, N'VVG4062', N'Aaliyah', N'Edwards', CAST(N'1911-06-11T22:46:05.000' AS DateTime))
		, (27, N'MJT7971', N'Morgan', N'Yarobi', CAST(N'1989-06-28T09:05:47.000' AS DateTime))
		, (28, N'BUT7150', N'Leslie', N'Robinson', CAST(N'1902-05-23T16:55:39.000' AS DateTime))
		, (29, N'OPG6356', N'James', N'Allen', CAST(N'1977-03-16T13:25:28.000' AS DateTime))
		, (30, N'GQR4331', N'Paige', N'Price', CAST(N'1917-05-24T02:47:37.000' AS DateTime))
		, (31, N'JRF8560', N'Makayla', N'Thomas', CAST(N'1967-12-08T07:53:40.000' AS DateTime))
		, (32, N'XMH1513', N'Paige', N'Hall', CAST(N'1972-02-27T18:21:08.000' AS DateTime))
		, (33, N'MQP7829', N'Maria', N'Rivera', CAST(N'1919-09-02T14:57:24.000' AS DateTime))
		, (34, N'YDW8134', N'Chloe', N'Hill', CAST(N'2019-03-25T15:02:21.000' AS DateTime))
		, (35, N'CIJ6543', N'Nathan', N'Alexander', CAST(N'1966-05-02T11:38:58.000' AS DateTime))
		, (36, N'GXM0774', N'Logan', N'James', CAST(N'1978-07-22T10:40:20.000' AS DateTime))
		, (37, N'UKQ8654', N'Maria', N'Moore', CAST(N'1995-04-30T05:38:16.000' AS DateTime))
		, (38, N'DXY7901', N'Shelby', N'Jackson', CAST(N'2015-10-07T08:43:47.000' AS DateTime))
		, (39, N'TDQ3454', N'Logan', N'Turner', CAST(N'1948-07-10T10:59:25.000' AS DateTime))
		, (40, N'DIW1147', N'Madeline', N'Alexander', CAST(N'2003-08-27T14:01:44.000' AS DateTime))
		, (41, N'GZM8594', N'Milissa', N'Wood', CAST(N'1992-10-06T00:05:44.000' AS DateTime))
		, (42, N'HWM6046', N'Nathaniel', N'Diaz', CAST(N'1957-07-02T17:26:50.000' AS DateTime))
		, (43, N'AVQ9016', N'Ryan', N'Garcia', CAST(N'1949-04-02T15:18:55.000' AS DateTime))
		, (44, N'YLT7403', N'Angie', N'Young', CAST(N'2017-06-15T16:36:31.000' AS DateTime))
		, (45, N'HJP1388', N'Jeremy', N'Collins', CAST(N'1966-08-06T05:55:42.000' AS DateTime))
		, (46, N'GHC6349', N'Mikayla', N'Main', CAST(N'1971-07-01T14:42:25.000' AS DateTime))
		, (47, N'CHM2632', N'Kathryn', N'Chambers', CAST(N'1929-08-06T07:27:29.000' AS DateTime))
		, (48, N'BOT3084', N'Hannah', N'Lopez', CAST(N'2016-09-16T12:49:32.000' AS DateTime))
		, (49, N'JBK3179', N'Abigail', N'Torres', CAST(N'1906-01-27T15:54:53.000' AS DateTime))
		, (50, N'HJJ6866', N'Claire', N'Rogers', CAST(N'1942-03-03T04:57:12.000' AS DateTime))
		, (51, N'FYS6851', N'Isabel', N'Martin', CAST(N'2017-05-06T22:51:33.000' AS DateTime))
		, (52, N'GAH5598', N'Jackson', N'Lewis', CAST(N'1931-04-13T15:09:14.000' AS DateTime))
		, (53, N'CHZ7734', N'Paul', N'Davis', CAST(N'1942-07-02T20:14:39.000' AS DateTime))
		, (54, N'PIY4254', N'Sophia', N'Martinez', CAST(N'1938-11-16T16:57:15.000' AS DateTime))
		, (55, N'NIG1153', N'Destiny', N'Tellies', CAST(N'1925-01-02T13:12:46.000' AS DateTime))
		, (56, N'ERA6537', N'Christian', N'Garcia', CAST(N'1941-03-20T16:48:10.000' AS DateTime))
		, (57, N'QRX3508', N'Maria', N'Getzlaff', CAST(N'2000-07-03T05:38:02.000' AS DateTime))
		, (58, N'ZTF1424', N'Tyler', N'Bennett', CAST(N'1995-02-19T09:51:24.000' AS DateTime))
		, (59, N'BTB8969', N'Kayla', N'Roberts', CAST(N'1990-10-02T18:30:02.000' AS DateTime))
		, (60, N'ITQ0952', N'Audrey', N'Pearson', CAST(N'1941-10-20T20:54:18.000' AS DateTime))
		, (61, N'UTW5963', N'Ethan', N'Watson', CAST(N'1940-08-17T15:38:00.000' AS DateTime))
		, (62, N'HOV8937', N'Nicholas', N'Garcia', CAST(N'1997-12-12T06:47:50.000' AS DateTime))
		, (63, N'UMA8887', N'Allison', N'Brandzin', CAST(N'2016-12-16T16:02:11.000' AS DateTime))
		, (64, N'QDE8473', N'Trevor', N'Morris', CAST(N'1934-09-13T21:38:17.000' AS DateTime))
		, (65, N'SSC1773', N'Blake', N'Baker', CAST(N'2016-03-08T09:58:41.000' AS DateTime))
		, (66, N'OIE1775', N'Marvis', N'Morris', CAST(N'1931-02-04T10:44:31.000' AS DateTime))
		, (67, N'ULW7415', N'Aaron', N'Iginla', CAST(N'1905-05-21T20:38:46.000' AS DateTime))
		, (68, N'IWZ6120', N'James', N'Diaz', CAST(N'1935-12-21T04:46:56.000' AS DateTime))
		, (69, N'GHE9184', N'Brandon', N'Rivera', CAST(N'1902-07-31T03:11:41.000' AS DateTime))
		, (70, N'HYJ2606', N'Angela', N'Campbell', CAST(N'1954-06-05T19:04:54.000' AS DateTime))
		, (71, N'SBU9942', N'Katherine', N'White', CAST(N'1956-07-02T10:45:08.000' AS DateTime))
		, (72, N'TOC2601', N'Lily', N'Simmons', CAST(N'1928-03-02T15:27:18.000' AS DateTime))
		, (73, N'NRW1079', N'Rebecca', N'Cox', CAST(N'1982-08-24T20:16:08.000' AS DateTime))
		, (74, N'SUC6186', N'Cathey', N'Gray', CAST(N'1993-06-24T12:34:17.000' AS DateTime))
		, (75, N'RTT0031', N'Megan', N'Roberts', CAST(N'1956-10-09T00:54:38.000' AS DateTime))
		, (76, N'KSP1074', N'Dylan', N'Young', CAST(N'1949-04-18T05:10:51.000' AS DateTime))
		, (77, N'TQZ8327', N'Jack', N'Phillips', CAST(N'1956-11-17T09:42:37.000' AS DateTime))
		, (78, N'JPR5002', N'Luke', N'Jenkins', CAST(N'2000-03-31T05:59:53.000' AS DateTime))
		, (79, N'JNR3937', N'Andrea', N'Williams', CAST(N'1905-12-11T21:32:35.000' AS DateTime))
		, (80, N'CIS0469', N'Jared', N'Cox', CAST(N'1964-10-14T07:06:23.000' AS DateTime))
		, (81, N'MMB4370', N'Taylor', N'Perry', CAST(N'1993-03-21T09:36:01.000' AS DateTime))
		, (82, N'XXQ7938', N'Paige', N'MacKenzie', CAST(N'2003-08-10T19:05:43.000' AS DateTime))
		, (83, N'WDN0018', N'Caroline', N'Bell', CAST(N'1919-07-23T10:31:03.000' AS DateTime))
		, (84, N'MFZ9529', N'Isabella', N'James', CAST(N'1994-03-31T09:27:29.000' AS DateTime))
		, (85, N'GUN7162', N'Cathey', N'Robinson', CAST(N'2005-01-11T04:58:52.000' AS DateTime))
		, (86, N'BCC5641', N'Vanessa', N'Price', CAST(N'2020-10-02T12:52:51.000' AS DateTime))
		, (87, N'VBY6624', N'Ashley', N'Carter', CAST(N'1956-08-02T04:43:41.000' AS DateTime))
		, (88, N'WWM0671', N'Jacqueline', N'Hayes', CAST(N'1900-12-21T15:42:08.000' AS DateTime))
		, (89, N'VQE2324', N'Paige', N'Collins', CAST(N'1968-05-11T02:29:17.000' AS DateTime))
		, (90, N'PLR1787', N'Alexis', N'MacKenzie', CAST(N'2016-04-15T16:36:46.000' AS DateTime))
		, (91, N'WYT8277', N'Courtney', N'Morris', CAST(N'1938-07-31T06:48:30.000' AS DateTime))
		, (92, N'RLO2623', N'Cary', N'Hughes', CAST(N'1929-02-19T17:47:50.000' AS DateTime))
		, (93, N'ZWV9837', N'Gavin', N'Gomes', CAST(N'1929-10-25T18:46:26.000' AS DateTime))
		, (94, N'QCL1930', N'Eric', N'Price', CAST(N'2016-04-04T23:38:59.000' AS DateTime))
		, (95, N'AHQ2617', N'Breanna', N'Brugger', CAST(N'1932-06-02T13:07:38.000' AS DateTime))
		, (96, N'KWL2830', N'Madeline', N'Anderson', CAST(N'2012-07-09T00:31:32.000' AS DateTime))
		
	) AS v ( [Id], [StudentCode], [FirstName], [LastName], [DateOfBirth] );

	SET IDENTITY_INSERT #dbo_Student OFF;

	SET IDENTITY_INSERT [dbo].[Student] ON;

	WITH cte_data as 
		(SELECT 
			[Id], [StudentCode], [FirstName], [LastName], [DateOfBirth] 
		FROM #dbo_Student)
	MERGE [dbo].[Student] as t
		USING cte_data as s ON t.[Id] = s.[Id]
		WHEN NOT MATCHED BY TARGET THEN
			INSERT 
				([Id], [StudentCode], [FirstName], [LastName], [DateOfBirth])
			VALUES 
				(s.[Id], s.[StudentCode], s.[FirstName], s.[LastName], s.[DateOfBirth])
		WHEN MATCHED 
			THEN UPDATE SET 
				[StudentCode] = s.[StudentCode], 
				[FirstName] = s.[FirstName],
				[LastName] = s.[LastName],
				[DateOfBirth] = s.[DateOfBirth]
	;

	SET IDENTITY_INSERT [dbo].Student OFF;

	DROP TABLE #dbo_Student;

	PRINT 'Finished Populating Students in dbo.Student'
END
GO

EXEC [dbo].[SeedTable_Student];

DROP PROCEDURE IF EXISTS [dbo].[SeedTable_Student] 
GO
