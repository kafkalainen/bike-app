CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `DataImports` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FileName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ImportDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_DataImports` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stations` (
    `Fid` bigint unsigned NOT NULL AUTO_INCREMENT,
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `NameInFinnish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NameInSwedish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NameInEnglish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AddressInFinnish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AddressInSwedish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CityInFinnish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CityInSwedish` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Operator` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Capacity` int NOT NULL,
    `X` double NOT NULL,
    `Y` double NOT NULL,
    CONSTRAINT `PK_Stations` PRIMARY KEY (`Fid`),
    CONSTRAINT `AK_Stations_Id` UNIQUE (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Journeys` (
    `Id` bigint unsigned NOT NULL AUTO_INCREMENT,
    `Departure` datetime(6) NOT NULL,
    `Return` datetime(6) NOT NULL,
    `DepartureStationId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `DepartureStationName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ReturnStationId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ReturnStationName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CoveredDistanceInMeters` double NOT NULL,
    `DurationInSeconds` int NOT NULL,
    CONSTRAINT `PK_Journeys` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Journeys_Stations_DepartureStationId` FOREIGN KEY (`DepartureStationId`) REFERENCES `Stations` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Journeys_Stations_ReturnStationId` FOREIGN KEY (`ReturnStationId`) REFERENCES `Stations` (`Id`) ON DELETE RESTRICT
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Journeys_DepartureStationId` ON `Journeys` (`DepartureStationId`);

CREATE INDEX `IX_Journeys_ReturnStationId` ON `Journeys` (`ReturnStationId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20230529205953_Init', '7.0.2');

COMMIT;

