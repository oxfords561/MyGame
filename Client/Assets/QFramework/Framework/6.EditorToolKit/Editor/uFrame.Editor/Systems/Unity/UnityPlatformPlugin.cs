using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QF.GraphDesigner;
using Invert.Data;
using QF;
using QFramework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QF.GraphDesigner.Unity
{
    [InitializeOnLoad]
    public class UnityPlatformPlugin : IAssetDeleted, IWorkspaceChanged, IChangeDatabase
    {

        static UnityPlatformPlugin()
        {
            InvertApplication.CachedAssembly(typeof (UnityPlatformPlugin).Assembly);
            InvertApplication.CachedAssembly(typeof(Vector3).Assembly);
            InvertApplication.CachedTypeAssembly(typeof(Vector3).Assembly);
            InvertGraphEditor.Prefs = new UnityPlatformPreferences();
            InvertApplication.Logger = new UnityPlatform();
            InvertGraphEditor.Platform = new UnityPlatform();
            InvertGraphEditor.PlatformDrawer = new UnityDrawer();
        }

        public void AssetDeleted(string filename)
        {
            // TODO 2.0 This is no longer valid
        }

        public void WorkspaceChanged(Workspace workspace)
        {
            if (InvertGraphEditor.DesignerWindow != null)
            {
                InvertGraphEditor.DesignerWindow.ProjectChanged(workspace);
            }
        }

        public void ChangeDatabase(IGraphConfiguration configuration)
        {
            
        }
    }


    

}
