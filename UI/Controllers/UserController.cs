using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Service;
using DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUsersService _userService;
        private readonly IMembersService _membersService;

        public UserController(IUsersService userService, IMembersService membersService)
        {
            _userService = userService;
            _membersService = membersService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(String id)
        {
            if (id == String.Empty)
                return BadRequest();

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var members = await _membersService.GetAllMembersAsync();
            ViewBag.MemberList = new SelectList(members, "Id", "FullName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.AddAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(String id)
        {
            if (id == String.Empty)
                return BadRequest();

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(String id)
        {
            if (id == String.Empty)
                return BadRequest();

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApplicationUser model)
        {
          
            await _userService.DeleteAsync(model);

            return RedirectToAction(nameof(Index));
        }

     
        [HttpPost]
        public async Task<IActionResult> Activate(String id)
        {
            await _userService.ActivateUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Deactivate(String id)
        {
            await _userService.DeactivateUserAsync(id);
            return RedirectToAction(nameof(Index));
        }

    
        [HttpGet]
        public async Task<IActionResult> ActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> InactiveUsers()
        {
            var users = await _userService.GetInactiveUsersAsync();
            return View(users);
        }
    }
}
