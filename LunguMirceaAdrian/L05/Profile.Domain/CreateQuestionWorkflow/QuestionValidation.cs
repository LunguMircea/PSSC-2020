using CSharp.Choices;
using LanguageExt.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class ValidQuestion
    {
        public interface IQuestionValidation { }

        public class UnvalidatedQuestion : IQuestionValidation
        {
            public CreateQuestionCMD Question { get; private set; }
            public UnvalidatedQuestion(CreateQuestionCMD question)
            {
                Question = question;
            }

            public static Result<UnvalidatedQuestion> Create(CreateQuestionCMD question)
            {
                string msg;
                if ((msg=IsQuestionValid(question)).Equals("Valid"))
                {
                    return new UnvalidatedQuestion(question);
                }
                else
                {
                    return new Result<UnvalidatedQuestion>(new InvalidQuestionException(msg));
                }
            }

            private static string IsQuestionValid(CreateQuestionCMD question)
            {
                if (question.QuestionText.Length > 10000)
                    return "Question too long!";
                if (question.Tags.Length == 0 || question.Tags.Length > 3)
                    return "Invalid number of tags";
                return "Valid";
            }

        }
        public class ValidatedQuestion : IQuestionValidation
        {
            public CreateQuestionCMD Question { get; private set; }
            public List<VoteEnum> Votes { get; private set; }
            public ValidatedQuestion(UnvalidatedQuestion question)
            {
                Question = question.Question ;
                Votes = new List<VoteEnum>();
            }
            public void AddVote(VoteEnum vote)
            {
                Votes.Add(vote);
            }
        }

    }

}
