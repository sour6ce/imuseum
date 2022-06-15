using System;
using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos;

public record InternalArtworkDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateOnly CreationDate { get; set; }
    public DateTime? IncorporatedDate { get; set; }
    public string Period { get; set; }
    public decimal Assessment { get; set; }
}