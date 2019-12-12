using System.ComponentModel;

namespace QF.GraphDesigner
{
    public interface IBackgroundCommand 
    {
        BackgroundWorker Worker { get; set; }
    }
}