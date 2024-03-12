// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

public class Book
{
    public string Title{get; set;}
    public string Author { get; set;}
    public string ISBN { get; set;}
    public int PublicationYear { get; set;}

    public Book(string title, string author, string iSBN, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = iSBN;
        PublicationYear = publicationYear;
    }
}

public class MediaItem
{
    public string Title { get; set; }
    public string MediaType { get; set; }
    public int Duration { get; set; }

    public MediaItem(string title, string mediaType, int duration)
    {
        Title = title;
        MediaType = mediaType;
        Duration = duration;
    }
}

public class Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Book> Books { get; set; }
    public List<MediaItem> MediaItems { get; set; }

    public Library(string name, string address)
    {
        Name = name;
        Address = address;
        Books = new List<Book>();
        MediaItems = new List<MediaItem>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void AddMediaItem(MediaItem mediaItem)
    {
        MediaItems.Add(mediaItem);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void RemoveMediaItem(MediaItem mediaItem)
    {
        MediaItems.Remove(mediaItem);
    }   

    public void PrintCatalog()
    {
        Console.WriteLine($"Catalog for {Name} at {Address}");
        Console.WriteLine("Books:");

        foreach (var book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author} ({book.PublicationYear}), ISBN: {book.ISBN}");
        }

        Console.WriteLine("Media Items:");
        foreach (var mediaItem in MediaItems)
        {
            Console.WriteLine($"{mediaItem.Title} ({mediaItem.MediaType}), Duration: {mediaItem.Duration} minutes");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Library library = new Library("Abrehot", "Arat Kilo, AddisAbaba");

        library.AddBook(new Book("Book1", "Author1", "ISBN123", 2000));
        library.AddBook(new Book("Book2", "Author2", "ISBN456", 2010));

        library.AddMediaItem(new MediaItem("DVD1", "DVD", 120));
        library.AddMediaItem(new MediaItem("CD1", "CD", 60));

        library.PrintCatalog();
        
        Book book = library.Books[0];
        library.RemoveBook(book);
        library.RemoveBook(book);

        library.PrintCatalog();
    }
}