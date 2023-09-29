using System.Text;

namespace HTMLBuilder
{
    public class HTMLElement
    {
        public string Name, Text;
        public List<HTMLElement> Elements = new List<HTMLElement>();
        private readonly int indentSize = 2;

        public HTMLElement()
        {

        }
        public HTMLElement(string name, string text)
        {
            this.Name = name ?? throw new ArgumentNullException(name);
            this.Text = text ?? throw new ArgumentNullException(text);
        }

        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indent * indentSize);
            sb.AppendLine($"{i}<{Name}>");

            if(!string.IsNullOrEmpty(Text) ) {
                sb.Append(new string(' ',(indent + 1) * indentSize));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements)
            {
                sb.AppendLine(e.ToStringImpl(indent + 1));
            }
            sb.Append($"{i}</{Name}>");

            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HTMLBuilder
    {
        HTMLElement root = new HTMLElement();
        public string rootName;

        public HTMLBuilder(string rootName) {
            root.Name = rootName;
            this.rootName = rootName;
        }

        public HTMLBuilder AddChildren(string name, string text)
        {
            var element = new HTMLElement(name, text);
            root.Elements.Add(element);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var html = new HTMLBuilder("ul")
                .AddChildren("li", "one")
                .AddChildren("li", "two");
            Console.WriteLine(html.ToString());
        }
    }
}