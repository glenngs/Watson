using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIproject
{
    class AIMain
    {
        static void Main(string[] args)
        {
/*


            String filename = System.Console.ReadLine();

            Reader read = new Reader(filename);

            read.read();

            System.Console.WriteLine(read.getCandidates()[0]);

             
*/
            if (args.Length == 0 || args[0].IndexOf(".csv") == -1)
            {
                Console.WriteLine("syntax is AIproject.exe <filename>");
                return;
            }
            var reader = new Reader(args[0]);
            //reader.read(); //this will be commented in to read in the files
        }
    }
}
