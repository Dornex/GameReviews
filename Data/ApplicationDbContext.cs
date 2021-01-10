using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GameReviews.Models;

namespace GameReviews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GameReviews.Models.Game> Game { get; set; }
        public DbSet<GameReviews.Models.Image> Image { get; set; }
        public DbSet<GameReviews.Models.Review> Review { get; set; }
    }
}
