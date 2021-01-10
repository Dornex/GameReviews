using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public string ReviewRefUserId { get; set; }
        public ApplicationUser User { get; set; }

        public int CommentRefReviewId { get; set; }
        public Review Review { get; set; }

    }
}
