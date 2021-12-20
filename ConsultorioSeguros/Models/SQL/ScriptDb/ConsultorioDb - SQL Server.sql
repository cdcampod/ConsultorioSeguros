USE [consultoriodb]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 12/20/2021 10:21:01 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
DROP TABLE [dbo].[Cliente]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 12/20/2021 10:21:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[cedula] [varchar](20) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[telefono] [varchar](20) NOT NULL,
	[edad] [int] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [consultoriodb]
GO

/****** Object:  Table [dbo].[Seguro]    Script Date: 12/20/2021 10:21:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Seguro]') AND type in (N'U'))
DROP TABLE [dbo].[Seguro]
GO

/****** Object:  Table [dbo].[Seguro]    Script Date: 12/20/2021 10:21:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Seguro](
	[codigo] [varchar](50) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[prima] [decimal](10, 2) NOT NULL,
	[suma_asegurada] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Seguro] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [consultoriodb]
GO

ALTER TABLE [dbo].[Seguro_cliente] DROP CONSTRAINT [FK_Seguro_cliente_Seguro]
GO

ALTER TABLE [dbo].[Seguro_cliente] DROP CONSTRAINT [FK_Seguro_cliente_Cliente]
GO

/****** Object:  Table [dbo].[Seguro_cliente]    Script Date: 12/20/2021 10:20:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Seguro_cliente]') AND type in (N'U'))
DROP TABLE [dbo].[Seguro_cliente]
GO

/****** Object:  Table [dbo].[Seguro_cliente]    Script Date: 12/20/2021 10:20:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Seguro_cliente](
	[id] [int] NOT NULL,
	[codigo_seguro] [varchar](50) NOT NULL,
	[cedula] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Seguro_cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Seguro_cliente]  WITH CHECK ADD  CONSTRAINT [FK_Seguro_cliente_Cliente] FOREIGN KEY([cedula])
REFERENCES [dbo].[Cliente] ([cedula])
GO

ALTER TABLE [dbo].[Seguro_cliente] CHECK CONSTRAINT [FK_Seguro_cliente_Cliente]
GO

ALTER TABLE [dbo].[Seguro_cliente]  WITH CHECK ADD  CONSTRAINT [FK_Seguro_cliente_Seguro] FOREIGN KEY([codigo_seguro])
REFERENCES [dbo].[Seguro] ([codigo])
GO

ALTER TABLE [dbo].[Seguro_cliente] CHECK CONSTRAINT [FK_Seguro_cliente_Seguro]
GO


