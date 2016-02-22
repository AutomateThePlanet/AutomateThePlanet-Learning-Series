namespace CSharp.Series.Tests.PropertiesAsLambdas
{
    public class Client
    {
        public string FistName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public override string ToString()
        {
            return string.Concat(this.FistName, " ", this.LastName);
        }

    }
}
