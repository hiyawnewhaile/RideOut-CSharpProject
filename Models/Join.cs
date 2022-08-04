#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RideOut.Models;

public class Join
{
    [Key]
    public int JoinId {get;set;}
    public int UserId {get;set;}
    public User? UserWhoJoined {get;set;}
    public int RideId {get;set;}
    public Ride? RideJoined {get;set;}
}