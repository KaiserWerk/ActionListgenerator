using System.Collections.Generic;
using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class Preset
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<XElement> Elements { get; set; }

        public Preset(string name, string desc, List<XElement> elems)
        {
            this.Name = name;
            this.Description = desc;
            this.Elements = elems;
        }

        public Preset HardCopy()
        {
            return new Preset(this.Name, this.Description, this.Elements);
        }
    }
}
