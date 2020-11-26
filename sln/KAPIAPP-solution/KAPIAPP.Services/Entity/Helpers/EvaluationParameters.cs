using System;
using System.Collections.Generic;
using System.Text;

namespace KAPIAPP.Services.Entity.Helpers
{
    public class EvaluationParameters : QueryStringParameters
    {
        public EvaluationParameters()
        {
            OrderBy = "DateEvaluation";
        }
        
        public string BoutiqueName { get; set; }
    }
}
