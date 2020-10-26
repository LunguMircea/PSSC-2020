using LanguageExt;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static Question.Domain.CreateQuestionWorkflow.ValidQuestion;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var cmd = new CreateQuestionCMD("Tema", "Ce trebuia sa mai fac pentru tema?", "LunguMirceaAdrian", new string[] { "homework" });
            var questionResult = UnvalidatedQuestion.Create(cmd);
            var result = questionResult.Match(
              Succ: question =>
              {
                  ValidatedQuestion vq;
                  Console.WriteLine("---------------Question is Valid-----------------\n");
                  Console.WriteLine( GetStringPresentQuestion(vq=new ValidatedQuestion(question)));
                  return vq;
              },
              Fail: ex =>
              {
                  Console.WriteLine($"Question is Invalid. Reason: {ex.Message}");
                  return null;
              }
              );
            Console.WriteLine("-------------------------------------------------\n");
            //Acum cu numar invalid de tags
            cmd = new CreateQuestionCMD("Tema", "Ce trebuia sa mai fac pentru tema?", "LunguMirceaAdrian", new string[] { "homework", "homework" , "homework" , "homework" });
            questionResult = UnvalidatedQuestion.Create(cmd);
            questionResult.Match(
            Succ: question =>
            { 
                  ValidatedQuestion vq;
                  Console.WriteLine("---------------Question is Valid-----------------\n");
                  Console.WriteLine( GetStringPresentQuestion(vq=new ValidatedQuestion(question)));
                  return vq;

             },
             Fail: ex =>
             {
                Console.WriteLine($"Question is Invalid. Reason: {ex.Message}");
                return null;
              }
         );
            

            Console.WriteLine("\n\n\n\n----------Si acum votam---------\n\n\n");
            var rand = new Random(DateTime.Now.TimeOfDay.Ticks.ToString().GetHashCode());
            for (int i = 0; i <rand.Next() % 30; i++)
                result.AddVote(rand.Next()%3 != 0? VoteEnum.Up : VoteEnum.Down);
            Console.WriteLine(GetStringPresentQuestion(result));

            Console.Read();

        }

        static public string GetStringPresentQuestion(ValidatedQuestion vq)
        {

            return "Titlul:  " + vq.Question.Title + "\n\n" +
                        vq.Question.QuestionText + "\n" +
                        "Tags:  " + formatTags(vq.Question.Tags) + "\n" +
                        "Scor: " + CountVotes(vq.Votes) + "  Din " + vq.Votes.Length() +" voturi\n"+
                        "\n--------------------------------------------\n";
        }

        static private string formatErrorMsg(List<string> errorMsgs)
        {
            string errorString = "";
            foreach (string errorMsg in errorMsgs)
                errorString += errorMsg + "\n";
            return errorString;
        }

        static private string formatTags(string[] tags)
        {
            string tagString = "";
            foreach (string tag in tags)
                tagString += tag + "  ";
            return tagString;
        }
        static private int CountVotes(List<VoteEnum>votes)
        {
            int score=0;
            foreach (VoteEnum vote in votes)
            {
                score += (int)vote;
            }
            return score;
        }
    }
}
