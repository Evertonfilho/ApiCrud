using ApiCrudReact.Model;
using ApiCrudReact.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosController : ControllerBase
    {
        private IDadosService _dadosService;

        public DadosController(IDadosService dadosService)
        {
            _dadosService = dadosService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Dados>>> GetDados()
        {
            try
            {
                var dados = await _dadosService.GetDados();
                return Ok(dados);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }

        }


        [HttpGet("UsuarioPorNome")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Dados>>> GetDadosByNome([FromQuery] string nome)
        {
            try
            {
                var dados = await _dadosService.GetDadosByNome(nome);
                if (dados == null)
                    return NotFound($"Não existe usuários com esse {nome}");
                return Ok(dados);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }

        }
        [HttpGet("{id:int}", Name = "GetDadosById")]
        public async Task<ActionResult<Dados>> GetDadosById(int id)
        {
            try
            {
                var dados = await _dadosService.GetDadosById(id);

                if(dados == null)
                    return NotFound($"Não existe usuário com id= {id}");

                return Ok(dados);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Dados dados)
        {
            try
            {
                await _dadosService.CreateDados(dados);
                return CreatedAtRoute(nameof(GetDadosById), new { id = dados.Id }, dados);
            }
            catch
            {
                return BadRequest("Request Inválido" );
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id,[FromBody] Dados dados)
        {
            try
            {
                if (dados.Id == id)
                {
                    await _dadosService.UpdateDados(dados);
                    return Ok($"Aluno com id= {id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var dados = await _dadosService.GetDadosById(id);
                if (dados != null)
                {
                    await _dadosService.DeleteDados(dados);
                    return Ok($"Aluno com id= {id} foi excluido com sucesso");
                }
                else
                {
                    return BadRequest($"Usuário de id={id} não encontrado");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }


    }
}
