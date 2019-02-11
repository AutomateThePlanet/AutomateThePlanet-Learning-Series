namespace TestAutomationReportingTestPortal
{
    public class Calculator
    {
        public int Square(int num) => num * num;

        public int Add(int num1, int num2) => num1 + num2;

        public int Multiply(int num1, int num2) => num1 * num2;

        public int Subtract(int num1, int num2)
        {
            if (num1 > num2)
            {
                return num1 - num2;
            }

            return num2 - num1;

        }

        public float Division(float num1, float num2) => num1 / num2;
    }
}
