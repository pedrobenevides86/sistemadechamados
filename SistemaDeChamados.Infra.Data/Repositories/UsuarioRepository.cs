﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeChamados.Domain.DTO;
using SistemaDeChamados.Domain.Entities;
using SistemaDeChamados.Domain.Interfaces.Repositories;

namespace SistemaDeChamados.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario ObterAtivoPorEmail(string email)
        {
            return context.Usuarios.FirstOrDefault(u => u.Email == email && u.EstaAtivo);
        }

        public IQueryable<Usuario> ObterReadOnly()
        {
            return context.Usuarios.AsNoTracking();
        }

        public async Task<IList<Usuario>> ObterAtivosAsync()
        {
            return await context.Usuarios.Where(u => u.EstaAtivo).ToListAsync();
        }

        public UsuarioDTO ObterParaEdicao(long id)
        {
            return context.Usuarios.Where(u => u.Id == id).Select(u => new UsuarioDTO{Email = u.Email, Nome = u.Nome, SetorId = u.SetorId}).FirstOrDefault();
        }
    }
}
