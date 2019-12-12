using System.Collections.Generic;

namespace QF.GraphDesigner
{
    public interface ITreeItem : IItem
    {
        IEnumerable<IItem> Children { get; }
    }
}