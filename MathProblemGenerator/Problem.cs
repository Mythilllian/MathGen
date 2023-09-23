using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using NCalc;
using NCalc.Domain;

namespace MathProblemGenerator
{
    public class Problem
    {
        [JsonPropertyName("vars")]
        List<Variable> variables;

        [JsonPropertyName("txt")]
        string problemText;
        //Format:
        //The man bought {x} apples and {y} bananas.
        //How many fruits did he buy?

        [JsonPropertyName("ans")]
        double answer;
        //Format:
        //{x+y}

        [JsonIgnore]
        Random rand;

        public Problem(List<Variable> variables, string problemText, double answer)
        {
            this.variables = variables;
            this.problemText = problemText;
            this.answer = answer;

            rand = new Random();

            foreach(var variable in variables)
            {
                variable.getVal(rand);
            }

            List<Expression> expressions = new List<Expression>();
            foreach (string expressionStr in GetBetween(problemText, '`'))
            {
                Expression expression = new Expression(expressionStr);
                SetVariables(ref expression, variables.ToArray());
                expressions.Add(expression);
            }
        }

        void SetVariables(ref Expression expression,Variable[] variables)
        {
            foreach(var variable in variables)
            {
                expression.Parameters[variable.character.ToString()] = variable.val;
            }
        }

        string[] GetBetween(string input, char seperator)
        {
            string pattern = seperator + @"([^" + seperator + @"]+)`";

            MatchCollection matches = Regex.Matches(input, pattern);
            List<string> resultArray = new List<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                resultArray.Add(matches[i].Groups[1].Value);
            }

            return resultArray.ToArray();
        }
    }
}
