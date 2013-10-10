﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AIproject
{
    class Reader
    {
        String filename;
        List<CandidateAnswer> candidates;

        public Reader(String filename)
        {
            this.filename = filename;
            this.candidates = new List<CandidateAnswer>();
            this.read();
        }


        public void read()
        {

            try
            {
                using (StreamReader read = new StreamReader(this.filename))
                {
                    while(true)
                    {
                        String line = read.ReadLine();
                        if(line == null){
                            break;
                        }
                        String[] values = line.Split(',');
                        int qID = -1;
                        int aID = -1;
                        List<int> dSet = new List<int>();
                        for (int i = 0; i < values.Length;i++)
                        {
                            if (i == 0)
                            {
                                qID = Convert.ToInt32(values[i]);

                            }
                            else if (i == 1)
                            {
                                aID = Convert.ToInt32(values[i]);
                            }
                            else
                            {
                                dSet.Add(Convert.ToInt32(values[i]));
                            }
                        }

                        CandidateAnswer temp = new CandidateAnswer(qID, aID, dSet);
                        this.candidates.Add(temp);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public List<CandidateAnswer> getCandidates()
        {
            return this.candidates;
        }
    }
}