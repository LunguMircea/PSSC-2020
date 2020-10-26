using System;

namespace Question.Domain.CreateQuestionWorkflow
{
    [Serializable]
    public class InvalidQuestionException : Exception
    {
        public InvalidQuestionException() { }

        public InvalidQuestionException(string v) : base("The question " +v)
        {
            
        }
        

    }
}
