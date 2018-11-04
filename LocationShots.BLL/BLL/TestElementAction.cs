using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public class TestElementAction
    {
        public string ElementType { get; set; }
        public string ElementId { get; set; }
        public string ElementContent { get; set; }

        public TestElementAction(string elementType, string elementId, string elementContent)
        {
            ElementType = elementType;
            ElementId = elementId;
            ElementContent = elementContent;
        }

        public override string ToString()
        {
            return $"ElementType:{ElementType}|ElementId:{ElementId}|ElementContent:{ElementContent}";
        }
    }
}
