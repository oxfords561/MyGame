using System.Windows.Input;

namespace QF.GraphDesigner
{
    public class ContextMenuItem
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Path { get; set; }
        public string Group { get; set; }
        public object Order { get; set; }
        public bool Checked { get; set; }
    }
}