using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class Likes
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
