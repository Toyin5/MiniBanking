using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiniCoreBanking.Domain;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public StatusTypes Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

}