using System.Collections.Generic;
using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class Preset
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<XElement> Elements { get; set; }
    }
}
