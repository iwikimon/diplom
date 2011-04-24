DROP TABLE IF EXISTS `User`;
DROP TABLE IF EXISTS `Project`;
DROP TABLE IF EXISTS `Userlog`;
DROP TABLE IF EXISTS `Folder`;
DROP TABLE IF EXISTS `File`;
DROP TABLE IF EXISTS `Chat`;
DROP TABLE IF EXISTS `Access`;
DROP TABLE IF EXISTS `Keytable`;
DROP TABLE IF EXISTS `ProdjectMembers`;

-- Add table KeyTable to the database
CREATE TABLE KeyTable
(  NextId INT NOT NULL);
INSERT INTO KeyTable VALUES (1);

-- Add table User to the database
CREATE TABLE `User` (Id INT NOT NULL PRIMARY KEY ,
`Login` TEXT NOT NULL,
`Password` TEXT NOT NULL,
`Registred` DATETIME NOT NULL,
`LastAccess` DATETIME NOT NULL,
`Name` TEXT NOT NULL ,
`Sname` TEXT NOT NULL ,
`Email` TEXT NOT NULL );

-- Add table Userlog to the database
CREATE TABLE `Userlog` (Id INT NOT NULL PRIMARY KEY ,
`Date` DATETIME NOT NULL,
`Message` TEXT NOT NULL,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (UserId) REFERENCES `User`(Id));

-- Add table Project to the database
CREATE TABLE `Project` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL,
`Sourcedir` TEXT NOT NULL,
`OwnerId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (OwnerId) REFERENCES `User`(Id));

-- Add table ProdjectMember to the database
CREATE TABLE `ProdjectMembers` (Id INT NOT NULL PRIMARY KEY ,
`ProjectId` INT NOT NULL DEFAULT 0,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProjectId) REFERENCES `Project`(Id),
FOREIGN KEY (UserId) REFERENCES `User`(Id));

-- Add table Folder to the database
CREATE TABLE `Folder` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL,
`Path` TEXT NOT NULL,
`ProjectId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProjectId) REFERENCES `Project`(Id));

-- Add table Chat to the database
CREATE TABLE `Chat` (Id INT NOT NULL PRIMARY KEY ,
`Date` DATETIME NOT NULL,
`Message` TEXT NOT NULL,
`ProjectId` INT NOT NULL DEFAULT 0,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (ProjectId) REFERENCES `Project`(Id),
FOREIGN KEY (UserId) REFERENCES `User`(Id));

-- Add table File to the database
CREATE TABLE `File` (Id INT NOT NULL PRIMARY KEY ,
`Name` TEXT NOT NULL,
`Path` TEXT NOT NULL,
`FolderId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (FolderId) REFERENCES `Folder`(Id));

-- Add table Access to the database
CREATE TABLE `Access` (Id INT NOT NULL PRIMARY KEY ,
`Rule` TEXT NOT NULL ,
`FileId` INT NOT NULL DEFAULT 0,
`FolderId` INT NOT NULL DEFAULT 0,
`UserId` INT NOT NULL DEFAULT 0,
FOREIGN KEY (FileId) REFERENCES `File`(Id),
FOREIGN KEY (FolderId) REFERENCES `Folder`(Id),
FOREIGN KEY (UserId) REFERENCES `User`(Id));

