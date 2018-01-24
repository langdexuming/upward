using Microsoft.AspNetCore.Mvc;
using Reggie.Blog.Controllers.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reggie.Blog.Controllers
{
    public class ManageController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult EssayCategories()
        {
            return RedirectToAction("Index", "EssayCategories");
        }

        public IActionResult InformalEssays()
        {
            return RedirectToAction("Index", "InformalEssays");
        }

        public IActionResult ContentFlags()
        {
            return RedirectToAction("Index", "ContentFlags");
        }

        public IActionResult SwitchFlags()
        {
            return RedirectToAction("Index", "SwitchFlags");
        }

        public IActionResult Skills()
        {
            return RedirectToAction("Index", "Skills");
        }

        public IActionResult JobExperiences()
        {
            return RedirectToAction("Index", "JobExperiences");
        }

        public IActionResult Samples()
        {
            return RedirectToAction("Index", "Samples");
        }
    }
}
