using CSharp.Choices;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Question.Domain.CreateReplayWorkflow
{
    [AsChoice]
    public static partial class ValidationReplayResult
    {
        public interface IReplayValidationResult { }

        public class ReplayUnchecked : IReplayValidationResult
        {
            public CreateReplayCMD Replay { get; private set; }
            public ReplayUnchecked(CreateReplayCMD arg)
            {
                Replay = arg;
            }
        }

        public class InvalidReplay : IReplayValidationResult
        {
            public readonly List<string> Errors;
            public InvalidReplay(List<string> errors)
            {
                Errors = new List<string>(errors);
            }
        }


        public class ValidReplay : IReplayValidationResult
        {
            public Guid ReplayID { get; private set; }
            public ReplayUnchecked Replay { get; private set; }
            public ValidReplay(Guid replayID, ReplayUnchecked replay)
            {
                Replay = replay;
                ReplayID = replayID;
            }
        }

        public class ReplayProfanityCheckedFailed : IReplayValidationResult
        {
            public Guid ProfanityCheckID { get; private set; }
            public ValidReplay Replay { get; private set; }

            public String Msg;
            public ReplayProfanityCheckedFailed(Guid pcID, ValidReplay replay,String msg)
            {
                ProfanityCheckID = pcID;
                Replay = replay;
                Msg = msg;
            }

        }


        public class ReplayProfanityCheckedPassed : IReplayValidationResult
        {
            public Guid ProfanityCheckID{ get; private set; }
            public ValidReplay Replay { get; private set; }
            public ReplayProfanityCheckedPassed(Guid pcID, ValidReplay replay)
            {
                ProfanityCheckID = pcID;
                Replay = replay;
            }
            
         }

        public class ReplayAcknoledgmentNotSent : IReplayValidationResult
        {
            public ReplayProfanityCheckedPassed Replay { get; private set; }
            public ReplayAcknoledgmentNotSent(ReplayProfanityCheckedPassed replay)
            {
                Replay = replay;
            }
        }

            public class ReplayAcknoledgmentSent : IReplayValidationResult
        {
            public ReplayAcknoledgmentNotSent Replay { get; private set; }
            public Guid QuestionAuthEMailID { get; private set; }
            public Guid ReplayAuthEMailID { get; private set; }
            public ReplayAcknoledgmentSent(ReplayAcknoledgmentNotSent replay, Guid questionAuthEMailID,Guid replayAuthEMailID)
            {
                Replay = replay;
                QuestionAuthEMailID = questionAuthEMailID;
                ReplayAuthEMailID = replayAuthEMailID;
                SendEmails();
            }
            private void SendEmails() {}
        }
        public class ReplayMatchError : IReplayValidationResult{ }


    }

}
