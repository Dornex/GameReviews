using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("ReviewRefUserId")]
        public ICollection<Review> Reviews { get; set; }

        [ForeignKey("CommentRefUserId")]
        public ICollection<Comment> Comments { get; set; }
    }
}
