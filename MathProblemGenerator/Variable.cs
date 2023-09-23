using System.Text.Json.Serialization;

namespace MathProblemGenerator
{
    public struct Variable
    {
        //Minimum should be <= maximum
        [JsonPropertyName("min")]
        public double minVal { get { return minVal; } set { minVal = value <= maxVal && value % factor == 0 ? value : minVal; } }

        //Maximum should be >= minimum
        [JsonPropertyName("max")]
        public double maxVal { get { return maxVal; } set { maxVal = minVal <= value && value % factor == 0 ? value : maxVal; } }

        //factor is what value is multiple of
        [JsonPropertyName("fac")]
        public double factor { get { return factor; } set {
                minVal = Math.Round((minVal / (double)value), MidpointRounding.AwayFromZero) * value;
                maxVal = Math.Round((maxVal / (double)value), MidpointRounding.AwayFromZero) * value;
                factor = value;
            } }

        [JsonPropertyName("char")]
        public char character { get; set; }

        [JsonIgnore]
        public double val { get; private set; }

        public Variable(float _minVal, float _maxVal, int _factor, char _character) : this()
        {
            minVal = _minVal;
            maxVal = _maxVal;
            factor = _factor;
            character = _character;
        }

        public double getVal(Random rand)
        {
            val = rand.Next((int)(minVal / factor), (int)(maxVal / factor + 1)) * factor;

            return val;
        }
    }
}