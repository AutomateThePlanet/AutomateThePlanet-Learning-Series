using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Enums;

namespace HybridTestFramework.UITests.Core.Extensions
{
    public class AdvancedBy : By
    {
        public AdvancedBy(SearchType type, string value, IElement parent) : base(type, value, parent)
        {
        }

        public static By IdEndingWith(string id)
        {
            return new By(SearchType.IdEndingWith, id);
        }

        public static By ValueEndingWith(string valueEndingWith)
        {
            return new By(SearchType.ValueEndingWith, valueEndingWith);
        }

        public static By Xpath(string xpath)
        {
            return new By(SearchType.XPath, xpath);
        }

        public static By LinkTextContaining(string linkTextContaing)
        {
            return new By(SearchType.LinkTextContaining, linkTextContaing);
        }

        public static By CssClass(string cssClass)
        {
            return new By(SearchType.CssClass, cssClass);
        }

        public static By CssClassContaining(string cssClassContaining)
        {
            return new By(SearchType.CssClassContaining, cssClassContaining);
        }

        public static By InnerTextContains(string innerText)
        {
            return new By(SearchType.InnerTextContains, innerText);
        }

        public static By NameEndingWith(string name)
        {
            return new By(SearchType.NameEndingWith, name);
        }

        public static By XPathContaining(string xpath)
        {
            return new By(SearchType.XPathContaining, xpath);
        }

        public static By IdContaining(string id)
        {
            return new By(SearchType.IdContaining, id);
        }
    }
}