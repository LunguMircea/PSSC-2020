using QuestionContext;
using System;
using System.Collections.Generic;
using System.Text;
using TextAnalize;

namespace QuestionContext
{

    public struct PublishQuestionCMD
    {
        public CreateQuestionCMD PublishedQuestion;
        static public List<CreateQuestionCMD> QuestionPublishedList = new List<CreateQuestionCMD>();
        public List<string> ErrorMessages { get; private set; }

        public PublishQuestionCMD(CreateQuestionCMD question)
        {
            PublishedQuestion = question;
            ErrorMessages = new List<string>();
            if (QuestionFormatChecker.ValidateQTitle(question.Title) == false)
                ErrorMessages.Add("Invalid Title");
            if (QuestionFormatChecker.ValidateQText(question.QuestionText) == false)
                ErrorMessages.Add("Invalid Question Body");
            if (QuestionFormatChecker.ValidateQtags(question.Tags) == false)
                ErrorMessages.Add("Unknown Tag");
            float ProfanityIndex = 100;
            if ((ProfanityIndex = TextAnalize.MachineCheck.MachineProfanityIndex(new List<string> { question.QuestionText, question.Title })) < 30)
            {  // Passed the machine profanity check
                question.ProfanityCheck = true;

            }
            else if (ProfanityIndex < 60)
            {  // Admin input is needed 
                question.ProfanityCheck = new Random(System.DateTime.UtcNow.Millisecond).Next() % 2 > 0 ? true : false;

            }
            else
            {   // Check failed hard
                question.ProfanityCheck = false;
            }

            if (ErrorMessages.Count == 0 && PublishedQuestion.ProfanityCheck)
                QuestionPublishedList.Add(PublishedQuestion);
        }




    }
    static class QuestionFormatChecker
    {
        internal static bool ValidateQTitle(string str)
        {
            if (str.Length > 3 && str.Length < 50)
                return true;
            return false;
        }

        internal static bool ValidateQtags(string[] tags)
        {
            foreach (string tag in tags)
                if (CreateQuestionCMD.TagList.Contains(tag) == false)
                    return false;
            return true;
        }

        internal static bool ValidateQText(string str)
        {
            if (str.Length > 10 && str.Length < 2000)
                return true;
            return false;
        }
    }
}

