using System.ComponentModel.DataAnnotations.Schema;
using bookish.Enums;

namespace bookish.Models.Data;

public class Copy
{
    public int Id { get; set; }
    public int BookId { get; set;}
    [ForeignKey(nameof(BookId))]
    public Book Book { get; set;} = null!;

    public Condition Condition {get; set;} = Condition.Pristine;

    public List<Loan> LoanHistory {get; set; } = [];

    public Boolean HasLiveLoan => LoanHistory.Any(loan => loan.DateReturned == null);

    public Loan? LiveLoan => LoanHistory.SingleOrDefault(loan => loan.DateReturned == null);
}