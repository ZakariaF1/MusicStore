using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStoreWebApp.DataAnnotationsValidations.Attributes
{
    public class MaxWordAttributes : ValidationAttribute
    {
        private readonly int _maxWords;
        public MaxWordAttributes(int maxWords)
        {
            _maxWords = maxWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var textValue = value.ToString();
            return textValue.Split(' ').Length > _maxWords
                ? new ValidationResult("Too long Address!")
                : ValidationResult.Success;
        }
    }
}
