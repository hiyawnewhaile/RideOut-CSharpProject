#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;


namespace RideOut.Models;

public class User
{
    [Key]
    public int UserId {get;set;}
    [Required]
    [MinLength(2)]
    public string FName {get;set;}
    [Required]
    [MinLength(2)]
    public string LName {get;set;}
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email {get;set;}
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    // [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage ="Password must be atleast 8 characters, must include a number and must also include a special character")]
    public string Password {get;set;}
    // Anything under the 'notMapped' will not go into the database
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Required]
    public string PassConfirm {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Ride> RidesPosted {get;set;} = new List<Ride>();
    public List<Bike> BikeOwned {get;set;} = new List<Bike>();
    public List<Join> RideJoined {get;set;} = new List<Join>();
}