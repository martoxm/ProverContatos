using Microsoft.AspNetCore.Mvc;
using ProverContatos.Application.UseCases.Contatos.Ativar;
using ProverContatos.Application.UseCases.Contatos.Buscar;
using ProverContatos.Application.UseCases.Contatos.Criar;
using ProverContatos.Application.UseCases.Contatos.Desativar;
using ProverContatos.Application.UseCases.Contatos.Editar;
using ProverContatos.Application.UseCases.Contatos.Excluir;
using ProverContatos.Application.UseCases.Contatos.Listar;
using ProverContatos.Communication.Requests;
using ProverContatos.Communication.Responses;

namespace ProverContatos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContatosController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseContatoJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar(
        [FromServices] ICriarContatoUseCase useCase,
        [FromBody] RequestCriarContatoJson request)
    {
        var response = await useCase.ExecutarAsync(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseContatoResumidoJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromServices] IListarContatosUseCase useCase)
    {
        var response = await useCase.ExecutarAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseContatoJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(
        [FromServices] IBuscarContatoUseCase useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.ExecutarAsync(id);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Editar(
        [FromServices] IEditarContatoUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RequestEditarContatoJson request)
    {
        await useCase.ExecutarAsync(id, request);
        return NoContent();
    }

    [HttpPatch("{id}/desativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar(
        [FromServices] IDesativarContatoUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.ExecutarAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/ativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Ativar(
        [FromServices] IAtivarContatoUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.ExecutarAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErroJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(
        [FromServices] IExcluirContatoUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.ExecutarAsync(id);
        return NoContent();
    }
}