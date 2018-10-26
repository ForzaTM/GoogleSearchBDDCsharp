using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAutoTest.Specs.Core
{
    public class TextElement : WebElement
    {
        public TextElement(WebElement element) : base(element)
        {

        }
        public string GetElementState()
        {
            return !Displayed
                ? "Not Displayed"
                : GetAttribute("class") != null ? "Displayed" : "Not Displayed";
        }
    }
}
