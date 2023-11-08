using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKY
{
    class ProductionRule
    {
        public string leftHandSide
        {
            get;
            set;
        }
        public string rightHandSide
        {
            get;
            set;
        }

        public ProductionRule(string leftStr, string rightStr)
        {
            leftHandSide = leftStr;
            rightHandSide = rightStr;
        }

    }
}
