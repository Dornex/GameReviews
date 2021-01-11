using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [ValidB]
        public decimal Price { get; set; }

        [ForeignKey("GameRefImgId")]
        public Image Image { get; set; }

        [ForeignKey("GameRefReviewId")]
        public ICollection<Review> Reviews { get; set; }

    }
}
