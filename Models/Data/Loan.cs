using System.ComponentModel.DataAnnotations.Schema;
using bookish.Constants;

namespace bookish.Models.Data;

public class Loan
{
    public int Id {get; set; }
    public int MemberId { set; get;}
    [ForeignKey("MemberId")]
    public Member Member { set; get;} = null!;

    public int CopyId {get; set;}
    [ForeignKey(nameof(CopyId))]
    public Copy Copy {get; set;} = null!;
    public DateOnly DateBorrowed { get; set;} = DateOnly.FromDateTime(DateTime.Today);

    public DateOnly DateDueBack { get; set; } = DateOnly.FromDateTime(DateTime.Today).AddDays(LoanConstants.DefaultLoanLength);
    public DateOnly? DateReturned { get; set;}
    
}