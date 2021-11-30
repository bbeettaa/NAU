using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_4_Krupina_225
{
    class ArithmeticalOperation
    {
        public event EventHandler<ArithmeticEventArgs> Handler; //! event
        readonly ArithmeticEventArgs eventArgs = new();

        delegate double BiOperation(double a, double b);
        BiOperation biOp;

        delegate double UnoOperation(double a);
        private static UnoOperation unoOp;

        bool firstOperation = true,
        isFirstArgFilled = false,
        isSecondArgFilled = false,
        isUnoFirstOperator = false,
        isUnoSecondOperator = false;

        List<int> pos1 = new();
        int pos2 = 0;
        String arg1 = "", arg2 = "", result = "";

        public void RegistreteHandler(EventHandler<ArithmeticEventArgs> Handler)
        {
            this.Handler = Handler;
        }



        public String Calculate(String str)
        {
            //Handler(this, str);
            eventArgs.message = $"{str}";
            Handler(this, eventArgs);

            ResetVariables();
            unoOp = delegate (double a) //! anonyme
            {
                if (a < 0)
                    return -a;
                return a;
            };

            while (true)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    switch (str[i])
                    {
                        case '+':
                            OperationWithPlus();
                            break;
                        case '-':
                            OperationWithMinus(str, i);
                            break;
                        case '*':
                            OperationWithMultiply(ref i);
                            break;
                        case '/':
                            OperationWithDivide(ref i);
                            break;
                        case '^':
                            OperationWithDegree(ref i);
                            break;
                        case '|':
                            OperationWithModule(ref i);
                            break;
                        case 's':
                            OperationWithSqrt(ref str, ref i);
                            break;
                        case '(':
                            OperationWithbrackets(ref str, ref i);
                            break;

                        default:
                            if (isUnoFirstOperator && isUnoSecondOperator)
                            {
                                PerformArifmeticUnaryOperation(ref str);
                                ResetVariables();
                                i = -1;
                                break;
                            }
                            if (!isFirstArgFilled)
                            {
                                arg1 += str[i];
                                pos1.Add(i);
                            }
                            else if (!isSecondArgFilled)
                            {
                                arg2 += str[i];
                                pos2 = i;
                            }
                            if (isSecondArgFilled ||
                                i + 1 == str.Length && arg1 != "" && arg2 != "")
                            {
                                PerformArifmeticBinaryOperation(ref str);
                                ResetVariables();
                                i = -1;
                            }
                            break;
                    }

                }
                try
                {
                    double.Parse(str);

                    eventArgs.message = $"{str}";
                    Handler(this, eventArgs);

                    break;
                }
                catch
                { }
                ResetVariables();
                firstOperation = false;
            }

            //Console.WriteLine(result);

            return str;
        }


        static private double Plus(double a, double b) => a + b; //! lambda
        static private double Minus(double a, double b) => a - b;
        static private double Multiply(double a, double b) => a * b;
        static private double Divide(double a, double b) => a / b;
        static private double Sqrt(double a) => Math.Sqrt(a);
        static private double Degree(double a, double b)
        {
            double res = a;
            for (int i = 1; i < b; i++)
                res *= a;
            return res;
        }


        private void OperationWithPlus()
        {
            if (arg2 != "")
            {
                isSecondArgFilled = true;
                //continue;
                return;
            }
            if (firstOperation)
            {
                arg1 = "";
                pos1 = new List<int>();
                isFirstArgFilled = false;
                return;
                //continue;
            }
            isFirstArgFilled = true;
            biOp = Plus;
        }
        private void OperationWithMinus(String str,int i)
        {
            if (arg2 != "")
            {
                isSecondArgFilled = true;
                return;
            }
            if (arg1 == "")
            {
                arg1 += str[i];
                pos1.Add(i);
                return;
            }
            if (firstOperation)
            {
                arg1 = "";
                pos1 = new List<int>
                { i};
                arg1 += str[i];
                return;
            }

            isFirstArgFilled = true;
            biOp = Minus;
        }
        private void OperationWithMultiply(ref int i)
        {
            isFirstArgFilled = true;
            if (arg2 != "")
            {
                isSecondArgFilled = true;
                i -= 2;
                return;
            }
            biOp = Multiply;
        }
        private void OperationWithDivide(ref int i)
        {
            isFirstArgFilled = true;
            if (arg2 != "")
            {
                isSecondArgFilled = true;
                i -= 2;
                return;
            }
            biOp = Divide;
        }
        private void OperationWithDegree(ref int i)
        {
            isFirstArgFilled = true;

            if (arg1 != "" && arg2 != "" && biOp != Degree)
            {
                pos1 = new List<int>
                {
                    pos2
                };
                arg1 = arg2;

                arg2 = "";
                isSecondArgFilled = false;
            }

            if (arg2 != "")
            {
                isSecondArgFilled = true;
                i -= 2;
                return;
            }
            biOp = Degree;
        }
        private void OperationWithModule(ref int i)
        {
            isFirstArgFilled = false;
            if (!isUnoFirstOperator)
            {
                isUnoFirstOperator = true;
                pos1 = new List<int>
                {
                    i + 1
                };
                arg1 = "";
                return;
            }
            if (isUnoFirstOperator)
            {
                isUnoSecondOperator = true;
                i -= 2;
                return;
            }
            i = -1;
            ResetVariables();
        }


        private void PerformArifmeticBinaryOperation(ref String str)
        {
            String operand = "";
            for (int i = pos1[0]; i < pos2 + 1; i++)
                operand += str[i];

            str = str.Remove(pos1[0], pos2 - pos1[0] + 1);
            result = biOp(double.Parse(arg1), double.Parse(arg2)).ToString();
            str = str.Insert(pos1[0], result);

            eventArgs.message = $" > {operand} = {result}\t{{{str}}}";
            Handler(this, eventArgs);
        }
        private void PerformArifmeticUnaryOperation(ref String str)
        {
            String operand = "";
            for (int i = pos1[0]-1; i <= pos1[^1]+1; i++)
                operand += str[i];

            str = str.Remove(pos1[0] - 1, pos1[^1] - pos1[0] + 3);

            result = unoOp(double.Parse(arg1)).ToString();
            result = Math.Round(double.Parse(result), 3).ToString();

            CheckFrontSymbolOperation(ref str);
            CheckBackSymbolOperation(ref str);

            eventArgs.message = $" > {operand} = {result}\t{{{str}}}";
            Handler(this, eventArgs);
        }
        private void CheckFrontSymbolOperation(ref String str)
        {
            if (pos1[0] - 2 >= 0)
            {
                int numberOfChar = str[pos1[0] - 2];
                if (numberOfChar >= 48 && numberOfChar <= 57)
                {
                    result = result.Insert(result.Length, "*");
                    str = str.Insert(pos1[0] - 1, result);
                }
                else
                    str = str.Insert(pos1[0] - 1, result);
            }
            else
                str = str.Insert(pos1[0] - 1, result);
        }
        private void CheckBackSymbolOperation(ref String str)
        {
            if (pos1[0] - result.Length + 1 < str.Length)
            {
                int indexOfChar1 = pos1[0] - result.Length + 1;
                int numberOfChar1 = str[indexOfChar1];
                if (numberOfChar1 >= 48 && numberOfChar1 <= 57)
                    str = str.Insert(indexOfChar1, "*");
            }
        }
        private void ResetVariables()
        {
            firstOperation = true;
            isFirstArgFilled = false;
            isSecondArgFilled = false;
            isUnoFirstOperator = false;
            isUnoSecondOperator = false;

            arg1 = "";
            arg2 = "";
            result = "";

            pos1 = new List<int>();
            pos2 = 0;
        }


        private void OperationWithSqrt(ref String str,ref int i)
        {
            String operand = "";
            if (!isUnoFirstOperator)
            {
                arg1 = "";
                pos1 = new List<int>();
                isUnoFirstOperator = true;
                pos1.Add(i);

                if (i + 4 < str.Length && str[i + 4] == '(')
                    return;

                if (str[i] == 's' && str[i + 1] == 'q' && str[i + 2] == 'r' && str[i + 3] == 't')
                {
                    int j = i + 4;
                    while (j < str.Length && str[j] >= '0' && str[j] <= '9')
                    {
                        arg1 += str[j];
                        j++;
                    }
                    pos1.Add(j);

                    unoOp = Sqrt;
                }
                for(int ii = pos1[0]; ii < pos1[1];ii++)
                    operand += str[ii];
                str = str.Remove(pos1[0], pos1[1]-pos1[0]);
                result = unoOp(double.Parse(arg1)).ToString();
                result = Math.Round(double.Parse(result), 3).ToString();
                str = str.Insert(pos1[0], result);
                arg1 = "";

                eventArgs.message = $" > {operand} = {result}\t{{{str}}}";
                Handler(this, eventArgs);

                i = -1;
                ResetVariables();

            }
        }
        private void OperationWithbrackets(ref String str, ref int i)
        {
            ArithmeticalOperation ar = new();
            ar.RegistreteHandler(Handler);

            String operand="";
            int j = i;
            //isUnoFirstOperator = true;
            pos1 = new List<int>
            {
                j
            };

            while (str[j]!=')')
            {
                j++;
                operand += str[j];
            }
            operand = operand.Replace(")","");
            pos1.Add(j);

            str = str.Remove(pos1[0], pos1[1]-pos1[0]+1);
            operand= ar.Calculate(operand);

            str = str.Insert(pos1[0], operand);
                arg1 = "";
            i = -1;
            ResetVariables();

            eventArgs.message = $"{str}";
            Handler(this, eventArgs);

            return;
        }
        
    }
}