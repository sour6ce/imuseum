using System;
using System.ComponentModel.DataAnnotations;

namespace IMuseum.Business.Dtos;

public record CreateInternalArtworkDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public DateTime AddDate { get; set; }
    [Required]
    public string Period { get; set; }
    [Required]
    [Range(0, 500000000)]
    public decimal Assessment { get; set; }
}