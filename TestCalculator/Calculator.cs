using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace CalculatorTestTask
{
    public class Calculator
    {
        static char[] operations = new char[] { '+', '/', '*'};
        static int lastBracketIndex = int.MaxValue;

        //Class for counting "simple" expression that has 2 operands and 1 operation e.g 11+4; 2/4 etc. 
        class Action
        {
            float operand1;
            float operand2;
            char operation;
            public Action(float operand1, float operand2, char operation)
            {
                this.operand1 = operand1;
                this.operand2 = operand2;
                this.operation = operation;
            }
            //Counts the result of the expression
            public float GetResult()
            {
                switch (operation)
                {
                    case '+':
                        result = operand1 + operand2;
                        break;
                    case '-':
                        result = operand1 - operand2;
                        break;
                    case '/':
                        result = operand1 / operand2;
                        break;
                    case '*':
                        result = operand1 * operand2;
                        break;
                }
                return result;
            }
            float result;
        }

        //Checks the validity of expression thats been in entered, returns false in there have been input mishaps, otherwise returns true
        public static bool CheckValidity(string startExpression)
        {
            int countOpenBracket = 0;
            int countCloseBracket = 0;
            int countOperations = 0;
            Regex strPattern = new Regex("^[-0-9+/*(),]*$");
            if (!strPattern.IsMatch(startExpression))
            {
                Console.WriteLine("Unknown Symbols");
                return false;
            }
            foreach (var c in startExpression)
            {
                if (c == '(')
                    countOpenBracket++;
                if (c == ')')
                    countCloseBracket++;
                foreach (var op in operations)
                {
                    if (c == op)
                        countOperations++;
                }
            }
            if (countCloseBracket != countOpenBracket)
            {
                Console.WriteLine("Not enough/Too many brackets");
                return false;
            }
            var test = startExpression.Replace("(", "").Replace(")","").Split(operations);
            foreach (var q in test)
            {
                if (float.TryParse(q, out float j))
                    continue;
                else
                {
                    Console.WriteLine("I can't see shit in this mist {0}", q);
                    return false;
                }
            }
            if (countOperations + 1 != (test = test.Where(x => !string.IsNullOrEmpty(x)).ToArray()).Length)
            {
                Console.WriteLine("Not enough operators or operations");
                return false;
            }
            return true;
        }

        //Calculate the expression via recursion, breaking it down to "simple" expressions consisting of 2 operands and 1 operation.
        public static string CalculateExpression(string expression)
        {
            //Check for brackets, if they exist parse them
            if (expression.Contains('('))
            {
                lastBracketIndex = expression.IndexOf(')');
                expression = expression.Substring(0, expression.IndexOf('('))+ ParseBrackets(expression.Substring(expression.IndexOf('(')+1));
                expression = CalculateExpression(expression);
            }
            float result;
            //Check if + operation exists
            if (expression.Split('+').Length > 1)
            {
                if (expression.Contains('+'))
                {
                    var subExpression = expression.Split('+', 2);
                    //check if subExpressions contain other expressions, if so recursively get their result e.g. 11+4+12
                    if (subExpression[0].Split(operations).Length > 1)
                    {
                        subExpression[0] = CalculateExpression(subExpression[0]);
                        expression = subExpression[0] + '+' + subExpression[1];
                    }
                    else if (subExpression[1].Split(operations).Length > 1)
                    {
                        subExpression[1] = CalculateExpression(subExpression[1]);
                        expression = subExpression[0] + '+' + subExpression[1];
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(subExpression[0]))
                            subExpression[0] = "0";
                        if (String.IsNullOrEmpty(subExpression[1]))
                            subExpression[1] = "0";
                        result = new Action(float.Parse(subExpression[0]), float.Parse(subExpression[1]), '+').GetResult();
                        return result.ToString();
                    }
                    expression =  CalculateExpression(expression);
                }
            }
            else if (expression.Split('/', '*').Length > 1)
            {
                if (expression.Contains('/'))
                {
                    var subExpression = expression.Split('/', 2);
                    //check if subExpressions contain other expressions, if so recursively get their result
                    if (subExpression[0].Split(operations).Length > 1)
                    {
                        subExpression[0] = CalculateExpression(subExpression[0]);
                        expression = subExpression[0] + '/' + subExpression[1];
                    }
                    else if (subExpression[1].Split(operations).Length > 1)
                    {
                        subExpression[1] = CalculateExpression(subExpression[1]);
                        expression = subExpression[0] + '/' + subExpression[1];
                    }
                    else
                    {
                        if (subExpression[1] == "0")
                            Console.WriteLine("Can't divide by zero");
                        result = new Action(float.Parse(subExpression[0]), float.Parse(subExpression[1]), '/').GetResult();
                        return result.ToString();
                    }
                    expression =  CalculateExpression(expression);
                }
                else
                {
                    var subExpression = expression.Split('*', 2);
                    //check if subExpressions contain other expressions, if so recursively get their result
                    if (subExpression[0].Split(operations).Length > 1)
                    {
                        subExpression[0] = CalculateExpression(subExpression[0]);
                        expression = subExpression[0] + '*' + subExpression[1];
                    }
                    else if (subExpression[1].Split(operations).Length > 1)
                    {
                        subExpression[1] = CalculateExpression(subExpression[1]);
                        expression = subExpression[0] + '*' + subExpression[1];
                    }
                    else
                    {
                        result = new Action(float.Parse(subExpression[0]), float.Parse(subExpression[1]), '*').GetResult();
                        return result.ToString();
                    }
                    expression = CalculateExpression(expression);
                }           
            }
            return expression;
        }

        //Parsing brackets from the expression via recursion, returns expression without brackets
        static string ParseBrackets(string expression)
        {
            if (expression.Contains('(') && expression.IndexOf('(') < expression.IndexOf(')'))
            {
                int lenghtOfSubExpr = expression.IndexOf(')') - expression.IndexOf('(');
                lastBracketIndex = expression.IndexOf('(');
                var subExpression = ParseBrackets(expression.Substring(expression.IndexOf('(') + 1, lenghtOfSubExpr));
                expression = "(" + expression.Substring(0, expression.IndexOf('(')) + subExpression + expression.Substring(expression.IndexOf(')') + 1);
            }
            else
            {
                string subExpression;
                if (expression.Contains(')'))
                {
                    subExpression = expression.Substring(0, expression.IndexOf(')'));
                    subExpression = CalculateExpression(subExpression);
                    expression = subExpression + expression.Substring(expression.IndexOf(')') + 1);
                }
                else
                {
                    subExpression = expression;
                    subExpression = CalculateExpression(subExpression);
                    expression = subExpression;
                }
            }
            return expression;
        }
        static void Main(string[] args)
        {
            string startingExpression = Console.ReadLine();
            startingExpression = startingExpression.Trim();
            startingExpression = startingExpression.Replace("-", "+-");
            startingExpression = startingExpression.Replace("*+-", "*-");
            startingExpression = startingExpression.Replace("/+-", "/-");
            if (CheckValidity(startingExpression))
            {
                var answer = CalculateExpression(startingExpression);
                Console.WriteLine("Final answer of the expression: {0}", answer);
            }
            Console.ReadKey();
        }
    }
}
