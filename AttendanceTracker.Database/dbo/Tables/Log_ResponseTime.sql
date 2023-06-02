CREATE TABLE [dbo].[Log_ResponseTime]
(
	[Id] BIGINT NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1, 1),
	[DateTimeRequestWasReceivedInUTC] DATETIME NOT NULL,
	[RequestPath] NVARCHAR(MAX) NOT NULL,
	[ResponseTimeInMilliseconds] BIGINT NOT NULL
)
