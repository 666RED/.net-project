USE [TestDatabase]
GO

/****** Object:  Table [dbo].[Borrower]    Script Date: 1/13/2024 1:41:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Borrower](
	[borrowerId] [int] IDENTITY(1,1) NOT NULL,
	[borrowerName] [varchar](max) NULL,
	[borrowerGender] [varchar](max) NULL,
	[borrowerAge] [int] NULL,
	[borrowerEmailAddress] [varchar](max) NULL,
	[borrowerPhoneNumber] [varchar](max) NULL,
	[borrowerAddress] [varchar](max) NULL,
	[borrowerFineStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[borrowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

