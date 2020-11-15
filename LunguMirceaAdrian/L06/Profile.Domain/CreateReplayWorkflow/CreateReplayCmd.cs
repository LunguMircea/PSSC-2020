using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Question.Domain.CreateReplayWorkflow;

namespace Question.Domain.CreateReplayWorkflow
{
    public struct CreateReplayCMD 
    {
        [Required]
        public int QuestionID { get; private set; }
        
        [ReplayTextValidity]
        [StringLength(maximumLength: 500, MinimumLength =10,ErrorMessage ="Invalid replay text length")]
        public string ReplayText { get; set; }

        [Required]
        public string Autor { get; private set; }

        [Required]
        [EmailAddress]
        public string EmailAddressQuestionAutor { get; private set; }

        [Required]
        [EmailAddress]
        public string EmailAddressReplayAutor { get; private set; }


        public CreateReplayCMD(int questionID, string text, string autor,string emailQAuthor,string emailRAuthor) 
        {
            QuestionID = questionID;
            ReplayText = text;
            Autor = autor;
            EmailAddressQuestionAutor = emailQAuthor;
            EmailAddressReplayAutor = emailRAuthor;
        }
    }
}