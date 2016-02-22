using System.Diagnostics;

namespace CSharp.Series.Tests
{
    //[DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerDisplay("Age {Age > 0 ? Age : 5}")]
    [DebuggerStepThroughAttribute]
    public class DebuggerDisplayTest
    {
        private string squirrelFirstNameName;
        private string squirrelLastNameName;

        public string SquirrelFirstNameName 
        {
            get
            {
                return squirrelFirstNameName;
            }
            set
            {
                squirrelFirstNameName = value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public string SquirrelLastNameName
        {
            get
            {
                return squirrelLastNameName;
            }
            set
            {
                squirrelLastNameName = value;
            }
        }

        public int Age { get; set; }

        private string DebuggerDisplay
        {
            get { return string.Format("{0} de {1}", SquirrelFirstNameName, SquirrelLastNameName); }
        }
    }
}