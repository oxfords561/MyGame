using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using QF.GraphDesigner;
using Invert.Data;
using QF;
using QFramework;
using UnityEditor;
using UnityEngine;

namespace QF.GraphDesigner
{
    public class FlagConfig 
    {
        public FlagConfig(Type @for, string flagName, NodeColor color)
        {
            For = @for;
            FlagName = flagName;
            Color = color;
        }

        public Type For { get; set; }

        public string FlagName { get; set; }

        public NodeColor Color { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

    }

    public class FlagSystem 
    {
        //private static Dictionary<Type, FlagConfig> _flagConfigs = new Dictionary<Type, FlagConfig>();

        //public static Dictionary<Type, FlagConfig> FlagConfigs
        //{
        //    get { return _flagConfigs; }
        //    private set { _flagConfigs = value; }
        //}
        private static Dictionary<string, FlagConfig> _flagsByName = new Dictionary<string, FlagConfig>();

        public static Dictionary<string, FlagConfig> FlagByName
        {
            get { return _flagsByName; }
            private set { _flagsByName = value; }
        }

        
    }


    public class NodeSystem :
        IOnMouseUpEvent
    {
        public QFrameworkContainer Container
        {
            get { return InvertApplication.Container; }
        }

        public static bool MinimalView
        {
            get { return EditorPrefs.GetBool("MinimalView", false); }
            set { EditorPrefs.SetBool("MinimalView",value); }
        }

     

        public void OnMouseUp(Drawer drawer, MouseEvent mouseEvent)
        {
            Container.Resolve<IRepository>().Commit();
        }
    }
}
