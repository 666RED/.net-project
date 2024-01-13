USE [TestDatabase]
GO

/****** Object:  Table [dbo].[librarian]    Script Date: 12/1/2024 9:13:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[librarian](
	[librarianId] [ntext] NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](max) NOT NULL,
	[username] [varchar](max) NOT NULL,
	[gender] [varchar](50) NOT NULL,
	[telephone] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

