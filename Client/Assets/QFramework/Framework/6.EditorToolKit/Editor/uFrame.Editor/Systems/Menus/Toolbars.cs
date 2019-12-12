using System;
using QF.GraphDesigner;
using QF;
using QFramework;

namespace QF.GraphDesigner
{
    public class Toolbars : IChangeDatabase, IWorkspaceChanged
    {
        public ToolbarUI ToolbarUI { get; set; }


        private void RefreshToolbar()
        {
            ToolbarUI.AllCommands.Clear();
            ToolbarUI.LeftCommands.Clear();
            ToolbarUI.RightCommands.Clear();
            ToolbarUI.BottomLeftCommands.Clear();
            ToolbarUI.BottomRightCommands.Clear();
        }

        public void ChangeDatabase(IGraphConfiguration configuration)
        {
            RefreshToolbar();
        }

        public void WorkspaceChanged(Workspace workspace)
        {
            RefreshToolbar();
        }
    }
}