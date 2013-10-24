using System;
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
                    String line = "";

                    while((line =read.ReadLine()) != null)
                    {

                        String[] values = line.Split(',');
                        Double qID = -1.0;
                        Double aID = -1.0;
                        Boolean correct = false;
                        List<Double> dSet = new List<Double>();
                        
                        for (int i = 0; i < values.Length;i++)
                        {
                            if (i == 1)
                            {
                                qID = Double.Parse(values[i]);
                                
                            }
                            else if (i == 0)
                            {
                                aID = Double.Parse(values[i]);
                            }
                            else if(i == (values.Length - 1))
                            {
                                if (values[i].CompareTo("true") == 0)
                                {
                                    correct = true;
                                }
                                else
                                {
                                    correct = false;
                                }
                            }
                            else
                            {
                                dSet.Add(Double.Parse(values[i]));
                            }
                        }

                        CandidateAnswer temp = new CandidateAnswer(qID, aID, dSet, correct);
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
