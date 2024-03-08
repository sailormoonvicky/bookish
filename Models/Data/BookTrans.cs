using Microsoft.Extensions.Localization;

namespace bookish.Models.Data;

public class BookTrans
{
    public required string ISBN { get; set;}
    public required string Title { get; init;}

    public required string Author {get; init;}

    public required string YearOfPublication {get; set;}

    public required string Publisher {get; set;}

    public required string ImageURLS {get; set;}

    public required string ImageURLM {get; set;}
    public required string ImageURLL {get; set;}

}