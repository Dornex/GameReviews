using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameReviews.Models
{
    public class ValidB : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Game game = (Game)validationContext.ObjectInstance;
            bool condition = game.Price > 0;

            return condition ? ValidationResult.Success : new ValidationResult("Pretul trebuie sa fie mai mare decat 0!");
        }
    }
}
