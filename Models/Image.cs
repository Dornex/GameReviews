using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string ImageTitle { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public string ImageString { get; set; }

        public int GameRefImgId { get; set; }
        public Game Game { get; set; }
    }
}
