CREATE DATABASE [moviehub]
GO
USE [moviehub]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[Year] [nvarchar](255) NULL,
	[Rental_Cost] [money] NULL,
	[Copies] [nvarchar](50) NULL,
	[Plot] [ntext] NULL,
	[Genre] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentedMovies]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentedMovies](
	[RMID] [int] IDENTITY(1,1) NOT NULL,
	[MovieIDFK] [int] NULL,
	[CustIDFK] [int] NULL,
	[DateRented] [datetime] NULL,
	[DateReturned] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ShowRentals]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ShowRentals]
AS
SELECT  RM.RMID, C.FirstName, C.LastName, M.Title, RM.DateRented, RM.DateReturned
FROM  Customers C INNER JOIN RentedMovies RM ON C.CustID = RM.CustIDFK 
INNER JOIN Movies M ON RM.MovieIDFK = M.MovieID;
GO
/****** Object:  View [dbo].[ShowRentalsOutNow]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ShowRentalsOutNow]
AS
SELECT RM.RMID, C.FirstName, C.LastName, M.Title, RM.DateRented, RM.DateReturned
FROM Customers C INNER JOIN RentedMovies RM ON C.CustID = RM.CustIDFK 
INNER JOIN Movies M ON RM.MovieIDFK = M.MovieID
WHERE RM.DateReturned IS NULL;
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (3, N'Clifton', N'Shelton', N'326 Academy Street', N'6722981')
GO
INSERT [dbo].[Customers] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (4, N'Dianne', N'Shelton', N'484 4th Street North', N'2084708')
GO
INSERT [dbo].[Customers] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (5, N'Stan', N'Short', N'875 Edgewood Drive', N'2615295')
GO
INSERT [dbo].[Customers] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (6, N'Therese', N'Shepherd', N'226 Front Street South', N'9208849')
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (1, N'N/A', N'Henry IV, Part 1', N'2018', 2.0000, N'3', N'1403:- Henry IV finds himself facing uprisings from the Welsh chieftain Owen Glendower and impetuous young Harry Hotspur,son of the Duke of Northumberland,angry with the king for not paying...        ', N'Comedy')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (2, N'N/A', N'Psalm 21', N'2009', 2.0000, N'', N'Henrik, a much beloved priest, doesn''t believe in hell. Upon receiving the news of his fathers death, he starts a journey that will take him through terrifying secrets, distorted childhood memories, and shake the foundation of his belief.', N'Horror, Sci-Fi, Thriller')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (3, N'PG', N'Superbabies: Baby Geniuses', N'2020', 5.0000, N'2', N'A group of smart-talking toddlers find themselves at the center of a media mogul''s experiment to crack the code to baby talk. The toddlers must race against time for the sake of babies everywhere.', N'Comedy, Family')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (4, N'PG', N'Superbabies: Baby Geniuses 2', N'2014', 5.0000, N'', N'A group of smart-talking toddlers find themselves at the center of a media mogul''s experiment to crack the code to baby talk. The toddlers must race against time for the sake of babies everywhere.', N'Comedy, Family')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (5, N'8', N'Harry Potter and the Deathly Hallows: Part 2', N'2011', 2.0000, N'', N'Harry, Ron and Hermione search for Voldemort''s remaining Horcruxes in their effort to destroy the Dark Lord as the final battle rages on at Hogwarts.', N'Adventure, Fantasy, Mystery')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (6, N'Not Rated', N'8½', N'2000', 2.0000, N'', N'A harried movie director retreats into his memories and fantasies.', N'Drama, Fantasy')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (7, N'Not Rated', N'8Â½', N'1963', 2.0000, N'', N'A harried movie director retreats into his memories and fantasies.', N'Drama, Fantasy')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (8, N'PG-13', N'Troll 2', N'2020', 2.0000, N'', N'A family vacationing in a small town discovers the entire town is inhabited by goblins in disguise as humans, who plan to eat them.', N'Fantasy, Horror, Mystery')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (9, N'R', N'Snatch', N'2017', 5.0000, N'1', N'Unscrupulous boxing promoters, violent bookmakers, a Russian gangster, incompetent amateur robbers, and supposedly Jewish jewelers fight to track down a priceless stolen diamond.', N'Comedy, Crime')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (10, N'R', N'Snatch 2', N'2000', 2.0000, N'', N'Unscrupulous boxing promoters, violent bookmakers, a Russian gangster, incompetent amateur robbers, and supposedly Jewish jewelers fight to track down a priceless stolen diamond.', N'Comedy, Crime')
GO
INSERT [dbo].[Movies] ([MovieID], [Rating], [Title], [Year], [Rental_Cost], [Copies], [Plot], [Genre]) VALUES (11, N'G', N'Toy Story 3', N'2010', 2.0000, N'', N'The toys are mistakenly delivered to a day-care center instead of the attic right before Andy leaves for college, and it''s up to Woody to convince the other toys that they weren''t abandoned and to return home.', N'Animation, Adventure, Comedy')
GO
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[RentedMovies] ON 
GO
INSERT [dbo].[RentedMovies] ([RMID], [MovieIDFK], [CustIDFK], [DateRented], [DateReturned]) VALUES (1, 4, 3, CAST(N'2021-06-20T22:35:33.000' AS DateTime), NULL)
GO
INSERT [dbo].[RentedMovies] ([RMID], [MovieIDFK], [CustIDFK], [DateRented], [DateReturned]) VALUES (2, 6, 5, CAST(N'2021-06-20T22:42:56.000' AS DateTime), CAST(N'2021-06-20T22:43:03.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[RentedMovies] OFF
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD FOREIGN KEY([CustIDFK])
REFERENCES [dbo].[Customers] ([CustID])
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD FOREIGN KEY([MovieIDFK])
REFERENCES [dbo].[Movies] ([MovieID])
GO
/****** Object:  StoredProcedure [dbo].[ShowCustomers]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowCustomers]
AS
SELECT  CustID, FirstName, LastName, Address, Phone,
(SELECT COUNT(*) FROM RentedMovies WHERE CustIDFK = C.CustID ) AS TotalRentals
FROM Customers C
ORDER BY TotalRentals DESC;
GO
/****** Object:  StoredProcedure [dbo].[ShowMovies]    Script Date: 8/8/2021 5:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowMovies]
AS
SELECT  MovieID, Rating, Title, Year, Rental_Cost, Copies, Genre, 
(SELECT COUNT(*) FROM RentedMovies WHERE MovieIDFK = M.MovieID) AS TotalRentals
FROM Movies M
ORDER BY TotalRentals DESC;
GO
