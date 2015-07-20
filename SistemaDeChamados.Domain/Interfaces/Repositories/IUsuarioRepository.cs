﻿using System.Linq;
using SistemaDeChamados.Domain.DTO;
using SistemaDeChamados.Domain.Entities;

namespace SistemaDeChamados.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Usuario ObterPorEmail(string email);
        IQueryable<Usuario> ObterReadOnly();
        UsuarioDTO ObterParaEdicao(long id);
        string ObterPasswordPorId(long id);
    }
}
