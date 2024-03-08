
using System.Threading.Tasks.Dataflow;
using bookish.Models.Data;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using bookish.Enums;
using System.Diagnostics.SymbolStore;

namespace bookish;

public class Library: DbContext
{
    public DbSet<Book> Books {get; set;} = null!;

    public DbSet<Copy> Copies { get; set; } = null!;

    public DbSet<Loan> Loans {get; set; } = null!;

    public DbSet<Member> Members {get; set; } = null!;

    public Library(DbContextOptions<Library> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        using var fileReader = new StreamReader("./Models/Data/Books.csv");
        using var csvData = new CsvReader(fileReader, CultureInfo.InvariantCulture);
        try{
            modelBuilder.Entity<Book>().HasMany(book => book.Copies).WithOne(copy => copy.Book);
            modelBuilder.Entity<Copy>().HasOne(copy => copy.Book).WithMany(book => book.Copies);
            modelBuilder.Entity<Copy>().HasMany(copy=>copy.LoanHistory).WithOne(loan=> loan.Copy);
            modelBuilder.Entity<Member>().HasMany(member => member.LoanHistory).WithOne(loan => loan.Member);
            modelBuilder.Entity<Loan>().HasOne(loan => loan.Member).WithMany(member => member.LoanHistory);
            modelBuilder.Entity<Loan>().HasOne(loan => loan.Copy).WithMany(copy => copy.LoanHistory);
            var cId = 1;
            var bId = 1;
            foreach(var book in csvData.GetRecords<BookTrans>())
            {
                var newBook = new Book

                {
                    Id = bId++,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    YearOfPublication = int.Parse(book.YearOfPublication),
                    Publisher = book.Publisher,
                    ImageURLS = book.ImageURLS,
                    ImageURLM = book.ImageURLM,
                    ImageURLL = book.ImageURLL,
                };
                modelBuilder.Entity<Book>().HasData(newBook);

                for(int i=0; i<2; i++)
                {
                    var newCopy = new Copy
                    {
                        Id=cId++,
                        BookId = bId-1,
                        Condition = Condition.Pristine
                    };
                    modelBuilder.Entity<Copy>().HasData(newCopy);
                }

            }
            
        }
        catch (CsvHelper.TypeConversion.TypeConverterException )
        {
            Console.WriteLine("Sorry, that file doesn't match the format.");
        }

        var member1 = new Member
        {
            Id = 1,
            UserName = "Vicky",
        };

        var member2 = new Member
        {
            Id = 2,
            UserName = "Tam",
        };

        var member3 = new Member
        {
            Id = 3,
            UserName = "Oli",
        }; 
        modelBuilder.Entity<Member>().HasData(member1,member2, member3); 

        var loan1 = new Loan
        {
            Id = 1,
            MemberId = 1,
            CopyId = 5,
            DateBorrowed = DateOnly.FromDateTime(DateTime.Today).AddDays(-20),
            DateDueBack = DateOnly.FromDateTime(DateTime.Today).AddDays(-6),
            DateReturned = DateOnly.FromDateTime(DateTime.Today).AddDays(-8),
        };   
        var loan2 = new Loan
        {
            Id = 2,
            MemberId = 2,
            CopyId = 10,
            DateBorrowed = DateOnly.FromDateTime(DateTime.Today).AddDays(-5),
            DateDueBack = DateOnly.FromDateTime(DateTime.Today).AddDays(9),
        }; 

        var loan3 = new Loan
        {
            Id = 3,
            MemberId = 3,
            CopyId = 7,
            DateBorrowed = DateOnly.FromDateTime(DateTime.Today).AddDays(-4),
            DateDueBack = DateOnly.FromDateTime(DateTime.Today).AddDays(10),
        };   
        modelBuilder.Entity<Loan>().HasData(loan1, loan3, loan2);
    }
}