using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PriorityQueueDemo;

namespace AIproject
{
    class GeneticAlgorithm
    {
        List<List<double>> generation;
        List<CandidateAnswer> answers;

        public GeneticAlgorithm(List<CandidateAnswer> answers)
        {
            this.answers = answers;
            var sizeOfFeatureSet = answers[0].dataSet.Count;
            var r = new Random();
            this.generation = new List<List<Double>>();
            for (int j = 0; j < 10; j++) {
                generation.Add(new List<double>());
                for (int i = 0; i < sizeOfFeatureSet; i++) {
                    this.generation[j].Add(r.NextDouble() *  r.Next(100));
                }
            }
        }

        public GeneticAlgorithm(List<CandidateAnswer> answers, string fileName) 
        {
            // Get the feature set based on file
            var lastLine = File.ReadLines(fileName).Last();
            this.answers = answers;
        }

        // So, we have a bit of an error, but it shouldn't be too hard to fix
        // mostly because of how nice and separated the bits of our algorithm are

        // The way it is supposed to work is we pull the best candidates from the last generation
        // to be the feature sets for the new generation
        // At the start of the new generation, we do crossover between the best candidates from the previous
        // Then we calculate the scores of the new crossedover ones and stuff
        // We get the best ones from that
        // Mutate them
        // Have new candidates

        public void runGeneticAlgorithm()
        {
            SortList();
            int generationNumber = 1;
            while (generationNumber > 0)
            {
                List<List<Double>> crossedOver = crossover(this.generation);
                
                PriorityQueue<double, List<Double>> generationScores = new PriorityQueue<double,List<double>>();

                foreach (List<Double> featureSet in crossedOver) //getting stuck in the crossOver
                {
                    generationScores.Add(new KeyValuePair<double,List<double>>(1/scoreFeatureSet(featureSet), featureSet));
                }

                this.generation.Clear();
                for (int i = 0; i < 5; i++) 
                {
                    generation.Add(generationScores.DequeueValue());
                }

                for (int i = 0; i < 5; i++)
                {
                    generation.Add(mutate(generationScores.DequeueValue()));
                }

                System.Console.WriteLine("Generation " + generationNumber + ":");
                foreach (List<Double> featureSet in this.generation) 
                {
                    System.Console.WriteLine("Candidate received a score of " + scoreFeatureSet(featureSet) + ".");
                }
                System.Console.WriteLine("===========================================");
                generationNumber++;
            }
        }

        public List<List<double>> crossover(List<List<Double>> generationCandidates)
        {
            List<List<Double>> crossedOvers = new List<List<Double>>();
            foreach (List<Double> featureSet in generationCandidates)
            {
                crossedOvers.Add(featureSet);
                foreach (List<Double> featureSet2 in generationCandidates)
                {
                    if (featureSet != featureSet2)
                    {
                        List<Double> zeldaForDays = featureSet;
                        Random r = new Random();
                        int indexu = r.Next(featureSet.Count);
                        zeldaForDays[indexu] = featureSet2[indexu];
                        crossedOvers.Add(zeldaForDays);
                    }
                }
            }
            return crossedOvers;
        }

        public double scoreFeatureSet(List<Double> featureSet)
        {
            List<List<CandidateAnswer>> answersSortedByQuestion = sortByQuestion();

            double tot = 0;
            foreach (List<CandidateAnswer> ansList in answersSortedByQuestion)
            {
                CandidateAnswer bestAnswer = ansList[0];
                double bestScore = score(bestAnswer, featureSet);
                foreach (CandidateAnswer ca in ansList)
                {
                    double caScore = score(ca, featureSet);
                    if (caScore > bestScore)
                    {
                        bestScore = caScore;
                        bestAnswer = ca;
                    }
                }
                if (bestAnswer.correct)
                {
                    tot += 1;
                }
                else
                {
                    tot -= 1;
                }
            }
            return tot;
        }

        public double score(CandidateAnswer ans, List<double> features)
        {
            double tot = 0;
            for (int i = 0; i < features.Count; i++)
            {
                tot += ans.dataSet[i] * features[i];
            }
            return tot;
        }

        public List<List<CandidateAnswer>> sortByQuestion()
        {
            List<List<CandidateAnswer>> toRet = new List<List<CandidateAnswer>>();
            toRet.Add(new List<CandidateAnswer>());
            int count = 0;
            int prev = this.answers[0].questionID;
            foreach (CandidateAnswer ca in this.answers)
            {
                if (ca.questionID == prev)
                {
                    toRet[count].Add(ca);
                }
                else
                {
                    count++;
                    toRet.Add(new List<CandidateAnswer>());
                    toRet[count].Add(ca);
                }
            }


            return toRet;
        }

        public void SortList()
        {
            this.answers.Sort(delegate(CandidateAnswer a, CandidateAnswer b)
            {
                int diff = a.questionID.CompareTo(b.questionID);
                return diff;
            });
        }

        public List<Double> mutate(List<Double> featureSet)
        {
            Random r = new Random();
            double randomWeight = r.NextDouble() * r.Next(1000);
            int randomIndex = r.Next(featureSet.Count);
            var mutation = featureSet;
            mutation[randomIndex] = randomWeight;
            return mutation;
        }

        public void storeWeights(List<double> featureSet)
        {
            string sassyBlackString = "";
            foreach (double featureWeight in featureSet)
            {
                sassyBlackString = sassyBlackString + featureWeight;
                sassyBlackString = sassyBlackString + ",";
            }
            sassyBlackString = sassyBlackString + "done";
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\glenngs\Desktop\AI repo\Watson\AIproject\AIproject\Results.txt", true))
            {
                file.WriteLine(sassyBlackString);
            }



        }
    }
}
