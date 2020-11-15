using Question.Domain.CreateReplayWorkflow;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Question.Domain.CreateReplayWorkflow.ValidationReplayResult;

namespace Test.App
{
    class Program
    {
        private static bool finished;
        static void Main(string[] args)
        {
            finished = false;
            CreateReplayCMD unvalidatedReplay = new CreateReplayCMD(1, "1+1=2", "LunguMirceaAdrian","exemplu@gmail.com", "exemplu@gmail.com");
            var validateReplayCMDResult = ProcessReplay(unvalidatedReplay);
            do   // Its cute this works
            {
                validateReplayCMDResult.Match(
                    ProcessReplayUnckecked,
                    ProcessReplayInvalid,
                    ProcessReplayValid,
                    ProcessReplayProfanityCheckFailed,
                    ProcessReplayProfanityCheckPassed,
                    ProcessReplayAcknoledgementNotSent,
                    ProcessReplayAcknoledgmentSent,
                    ProcessReplayError
                    );
                Console.WriteLine("\n-----------------------------------\n");
                validateReplayCMDResult = ProcessReplay(validateReplayCMDResult);
            } while ( !finished );
            Console.ReadLine();
        }

        private static IReplayValidationResult ProcessReplayError(ReplayMatchError arg)
        {
            Console.WriteLine("Something went wrong");
            return null;
        }

        private static IReplayValidationResult ProcessReplayUnckecked(ReplayUnchecked arg)
        {
            Console.WriteLine("This replay is unchecked and has no ID");
            return arg;
        }

        private static IReplayValidationResult ProcessReplayAcknoledgmentSent(ReplayAcknoledgmentSent arg)
        {
            Console.WriteLine("Replay with ID: " + arg.Replay.Replay.Replay.ReplayID + "\nHas sent the acknoledgement");
            Console.WriteLine("Has sent mail to the replay author, mail ID is: " + arg.ReplayAuthEMailID);
            Console.WriteLine("Has sent mail to the question author, mail ID is: " + arg.QuestionAuthEMailID);
            Console.WriteLine("-----Works done !!!-----");
            return arg;
        }

        private static IReplayValidationResult ProcessReplayAcknoledgementNotSent(ReplayAcknoledgmentNotSent arg)
        {
            Console.WriteLine("Replay with ID: " + arg.Replay.Replay.ReplayID + "\nHas yet to sent the acknoledgement");
            return arg;
        }

        private static IReplayValidationResult ProcessReplayProfanityCheckPassed(ReplayProfanityCheckedPassed arg)
        {
            Console.WriteLine("Replay with ID: " + arg.Replay.ReplayID + "\nPassed profanity check " + arg.ProfanityCheckID);
            return arg;
        }

        private static IReplayValidationResult ProcessReplayProfanityCheckFailed(ReplayProfanityCheckedFailed arg)
        {
            Console.WriteLine("Replay with ID: " + arg.Replay.ReplayID + "\nFailed profanity check " + arg.ProfanityCheckID);
            return arg;
        }

        private static IReplayValidationResult ProcessReplayValid(ValidReplay arg)
        {
            Console.WriteLine("Replay with ID: " + arg.ReplayID + "\nHas been validated");
            return arg;
        }

        private static IReplayValidationResult ProcessReplayInvalid(InvalidReplay arg)
        {
            Console.WriteLine("Replay is invalid:");
            foreach (var error in arg.Errors)
                Console.WriteLine(error);
            return arg;
        }

        public static IReplayValidationResult ProcessReplay(object obj)
        {
            switch (obj.GetType().Name)
            {
                
                case "CreateReplayCMD":
                    return new ReplayUnchecked((CreateReplayCMD)obj);
                    break;

                case "ReplayUnchecked":                     // We do validity check
                    var validationErrors = new List<string>();
                    
                    ReplayUnchecked aux = (ReplayUnchecked)obj;
                 
          
                    if (new Random().Next(3) > 1) 
                    {                       
                        
                         validationErrors.Add("Problems with replay text length" ); // ToDo
                    }
                    if (new Random().Next(3) > 1)
                    {
                        validationErrors.Add("Problems with email address");
                    }
                    if (validationErrors.Length() != 0)
                    {
                        return new InvalidReplay(validationErrors);
                    }
                    return new ValidReplay(Guid.NewGuid(), (ReplayUnchecked)obj);
                    break;


                case "InvalidReplay":              // Nothing we can do with it end
                    finished = true;
                    return (InvalidReplay)obj;
                    break;

                case "ValidReplay":                 // We do profanity check
                    if (new Random().Next(3) > 1)
                    {
                        return new ReplayProfanityCheckedFailed(Guid.NewGuid(), (ValidReplay)obj, "Profanity Check failed");
                    }
                    return new ReplayProfanityCheckedPassed(Guid.NewGuid(), (ValidReplay)obj);

                    break;

                case "ReplayProfanityCheckedFailed":   // Nothing more we can do with it end
                    finished = true;
                    return (ReplayProfanityCheckedFailed)obj;
                    break;

                case "ReplayProfanityCheckedPassed":                 // Now we can send the mails
                    return new ReplayAcknoledgmentNotSent( (ReplayProfanityCheckedPassed)obj);
                  
                    break;

                case "ReplayAcknoledgmentNotSent":                  // Try resend
                    if (new Random().Next(3) > 10)
                    {
                      //  Console.WriteLine("Haven't sent the mails yet please be pacient");
                        return (ReplayAcknoledgmentNotSent)obj;
                    }
                    //Console.WriteLine("Have finished sending the mails :D");
                    return new ReplayAcknoledgmentSent((ReplayAcknoledgmentNotSent)obj, Guid.NewGuid(), Guid.NewGuid());
                    break;

                case "ReplayAcknoledgmentSent":   // Jobs done the END
                    finished = true;
                    return (ReplayAcknoledgmentSent)obj;
                    break;


                default: return new ReplayMatchError();   // Somthing went really wrong
           

            }

        }
    }
}
