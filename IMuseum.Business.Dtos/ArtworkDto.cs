using System;
using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos;

public record ArtworkDto
{
    public Guid ArtworkId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime AddDate { get; set; }
    public string Period { get; set; }
    public decimal Assessment { get; set; }
}