/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50510
Source Host           : localhost:3306
Source Database       : ideservice

Target Server Type    : MYSQL
Target Server Version : 50510
File Encoding         : 65001

Date: 2011-04-10 01:29:53
*/
SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `chat`
-- ----------------------------
DROP TABLE IF EXISTS `chat`;
CREATE TABLE `chat` (
  `Id` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `Message` text NOT NULL,
  `ProdjectId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `ProdjectId` (`ProdjectId`),
  CONSTRAINT `chat_ibfk_1` FOREIGN KEY (`ProdjectId`) REFERENCES `prodject` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `file`
-- ----------------------------
DROP TABLE IF EXISTS `file`;
CREATE TABLE `file` (
  `Id` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Path` text NOT NULL,
  `ProdjectId` int(11) NOT NULL DEFAULT '0',
  `FileAccessId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `ProdjectId` (`ProdjectId`),
  KEY `FileAccessId` (`FileAccessId`),
  CONSTRAINT `file_ibfk_1` FOREIGN KEY (`ProdjectId`) REFERENCES `prodject` (`Id`),
  CONSTRAINT `file_ibfk_2` FOREIGN KEY (`FileAccessId`) REFERENCES `fileaccess` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `fileaccess`
-- ----------------------------
DROP TABLE IF EXISTS `fileaccess`;
CREATE TABLE `fileaccess` (
  `Id` int(11) NOT NULL,
  `Rule` text NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `folder`
-- ----------------------------
DROP TABLE IF EXISTS `folder`;
CREATE TABLE `folder` (
  `Id` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Path` text NOT NULL,
  `ProdjectId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `ProdjectId` (`ProdjectId`),
  CONSTRAINT `folder_ibfk_1` FOREIGN KEY (`ProdjectId`) REFERENCES `prodject` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `keytable`
-- ----------------------------
DROP TABLE IF EXISTS `keytable`;
CREATE TABLE `keytable` (
  `NextId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `prodject`
-- ----------------------------
DROP TABLE IF EXISTS `prodject`;
CREATE TABLE `prodject` (
  `Id` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Sourcedir` text NOT NULL,
  `Members` text NOT NULL,
  `OwnerId` int(11) NOT NULL DEFAULT '0',
  `MemberId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `OwnerId` (`OwnerId`),
  KEY `MemberId` (`MemberId`),
  CONSTRAINT `prodject_ibfk_1` FOREIGN KEY (`OwnerId`) REFERENCES `user` (`Id`),
  CONSTRAINT `prodject_ibfk_2` FOREIGN KEY (`MemberId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `Id` int(11) NOT NULL,
  `Login` text NOT NULL,
  `Password` text NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `userinfo`
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `Id` int(11) NOT NULL,
  `Registred` datetime NOT NULL,
  `LastAccess` datetime NOT NULL,
  `Name` text NOT NULL,
  `Sname` text NOT NULL,
  `Email` text NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `userinfo_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
-- ----------------------------
-- Table structure for `userlog`
-- ----------------------------
DROP TABLE IF EXISTS `userlog`;
CREATE TABLE `userlog` (
  `Id` int(11) NOT NULL,
  `Date` datetime NOT NULL,
  `Message` text NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `userlog_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;