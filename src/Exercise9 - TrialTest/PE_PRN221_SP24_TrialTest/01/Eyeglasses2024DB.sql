USE master
GO

CREATE DATABASE Eyeglasses2024DB
GO

USE Eyeglasses2024DB
GO

CREATE TABLE StoreAccount(
  AccountID int primary key,
  AccountPassword nvarchar(40) not null,
  FullName nvarchar(60) not null,
  EmailAddress nvarchar(60) unique, 
  Role int
)
GO

INSERT INTO StoreAccount VALUES(55 ,N'@5', N'Branch Administrator', 'admin@Eyeglasses.com.de', 1);
INSERT INTO StoreAccount VALUES(56 ,N'@5', N'Branch Staff', 'staff@Eyeglasses.com.de', 2);
INSERT INTO StoreAccount VALUES(57 ,N'@5', N'Branch Manager', 'manager@Eyeglasses.com.de', 3);
INSERT INTO StoreAccount VALUES(58 ,N'@5', N'Branch Customer', 'customer@Eyeglasses.com.de', 4);
GO


CREATE TABLE LensType(
  LensTypeId nvarchar(30) primary key,
  LensTypeName nvarchar(100) not null,
  LensTypeDescription nvarchar(250) not null, 
  IsPrescription bit
)
GO
INSERT INTO LensType VALUES(N'LT00123', N'Distance (Single Vision)', N'Single-vision lenses in eyeglasses correct your vision for just one distance—either close up or far away. ', 1)
GO
INSERT INTO LensType VALUES(N'LT00124', N'Reading (Single Vision)', N'Single vision lenses are the type of lenses that are prescribed for one specific type of vision issue like nearsightedness, astigmatism, or farsightedness. ', 1)
GO
INSERT INTO LensType VALUES(N'LT00125', N'Progressive / Bifocal', N'A progressive lens will give you three different vision powers within a single lens for a better, more flattering, and natural way to visualize your world.', 1)
GO
INSERT INTO LensType VALUES(N'LT00126', N'Frames with Display Lenses', N'Choose this option if you do not want us to fill your frames with prescription or any add-ons. The lenses are the original demos from the frames manufacturer.', 0)
GO
INSERT INTO LensType VALUES(N'LT00127', N'Non-Corrective (Plano) Lenses', N'Plano lenses are made of ophthalmic grade material, and you may add anti–scratch, anti–glare, protective UV coatings, or lens tint.', 0)
GO
INSERT INTO LensType VALUES(N'LT00128', N'Blue Light lenses', N'Blue-Light Protection: Overexposure to blue light emitted by computer, tablet and smartphone screens can cause eye strain.', 0)
GO



CREATE TABLE Eyeglasses(
 EyeglassesId int primary key,
 EyeglassesName nvarchar(100) not null,
 EyeglassesDescription nvarchar(250),
 FrameColor nvarchar(50),
 Price decimal,
 Quantity int, 
 CreatedDate Datetime,
 LensTypeId nvarchar(30) references LensType(LensTypeId) on delete cascade on update cascade
)
GO

INSERT INTO Eyeglasses VALUES(6651, N'PRADA PR 09ZV', N'Special Offer: Add prescription Lenses to this frame for $19.95 ', N'Etruscan Marble', 140.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00123')
GO
INSERT INTO Eyeglasses VALUES(6652, N'PRADA PR 18WV', N'Special Offer: Add prescription Lenses to this frame for $19.95 ($40.00 Value)', N'Black', 123.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00123')
GO
INSERT INTO Eyeglasses VALUES(6653, N'RAY-BAN RX5228', N'The glasses were perfect and they were very well made. ', N'2000 Black', 240.5, 400, CAST(N'2023-08-16' AS DateTime), 'LT00123')
GO
INSERT INTO Eyeglasses VALUES(6654, N'Rectangle Glasses 2036321', N'High-quality frame. Anti-scratch coating.', N'Brown', 180.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00124')
GO
INSERT INTO Eyeglasses VALUES(6655, N'Round Glasses 3226421', N'Feature: Nose Pads , Spring Hinges , High Rx', N'Black', 147.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00126')
GO
INSERT INTO Eyeglasses VALUES(6656, N'Cat-Eye Glasses 3225914', N'Feature: Nose Pads , Spring Hinges , High Rx ', N'White', 199.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00126')
GO
INSERT INTO Eyeglasses VALUES(6657, N'Flirty Birdie 4454118', N'Each Iris Apfel x Zenni purchase includes a free gift', N'Grey', 240.5, 100, CAST(N'2023-08-16' AS DateTime), 'LT00126')
GO
INSERT INTO Eyeglasses VALUES(6658, N'Good To Be Square 4452924', N'Available for Single Vision, Progressive, and Non-Prescription.', N'Light Green', 440.5, 200, CAST(N'2023-08-16' AS DateTime), 'LT00123')
GO
INSERT INTO Eyeglasses VALUES(6659, N'Half & Half 4453239', N'Feature: Spring Hinges , Custom engraving , High Rx , Universal Bridge Fit ', N'Blue and Grey', 445.5, 100, CAST(N'2023-08-16' AS DateTime), 'LT00123')
GO

