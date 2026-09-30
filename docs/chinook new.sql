# File name: C:/Users/ZakariaAhmad/Desktop/chinook.sql
# Creation date: 11/13/2018
# Created by SQLite to MySQL 2.1 [Demo]
# --------------------------------------------------
# More conversion tools at http://www.convert-in.com

SET NAMES utf8;

DROP DATABASE IF EXISTS `chinookdatabase`;
CREATE DATABASE `chinookdatabase`;
USE `chinookdatabase`;

#
# Table structure for table 'albums'
#

DROP TABLE IF EXISTS `albums` CASCADE;
CREATE TABLE `albums` (
  `AlbumId` INT NOT NULL auto_increment,
  `Title` VARCHAR(160) CHARACTER SET utf8 NOT NULL,
  `ArtistId` INT NOT NULL,
  PRIMARY KEY (`AlbumId`),
  INDEX `IFK_AlbumArtistId` (`ArtistId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'albums'
#

LOCK TABLES `albums` WRITE;
INSERT IGNORE INTO `albums`(`AlbumId`, `Title`, `ArtistId`) VALUES(1, 'For Those About To Rock We Salute You', 1), (2, 'Balls to the Wall', 2), (3, 'Restless and Wild', 2), (4, 'Let There Be Rock', 1), (5, 'Big Ones', 3), (6, 'Jagged Little Pill', 4), (7, 'Facelift', 5), (8, 'Warner 25 Anos', 6), (9, 'Plays Metallica By Four Cellos', 7), (10, 'Audioslave', 8), (11, 'Out Of Exile', 8), (12, 'BackBeat Soundtrack', 9), (13, 'The Best Of Billy Cobham', 10), (14, 'Alcohol Fueled Brewtality Live! [Disc 1]', 11), (15, 'Alcohol Fueled Brewtality Live! [Disc 2]', 11), (16, 'Black Sabbath', 12), (17, 'Black Sabbath Vol. 4 (Remaster)', 12), (18, 'Body Count', 13), (19, 'Chemical Wedding', 14), (20, 'The Best Of Buddy Guy - The Millenium Collection', 15), (21, 'Prenda Minha', 16), (22, 'Sozinho Remix Ao Vivo', 16), (23, 'Minha Historia', 17), (24, 'Afrociberdelia', 18), (25, 'Da Lama Ao Caos', 18), (26, 'Acústico MTV [Live]', 19), (27, 'Cidade Negra - Hits', 19), (28, 'Na Pista', 20), (29, 'Axé Bahia 2001', 21), (30, 'BBC Sessions [Disc 1] [Live]', 22), (31, 'Bongo Fury', 23), (32, 'Carnaval 2001', 21), (33, 'Chill: Brazil (Disc 1)', 24), (34, 'Chill: Brazil (Disc 2)', 6), (35, 'Garage Inc. (Disc 1)', 50), (36, 'Greatest Hits II', 23), (37, 'Greatest Kiss', 45), (38, 'Heart of the Night', 4), (39, 'International Superhits', 34), (40, 'Into The Light', 22), (41, 'Meus Momentos', 1), (42, 'Minha História', 6), (43, 'MK III The Final Concerts [Disc 1]', 9), (44, 'Physical Graffiti [Disc 1]', 45), (45, 'Sambas De Enredo 2001', 2), (46, 'Supernatural', 4), (47, 'The Best of Ed Motta', 12), (48, 'The Essential Miles Davis [Disc 1]', 14), (49, 'The Essential Miles Davis [Disc 2]', 10), (50, 'The Final Concerts (Disc 2)', 38);
UNLOCK TABLES;

#
# Table structure for table 'artists'
#

DROP TABLE IF EXISTS `artists` CASCADE;
CREATE TABLE `artists` (
  `ArtistId` INT NOT NULL auto_increment,
  `Name` VARCHAR(120) CHARACTER SET utf8,
  PRIMARY KEY (`ArtistId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'artists'
#

LOCK TABLES `artists` WRITE;
INSERT IGNORE INTO `artists`(`ArtistId`, `Name`) VALUES(1, 'AC/DC'), (2, 'Accept'), (3, 'Aerosmith'), (4, 'Alanis Morissette'), (5, 'Alice In Chains'), (6, 'Antônio Carlos Jobim'), (7, 'Apocalyptica'), (8, 'Audioslave'), (9, 'BackBeat'), (10, 'Billy Cobham'), (11, 'Black Label Society'), (12, 'Black Sabbath'), (13, 'Body Count'), (14, 'Bruce Dickinson'), (15, 'Buddy Guy'), (16, 'Caetano Veloso'), (17, 'Chico Buarque'), (18, 'Chico Science & Nação Zumbi'), (19, 'Cidade Negra'), (20, 'Cláudio Zoli'), (21, 'Various Artists'), (22, 'Led Zeppelin'), (23, 'Frank Zappa & Captain Beefheart'), (24, 'Marcos Valle'), (25, 'Milton Nascimento & Bebeto'), (26, 'Azymuth'), (27, 'Gilberto Gil'), (28, 'João Gilberto'), (29, 'Bebel Gilberto'), (30, 'Jorge Vercilo'), (31, 'Baby Consuelo'), (32, 'Ney Matogrosso'), (33, 'Luiz Melodia'), (34, 'Nando Reis'), (35, 'Pedro Luís & A Parede'), (36, 'O Rappa'), (37, 'Ed Motta'), (38, 'Banda Black Rio'), (39, 'Fernanda Porto'), (40, 'Os Cariocas'), (41, 'Elis Regina'), (42, 'Milton Nascimento'), (43, 'A Cor Do Som'), (44, 'Kid Abelha'), (45, 'Sandra De Sá'), (46, 'Jorge Ben'), (47, 'Hermeto Pascoal'), (48, 'Barão Vermelho'), (49, 'Edson, DJ Marky & DJ Patife Featuring Fernanda Porto'), (50, 'Metallica');
UNLOCK TABLES;

#
# Table structure for table 'customers'
#

DROP TABLE IF EXISTS `customers` CASCADE;
CREATE TABLE `customers` (
  `CustomerId` INT NOT NULL auto_increment,
  `FirstName` VARCHAR(40) CHARACTER SET utf8 NOT NULL,
  `LastName` VARCHAR(20) CHARACTER SET utf8 NOT NULL,
  `Company` VARCHAR(80) CHARACTER SET utf8,
  `Address` VARCHAR(70) CHARACTER SET utf8,
  `City` VARCHAR(40) CHARACTER SET utf8,
  `State` VARCHAR(40) CHARACTER SET utf8,
  `Country` VARCHAR(40) CHARACTER SET utf8,
  `PostalCode` VARCHAR(10) CHARACTER SET utf8,
  `Phone` VARCHAR(24) CHARACTER SET utf8,
  `Fax` VARCHAR(24) CHARACTER SET utf8,
  `Email` VARCHAR(60) CHARACTER SET utf8 NOT NULL,
  `SupportRepId` INT,
  PRIMARY KEY (`CustomerId`),
  INDEX `IFK_CustomerSupportRepId` (`SupportRepId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'customers'
#

LOCK TABLES `customers` WRITE;
INSERT IGNORE INTO `customers`(`CustomerId`, `FirstName`, `LastName`, `Company`, `Address`, `City`, `State`, `Country`, `PostalCode`, `Phone`, `Fax`, `Email`, `SupportRepId`) VALUES(1, 'Luís', 'Gonçalves', 'Embraer - Empresa Brasileira de Aeronáutica S.A.', 'Av. Brigadeiro Faria Lima, 2170', 'São José dos Campos', 'SP', 'Brazil', '12227-000', '+55 (12) 3923-5555', '+55 (12) 3923-5566', 'luisg@embraer.com.br', 3), (2, 'Leonie', 'Köhler', NULL, 'Theodor-Heuss-Straße 34', 'Stuttgart', NULL, 'Germany', '70174', '+49 0711 2842222', NULL, 'leonekohler@surfeu.de', 5), (3, 'François', 'Tremblay', NULL, '1498 rue Bélanger', 'Montréal', 'QC', 'Canada', 'H2G 1A7', '+1 (514) 721-4711', NULL, 'ftremblay@gmail.com', 3), (4, 'Bjørn', 'Hansen', NULL, 'Ullevålsveien 14', 'Oslo', NULL, 'Norway', '0171', '+47 22 44 22 22', NULL, 'bjorn.hansen@yahoo.no', 4), (5, 'František', 'Wichterlová', 'JetBrains s.r.o.', 'Klanova 9/506', 'Prague', NULL, 'Czech Republic', '14700', '+420 2 4172 5555', '+420 2 4172 5555', 'frantisekw@jetbrains.com', 4), (6, 'Helena', 'Holý', NULL, 'Rilská 3174/6', 'Prague', NULL, 'Czech Republic', '14300', '+420 2 4177 0449', NULL, 'hholy@gmail.com', 5), (7, 'Astrid', 'Gruber', NULL, 'Rotenturmstraße 4, 1010 Innere Stadt', 'Vienne', NULL, 'Austria', '1010', '+43 01 5134505', NULL, 'astrid.gruber@apple.at', 5), (8, 'Daan', 'Peeters', NULL, 'Grétrystraat 63', 'Brussels', NULL, 'Belgium', '1000', '+32 02 219 03 03', NULL, 'daan_peeters@apple.be', 4), (9, 'Kara', 'Nielsen', NULL, 'Sønder Boulevard 51', 'Copenhagen', NULL, 'Denmark', '1720', '+453 3331 9991', NULL, 'kara.nielsen@jubii.dk', 4), (10, 'Eduardo', 'Martins', 'Woodstock Discos', 'Rua Dr. Falcão Filho, 155', 'São Paulo', 'SP', 'Brazil', '01007-010', '+55 (11) 3033-5446', '+55 (11) 3033-4564', 'eduardo@woodstock.com.br', 4), (11, 'Alexandre', 'Rocha', 'Banco do Brasil S.A.', 'Av. Paulista, 2022', 'São Paulo', 'SP', 'Brazil', '01310-200', '+55 (11) 3055-3278', '+55 (11) 3055-8131', 'alero@uol.com.br', 5), (12, 'Roberto', 'Almeida', 'Riotur', 'Praça Pio X, 119', 'Rio de Janeiro', 'RJ', 'Brazil', '20040-020', '+55 (21) 2271-7000', '+55 (21) 2271-7070', 'roberto.almeida@riotur.gov.br', 3), (13, 'Fernanda', 'Ramos', NULL, 'Qe 7 Bloco G', 'Brasília', 'DF', 'Brazil', '71020-677', '+55 (61) 3363-5547', '+55 (61) 3363-7855', 'fernadaramos4@uol.com.br', 4), (14, 'Mark', 'Philips', 'Telus', '8210 111 ST NW', 'Edmonton', 'AB', 'Canada', 'T6G 2C7', '+1 (780) 434-4554', '+1 (780) 434-5565', 'mphilips12@shaw.ca', 5), (15, 'Jennifer', 'Peterson', 'Rogers Canada', '700 W Pender Street', 'Vancouver', 'BC', 'Canada', 'V6C 1G8', '+1 (604) 688-2255', '+1 (604) 688-8756', 'jenniferp@rogers.ca', 3), (16, 'Frank', 'Harris', 'Google Inc.', '1600 Amphitheatre Parkway', 'Mountain View', 'CA', 'USA', '94043-1351', '+1 (650) 253-0000', '+1 (650) 253-0000', 'fharris@google.com', 4), (17, 'Jack', 'Smith', 'Microsoft Corporation', '1 Microsoft Way', 'Redmond', 'WA', 'USA', '98052-8300', '+1 (425) 882-8080', '+1 (425) 882-8081', 'jacksmith@microsoft.com', 5), (18, 'Michelle', 'Brooks', NULL, '627 Broadway', 'New York', 'NY', 'USA', '10012-2612', '+1 (212) 221-3546', '+1 (212) 221-4679', 'michelleb@aol.com', 3), (19, 'Tim', 'Goyer', 'Apple Inc.', '1 Infinite Loop', 'Cupertino', 'CA', 'USA', '95014', '+1 (408) 996-1010', '+1 (408) 996-1011', 'tgoyer@apple.com', 3), (20, 'Dan', 'Miller', NULL, '541 Del Medio Avenue', 'Mountain View', 'CA', 'USA', '94040-111', '+1 (650) 644-3358', NULL, 'dmiller@comcast.com', 4), (21, 'Kathy', 'Chase', NULL, '801 W 4th Street', 'Reno', 'NV', 'USA', '89503', '+1 (775) 223-7665', NULL, 'kachase@hotmail.com', 5), (22, 'Heather', 'Leacock', NULL, '120 S Orange Ave', 'Orlando', 'FL', 'USA', '32801', '+1 (407) 999-7788', NULL, 'hleacock@gmail.com', 4), (23, 'John', 'Gordon', NULL, '69 Salem Street', 'Boston', 'MA', 'USA', '2113', '+1 (617) 522-1333', NULL, 'johngordon22@yahoo.com', 4), (24, 'Frank', 'Ralston', NULL, '162 E Superior Street', 'Chicago', 'IL', 'USA', '60611', '+1 (312) 332-3232', NULL, 'fralston@gmail.com', 3), (25, 'Victor', 'Stevens', NULL, '319 N. Frances Street', 'Madison', 'WI', 'USA', '53703', '+1 (608) 257-0597', NULL, 'vstevens@yahoo.com', 5), (26, 'Richard', 'Cunningham', NULL, '2211 W Berry Street', 'Fort Worth', 'TX', 'USA', '76110', '+1 (817) 924-7272', NULL, 'ricunningham@hotmail.com', 4), (27, 'Patrick', 'Gray', NULL, '1033 N Park Ave', 'Tucson', 'AZ', 'USA', '85719', '+1 (520) 622-4200', NULL, 'patrick.gray@aol.com', 4), (28, 'Julia', 'Barnett', NULL, '302 S 700 E', 'Salt Lake City', 'UT', 'USA', '84102', '+1 (801) 531-7272', NULL, 'jubarnett@gmail.com', 5), (29, 'Robert', 'Brown', NULL, '796 Dundas Street West', 'Toronto', 'ON', 'Canada', 'M6J 1V1', '+1 (416) 363-8888', NULL, 'robbrown@shaw.ca', 3), (30, 'Edward', 'Francis', NULL, '230 Elgin Street', 'Ottawa', 'ON', 'Canada', 'K2P 1L7', '+1 (613) 234-3322', NULL, 'edfrancis@yachoo.ca', 3), (31, 'Martha', 'Silk', NULL, '194A Chain Lake Drive', 'Halifax', 'NS', 'Canada', 'B3S 1C5', '+1 (902) 450-0450', NULL, 'marthasilk@gmail.com', 5), (32, 'Aaron', 'Mitchell', NULL, '696 Osborne Street', 'Winnipeg', 'MB', 'Canada', 'R3L 2B9', '+1 (204) 452-6452', NULL, 'aaronmitchell@yahoo.ca', 4), (33, 'Ellie', 'Sullivan', NULL, '5112 48 Street', 'Yellowknife', 'NT', 'Canada', 'X1A 1N6', '+1 (867) 920-2233', NULL, 'ellie.sullivan@shaw.ca', 3), (34, 'João', 'Fernandes', NULL, 'Rua da Assunção 53', 'Lisbon', NULL, 'Portugal', NULL, '+351 (213) 466-111', NULL, 'jfernandes@yahoo.pt', 4), (35, 'Madalena', 'Sampaio', NULL, 'Rua dos Campeões Europeus de Viena, 4350', 'Porto', NULL, 'Portugal', NULL, '+351 (225) 022-448', NULL, 'masampaio@sapo.pt', 4), (36, 'Hannah', 'Schneider', NULL, 'Tauentzienstraße 8', 'Berlin', NULL, 'Germany', '10789', '+49 030 26550280', NULL, 'hannah.schneider@yahoo.de', 5), (37, 'Fynn', 'Zimmermann', NULL, 'Berger Straße 10', 'Frankfurt', NULL, 'Germany', '60316', '+49 069 40598889', NULL, 'fzimmermann@yahoo.de', 3), (38, 'Niklas', 'Schröder', NULL, 'Barbarossastraße 19', 'Berlin', NULL, 'Germany', '10779', '+49 030 2141444', NULL, 'nschroder@surfeu.de', 3), (39, 'Camille', 'Bernard', NULL, '4, Rue Milton', 'Paris', NULL, 'France', '75009', '+33 01 49 70 65 65', NULL, 'camille.bernard@yahoo.fr', 4), (40, 'Dominique', 'Lefebvre', NULL, '8, Rue Hanovre', 'Paris', NULL, 'France', '75002', '+33 01 47 42 71 71', NULL, 'dominiquelefebvre@gmail.com', 4), (41, 'Marc', 'Dubois', NULL, '11, Place Bellecour', 'Lyon', NULL, 'France', '69002', '+33 04 78 30 30 30', NULL, 'marc.dubois@hotmail.com', 5), (42, 'Wyatt', 'Girard', NULL, '9, Place Louis Barthou', 'Bordeaux', NULL, 'France', '33000', '+33 05 56 96 96 96', NULL, 'wyatt.girard@yahoo.fr', 3), (43, 'Isabelle', 'Mercier', NULL, '68, Rue Jouvence', 'Dijon', NULL, 'France', '21000', '+33 03 80 73 66 99', NULL, 'isabelle_mercier@apple.fr', 3), (44, 'Terhi', 'Hämäläinen', NULL, 'Porthaninkatu 9', 'Helsinki', NULL, 'Finland', '00530', '+358 09 870 2000', NULL, 'terhi.hamalainen@apple.fi', 3), (45, 'Ladislav', 'Kovács', NULL, 'Erzsébet krt. 58.', 'Budapest', NULL, 'Hungary', 'H-1073', NULL, NULL, 'ladislav_kovacs@apple.hu', 3), (46, 'Hugh', 'O\'Reilly', NULL, '3 Chatham Street', 'Dublin', 'Dublin', 'Ireland', NULL, '+353 01 6792424', NULL, 'hughoreilly@apple.ie', 3), (47, 'Lucas', 'Mancini', NULL, 'Via Degli Scipioni, 43', 'Rome', 'RM', 'Italy', '00192', '+39 06 39733434', NULL, 'lucas.mancini@yahoo.it', 5), (48, 'Johannes', 'Van der Berg', NULL, 'Lijnbaansgracht 120bg', 'Amsterdam', 'VV', 'Netherlands', '1016', '+31 020 6223130', NULL, 'johavanderberg@yahoo.nl', 5), (49, 'Stanisław', 'Wójcik', NULL, 'Ordynacka 10', 'Warsaw', NULL, 'Poland', '00-358', '+48 22 828 37 39', NULL, 'stanisław.wójcik@wp.pl', 4), (50, 'Enrique', 'Muñoz', NULL, 'C/ San Bernardo 85', 'Madrid', NULL, 'Spain', '28015', '+34 914 454 454', NULL, 'enrique_munoz@yahoo.es', 5);
UNLOCK TABLES;

#
# Table structure for table 'employees'
#

DROP TABLE IF EXISTS `employees` CASCADE;
CREATE TABLE `employees` (
  `EmployeeId` INT NOT NULL auto_increment,
  `LastName` VARCHAR(20) CHARACTER SET utf8 NOT NULL,
  `FirstName` VARCHAR(20) CHARACTER SET utf8 NOT NULL,
  `Title` VARCHAR(30) CHARACTER SET utf8,
  `ReportsTo` INT,
  `BirthDate` DATETIME,
  `HireDate` DATETIME,
  `Address` VARCHAR(70) CHARACTER SET utf8,
  `City` VARCHAR(40) CHARACTER SET utf8,
  `State` VARCHAR(40) CHARACTER SET utf8,
  `Country` VARCHAR(40) CHARACTER SET utf8,
  `PostalCode` VARCHAR(10) CHARACTER SET utf8,
  `Phone` VARCHAR(24) CHARACTER SET utf8,
  `Fax` VARCHAR(24) CHARACTER SET utf8,
  `Email` VARCHAR(60) CHARACTER SET utf8,
  PRIMARY KEY (`EmployeeId`),
  INDEX `IFK_EmployeeReportsTo` (`ReportsTo`)
) ENGINE=InnoDB;

#
# Dumping data for table 'employees'
#

LOCK TABLES `employees` WRITE;
INSERT IGNORE INTO `employees`(`EmployeeId`, `LastName`, `FirstName`, `Title`, `ReportsTo`, `BirthDate`, `HireDate`, `Address`, `City`, `State`, `Country`, `PostalCode`, `Phone`, `Fax`, `Email`) VALUES(1, 'Adams', 'Andrew', 'General Manager', NULL, '1962-02-18 00:00:00', '2002-08-14 00:00:00', '11120 Jasper Ave NW', 'Edmonton', 'AB', 'Canada', 'T5K 2N1', '+1 (780) 428-9482', '+1 (780) 428-3457', 'andrew@chinookcorp.com'), (2, 'Edwards', 'Nancy', 'Sales Manager', 1, '1958-12-08 00:00:00', '2002-05-01 00:00:00', '825 8 Ave SW', 'Calgary', 'AB', 'Canada', 'T2P 2T3', '+1 (403) 262-3443', '+1 (403) 262-3322', 'nancy@chinookcorp.com'), (3, 'Peacock', 'Jane', 'Sales Support Agent', 2, '1973-08-29 00:00:00', '2002-04-01 00:00:00', '1111 6 Ave SW', 'Calgary', 'AB', 'Canada', 'T2P 5M5', '+1 (403) 262-3443', '+1 (403) 262-6712', 'jane@chinookcorp.com'), (4, 'Park', 'Margaret', 'Sales Support Agent', 2, '1947-09-19 00:00:00', '2003-05-03 00:00:00', '683 10 Street SW', 'Calgary', 'AB', 'Canada', 'T2P 5G3', '+1 (403) 263-4423', '+1 (403) 263-4289', 'margaret@chinookcorp.com'), (5, 'Johnson', 'Steve', 'Sales Support Agent', 2, '1965-03-03 00:00:00', '2003-10-17 00:00:00', '7727B 41 Ave', 'Calgary', 'AB', 'Canada', 'T3B 1Y7', '1 (780) 836-9987', '1 (780) 836-9543', 'steve@chinookcorp.com'), (6, 'Mitchell', 'Michael', 'IT Manager', 1, '1973-07-01 00:00:00', '2003-10-17 00:00:00', '5827 Bowness Road NW', 'Calgary', 'AB', 'Canada', 'T3B 0C5', '+1 (403) 246-9887', '+1 (403) 246-9899', 'michael@chinookcorp.com'), (7, 'King', 'Robert', 'IT Staff', 6, '1970-05-29 00:00:00', '2004-01-02 00:00:00', '590 Columbia Boulevard West', 'Lethbridge', 'AB', 'Canada', 'T1K 5N8', '+1 (403) 456-9986', '+1 (403) 456-8485', 'robert@chinookcorp.com'), (8, 'Callahan', 'Laura', 'IT Staff', 6, '1968-01-09 00:00:00', '2004-03-04 00:00:00', '923 7 ST NW', 'Lethbridge', 'AB', 'Canada', 'T1H 1Y8', '+1 (403) 467-3351', '+1 (403) 467-8772', 'laura@chinookcorp.com');
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;

#
# Table structure for table 'genres'
#

DROP TABLE IF EXISTS `genres` CASCADE;
CREATE TABLE `genres` (
  `GenreId` INT NOT NULL auto_increment,
  `Name` VARCHAR(120) CHARACTER SET utf8,
  PRIMARY KEY (`GenreId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'genres'
#

LOCK TABLES `genres` WRITE;
INSERT IGNORE INTO `genres`(`GenreId`, `Name`) VALUES(1, 'Rock'), (2, 'Jazz'), (3, 'Metal'), (4, 'Alternative & Punk'), (5, 'Rock And Roll'), (6, 'Blues'), (7, 'Latin'), (8, 'Reggae'), (9, 'Pop'), (10, 'Soundtrack'), (11, 'Bossa Nova'), (12, 'Easy Listening'), (13, 'Heavy Metal'), (14, 'R&B/Soul'), (15, 'Electronica/Dance'), (16, 'World'), (17, 'Hip Hop/Rap'), (18, 'Science Fiction'), (19, 'TV Shows'), (20, 'Sci Fi & Fantasy'), (21, 'Drama'), (22, 'Comedy'), (23, 'Alternative'), (24, 'Classical'), (25, 'Opera');
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;

#
# Table structure for table 'invoice_items'
#

DROP TABLE IF EXISTS `invoice_items` CASCADE;
CREATE TABLE `invoice_items` (
  `InvoiceLineId` INT NOT NULL auto_increment,
  `InvoiceId` INT NOT NULL,
  `TrackId` INT NOT NULL,
  `UnitPrice` DECIMAL(10,2) NOT NULL,
  `Quantity` INT NOT NULL,
  PRIMARY KEY (`InvoiceLineId`),
  INDEX `IFK_InvoiceLineTrackId` (`TrackId`),
  INDEX `IFK_InvoiceLineInvoiceId` (`InvoiceId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'invoice_items'
#

LOCK TABLES `invoice_items` WRITE;
INSERT IGNORE INTO `invoice_items`(`InvoiceLineId`, `InvoiceId`, `TrackId`, `UnitPrice`, `Quantity`) VALUES(1, 1, 2, 9.8999999999999999e-01, 1), (2, 1, 4, 9.8999999999999999e-01, 1), (3, 2, 6, 9.8999999999999999e-01, 1), (4, 2, 8, 9.8999999999999999e-01, 1), (5, 2, 10, 9.8999999999999999e-01, 1), (6, 2, 12, 9.8999999999999999e-01, 1), (7, 3, 16, 9.8999999999999999e-01, 1), (8, 3, 20, 9.8999999999999999e-01, 1), (9, 3, 24, 9.8999999999999999e-01, 1), (10, 3, 28, 9.8999999999999999e-01, 1), (11, 3, 32, 9.8999999999999999e-01, 1), (12, 3, 36, 9.8999999999999999e-01, 1), (13, 4, 42, 9.8999999999999999e-01, 1), (14, 4, 48, 9.8999999999999999e-01, 1), (15, 4, 23, 9.8999999999999999e-01, 1), (16, 4, 34, 9.8999999999999999e-01, 1), (17, 4, 1, 9.8999999999999999e-01, 1), (18, 4, 23, 9.8999999999999999e-01, 1), (19, 4, 45, 9.8999999999999999e-01, 1), (20, 4, 23, 9.8999999999999999e-01, 1), (21, 4, 40, 9.8999999999999999e-01, 1), (22, 5, 21, 9.8999999999999999e-01, 1), (23, 5, 11, 9.8999999999999999e-01, 1), (24, 5, 1, 9.8999999999999999e-01, 1), (25, 5, 34, 9.8999999999999999e-01, 1), (26, 5, 13, 9.8999999999999999e-01, 1), (27, 5, 17, 9.8999999999999999e-01, 1), (28, 5, 19, 9.8999999999999999e-01, 1), (29, 5, 15, 9.8999999999999999e-01, 1), (30, 5, 25, 9.8999999999999999e-01, 1), (31, 5, 28, 9.8999999999999999e-01, 1), (32, 5, 24, 9.8999999999999999e-01, 1), (33, 5, 1, 9.8999999999999999e-01, 1), (34, 5, 2, 9.8999999999999999e-01, 1), (35, 5, 3, 9.8999999999999999e-01, 1), (36, 6, 4, 9.8999999999999999e-01, 1), (37, 7, 5, 9.8999999999999999e-01, 1), (38, 7, 6, 9.8999999999999999e-01, 1), (39, 8, 7, 9.8999999999999999e-01, 1), (40, 8, 8, 9.8999999999999999e-01, 1), (41, 9, 9, 9.8999999999999999e-01, 1), (42, 9, 12, 9.8999999999999999e-01, 1), (43, 9, 31, 9.8999999999999999e-01, 1), (44, 9, 13, 9.8999999999999999e-01, 1), (45, 10, 14, 9.8999999999999999e-01, 1), (46, 10, 15, 9.8999999999999999e-01, 1), (47, 10, 23, 9.8999999999999999e-01, 1), (48, 10, 24, 9.8999999999999999e-01, 1), (49, 10, 34, 9.8999999999999999e-01, 1), (50, 10, 43, 9.8999999999999999e-01, 1);
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;

#
# Table structure for table 'invoices'
#

DROP TABLE IF EXISTS `invoices` CASCADE;
CREATE TABLE `invoices` (
  `InvoiceId` INT NOT NULL auto_increment,
  `CustomerId` INT NOT NULL,
  `InvoiceDate` DATETIME NOT NULL,
  `BillingAddress` VARCHAR(70) CHARACTER SET utf8,
  `BillingCity` VARCHAR(40) CHARACTER SET utf8,
  `BillingState` VARCHAR(40) CHARACTER SET utf8,
  `BillingCountry` VARCHAR(40) CHARACTER SET utf8,
  `BillingPostalCode` VARCHAR(10) CHARACTER SET utf8,
  `Total` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`InvoiceId`),
  INDEX `IFK_InvoiceCustomerId` (`CustomerId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'invoices'
#

LOCK TABLES `invoices` WRITE;
INSERT IGNORE INTO `invoices`(`InvoiceId`, `CustomerId`, `InvoiceDate`, `BillingAddress`, `BillingCity`, `BillingState`, `BillingCountry`, `BillingPostalCode`, `Total`) VALUES(1, 2, '2009-01-01 00:00:00', 'Theodor-Heuss-Straße 34', 'Stuttgart', NULL, 'Germany', '70174', 1.9800000000000000e+00), (2, 4, '2009-01-02 00:00:00', 'Ullevålsveien 14', 'Oslo', NULL, 'Norway', '0171', 3.9600000000000000e+00), (3, 8, '2009-01-03 00:00:00', 'Grétrystraat 63', 'Brussels', NULL, 'Belgium', '1000', 5.9400000000000004e+00), (4, 14, '2009-01-06 00:00:00', '8210 111 ST NW', 'Edmonton', 'AB', 'Canada', 'T6G 2C7', 8.9100000000000001e+00), (5, 23, '2009-01-11 00:00:00', '69 Salem Street', 'Boston', 'MA', 'USA', '2113', 1.3859999999999999e+01), (6, 37, '2009-01-19 00:00:00', 'Berger Straße 10', 'Frankfurt', NULL, 'Germany', '60316', 9.8999999999999999e-01), (7, 38, '2009-02-01 00:00:00', 'Barbarossastraße 19', 'Berlin', NULL, 'Germany', '10779', 1.9800000000000000e+00), (8, 40, '2009-02-01 00:00:00', '8, Rue Hanovre', 'Paris', NULL, 'France', '75002', 1.9800000000000000e+00), (9, 42, '2009-02-02 00:00:00', '9, Place Louis Barthou', 'Bordeaux', NULL, 'France', '33000', 3.9600000000000000e+00), (10, 46, '2009-02-03 00:00:00', '3 Chatham Street', 'Dublin', 'Dublin', 'Ireland', NULL, 5.9400000000000004e+00), (11, 23, '2009-02-06 00:00:00', '202 Hoxton Street', 'London', NULL, 'United Kingdom', 'N1 5LH', 8.9100000000000001e+00), (12, 2, '2009-02-11 00:00:00', 'Theodor-Heuss-Straße 34', 'Stuttgart', NULL, 'Germany', '70174', 1.3859999999999999e+01), (13, 16, '2009-02-19 00:00:00', '1600 Amphitheatre Parkway', 'Mountain View', 'CA', 'USA', '94043-1351', 9.8999999999999999e-01), (14, 17, '2009-03-04 00:00:00', '1 Microsoft Way', 'Redmond', 'WA', 'USA', '98052-8300', 1.9800000000000000e+00), (15, 19, '2009-03-04 00:00:00', '1 Infinite Loop', 'Cupertino', 'CA', 'USA', '95014', 1.9800000000000000e+00), (16, 21, '2009-03-05 00:00:00', '801 W 4th Street', 'Reno', 'NV', 'USA', '89503', 3.9600000000000000e+00), (17, 25, '2009-03-06 00:00:00', '319 N. Frances Street', 'Madison', 'WI', 'USA', '53703', 5.9400000000000004e+00), (18, 31, '2009-03-09 00:00:00', '194A Chain Lake Drive', 'Halifax', 'NS', 'Canada', 'B3S 1C5', 8.9100000000000001e+00), (19, 40, '2009-03-14 00:00:00', '8, Rue Hanovre', 'Paris', NULL, 'France', '75002', 1.3859999999999999e+01), (20, 10, '2009-03-22 00:00:00', '110 Raeburn Pl', 'Edinburgh ', NULL, 'United Kingdom', 'EH4 1HH', 9.8999999999999999e-01), (21, 16, '2009-04-04 00:00:00', '421 Bourke Street', 'Sidney', 'NSW', 'Australia', '2010', 1.9800000000000000e+00), (22, 8, '2009-04-04 00:00:00', 'Calle Lira, 198', 'Santiago', NULL, 'Chile', NULL, 1.9800000000000000e+00), (23, 17, '2009-04-05 00:00:00', '3,Raj Bhavan Road', 'Bangalore', NULL, 'India', '560001', 3.9600000000000000e+00), (24, 4, '2009-04-06 00:00:00', 'Ullevålsveien 14', 'Oslo', NULL, 'Norway', '0171', 5.9400000000000004e+00), (25, 10, '2009-04-09 00:00:00', 'Rua Dr. Falcão Filho, 155', 'São Paulo', 'SP', 'Brazil', '01007-010', 8.9100000000000001e+00), (26, 19, '2009-04-14 00:00:00', '1 Infinite Loop', 'Cupertino', 'CA', 'USA', '95014', 1.3859999999999999e+01), (27, 33, '2009-04-22 00:00:00', '5112 48 Street', 'Yellowknife', 'NT', 'Canada', 'X1A 1N6', 9.8999999999999999e-01), (28, 34, '2009-05-05 00:00:00', 'Rua da Assunção 53', 'Lisbon', NULL, 'Portugal', NULL, 1.9800000000000000e+00), (29, 36, '2009-05-05 00:00:00', 'Tauentzienstraße 8', 'Berlin', NULL, 'Germany', '10789', 1.9800000000000000e+00), (30, 38, '2009-05-06 00:00:00', 'Barbarossastraße 19', 'Berlin', NULL, 'Germany', '10779', 3.9600000000000000e+00), (31, 42, '2009-05-07 00:00:00', '9, Place Louis Barthou', 'Bordeaux', NULL, 'France', '33000', 5.9400000000000004e+00), (32, 48, '2009-05-10 00:00:00', 'Lijnbaansgracht 120bg', 'Amsterdam', 'VV', 'Netherlands', '1016', 8.9100000000000001e+00), (33, 15, '2009-05-15 00:00:00', 'Calle Lira, 198', 'Santiago', NULL, 'Chile', NULL, 1.3859999999999999e+01), (34, 12, '2009-05-23 00:00:00', 'Praça Pio X, 119', 'Rio de Janeiro', 'RJ', 'Brazil', '20040-020', 9.8999999999999999e-01), (35, 13, '2009-06-05 00:00:00', 'Qe 7 Bloco G', 'Brasília', 'DF', 'Brazil', '71020-677', 1.9800000000000000e+00), (36, 15, '2009-06-05 00:00:00', '700 W Pender Street', 'Vancouver', 'BC', 'Canada', 'V6C 1G8', 1.9800000000000000e+00), (37, 17, '2009-06-06 00:00:00', '1 Microsoft Way', 'Redmond', 'WA', 'USA', '98052-8300', 3.9600000000000000e+00), (38, 21, '2009-06-07 00:00:00', '801 W 4th Street', 'Reno', 'NV', 'USA', '89503', 5.9400000000000004e+00), (39, 27, '2009-06-10 00:00:00', '1033 N Park Ave', 'Tucson', 'AZ', 'USA', '85719', 8.9100000000000001e+00), (40, 36, '2009-06-15 00:00:00', 'Tauentzienstraße 8', 'Berlin', NULL, 'Germany', '10789', 1.3859999999999999e+01), (41, 50, '2009-06-23 00:00:00', 'C/ San Bernardo 85', 'Madrid', NULL, 'Spain', '28015', 9.8999999999999999e-01), (42, 45, '2009-07-06 00:00:00', 'Celsiusg. 9', 'Stockholm', NULL, 'Sweden', '11230', 1.9800000000000000e+00), (43, 34, '2009-07-06 00:00:00', '113 Lupus St', 'London', NULL, 'United Kingdom', 'SW1V 3EN', 1.9800000000000000e+00), (44, 23, '2009-07-07 00:00:00', '421 Bourke Street', 'Sidney', 'NSW', 'Australia', '2010', 3.9600000000000000e+00), (45, 5, '2009-07-08 00:00:00', '3,Raj Bhavan Road', 'Bangalore', NULL, 'India', '560001', 5.9400000000000004e+00), (46, 6, '2009-07-11 00:00:00', 'Rilská 3174/6', 'Prague', NULL, 'Czech Republic', '14300', 8.9100000000000001e+00), (47, 15, '2009-07-16 00:00:00', '700 W Pender Street', 'Vancouver', 'BC', 'Canada', 'V6C 1G8', 1.3859999999999999e+01), (48, 29, '2009-07-24 00:00:00', '796 Dundas Street West', 'Toronto', 'ON', 'Canada', 'M6J 1V1', 9.8999999999999999e-01), (49, 30, '2009-08-06 00:00:00', '230 Elgin Street', 'Ottawa', 'ON', 'Canada', 'K2P 1L7', 1.9800000000000000e+00), (50, 32, '2009-08-06 00:00:00', '696 Osborne Street', 'Winnipeg', 'MB', 'Canada', 'R3L 2B9', 1.9800000000000000e+00);
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;
ALTER TABLE `invoices` MODIFY `InvoiceDate` DATE NOT NULL;

#
# Table structure for table 'media_types'
#

DROP TABLE IF EXISTS `media_types` CASCADE;
CREATE TABLE `media_types` (
  `MediaTypeId` INT NOT NULL auto_increment,
  `Name` VARCHAR(120) CHARACTER SET utf8,
  PRIMARY KEY (`MediaTypeId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'media_types'
#

LOCK TABLES `media_types` WRITE;
INSERT IGNORE INTO `media_types`(`MediaTypeId`, `Name`) VALUES(1, 'MPEG audio file'), (2, 'Protected AAC audio file'), (3, 'Protected MPEG-4 video file'), (4, 'Purchased AAC audio file'), (5, 'AAC audio file');
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;
ALTER TABLE `invoices` MODIFY `InvoiceDate` DATE NOT NULL;

#
# Table structure for table 'playlist_track'
#

DROP TABLE IF EXISTS `playlist_track` CASCADE;
CREATE TABLE `playlist_track` (
  `PlaylistId` INT NOT NULL auto_increment,
  `TrackId` INT NOT NULL,
  PRIMARY KEY (`PlaylistId`, `TrackId`),
  INDEX `IFK_PlaylistTrackTrackId` (`TrackId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'playlist_track'
#

LOCK TABLES `playlist_track` WRITE;
INSERT IGNORE INTO `playlist_track`(`PlaylistId`, `TrackId`) VALUES(1, 4), (1, 7), (1, 23), (1, 12), (2, 5), (1, 23), (3, 5), (1, 32), (1, 1), (1, 1), (1, 43), (1, 25), (1, 12), (2, 34), (6, 23), (13, 21), (1, 43);
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;
ALTER TABLE `invoices` MODIFY `InvoiceDate` DATE NOT NULL;

#
# Table structure for table 'playlists'
#

DROP TABLE IF EXISTS `playlists` CASCADE;
CREATE TABLE `playlists` (
  `PlaylistId` INT NOT NULL auto_increment,
  `Name` VARCHAR(120) CHARACTER SET utf8,
  PRIMARY KEY (`PlaylistId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'playlists'
#

LOCK TABLES `playlists` WRITE;
INSERT IGNORE INTO `playlists`(`PlaylistId`, `Name`) VALUES(1, 'Music'), (2, 'Movies'), (3, 'TV Shows'), (4, 'Audiobooks'), (5, '90’s Music'), (6, 'Audiobooks'), (7, 'Movies'), (8, 'Music'), (9, 'Music Videos'), (10, 'TV Shows'), (11, 'Brazilian Music'), (12, 'Classical'), (13, 'Classical 101 - Deep Cuts'), (14, 'Classical 101 - Next Steps'), (15, 'Classical 101 - The Basics'), (16, 'Grunge'), (17, 'Heavy Metal Classic'), (18, 'On-The-Go 1');
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;
ALTER TABLE `invoices` MODIFY `InvoiceDate` DATE NOT NULL;

#
# Table structure for table 'tracks'
#

DROP TABLE IF EXISTS `tracks` CASCADE;
CREATE TABLE `tracks` (
  `TrackId` INT NOT NULL auto_increment,
  `Name` VARCHAR(200) CHARACTER SET utf8 NOT NULL,
  `AlbumId` INT,
  `MediaTypeId` INT NOT NULL,
  `GenreId` INT,
  `Composer` VARCHAR(220) CHARACTER SET utf8,
  `Milliseconds` INT NOT NULL,
  `Bytes` INT,
  `UnitPrice` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`TrackId`),
  INDEX `IFK_TrackMediaTypeId` (`MediaTypeId`),
  INDEX `IFK_TrackGenreId` (`GenreId`),
  INDEX `IFK_TrackAlbumId` (`AlbumId`)
) ENGINE=InnoDB;

#
# Dumping data for table 'tracks'
#

LOCK TABLES `tracks` WRITE;
INSERT IGNORE INTO `tracks`(`TrackId`, `Name`, `AlbumId`, `MediaTypeId`, `GenreId`, `Composer`, `Milliseconds`, `Bytes`, `UnitPrice`) VALUES(1, 'For Those About To Rock (We Salute You)', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 343719, 11170334, 9.8999999999999999e-01), (2, 'Balls to the Wall', 2, 2, 1, NULL, 342562, 5510424, 9.8999999999999999e-01), (3, 'Fast As a Shark', 3, 2, 1, 'F. Baltes, S. Kaufman, U. Dirkscneider & W. Hoffman', 230619, 3990994, 9.8999999999999999e-01), (4, 'Restless and Wild', 3, 2, 1, 'F. Baltes, R.A. Smith-Diesel, S. Kaufman, U. Dirkscneider & W. Hoffman', 252051, 4331779, 9.8999999999999999e-01), (5, 'Princess of the Dawn', 3, 2, 1, 'Deaffy & R.A. Smith-Diesel', 375418, 6290521, 9.8999999999999999e-01), (6, 'Put The Finger On You', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 205662, 6713451, 9.8999999999999999e-01), (7, 'Let\'s Get It Up', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 233926, 7636561, 9.8999999999999999e-01), (8, 'Inject The Venom', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 210834, 6852860, 9.8999999999999999e-01), (9, 'Snowballed', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 203102, 6599424, 9.8999999999999999e-01), (10, 'Evil Walks', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 263497, 8611245, 9.8999999999999999e-01), (11, 'C.O.D.', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 199836, 6566314, 9.8999999999999999e-01), (12, 'Breaking The Rules', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 263288, 8596840, 9.8999999999999999e-01), (13, 'Night Of The Long Knives', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 205688, 6706347, 9.8999999999999999e-01), (14, 'Spellbound', 1, 1, 1, 'Angus Young, Malcolm Young, Brian Johnson', 270863, 8817038, 9.8999999999999999e-01), (15, 'Go Down', 4, 1, 1, 'AC/DC', 331180, 10847611, 9.8999999999999999e-01), (16, 'Dog Eat Dog', 4, 1, 1, 'AC/DC', 215196, 7032162, 9.8999999999999999e-01), (17, 'Let There Be Rock', 4, 1, 1, 'AC/DC', 366654, 12021261, 9.8999999999999999e-01), (18, 'Bad Boy Boogie', 4, 1, 1, 'AC/DC', 267728, 8776140, 9.8999999999999999e-01), (19, 'Problem Child', 4, 1, 1, 'AC/DC', 325041, 10617116, 9.8999999999999999e-01), (20, 'Overdose', 4, 1, 1, 'AC/DC', 369319, 12066294, 9.8999999999999999e-01), (21, 'Hell Ain\'t A Bad Place To Be', 4, 1, 1, 'AC/DC', 254380, 8331286, 9.8999999999999999e-01), (22, 'Whole Lotta Rosie', 4, 1, 1, 'AC/DC', 323761, 10547154, 9.8999999999999999e-01), (23, 'Walk On Water', 5, 1, 1, 'Steven Tyler, Joe Perry, Jack Blades, Tommy Shaw', 295680, 9719579, 9.8999999999999999e-01), (24, 'Love In An Elevator', 5, 1, 1, 'Steven Tyler, Joe Perry', 321828, 10552051, 9.8999999999999999e-01), (25, 'Rag Doll', 5, 1, 1, 'Steven Tyler, Joe Perry, Jim Vallance, Holly Knight', 264698, 8675345, 9.8999999999999999e-01), (26, 'What It Takes', 5, 1, 1, 'Steven Tyler, Joe Perry, Desmond Child', 310622, 10144730, 9.8999999999999999e-01), (27, 'Dude (Looks Like A Lady)', 5, 1, 1, 'Steven Tyler, Joe Perry, Desmond Child', 264855, 8679940, 9.8999999999999999e-01), (28, 'Janie\'s Got A Gun', 5, 1, 1, 'Steven Tyler, Tom Hamilton', 330736, 10869391, 9.8999999999999999e-01), (29, 'Cryin\'', 5, 1, 1, 'Steven Tyler, Joe Perry, Taylor Rhodes', 309263, 10056995, 9.8999999999999999e-01), (30, 'Amazing', 5, 1, 1, 'Steven Tyler, Richie Supa', 356519, 11616195, 9.8999999999999999e-01), (31, 'Blind Man', 5, 1, 1, 'Steven Tyler, Joe Perry, Taylor Rhodes', 240718, 7877453, 9.8999999999999999e-01), (32, 'Deuces Are Wild', 5, 1, 1, 'Steven Tyler, Jim Vallance', 215875, 7074167, 9.8999999999999999e-01), (33, 'The Other Side', 5, 1, 1, 'Steven Tyler, Jim Vallance', 244375, 7983270, 9.8999999999999999e-01), (34, 'Crazy', 5, 1, 1, 'Steven Tyler, Joe Perry, Desmond Child', 316656, 10402398, 9.8999999999999999e-01), (35, 'Eat The Rich', 5, 1, 1, 'Steven Tyler, Joe Perry, Jim Vallance', 251036, 8262039, 9.8999999999999999e-01), (36, 'Angel', 5, 1, 1, 'Steven Tyler, Desmond Child', 307617, 9989331, 9.8999999999999999e-01), (37, 'Livin\' On The Edge', 5, 1, 1, 'Steven Tyler, Joe Perry, Mark Hudson', 381231, 12374569, 9.8999999999999999e-01), (38, 'All I Really Want', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 284891, 9375567, 9.8999999999999999e-01), (39, 'You Oughta Know', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 249234, 8196916, 9.8999999999999999e-01), (40, 'Perfect', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 188133, 6145404, 9.8999999999999999e-01), (41, 'Hand In My Pocket', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 221570, 7224246, 9.8999999999999999e-01), (42, 'Right Through You', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 176117, 5793082, 9.8999999999999999e-01), (43, 'Forgiven', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 300355, 9753256, 9.8999999999999999e-01), (44, 'You Learn', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 239699, 7824837, 9.8999999999999999e-01), (45, 'Head Over Feet', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 267493, 8758008, 9.8999999999999999e-01), (46, 'Mary Jane', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 280607, 9163588, 9.8999999999999999e-01), (47, 'Ironic', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 229825, 7598866, 9.8999999999999999e-01), (48, 'Not The Doctor', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 227631, 7604601, 9.8999999999999999e-01), (49, 'Wake Up', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 293485, 9703359, 9.8999999999999999e-01), (50, 'You Oughta Know (Alternate)', 6, 1, 1, 'Alanis Morissette & Glenn Ballard', 491885, 16008629, 9.8999999999999999e-01);
UNLOCK TABLES;
ALTER TABLE `employees` MODIFY `BirthDate` DATE;
ALTER TABLE `employees` MODIFY `HireDate` DATE;
ALTER TABLE `invoices` MODIFY `InvoiceDate` DATE NOT NULL;
