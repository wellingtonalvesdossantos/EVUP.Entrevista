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
    public class ClienteController : Controller
    {
        private IRepository<Cliente> repositorio = DependencyManager.Resolve<IRepository<Cliente>>();

        List<ClienteVM> GetAll()
        {
            return repositorio.Table.Select(c => new ClienteVM()
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

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(ClienteVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Cliente()
                    {
                        Nome = model.Nome,
                        Telefone = model.Telefone,
                        Endereco = model.Endereco,
                        Email = model.Email,
                        Cidade = model.Cidade,
                        Genero = model.Genero
                    };

                    using (var trans = repositorio.BeginTransaction())
                    {
                        repositorio.Insert(entity);

                        if (trans != null) repositorio.Commit();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            var model = repositorio.Table
                .Where(c => c.Id == id)
                .Select(c => new ClienteVM()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    Endereco = c.Endereco,
                    Email = c.Email,
                    Cidade = c.Cidade,
                    Genero = c.Genero
                }).FirstOrDefault();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(ClienteVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Cliente()
                    {
                        Id = model.Id,
                        Nome = model.Nome,
                        Telefone = model.Telefone,
                        Endereco = model.Endereco,
                        Email = model.Email,
                        Cidade = model.Cidade,
                        Genero = model.Genero
                    };

                    using (var trans = repositorio.BeginTransaction())
                    {
                        repositorio.Update(entity);

                        if (trans != null) repositorio.Commit();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var trans = repositorio.BeginTransaction())
                {
                    repositorio.Delete(id);

                    if (trans != null) repositorio.Commit();
                }

                return Json(GetAll());
            }
            catch
            {
                return View();
            }
        }
    }
}
