using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Model
    {

        public List<string> SplitCalc(string calc)
        {
            List<string> calcul = new List<string>();
            string finalCalc = calc;
            List<char> operators = new List<char>() { '+', '-', '*', '/', '(', ')' };
            
            foreach (var c in operators)
            {
                int i = 0;
                while ((i = finalCalc.IndexOf(c, i)) != -1)
                {

                    if (c == '(')
                    {
                        if (0 > (i - 1))
                        {
                            
                            finalCalc = finalCalc.Insert(i + 1, "|");
                        }
                        else
                        {
                            finalCalc = finalCalc.Insert(i - 1, "|");
                            finalCalc = finalCalc.Insert(i+2, "|");
                        }
                        }
                    if (c == ')')
                    {
                        finalCalc = finalCalc.Insert(i, "|");
                        finalCalc = finalCalc.Insert(i + 2, "|");
                    }
                    if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        
                            finalCalc = finalCalc.Insert(i, "|");
                            finalCalc = finalCalc.Insert(i + 2, "|");
                        
                    }
                    // Increment the index.
                    i=i+3;
                }
            }

            calcul = finalCalc.Split('|').ToList();
            calcul.RemoveAll(x => string.IsNullOrEmpty(x));
            //Console.WriteLine(finalCalc,calcul.Count);
            return calcul;
        }


        public double calcul(string calc) 
        {
            List<string> toCalc = SplitCalc(calc);
            List<string> abc = handleParant(toCalc);
            calcMaking("*", abc);
            calcMaking("/", abc);
            calcMaking("-", abc);
            calcMaking("+", abc);
            Console.Out.WriteLine(abc[0]);



            return 0;
        }

        public List<string> handleParant(List<string> toCalc) 
        {
           
            int parantIndex;
            int closeParentIndex=0;
            while ((parantIndex = toCalc.FindIndex(r => r.Equals("("))) != -1)
            {
                List<string> tempList = new List<string>();
                int numParant = 1;
                int foundEndParent = 0;
                int i = parantIndex + 1;
                while (foundEndParent != numParant)
                {
                    tempList.Add(toCalc[i]);
                    if (toCalc[i].Equals("("))
                        numParant++;
                    if (toCalc[i].Equals(")"))
                        foundEndParent++;
                    if (foundEndParent == numParant)
                        closeParentIndex = i;
                    i++;
                } //Trouve l'index de la parenthese fermante 
                tempList.RemoveAt(tempList.Count - 1); //Supprime la parenthese fermante

                //s'il n'y a pas de parentheses dans les parentheses
                if (!tempList.Contains("("))
                {
                    calcMaking("*",tempList);
                    calcMaking("/", tempList);
                    calcMaking("-", tempList);
                    calcMaking("+", tempList);
                    
                    if (toCalc.Contains("(")) 
                    {
                        for (int x = parantIndex; x < closeParentIndex+1; x++)
                        {
                            toCalc.RemoveAt(parantIndex);
                        }
                        toCalc.Add(tempList[0]);
                    }
                }
                else 
                { 
                    List<string> tmp = handleParant(tempList);
                    for (int x = parantIndex; x < closeParentIndex+1; x++)
                    {
                        toCalc.RemoveAt(parantIndex);
                    }

                    calcMaking("*", tmp);
                    calcMaking("/", tmp);
                    calcMaking("-", tmp);
                    calcMaking("+", tmp);
                    
                    toCalc.Insert(parantIndex,tmp[0]);
                }
            }
            return toCalc;
        }

        public void calcMaking(string op, List<string> tmpCalc) 
        {
            while (tmpCalc.Contains(op))
            {
                int index = tmpCalc.FindIndex(r => r.Equals(op));

                try {
                    double b = double.Parse(tmpCalc[index + 1]);
                    double a = double.Parse(tmpCalc[index - 1]);
                    
                        for (int x = 0; x < 3; x++)
                    {
                        tmpCalc.RemoveAt(index - 1);
                    }
                    tmpCalc.Insert(index - 1, makeCalc(op, a, b).ToString());
                }catch(Exception ex)
                {
                    double.TryParse(tmpCalc[index + 1], out double b);
                    for (int x = 0; x < 2; x++)
                    {
                        tmpCalc.RemoveAt(index);
                    }
                    tmpCalc.Insert(index, makeCalc(op, 0, b).ToString());
                }
                
                }
        }
        public double makeCalc(string op, double a, double b)
        {
            double result = 0; 
            switch (op) 
            {
                case "+":
                    result = a + b;
                    break;
                case "-":
                    result = a - b;
                    break;
                case "*":
                    result = a * b;
                    break;
                case "/":
                    result = a / b;
                    break;
            }


            return result;
        }
    }

 
}
