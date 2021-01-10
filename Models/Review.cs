using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Content { get; set; }

        public int GameRefReviewId { get; set; }
        public Game Game { get; set; }

        public int CommentRefReviewId { get; set; }
        public ICollection<Comment> Comments;

        public string ReviewRefUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
