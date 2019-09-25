CREATE TABLE [dbo].[Usuario]
(
	[Username] varchar(50) NOT NULL PRIMARY KEY,
	[Password] varchar(64) NOT NULL,
	[Salt] varchar(64) not null unique
)
