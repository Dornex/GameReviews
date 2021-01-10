using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameReviews.Data;
using GameReviews.Models;
using Microsoft.AspNetCore.Identity;

namespace GameReviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ReviewsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Review.Include(r => r.Game).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Game)
                .Include(r => r.User)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost]
        [Route("{controller}/{action}/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            review.Game = _context.Game.FirstOrDefault(g => g.Id == review.GameRefReviewId);
            review.Id = 0;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                review.ReviewRefUserId = user.Id;
                _context.Add(review);
                await _context.SaveChangesAsync();

                var game = _context.Game.FirstOrDefault(game => game.Id == review.GameRefReviewId);
                return RedirectToAction("Details", "Games", new { id = game.Id });
            }
            return View(review);
        }

        // GET: Reviews/Create
        [Route("{controller}/{action}/{id}")]
        public IActionResult Create(int id)
        {
            Console.WriteLine("Game id in GET:" + id);
            ViewData["GameId"] = id;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["GameRefReviewId"] = new SelectList(_context.Game, "Id", "Genre", review.GameRefReviewId);
            ViewData["ReviewRefUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", review.ReviewRefUserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Content,GameRefReviewId,CommentRefReviewId,ReviewRefUserId")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameRefReviewId"] = new SelectList(_context.Game, "Id", "Genre", review.GameRefReviewId);
            ViewData["ReviewRefUserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Id", review.ReviewRefUserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Game)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
