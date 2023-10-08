using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class Action
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public XElement Element { get; set; }

        public Action(string name, string desc, XElement elem)
        {
            this.Name = name;
            this.Description = desc;
            this.Element = elem;
        }

        public Action HardCopy()
        {
            return new Action(this.Name, this.Description, this.Element);
        }
    }
}
