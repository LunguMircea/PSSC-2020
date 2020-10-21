using QuestionContext;
using System;
using System.Collections.Generic;
using static QuestionDomain.PublishQuestionResult;
using QuestionDomain;

namespace UserInterfaceContext.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var cmd = new CreateQuestionCMD("Tema", "Ce trebuia sa mai fac pentru tema?", "LunguMirceaAdrian", new string[] { "homework" });
            var result = new PublishQuestionCMD(cmd);
            Console.WriteLine(new PresentQuestion(result).ShowResult);

            //Implementare2
            Console.WriteLine("Metoda de la clasa-------------\n");
            var result2 = PublishQuestionMethod2(result);
            result2.Match(
                 ProcessPublishQuestion,
                 ProcessQuestionProfane,
                 ProcessQuestionInvalid
             );
            // Si acum cu date invalide (text prea scurt)
            Console.WriteLine("\n-------Acum cu date invalide----------------\n");
            var cmd2 = new CreateQuestionCMD("OMG", "tot scurt", "Gogu", new string[] { "un tag invalid" });
            var result3 = new PublishQuestionCMD(cmd2);
            Console.WriteLine(new PresentQuestion(result3).ShowResult);
            // Acum again cu interfata si match
            Console.WriteLine("Metoda de la clasa-------------\n");
            var result4 = PublishQuestionMethod2(result3);
            result4.Match(
                 ProcessPublishQuestion,
                 ProcessQuestionProfane,
                 ProcessQuestionInvalid
             );
            Console.ReadLine();

        }

        private static IPublishQuestionResult ProcessQuestionInvalid(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        private static IPublishQuestionResult ProcessQuestionProfane(QuestionProfane questionNotCreatedResult)
        {
            Console.WriteLine($"Question not published: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }

        private static IPublishQuestionResult ProcessPublishQuestion(PublishQuestion publishedQuestion)
        {
            Console.WriteLine("Publicata intrabarea: "+publishedQuestion.QuestionId+"   Creata de "+ publishedQuestion.User);
            return publishedQuestion ;
        }

        public static IPublishQuestionResult PublishQuestionMethod2(PublishQuestionCMD publishQuestionCommand)
        {
            if (publishQuestionCommand.ErrorMessages.Count>0)
            {
                return new QuestionValidationFailed(publishQuestionCommand.ErrorMessages);
            }

            if (TextAnalize.MachineCheck.MachineProfanityIndex(new List<string>{ publishQuestionCommand.PublishedQuestion.QuestionText,publishQuestionCommand.PublishedQuestion.Title}) > 80)
            {
                return new QuestionProfane("Failed profanity check");
            }

            var questionId = Guid.NewGuid();
            var result = new PublishQuestion(questionId,  publishQuestionCommand.PublishedQuestion.Autor);

            //execute logic
            return result;
        }
    
    }
}
