using BLL.Interfaces.IService;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LMS.PL.Controllers
{

     [Authorize(Roles = "Admin , User")]
    public class MemberController : Controller
    {
        private readonly IMembersService _membersService;

        public MemberController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _membersService.GetAllMembersAsync();
            return View(members);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var member = await _membersService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _membersService.AddMemberAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var member = await _membersService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _membersService.UpdateMemberAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var member = await _membersService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(MemberDTO model)
        {
            await _membersService.DeleteMemberAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CanBorrow(Guid id)
        {
            var member = await _membersService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            bool canBorrow = await _membersService.CanBorrowAsync(id);
            ViewBag.CanBorrow = canBorrow;

            return View(member);
        }

        public async Task<IActionResult> BorrowedBooks(Guid id)
        {
            var books = await _membersService.GetBorrowedBooksAsync(id);
            var member = await _membersService.GetMemberByIdAsync(id);

            if (member == null)
                return NotFound();

            ViewBag.Member = member;
            return View(books);
        }

        public async Task<IActionResult> Fines(Guid id)
        {
            var totalFine = await _membersService.GetOutstandingFinesAsync(id);
            var member = await _membersService.GetMemberByIdAsync(id);

            if (member == null)
                return NotFound();

            ViewBag.Member = member;
            return View(totalFine);
        }

        public async Task<IActionResult> Profile(Guid id)
        {
            var member = await _membersService.GetMemberProfileWithHistoryAsync(id);
            if (member == null)
                return NotFound();

            return View(member);
        }
    }
}
