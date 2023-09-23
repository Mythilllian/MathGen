using System.Text.Json.Serialization;

namespace MathProblemGenerator
{
    public class Problem
    {
        [JsonPropertyName("vars")]
        List<Variable> variables;

        [JsonPropertyName("txt")]
        string problemText;

        [JsonPropertyName("ans")]
        double answer;

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
        }
    }
}
