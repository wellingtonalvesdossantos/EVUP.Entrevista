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
    public class UsuarioController : Controller
    {
        private IRepository<Usuario> repositorio = DependencyManager.Resolve<IRepository<Usuario>>();

        List<UsuarioVM> GetAll()
        {
            return repositorio.Table.Select(c => new UsuarioVM()
            {
                Id = c.Id,
                Nome = c.Nome,
                Login = c.Login,
                IsAdmin = c.IsAdmin,
            }).ToList();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View(GetAll());
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(UsuarioVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Usuario()
                    {
                        Nome = model.Nome,
                        Login = model.Login,
                        IsAdmin = model.IsAdmin,
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var model = repositorio.Table
                .Where(c => c.Id == id)
                .Select(c => new UsuarioVM()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Login = c.Login,
                    IsAdmin = c.IsAdmin
                }).FirstOrDefault();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(UsuarioVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = new Usuario()
                    {
                        Id = model.Id,
                        Nome = model.Nome,
                        Login = model.Login,
                        IsAdmin = model.IsAdmin
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

        // POST: Usuario/Delete/5
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
