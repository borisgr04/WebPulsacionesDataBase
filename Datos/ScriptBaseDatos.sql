CREATE DATABASE [Pulsaciones];
USE  [Pulsaciones]

CREATE TABLE [dbo].[Persona](
	[Identificacion] [nvarchar](10) NOT NULL PRIMARY KEY,
	[Nombre] [nvarchar](50) NULL,
	[Sexo] [nvarchar](2) NULL,
	[Edad] [int] NULL,
	[Pulsacion] [numeric](18, 0) NULL,
) 
GO

COMMIT