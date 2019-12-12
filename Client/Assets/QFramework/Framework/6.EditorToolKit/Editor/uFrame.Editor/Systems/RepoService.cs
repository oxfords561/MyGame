using Invert.Data;
using QF;
using QFramework;

namespace QF.GraphDesigner
{
    public class RepoService
    {
        
        public IRepository Repository
        {
            get { return Container.Resolve<IRepository>(); }
        }
        
        public QFrameworkContainer Container
        {
            get { return InvertApplication.Container; }
        }
    }
}