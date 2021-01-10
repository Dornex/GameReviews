using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameReviews.Data;
using GameReviews.Models;
using System.IO;

namespace GameReviews.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var games = await _context.Game.Include(game => game.Image).Include(game => game.Reviews).ToListAsync();
            foreach (var game in games)
            {
                Image img = game.Image;
                string imageBase64Data = Convert.ToBase64String(img.ImageData);
                string imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                img.ImageString = imageDataUrl;
            }
            return View(games);
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(game => game.Image)
                .Include(game => game.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);

            foreach (var review in game.Reviews)
            {
                review.Comments = _context.Comment.Where(comment => comment.CommentRefReviewId == review.Id).ToList();
                review.User = _context.Users.FirstOrDefault(user => user.Id == review.ReviewRefUserId);

                foreach (var comment in review.Comments)
                {
                    comment.User = _context.Users.FirstOrDefault(user => user.Id == comment.ReviewRefUserId);
                }
            }

            if (game == null)
            {
                return NotFound();
            }

            Image img = game.Image;
            string imageBase64Data = Convert.ToBase64String(img.ImageData);
            string imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            img.ImageString = imageDataUrl;

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Genre,Price")] Game game)
        {
            var file = Request.Form.Files[0];
            Image img = new Image();
            img.ImageTitle = game.Name;

            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            img.ImageData = ms.ToArray();

            ms.Close();
            ms.Dispose();

            game.Image = img;

            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Genre,Price")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
