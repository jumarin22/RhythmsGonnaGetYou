-- Create role.
CREATE ROLE foo SUPERUSER PASSWORD 'secret';

-- Create Tables.
CREATE TABLE "Bands" (
  "Id" SERIAL PRIMARY KEY,
  "Name" TEXT NOT NULL,
  "CountryOfOrigin" TEXT,
  "NumberOfMembers" INT,
  "Website" TEXT,
  "Style" TEXT,
  "IsSigned" BOOLEAN,
  "ContactName" TEXT,
  "ContactPhoneNumber" INT
);

CREATE TABLE "Albums"(
  "Id" SERIAL PRIMARY KEY,
  "Title" TEXT,
  "IsExplicit" BOOLEAN,
  "ReleaseDate" DATE,
  "BandId" INTEGER REFERENCES "Bands" ("Id")
);

CREATE TABLE "Songs" (
  "Id" SERIAL PRIMARY KEY,
  "Track" SMALLINT,
  "Title" TEXT,
  "Duration" TEXT,
  "AlbumId" INTEGER REFERENCES "Albums" ("Id")
);


-- Insert Bands.
INSERT INTO "Bands"("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('The Beatles', 'England', '4', 'www.thebeatles.com', 'Pop', 'false', 'Mickey Mouse', '666666666');

INSERT INTO "Bands"("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Rage Against The Machine', 'USA', '4', 'www.ratm.com', 'Rap metal', 'true', 'Uncle Sam', '123456789');

INSERT INTO "Bands"("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Daft Punk', 'France', '2', 'www.daftpunk.com', 'House', 'false', 'Robot', '555555555');


-- Insert Albums.
INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId") 
VALUES ('Please Please Me', 'false', '1963-03-22', '1');

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId") 
VALUES ('Evil Empire', 'true', '1996-04-16', '2');

INSERT INTO "Albums" ("Title", "IsExplicit", "ReleaseDate", "BandId") 
VALUES ('Discovery', 'false', '2001-03-12', '3');


-- Insert Songs.
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('1', 'I Saw Her Standing There', '2:55', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('2', 'Misery', '1:49', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('3', 'Anna (Go to Him)', '2:55', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('4', 'Chains', '2:23', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('5', 'Boys', '2:24', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('6', 'Ask Me Why', '2:24', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('7', 'Please Please Me', '1:59', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('8', 'Love Me Do', '2:21', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('9', 'P.S. I Love You"', '2:04', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('10', 'Baby It''s You' , '2:40', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('11', 'Do You Want to Know a Secret', '1:56', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('12', 'A Taste of Honey', '2:03', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('13', 'There''s a Place', '1:51', '1');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('14', 'Twist and Shout', '2:32', '1');

INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('1', 'People of the Sun', '2:30', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('2',	'Bulls on Parade',	'3:49', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('3',	'Vietnow', '4:39', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('4',	'Revolver', '5:30', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('5',	'Snakecharmer',	'3:56', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('6',	'Tire Me', '3:00', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('7',	'Down Rodeo', '5:20', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('8',	'Without a Face',	'3:36', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('9',	'Wind Below',	'5:50', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('10', 'Roll Right', '4:22', '2');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('11', 'Year of tha Boomerang',	'4:02', '2');

INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('1',	'One More Time',	'5:20', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('2',	'Aerodynamic',	'3:27', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('3',	'Digital Love',	'4:58', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('4',	'Harder, Better, Faster, Stronger',	'3:45', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('5',	'Crescendolls',	'3:31', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('6',	'Nightvision',	'1:44', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('7',	'Superheroes',	'3:57', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('8',	'High Life',	'3:22', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('9',	'Something About Us', '3:51', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('10', 'Voyager','3:47', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('11', 'Veridis Quo',	'5:44', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('12', 'Short Circuit', '3:26', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('13', 'Face to Face', '3:58', '3');
INSERT INTO "Songs" ("Track", "Title", "Duration", "AlbumId") VALUES ('14', 'Too Long', '10:00', '3');