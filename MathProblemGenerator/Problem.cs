using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using NCalc;

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
        string answer;
        //Format:
        //{x+y}

        [JsonIgnore]
        Random rand;

        public Problem(List<Variable> variables, string problemText, string answer)
        {
            this.variables = variables;
            this.problemText = problemText;
            this.answer = answer;

            rand = new Random();

            CreateProblem();
        }

        void CreateProblem()
        {
            for (int i = 0; i < variables.Count; i++)
            {
                variables[i].getVal(rand);
            }

            Evaluate(new Expression(answer));

            Regex rgx = new Regex(@"(`[^`]+)`");

            foreach (string expressionStr in rgx.Matches(problemText).Cast<Match>().Select(m => m.Value).ToArray())
            {
                Expression expression = new Expression(expressionStr);
                problemText = rgx.Replace(problemText, Evaluate(expression), 1);
            }
        }

        string Evaluate(Expression expression)
        {
            foreach(var variable in variables)
            {
                expression.Parameters[variable.character.ToString()] = variable.val;
            }
            return (string)expression.Evaluate();
        }
    }
}
