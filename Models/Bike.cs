#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RideOut.Models;

public class Bike
{
    [Key]
    public int BikeId {get;set;}
    [Required]
    [Range(1000,3000)]
    public int Year {get;set;}
    [Required]
    public string Make  {get;set;}
    [Required]
    public string Model {get;set;}
    [Required]
    public int Discplacement {get;set;}
    public string NickName {get;set;}
    [Required]
    public string BikeType {get;set;}
    [Required]
    public string ForSale {get;set;}
    public byte[]? Img {get;set;} = null;
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public int UserId {get;set;}
    public User? BikeOwner {get;set;}
}