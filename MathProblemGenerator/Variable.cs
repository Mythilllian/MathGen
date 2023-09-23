namespace MathProblemGenerator
{
    public struct Variable
    {
        //Minimum should be <= maximum
        public float minVal { get { return minVal; } set { minVal = value <= maxVal ? value : minVal; } }

        //Maximum should be >= minimum
        public float maxVal { get { return maxVal; } set { maxVal = minVal <= value ? value : maxVal; } }

        //Precision is what value is multiple of
        public int precision = 1;

        public char character;

        public Variable(float minVal, float maxVal, int precision, char character)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.precision = precision
        }
    }
}