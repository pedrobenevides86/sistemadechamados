﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SistemaDeChamados.Application.Interface;
using SistemaDeChamados.Application.ViewModels;
using SistemaDeChamados.Web.Controllers;

namespace SistemaDeChamados.Web.Tests
{
    [TestClass]
    public class DadoUmUsuariosController
    {
        private UsuariosController usuariosController;
        private IUsuarioAppService usuarioAppService;

        [TestInitialize]
        public void Setup()
        {
            usuarioAppService = Substitute.For<IUsuarioAppService>();
            usuariosController = new UsuariosController(usuarioAppService);
        }

        [TestMethod]
        public void IndexDeveListarTodosOsUsuariosEPassarParaAView()
        {
            usuariosController.Index();
            usuarioAppService.Received().ObterReadOnly();
        }

        [TestMethod]
        public void NovoComChamadaGetDevePassarUmUsuarioViewModelParaAView()
        {
            var result = (ViewResult)usuariosController.Novo();
            Assert.AreEqual(typeof(UsuarioVM), result.Model.GetType());
        }

        [TestMethod]
        public void AoRealizarUmPostParaAActionNovoDeveChamarOMetodoCreateDoUsuarioAppServiceSeModelIsValid()
        {
            var vm = new UsuarioVM
            {
                Nome = "Fulano",
                Email = "fulano@mail.com",
                Password = "123456",
                ConfirmacaoPassword = "123456"
            };

            usuariosController.Novo(vm);
            usuarioAppService.Received().Create(vm);
        }

        [TestMethod]
        public void AoRealizarUmPostParaAActionNovoDeveRetornarAViewSeModelIsNotValid()
        {
            //var badModel = ObterViewModelInvalido();
            //var result = (ViewResult)usuariosController.Novo(badModel);
            //Assert.AreEqual(typeof(UsuarioVM), result.Model.GetType());
        }


        [TestMethod]
        public void EdicaoComChamadaGetDevePopularOViewModelEId()
        {
            const int idProcurado = 1;
            usuarioAppService.ObterParaEdicao(idProcurado).Returns(new UsuarioVM());
            var result = (ViewResult)usuariosController.Edicao(idProcurado);

            Assert.AreEqual(idProcurado, ((UsuarioVM)result.Model).Id);
        }

        [TestMethod]
        public void AoRealizarUmPostParaAActionEdicaoDeveRetornarAViewSeModelIsNotValid()
        {
            var badModel = ObterViewModelInvalido();
            var result = (ViewResult)usuariosController.Edicao(badModel);
            Assert.AreEqual(typeof(UsuarioVM), result.Model.GetType());
        }

        [TestMethod]
        public void AoRealizarUmPostParaAActionEdicaoDeveChamarOMetodoCreateDoUsuarioAppServiceSeModelIsValid()
        {
            var vm = new UsuarioVM
            {
                Nome = "Fulano",
                Email = "fulano@mail.com"
            };

            usuariosController.Edicao(vm);
            usuarioAppService.Received().Update(vm);
        }

        private UsuarioVM ObterViewModelInvalido()
        {
            var badModel = new UsuarioVM();
            var validationContext = new ValidationContext(badModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(badModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                usuariosController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            return badModel;
        }

    }
}
