using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIproject
{
    class GeneticAlgorithm
    {
        List<double> featureWeights;
        List<CandidateAnswer> answers;

        public GeneticAlgorithm(List<CandidateAnswer> answers)
        {
            this.answers = answers;
            var sizeOfFeatureSet = answers[0].dataSet.Count;
            for (int i = 0; i < sizeOfFeatureSet; i++) {
                featureWeights.Add(1);
            }
        }

        public GeneticAlgorithm(List<CandidateAnswer> answers, string fileName) 
        {
            // Get the feature set based on file
            this.answers = answers;
        }

        public void runGeneticAlgorithm()
        {
            SortList();
            int generationNumber = 0;
            while (generationNumber >= 0)
            {
                var bestScore = scoreMutation(featureWeights);
                for (int i = 0; i < 10; i++)
                {
                    var mutation = mutate(featureWeights);
                    var mutationScore = scoreMutation(mutation);
                    if (mutationScore > bestScore)
                    {
                        bestScore = mutationScore;
                        featureWeights = mutation;
                    }
                }
                storeWeights(featureWeights);
                Console.WriteLine("Generation " + generationNumber + " recieved a score of " + bestScore);
                generationNumber++;
            }
        }

        public double scoreMutation(List<Double> featureSet)
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
