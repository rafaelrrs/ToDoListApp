using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.App.Infra;
using ToDoList.App.Models;

namespace ToDoList.App.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoRestClient restClient;

        public ToDoController()
        {
            this.restClient = new ToDoRestClient();
        }

        // GET: ToDoController
        public ActionResult Index()
        {
            var model = this.restClient.GetAll();

            return View(model);
        }

        // GET: ToDoController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = this.restClient.GetById(id);

            return View(model);
        }

        // GET: ToDoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoModel model)
        {
            try
            {
                this.restClient.Save(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = this.restClient.GetById(id);

            return View(model);
        }

        // POST: ToDoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ToDoModel model)
        {
            try
            {
                this.restClient.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = this.restClient.GetById(id);

            return View(model);
        }

        // POST: ToDoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, ToDoModel model)
        {
            try
            {
                this.restClient.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
