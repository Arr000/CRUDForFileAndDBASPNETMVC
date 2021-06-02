using DatingDataCommon;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataDB;
using HobbyProjectDating.Models;

namespace HobbyProjectDating.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel userview)
        {
            var user = new User()
            {
                Email = userview.email,
                Id = userview.Id,
                FirstName = userview.FirstName,
                LastName = userview.LastName,
                Country = userview.Country,
                Hobby = userview.Hobby
            };

            _userService.Add(user);

            return RedirectToAction("Display", "User");
        }

        public IActionResult Display()
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            var listOfUserDB = _userService.List();

            foreach (var u in listOfUserDB)
            {
                var userViewModel = new UserViewModel()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Hobby = u.Hobby,
                    Country = u.Country
                };
                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UserViewModel userviewmodel)
        {
            var user = new User
            {
                FirstName = userviewmodel.FirstName,
                LastName = userviewmodel.LastName,
                Country = userviewmodel.Country,
                Hobby = userviewmodel.Hobby,
                Email = userviewmodel.email
            };

            _userService.Update(user);

            return View();
        }

       public IActionResult Delete()
        {
            var users = _userService.List();
            List<UserViewModel> userviewmodels = new List<UserViewModel>();

            foreach(var user in users)
            {
                UserViewModel userviewmodel = new UserViewModel
                {
                    email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Country = user.Country,
                    Hobby = user.Hobby
                };
                userviewmodels.Add(userviewmodel);
            }
            
            return View(userviewmodels);
        }

        [HttpPost]
        public IActionResult Delete(string id)
         {
            _userService.Delete(id);
            return RedirectToAction("Display", "User");
        }
    }
}
