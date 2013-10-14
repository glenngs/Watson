using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIproject
{
    class AIMain
    {
        static void Main(string[] args)
        {


            String filename = System.Console.ReadLine();

            Reader read = new Reader(filename);

            read.read();

            System.Console.WriteLine(read.getCandidates()[0]);

             


        }
    }
}
