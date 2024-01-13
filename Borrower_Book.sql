USE [TestDatabase]
GO

/****** Object:  Table [dbo].[Borrower_Book]    Script Date: 1/13/2024 1:42:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Borrower_Book](
	[borrower_bookId] [int] IDENTITY(1,1) NOT NULL,
	[bookId] [int] NOT NULL,
	[borrowerId] [int] NOT NULL,
	[returnDate] [date] NOT NULL,
	[borrowDate] [date] NOT NULL,
	[returnStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[borrower_bookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Borrower_Book] ADD  DEFAULT ((0)) FOR [returnStatus]
GO

ALTER TABLE [dbo].[Borrower_Book]  WITH CHECK ADD FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([bookId])
GO

ALTER TABLE [dbo].[Borrower_Book]  WITH CHECK ADD FOREIGN KEY([borrowerId])
REFERENCES [dbo].[Borrower] ([borrowerId])
GO

