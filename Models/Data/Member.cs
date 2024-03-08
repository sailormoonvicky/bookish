using System.ComponentModel.DataAnnotations.Schema;

namespace bookish.Models.Data;


public class Member
{
    public required int Id {get; set;}
    public required string UserName {get; set;}

    public  List<Loan> LoanHistory {get; set; } = [];
    [NotMapped]
    public List<Loan> ActiveLoans => LoanHistory.Where(loan => loan.DateReturned == null).ToList();

}