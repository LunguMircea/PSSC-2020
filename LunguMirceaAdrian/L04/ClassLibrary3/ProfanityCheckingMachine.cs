using System;
using TextAnalizeMachine;
 namespace TextAnalizeMachine
{
     static public class ProfanityCheckingMachine
    {
        static public float ProfanityCheck()
        {
            Random r = new Random();
            return r.Next() % 100;
        }
    }
}
