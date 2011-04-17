DROP TABLE IF EXISTS KeyTable;
CREATE TABLE KeyTable
(  NextId INT NOT NULL);
INSERT INTO KeyTable VALUES (1);
-- Add table Users to the database
CREATE TABLE `User` (Id INT NOT NULL PRIMARY KEY ,
`Login` TEXT NOT NULL  ,
`Password` TEXT NOT NULL  ) ;
-- Add table FileAccesses to the database
CREATE TABLE `FileAccess` (Id INT NOT NULL PRIMARY KEY ,
`Rule` TEXT NOT NULL  ) ;
-- Add table Userinfos to the database
CREATE TABLE `Userinfo` (Id INT NOT NULL PRIMARY KEY ,
`Registred` DATETIME NOT NULL,
`LastAccess` DATETIME NOT NULL,
`Name` TEXT NOT NULL  ,
`Sname` TEXT NOT NULL  ,
`Email` TEXT NOT NULL  ,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (UserId) REFERENCES `User`(Id)) ;
-- Add table Userlogs to the database
CREATE TABLE `Userlog` (Id INT NOT NULL PRIMARY KEY ,
`Date` DATETIME NOT NULL,
`Message` TEXT NOT NULL  ,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (UserId) REFERENCES `User`(Id)) ;
-- Add table Prodjects to the database
CREATE TABLE `Prodject` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL  ,
`Sourcedir` TEXT NOT NULL  ,
`Members` TEXT NOT NULL  ,
`OwnerId` INT NOT NULL DEFAULT 0,
`MemberId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (OwnerId) REFERENCES `User`(Id),
FOREIGN KEY (MemberId) REFERENCES `User`(Id)) ;
-- Add table Folders to the database
CREATE TABLE `Folder` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL  ,
`Path` TEXT NOT NULL  ,
`ProdjectId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProdjectId) REFERENCES `Prodject`(Id)) ;
-- Add table Files to the database
CREATE TABLE `File` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL  ,
`Path` TEXT NOT NULL  ,
`ProdjectId` INT NOT NULL DEFAULT 0,
`FileAccessId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProdjectId) REFERENCES `Prodject`(Id),
FOREIGN KEY (FileAccessId) REFERENCES `FileAccess`(Id)) ;
-- Add table Chats to the database
CREATE TABLE `Chat` (Id INT NOT NULL PRIMARY KEY ,
`Date` DATETIME NOT NULL,
`Message` TEXT NOT NULL  ,
`ProdjectId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProdjectId) REFERENCES `Prodject`(Id)) ;
-- One or more of the above statements failed.  Review the log for error info.
-- Depending on your database, changes may have been rolled back or may have
-- been partially applied.
