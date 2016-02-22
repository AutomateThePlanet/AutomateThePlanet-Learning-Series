using System.Diagnostics;

namespace CSharp.Series.Tests
{
    public static class DebuggerBrowsableTest
    {
        private static string squirrelFirstNameName;
        private static string squirrelLastNameName;

        // The following DebuggerBrowsableAttribute prevents the property following it 
        // from appearing in the debug window for the class.
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static string SquirrelFirstNameName 
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
        public static string SquirrelLastNameName
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
    }
}