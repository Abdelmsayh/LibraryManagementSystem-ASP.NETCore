using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Service;
using DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LMS.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesService _roleService;

        public RoleController(IRolesService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (!ModelState.IsValid)
                return View(role);

            await _roleService.AddRoleAsync(role);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(String id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRole role)
        {
            if (!ModelState.IsValid)
                return View(role);

            var existingRole = await _roleService.GetByIdAsync(role.Id);
            if (existingRole == null)
                return NotFound();

            existingRole.Name = role.Name;
            existingRole.Description = role.Description;
            existingRole.NormalizedName = role.Name.ToUpper();

            await _roleService.UpdateRoleAsync(existingRole);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(String id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApplicationRole model)
        {
            
            await _roleService.DeleteRoleAsync(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(String id)
        {
            if (id == String.Empty)
                return BadRequest("Invalid category ID.");

            var category = await _roleService.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return View(category);
        }


        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var data = await _roleService.GetUserInRole(RoleId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleDTO> model, string RoleId)
        {

            var result = await _roleService.AssignUserToRole(model, RoleId);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }


    }
}
