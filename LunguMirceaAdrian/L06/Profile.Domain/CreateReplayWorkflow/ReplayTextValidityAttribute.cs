using System;
using System.ComponentModel.DataAnnotations;

namespace Question.Domain.CreateReplayWorkflow
{
    public class ReplayTextValidityAttribute : ValidationAttribute
    {

        public ReplayTextValidityAttribute(string text)
        {
            Text = text;
        }
        public string Text { get; set; }
        public ReplayTextValidityAttribute() { }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Replay text is null");
            }
            if (Text.Length >= 10 && Text.Length <= 500)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid replay text lenghth");
            }
        }
    }
}