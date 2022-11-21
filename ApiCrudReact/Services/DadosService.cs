using ApiCrudReact.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudReact.Services
{
    public class DadosService : IDadosService
    {

        private readonly ApiDbContext _context;

        public DadosService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dados>> GetDados()
        {
            try
            {
                return await _context.DadosUsuarios.ToListAsync();
            }
            catch
            {
                throw;
            }
            
        }
        public async Task<IEnumerable<Dados>> GetDadosByNome(string nome)
        {
            IEnumerable<Dados> dados;
            if(!string.IsNullOrWhiteSpace(nome))
            {
                dados = await _context.DadosUsuarios.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                dados = await GetDados();
            }
            return dados;
        }
        public async Task<Dados> GetDadosById(int id)
        {
            var dados = await _context.DadosUsuarios.FindAsync(id);
            return dados;
        }
        public async Task CreateDados(Dados dados)
        {
            _context.DadosUsuarios.Add(dados);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateDados(Dados dados)
        {
            _context.Entry(dados).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDados(Dados dados)
        {
            _context.DadosUsuarios.Remove(dados);
            await _context.SaveChangesAsync();
        }

    }
}
