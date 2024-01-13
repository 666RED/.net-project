USE [TestDatabase]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 1/13/2024 1:41:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Book](
	[bookId] [int] IDENTITY(1,1) NOT NULL,
	[bookTitle] [nvarchar](max) NULL,
	[bookAuthor] [nvarchar](max) NULL,
	[bookRackNumber] [nvarchar](max) NULL,
	[bookPages] [int] NULL,
	[bookISBN] [nvarchar](max) NULL,
	[bookPublisher] [nvarchar](max) NULL,
	[bookPublicationDate] [date] NULL,
	[bookLanguage] [nvarchar](max) NULL,
	[bookAvailability] [bit] NULL,
	[bookPrice] [decimal](5, 2) NULL,
	[deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[bookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

