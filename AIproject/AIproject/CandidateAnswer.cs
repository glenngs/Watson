using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIproject
{   
    
    class CandidateAnswer
    {

        public Double questionID;
        public Double answerID;
        public List<Double> dataSet;
        public Boolean correct;


        public CandidateAnswer(Double qID, Double aID, List<Double> dSet)
        {
            this.correct = false;
            this.questionID = qID;
            this.answerID = aID;
            this.dataSet = dSet;

        }

        public CandidateAnswer(Double qID, Double aID, List<Double> dSet, Boolean correct)
        {
            this.correct = correct;
            this.questionID = qID;
            this.answerID = aID;
            this.dataSet = dSet;

        }

    }
}
