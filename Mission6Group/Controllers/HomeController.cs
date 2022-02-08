using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Mission6Group.Models;

namespace Mission6Group.Controllers
{
         public class HomeController : Controller
    {
        private TaskContext task { get; set; }

        public HomeController(TaskContext _task)
        {
            task = _task;
        }

        public IActionResult Index()
        {
            // get data from taskresponse model
            // get data which has completed as false
            // include category data
            // have a list format
            var taskList = task.Tasks
                .Where(x => x.Completed == false)
                .Include(x => x.Category)
                .ToList();
            return View(taskList);
        }

        [HttpGet]
        public IActionResult Task()
        {
            // get data from category model and put it in ViewBag
            ViewBag.Categories = task.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Task(TaskResponse ar)
        {
            // validation
            if (ModelState.IsValid)
            {
                task.Add(ar);
                task.SaveChanges();
                // after save a new record, call Index function
                return RedirectToAction("Index");
            }
            // validation fail -> return the form view again
            ViewBag.Categories = task.Categories.ToList();
            return View();
        }

        // Edit a record (GET)
        [HttpGet]
        public IActionResult Edit(int recordId)
        {
            // set this variable as false to indicate I am on Editing
            ViewBag.New = false;
            // get data from category model to display them on a cshtml
            ViewBag.Categories = task.Categories.ToList();

            // find a specific movie by its id
            var application = task.Tasks.Single(x => x.TaskId == recordId);
            return View("Task", application);
        }

        // Edit a record (POST)
        [HttpPost]
        public IActionResult Edit(TaskResponse ar)
        {
            // model validation
            if (ModelState.IsValid)
            {
                // edit a specific model
                task.Update(ar);
                task.SaveChanges();

                // show them in the index page
                return RedirectToAction("Index");
            }
            ViewBag.New = false;
            // if the model is not validated, get data from category model and show form cshtml
            ViewBag.Categories = task.Tasks.ToList();
            return View("Task", ar);
        }

        // Delete
        public IActionResult Delete(int recordId)
        {
            // find a record from DB by its id
            var record = task.Tasks.Single(x => x.TaskId == recordId);
            task.Responses.Remove(record);
            task.SaveChanges();

            var taskList = task.Tasks
                .Include(x => x.Category)
                .ToList();

            return RedirectToAction("Index", taskList);
        }

        // mark as complete
        public IActionResult MarkComplete(int recordId)
        {
            // get a single data by its recordId
            var record = task.Tasks.Single(x => x.TaskId == recordId);

            // change its completed as true
            record.Completed = true;
            task.SaveChanges();
            // call index function
            return RedirectToAction("Index");
        }

    }
}
