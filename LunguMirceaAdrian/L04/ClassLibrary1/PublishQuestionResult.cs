using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Choices;

namespace QuestionDomain
{
    [AsChoice]
    public static partial class PublishQuestionResult
    {
        public interface IPublishQuestionResult { }

        public class PublishQuestion : IPublishQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string User { get; private set; }

            public PublishQuestion(Guid questionId, string user)
            {
                QuestionId = questionId;
                User = user;
            }
        }

        public class QuestionProfane : IPublishQuestionResult
        {
            public string Reason { get; set; }

            public QuestionProfane(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : IPublishQuestionResult
        {
            public List<string> ValidationErrors { get; private set; }

            public QuestionValidationFailed(List<string> errors)
            {
                ValidationErrors = errors;
            }
        }
    }
}
