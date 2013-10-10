using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIproject
{   
    
    class CandidateAnswer
    {

        private int questionID;
        private int answerID;
        private List<int> dataSet;



        public CandidateAnswer(int qID, int aID, List<int> dSet)
        {

            this.questionID = qID;
            this.answerID = aID;
            this.dataSet = dSet;

        }


    }
}
