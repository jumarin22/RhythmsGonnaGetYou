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



            // Create a menu system that shows the following options to the user until they choose to quit your program

            // Add a new band, album, or song.
            // Quit the program

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
                        // ;
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
