CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Hostname] VARCHAR(50) NOT NULL, 
    [Nameservers] VARCHAR(MAX) NOT NULL, 
    [Ip] VARCHAR(50) NOT NULL
)
