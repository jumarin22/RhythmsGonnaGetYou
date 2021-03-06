using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// Run RhythmScript.sql to create database. 

namespace RhythmsGonnaGetYou
{

    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public int ContactPhoneNumber { get; set; }
    }

    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }

        public Band Band { get; set; }
    }

    class Song
    {
        public int Id { get; set; }
        public int Track { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }

    class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Rhythm;Username=foo;Password=secret");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            var bandCount = context.Bands.Count();
            Console.WriteLine($"There are {bandCount} bands!");

            // Main user interface.
            var keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("(A)dd a band, album, or song.");
                Console.WriteLine("(S)ee bands.");
                Console.WriteLine("(L)et a band go.");
                Console.WriteLine("(R)e-sign a band.");
                Console.WriteLine("(V)iew albums.");
                Console.WriteLine("(Q)uit.");
                var userChoice = Console.ReadLine().ToLower();

                switch (userChoice)
                {
                    case "a":
                        AddSomething();
                        break;
                    case "s":
                        SeeBands();
                        break;
                    case "l":
                        LetGoBand();
                        break;
                    case "r":
                        ReSignBand();
                        break;
                    case "v":
                        ViewAlbums();
                        break;
                    case "q":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Sorry, I don't understand.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private static void AddSomething()
        {
            Console.WriteLine("What would you like to add?");
            Console.WriteLine("(B)and\n(A)lbum\n(S)ong");
            var uChoice = Console.ReadLine().ToLower();

            switch (uChoice)
            {
                case "b":
                    AddBand();
                    break;
                case "a":
                    AddAlbum();
                    break;
                case "s":
                    AddSong();
                    break;
                default:
                    Console.WriteLine("Sorry, I don't understand.");
                    break;
            }
        }

        private static void AddSong()
        {
            var context = new RhythmsGonnaGetYouContext();
            Console.WriteLine("Please enter the song information:");
            Console.Write("Track Number: "); var songTrack = Console.ReadLine();
            Console.Write("Title: "); var songTitle = Console.ReadLine();
            Console.Write("Duration (mm:ss): "); var songDuration = Console.ReadLine();
            Console.Write("Album ID: "); var songAlbum = Console.ReadLine();

            var newSong = new Song
            {
                Track = int.Parse(songTrack),
                Title = songTitle,
                Duration = songDuration,
                AlbumId = int.Parse(songAlbum)
            };

            context.Songs.Add(newSong);
            context.SaveChanges();
        }

        private static void AddAlbum()
        {
            var context = new RhythmsGonnaGetYouContext();
            Console.WriteLine("Please enter the album information:");
            Console.Write("Title: "); var albumTitle = Console.ReadLine();
            Console.Write("Is the album explicit? (true / false): "); var albumExplicit = Console.ReadLine();
            Console.Write("Release Date: YYYY-MM-DD: "); var albumDate = Console.ReadLine();
            Console.Write("Band ID: "); var albumBand = Console.ReadLine();


            var newAlbum = new Album
            {
                Title = albumTitle,
                IsExplicit = bool.Parse(albumExplicit),
                ReleaseDate = DateTime.Parse(albumDate),
                BandId = int.Parse(albumBand)
            };

            context.Albums.Add(newAlbum);
            context.SaveChanges();
        }

        private static void AddBand()
        {
            var context = new RhythmsGonnaGetYouContext();
            Console.WriteLine("Please enter the band information:");
            Console.Write("Name: "); var bandName = Console.ReadLine();
            Console.Write("Country: "); var bandCountry = Console.ReadLine();
            Console.Write("Number of Members: "); var bandMembers = Console.ReadLine();
            Console.Write("Website: "); var bandWebsite = Console.ReadLine();
            Console.Write("Style: "); var bandStyle = Console.ReadLine();
            Console.Write("Are they contracted? (true / false): "); var bandIsSigned = Console.ReadLine();
            Console.Write("Contact Name: "); var bandContact = Console.ReadLine();
            Console.Write("Contact Phone: "); var bandPhone = Console.ReadLine();

            var newBand = new Band
            {
                Name = bandName,
                CountryOfOrigin = bandCountry,
                NumberOfMembers = int.Parse(bandMembers),
                Website = bandWebsite,
                Style = bandStyle,
                IsSigned = bool.Parse(bandIsSigned),
                ContactName = bandContact,
                ContactPhoneNumber = int.Parse(bandPhone)
            };

            context.Bands.Add(newBand);
            context.SaveChanges();
        }

        private static void SeeBands()
        {
            var context = new RhythmsGonnaGetYouContext();
            Console.WriteLine("(S)ee all bands in the database.");
            Console.WriteLine("(C)contracted bands only.");
            Console.WriteLine("(N)on-contracted bands only.");
            var uChoice = Console.ReadLine().ToLower();

            if (uChoice == "s")
            {
                Console.WriteLine("Here are all the bands in the database:");
                foreach (var band in context.Bands)
                {
                    Console.WriteLine($"{band.Name}");
                }
            }
            else if (uChoice == "c")
            {
                Console.WriteLine("Here are the contracted bands...");
                foreach (var band in context.Bands)
                {
                    if (band.IsSigned == true)
                        Console.WriteLine(band.Name);
                }
            }
            else if (uChoice == "n")
            {
                Console.WriteLine("Here are bands that are not under contract...");
                foreach (var band in context.Bands)
                {
                    if (band.IsSigned == false)
                        Console.WriteLine(band.Name);
                }
            }
        }

        // Let a band go (update isSigned to false).
        private static void LetGoBand()
        {
            var context = new RhythmsGonnaGetYouContext();

            Console.WriteLine("What is the name of the band that you want to let go?");
            var uAnswer = Console.ReadLine().ToLower();

            var existingBand = context.Bands.FirstOrDefault(band => band.Name.ToLower().Contains(uAnswer));

            if (existingBand != null)
            {
                existingBand.IsSigned = false;
                Console.WriteLine($"Let go of {existingBand.Name}");
                context.SaveChanges();
            }
            else
                Console.WriteLine($"Couldn't find {uAnswer} in the database.");

        }

        // Resign a band (update isSigned to true).
        private static void ReSignBand()
        {
            var context = new RhythmsGonnaGetYouContext();

            Console.WriteLine("What is the name of the band that you want to re-sign?");
            var uAnswer = Console.ReadLine().ToLower();

            var existingBand = context.Bands.FirstOrDefault(band => band.Name.ToLower().Contains(uAnswer));

            if (existingBand != null)
            {
                existingBand.IsSigned = true;
                Console.WriteLine($"Re-signed {existingBand.Name}");
                context.SaveChanges();
            }
            else
                Console.WriteLine($"Couldn't find {uAnswer} in the database.");

        }

        // Prompt for a band name and view all their albums.
        // View all albums ordered by ReleaseDate. 
        private static void ViewAlbums()
        {
            var context = new RhythmsGonnaGetYouContext();
            var bandName = context.Albums.Include(album => album.Band);

            Console.WriteLine("Enter band name to view their albums, or");
            Console.WriteLine("(V)iew all albums in the database, ordered by release date. ");
            var uChoice = Console.ReadLine().ToLower();

            if ((uChoice == "v"))
            {
                Console.WriteLine("Here are all albums in the database,ordered by release date...");
                var albumsOrdered = context.Albums.OrderBy(album => album.ReleaseDate);
                foreach (var album in bandName)
                {
                    Console.WriteLine($"{album.ReleaseDate.ToString("yyyy-MM-dd")}, {album.Title} by {album.Band.Name}");
                }
            }

            else
            {
                var existingBand = context.Bands.FirstOrDefault(band => band.Name.ToLower().Contains(uChoice));

                if (existingBand != null)
                {
                    Console.WriteLine($"Albums from {existingBand.Name}");
                    foreach (var album in context.Albums)
                    {
                        if (album.BandId == existingBand.Id)
                            Console.WriteLine(album.Title);
                    }
                }
                else
                    Console.WriteLine($"Couldn't find {uChoice} in the database.");
            }
        }
    }
}
