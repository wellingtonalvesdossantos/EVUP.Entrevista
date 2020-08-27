using EVUP.Entrevista.Core.Entidades;
using EVUP.Entrevista.FMK;
using EVUP.Entrevista.FMK.Infraestrutura;
using EVUP.Entrevista.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVUP.Entrevista.Web.Controllers
{
    public class RelTarefa8Controller : Controller
    {
        private IRepository<Cliente> repositorio = DependencyManager.Resolve<IRepository<Cliente>>();
        private IRepository<Usuario> repUsuarios = DependencyManager.Resolve<IRepository<Usuario>>();

        List<ClienteVM> GetAll()
        {
            var ids = repUsuarios.Table.Where(x => x.IsAdmin).Select(c => (long?)c.Id).ToArray();
            return repositorio.Table.Where(x => ids.Contains(x.UsuarioId)).Select(c => new ClienteVM()
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone,
                Endereco = c.Endereco,
                Email = c.Email,
                Cidade = c.Cidade,
                Genero = c.Genero
            }).ToList();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View(GetAll());
        }
    }
}
