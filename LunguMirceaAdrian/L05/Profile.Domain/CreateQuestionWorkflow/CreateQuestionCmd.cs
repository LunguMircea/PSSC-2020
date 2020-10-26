using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    public struct CreateQuestionCMD
    {
        [Required]
        public string Title { get; private set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string Autor { get; private set; }

        public string[] Tags { get; private set; }
        public bool ProfanityCheck { get; set; }

        public static List<string> TagList = new List<string> { "C++", "Java", "Algo", "Debug", "Micro", "criptography", "interview", "games", "homework", "news", "git" };


        public CreateQuestionCMD(string title, string text, string autor, string[] tags)
        {
            Title = title;
            QuestionText = text;
            Autor = autor;
            Tags = tags;
            ProfanityCheck = false;
           // Votes = new List<VoteEnum>();

        }
        public CreateQuestionCMD(CreateQuestionCMD question)
        {
            Title = question.Title;
            QuestionText = question.QuestionText;
            Autor = question.Autor;
            Tags = question.Tags;
            ProfanityCheck = question.ProfanityCheck;
           // Votes = new List<VoteEnum>();
        }
    }
}