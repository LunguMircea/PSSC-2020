using System;
using System.Collections.Generic;

namespace QuestionContext
{
    public struct PresentQuestion
    {
        private string result;

        public string ShowResult { get => result;  }

        public PresentQuestion(PublishQuestionCMD question)
        {
            result = "";
            result = "Titlul:  " + question.PublishedQuestion.Title + "\n\n" +
                        question.PublishedQuestion.QuestionText + "\n" +
                        "Tags:  " + formatTags(question.PublishedQuestion.Tags) +"\n"+
                        formatErrorMsg(question.ErrorMessages) +
                        "\n--------------------------------------------\n";
        }

        private string formatErrorMsg(List<string> errorMsgs)
        {
            string errorString = "";
            foreach (string errorMsg in errorMsgs)
                errorString += errorMsg + "\n";
            return errorString;
        }

        private string formatTags(string[] tags)
        {
            string tagString="";
            foreach (string tag in tags)
                tagString += tag + "  ";
            return tagString;
        }
      

    }

}

