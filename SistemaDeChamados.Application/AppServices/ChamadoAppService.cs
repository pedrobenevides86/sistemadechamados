﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SistemaDeChamados.Application.Interface;
using SistemaDeChamados.Application.Interface.Services;
using SistemaDeChamados.Application.ViewModels;
using SistemaDeChamados.Domain.Entities;
using SistemaDeChamados.Domain.Interfaces.Services;

namespace SistemaDeChamados.Application.AppServices
{
    public class ChamadoAppService : AppService, IChamadoAppService
    {
        private readonly IChamadoService chamadoService;

        public ChamadoAppService(IChamadoService chamadoService, IServiceLocator serviceLocator)
            : base(serviceLocator)
        {
            this.chamadoService = chamadoService;
        }

        public void Create(CriacaoChamadoVM chamadoVM)
        {
            var chamado = Mapper.Map<Chamado>(chamadoVM);
            BeginTransaction();
            chamadoService.Create(chamado);
            Commit();
        }

        public IEnumerable<ChamadoIndexVM> Retrieve()
        {
            var listaDeChamados = chamadoService.Obter();
            return Mapper.Map<IList<ChamadoIndexVM>>(listaDeChamados.ToList());
        }

        public void Update(ChamadoVM chamadoVM)
        {
            var chamado = Mapper.Map<Chamado>(chamadoVM);
            BeginTransaction();
            chamadoService.Update(chamado);
            Commit();
        }

        public void Delete(long id)
        {
            BeginTransaction();
            chamadoService.Delete(id);
            Commit();
        }

        public ChamadoVM GetById(long id)
        {
            var chamado = chamadoService.GetById(id);
            return Mapper.Map<ChamadoVM>(chamado);
        }
    }
}
