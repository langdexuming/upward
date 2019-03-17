
CREATE TABLE `ContentFlags` (
    `Id` int NOT NULL,
    `Name` varchar(20) NULL,
    `Content` varchar(500) NULL,
    `Remark` varchar(200) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `EssayCategories` (
    `EssayCategoryId` int NOT NULL,
    `Title` varchar(20) NULL,
    `Description` varchar(200) NULL,
    `Remark` varchar(200) NULL,
    PRIMARY KEY (`EssayCategoryId`)
);

CREATE TABLE `JobExperiences` (
    `Id` int NOT NULL,
    `CompanyName` varchar(50) NOT NULL,
    `Position` varchar(50) NOT NULL,
    `JobContent` varchar(2000) NOT NULL,
    `StartDate` Date NOT NULL,
    `EndDate` Date NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `LeaveMessages` (
    `Id` bigint NOT NULL,
    `Message` varchar(500) NOT NULL,
    `UserName` varchar(50) NOT NULL,
    `CreateDateTime` datetime NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Samples` (
    `Id` int NOT NULL,
    `Title` varchar(50) NULL,
    `Description` varchar(2000) NULL,
    `ViewUrl` varchar(200) NULL,
    `SourceUrl` varchar(200) NULL,
    `CreateDateTime` datetime NOT NULL,
    `LastUpdateDateTime` datetime NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Skills` (
    `Id` int NOT NULL,
    `Name` varchar(20) NOT NULL,
    `Level` int NOT NULL,
    `CreateDateTime` datetime NOT NULL,
    `LastUpdateDateTime` datetime NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `SwitchFlags` (
    `Id` int NOT NULL,
    `Name` varchar(50) NULL,
    `IsVaild` bit NOT NULL,
    `Remark` varchar(200) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `InformalEssays` (
    `Id` bigint NOT NULL,
    `EssayCategoryId` int NOT NULL,
    `Title` varchar(200) NOT NULL,
    `Message` text NOT NULL,
    `UserName` varchar(50) NULL,
    `CreateDateTime` datetime NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_InformalEssays_EssayCategories_EssayCategoryId` FOREIGN KEY (`EssayCategoryId`) REFERENCES `EssayCategories` (`EssayCategoryId`) ON DELETE CASCADE
);

CREATE INDEX `IX_InformalEssays_EssayCategoryId` ON `InformalEssays` (`EssayCategoryId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20190317043733_20190307_InitialCreate', '2.2.3-servicing-35854');

