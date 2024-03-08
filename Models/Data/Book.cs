using Microsoft.Extensions.Localization;

namespace bookish.Models.Data;

public class Book
{
    public required int Id {get; set;}
    public required string ISBN { get; set;}
    public required string Title { get; init;}

    public required string Author {get; init;}

    public required int YearOfPublication {get; set;}

    public required string Publisher {get; set;}

    public required string ImageURLS {get; set;}

    public required string ImageURLM {get; set;}
    public required string ImageURLL {get; set;}

    public List<Copy> Copies {get; set; } = [];
    
    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, Year of Publish: {YearOfPublication}, Publisher: {Publisher}";
    }
}