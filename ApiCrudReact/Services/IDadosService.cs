using ApiCrudReact.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudReact.Services
{
    public interface IDadosService
    {
        Task<IEnumerable<Dados>> GetDados();
        Task<Dados> GetDadosById(int id);
        Task<IEnumerable<Dados>> GetDadosByNome(string nome);
        Task CreateDados(Dados dados);
        Task UpdateDados(Dados dados);
        Task DeleteDados(Dados dados);


    }
}
