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

        List<ClienteVM> GetAll()
        {
            return repositorio.Table.Where(x => x.Genero == "FEMININO" && x.Cidade == "JUNDIAÍ").Select(c => new ClienteVM()
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
