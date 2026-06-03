using ClinicaEscolaBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClinicaEscolaBase.Dtos;
using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class AnexoController(
    IAnexoService anexoService,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(int documentoId, IFormFile arquivo, CancellationToken cancellationToken)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null)
        {
            return Unauthorized();
        }

        var result = await anexoService.UploadAsync(
            new AnexoUploadDto(documentoId, arquivo, usuarioId),
            cancellationToken);

        return ToActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> Download(int id, CancellationToken cancellationToken)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null)
        {
            return Unauthorized();
        }

        var result = await anexoService.DownloadAsync(
            new AnexoDownloadDto(id, usuarioId),
            cancellationToken);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            return ToActionResult(result);
        }

        var download = result.Data!;
        return File(download.Content, download.ContentType, download.FileName);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null)
        {
            return Unauthorized();
        }

        var result = await anexoService.DeleteAsync(
            new AnexoDeleteDto(id, usuarioId),
            cancellationToken);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            return ToActionResult(result);
        }

        return Ok(new { success = true, message = result.Message });
    }

    private IActionResult ToActionResult<T>(AnexoOperationResultDto<T> result)
        => result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(new { success = true, message = result.Message, data = result.Data }),
            HttpStatusCode.BadRequest => BadRequest(result.Message),
            HttpStatusCode.NotFound => NotFound(result.Message),
            HttpStatusCode.Forbidden => Forbid(),
            _ => StatusCode((int)result.StatusCode, result.Message)
        };

    private IActionResult ToActionResult(AnexoOperationResultDto result)
        => result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(new { success = true, message = result.Message }),
            HttpStatusCode.BadRequest => BadRequest(result.Message),
            HttpStatusCode.NotFound => NotFound(result.Message),
            HttpStatusCode.Forbidden => Forbid(),
            _ => StatusCode((int)result.StatusCode, result.Message)
        };
}