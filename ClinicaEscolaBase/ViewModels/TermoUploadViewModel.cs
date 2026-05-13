using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ClinicaEscolaBase.ViewModels;

public class TermoUploadViewModel
{
    [Required]
    public Guid PacienteId { get; set; }

    public string? PacienteNome { get; set; }

    [Required]
    public int ProntuarioId { get; set; }

    [Display(Name = "Arquivo PDF")]
    [Required(ErrorMessage = "Selecione um arquivo PDF para upload.")]
    public IFormFile? Arquivo { get; set; }
}
