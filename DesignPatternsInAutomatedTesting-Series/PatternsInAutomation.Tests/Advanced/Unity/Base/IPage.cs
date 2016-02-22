using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsInAutomation.Tests.Advanced.Unity.Base
{
    public interface IPage<M, V>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new()
    {
        V Validate();

        void Navigate(string part = "");
    }
}
