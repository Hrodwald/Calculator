using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Model
    {







        public void SplitCalc(string calc)
        {
            string finalCalc = calc;
            List<char> operators = new List<char>() { '+', '-', '*', '/', '(', ')' };
            
            foreach (var c in operators)
            {
                int i = 0;
                while ((i = finalCalc.IndexOf(c, i)) != -1)
                {

                    if (c == '(')
                    {
                       finalCalc = finalCalc.Insert(i - 1, "|");
                    }
                    if (c == ')')
                    {
                        finalCalc = finalCalc.Insert(i+1, "|");
                    }
                    if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        finalCalc = finalCalc.Insert(i, "|");
                        finalCalc = finalCalc.Insert(i + 2, "|");
                    }
                    // Increment the index.
                    i=i+3;
                }
                Console.WriteLine(finalCalc);

            }
        }


    }
}
