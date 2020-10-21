using System;
using System.Collections.Generic;

namespace TextAnalize
{
    static public class MachineCheck
    {
        static public float MachineProfanityIndex(List<string>list)
        {
            Random r = new Random(System.DateTime.UtcNow.Millisecond);
            return r.Next() % 100;
        }
    }
}
