#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RideOut.Models;

public class Ride
{
    [Key]
    public int RideId {get;set;}
    [Required]
    public string Title {get;set;}
    [Required]
    public string Address {get;set;}
    [Required]
    public string City {get;set;}
    [Required]
    public string State {get;set;}
    [Required]
    public string Zip {get;set;}
    [Required]
    public DateTime DateNTime {get;set;}
    [Required]
    public double Distance {get;set;}
    [Required]
    public string BikeType {get;set;}
    [Required]
    public string Exclusive {get;set;}
    [Required]
    public string Description {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public int UserId {get;set;}
    public User? RideCoordinator {get;set;}
    public List<Join> PeopleWhoJoined {get;set;} = new List<Join>();
}