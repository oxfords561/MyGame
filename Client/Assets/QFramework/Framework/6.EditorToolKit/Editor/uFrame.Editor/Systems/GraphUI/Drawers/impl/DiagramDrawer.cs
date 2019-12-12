using System;
using System.Collections.Generic;
using System.Linq;
using Invert.Common;
using UnityEngine;
using NotifyCollectionChangedEventArgs = QF.MVVM.NotifyCollectionChangedEventArgs;

namespace QF.GraphDesigner
{    
    public class DiagramDrawer : Drawer, IInputHandler
    {
        public delegate void SelectionChangedEventArgs(IDiagramNode oldData, IDiagramNode newData);
        public event SelectionChangedEventArgs SelectionChanged;

        private IDrawer mNodeDrawerAtMouse;
        private SelectionRectHandler mSelectionRectHandler;
        private IDrawer[] mCachedChildren = { };
        private Dictionary<IGraphFilter, Vector2> mCachedPaths;

        public static float Scale
        {
            get { return InvertGraphEditor.CurrentDiagramViewModel.Scale; }
        }

        public IDiagramNode CurrentMouseOverNode { get; set; }

        public DiagramViewModel DiagramViewModel
        {
            get { return this.DataContext as DiagramViewModel; }
            set { this.DataContext = value; }
        }


        public Rect Rect { get; set; }



        public DiagramDrawer(DiagramViewModel viewModel)
        {
            DiagramViewModel = viewModel;
        }

        public static Rect CreateSelectionRect(Vector2 start, Vector2 current)
        {
            if (current.x > start.x)
            {
                if (current.y > start.y)
                {
                    return new Rect(start.x, start.y,
                        current.x - start.x, current.y - start.y);
                }
                else
                {
                    return new Rect(
                        start.x, current.y, current.x - start.x, start.y - current.y);
                }
            }
            else
            {
                if (current.y > start.y)
                {
                    // x is less and y is greater
                    return new Rect(
                        current.x, start.y, start.x - current.x, current.y - start.y);
                }
                else
                {
                    // both are less
                    return new Rect(
                        current.x, current.y, start.x - current.x, start.y - current.y);
                }
            }
        }


        public void DrawTabs(IPlatformDrawer platform, Rect tabsRect)
        {
         
        }

        public void DrawBreadcrumbs(IPlatformDrawer platform,  float y)
        {

        }

        public override void Draw(IPlatformDrawer platform, float scale)
        {

        }
        public override void OnMouseDoubleClick(MouseEvent mouseEvent)
        {
           
            DiagramViewModel.LastMouseEvent = mouseEvent;
            if (DrawersAtMouse == null)
            {

                DrawersAtMouse = GetDrawersAtPosition(this, mouseEvent.MousePosition).ToArray();
            }
            base.OnMouseDoubleClick(mouseEvent);
            if (DrawersAtMouse.Length < 1)
            {
                if (mouseEvent.ModifierKeyStates.Alt)
                {
                   // DiagramViewModel.ShowContainerDebug();
                }
                else
                {
                    DiagramViewModel.ShowQuickAdd();
                }

                return;
            }
            if (!BubbleEvent(d => d.OnMouseDoubleClick(mouseEvent), mouseEvent))
            {

                return;
            }
            else
            {
               
            }

            DiagramViewModel.Navigate();

            Refresh((IPlatformDrawer)InvertGraphEditor.PlatformDrawer);
            //Refresh((IPlatformDrawer)InvertGraphEditor.PlatformDrawer);
     

        }

        public override void OnMouseEnter(MouseEvent e)
        {
            base.OnMouseEnter(e);
            BubbleEvent(d => d.OnMouseEnter(e), e);
        }
        public override void OnMouseExit(MouseEvent e)
        {
            base.OnMouseExit(e);
            DiagramViewModel.LastMouseEvent = e;
            BubbleEvent(d => d.OnMouseExit(e), e);
        }

        public override void OnMouseDown(MouseEvent mouseEvent)
        {
            base.OnMouseDown(mouseEvent);
            DiagramViewModel.LastMouseEvent = mouseEvent;
            if (DrawersAtMouse == null) return;
            if (!DrawersAtMouse.Any())
            {
                DiagramViewModel.NothingSelected();
                if (mouseEvent.ModifierKeyStates.Ctrl)
                {
                    DiagramViewModel.ShowQuickAdd();
                }
                mouseEvent.Begin(SelectionRectHandler);
            }
            else
            {
                BubbleEvent(d => d.OnMouseDown(mouseEvent), mouseEvent);
            }
        }

        public bool BubbleEvent(Action<IDrawer> action, MouseEvent e)
        {
            if (DrawersAtMouse == null) return true;

            foreach (var item in DrawersAtMouse.OrderByDescending(p => p.ZOrder))
            {
               // if (!item.Enabled) continue;
                action(item);
                if (e.NoBubble)
                {
                    e.NoBubble = false;
                    return false;

                    break;
                }
            }
            return true;
        }

        public override void OnMouseMove(MouseEvent e)
        {
            base.OnMouseMove(e);
            DiagramViewModel.LastMouseEvent = e;
            if (e.IsMouseDown && e.MouseButton == 0 && !e.ModifierKeyStates.Any)
            {
                foreach (var item in Children.OfType<DiagramNodeDrawer>())
                {
                    if (item.ViewModelObject.IsSelected)
                    {
#if UNITY_EDITOR
                        if (DiagramViewModel.Settings.Snap)
                        {
                            item.ViewModel.Position += e.MousePositionDeltaSnapped;
                            item.ViewModel.Position = item.ViewModel.Position.Snap(DiagramViewModel.Settings.SnapSize);
                        }
                        else
                        {
#endif
                            item.ViewModel.Position += e.MousePositionDelta;
#if UNITY_EDITOR
                        }
#endif
                        if (item.ViewModel.Position.x < 0)
                        {
                            item.ViewModel.Position = new Vector2(0f, item.ViewModel.Position.y);
                        }
                        if (item.ViewModel.Position.y < 0)
                        {
                            item.ViewModel.Position = new Vector2(item.ViewModel.Position.x, 0f);
                        }
                        item.Dirty = true;
                        //item.Refresh((IPlatformDrawer)InvertGraphEditor.PlatformDrawer,item.Bounds.position,false);
                    }
                }
            }
            else
            {

                var nodes = GetDrawersAtPosition(this, e.MousePosition).ToArray();

                //NodeDrawerAtMouse = nodes.FirstOrDefault();

                if (DrawersAtMouse != null)
                    foreach (var item in nodes)
                    {
                        var alreadyInside = DrawersAtMouse.Contains(item);
                        if (!alreadyInside)
                        {
                            item.OnMouseEnter(e);
                        }
                    }
                if (DrawersAtMouse != null)
                    foreach (var item in DrawersAtMouse)
                    {
                        if (!nodes.Contains(item))
                        {
                            item.OnMouseExit(e);
                        }
                    }

                DrawersAtMouse = nodes;
                foreach (var node in DrawersAtMouse)
                {
                    node.OnMouseMove(e);
                }
            }
        }

        public IDrawer[] DrawersAtMouse { get; set; }

        public IEnumerable<IDrawer> GetDrawersAtPosition(IDrawer parent, Vector2 point)
        {
            foreach (var child in parent.Children)
            {
                if (child.Bounds.Contains(point))
                {
                    if (child.Children != null && child.Children.Count > 0)
                    {
                        var result = GetDrawersAtPosition(child, point);
                        foreach (var item in result)
                        {
                            yield return item;
                        }
                    }
                    yield return child;
                }
            }
        }
        public override void OnMouseUp(MouseEvent mouseEvent)
        {
            DiagramViewModel.LastMouseEvent = mouseEvent;
            BubbleEvent(d => d.OnMouseUp(mouseEvent), mouseEvent);

        }

        public override void OnRightClick(MouseEvent mouseEvent)
        {
            DiagramViewModel.LastMouseEvent = mouseEvent;
            BubbleEvent(d => d.OnRightClick(mouseEvent), mouseEvent);
            if (DrawersAtMouse == null)
            {
                ShowAddNewContextMenu(mouseEvent);
                return;

            }
            //var item = DrawersAtMouse.OrderByDescending(p=>p.ZOrder).FirstOrDefault();
            IDrawer item = DrawersAtMouse.OfType<ConnectorDrawer>().FirstOrDefault();
            if (item != null)
            {
                InvertApplication.SignalEvent<IShowContextMenu>(_=>_.Show(mouseEvent,item.ViewModelObject));
                return;
            }
            item = DrawersAtMouse.OfType<ItemDrawer>().FirstOrDefault();
            if (item != null)
            {
                if (item.Enabled)
                ShowItemContextMenu(mouseEvent);
                return;
            }
            item = DrawersAtMouse.OfType<DiagramNodeDrawer>().FirstOrDefault();
            if (item == null)
                item = DrawersAtMouse.OfType<HeaderDrawer>().FirstOrDefault();
            if (item != null)
            {
                if (!item.ViewModelObject.IsSelected)
                item.ViewModelObject.Select();
                ShowContextMenu(mouseEvent);
                return;
            }
            ShowAddNewContextMenu(mouseEvent);
        }

        public override void Refresh(IPlatformDrawer platform, Vector2 position, bool hardRefresh = true)
        {
            base.Refresh(platform, position, hardRefresh);
            // Eventually it will all be viewmodels
            if (DiagramViewModel == null) return;
            Dictionary<IGraphFilter, Vector2> dictionary = new Dictionary<IGraphFilter, Vector2>();
            
            var first = true;
            foreach (var filter in new [] {DiagramViewModel.GraphData.RootFilter}.Concat(this.DiagramViewModel.GraphData.GetFilterPath()).Reverse())
            {


                var name = first ? filter.Name : "< " + filter.Name;
                dictionary.Add(filter, platform.CalculateTextSize(name, first ? CachedStyles.GraphTitleLabel : CachedStyles.ItemTextEditingStyle));
                first = false;
            }
                
            mCachedPaths = dictionary;

            Children.Clear();
            DiagramViewModel.Load(hardRefresh);
            Children.Add(SelectionRectHandler);
            Dirty = true;
            //_cachedChildren = Children.OrderBy(p => p.ZOrder).ToArray();
        }

        public void Save()
        {
            DiagramViewModel.Save();
        }

        public void ShowAddNewContextMenu(MouseEvent mouseEvent)
        {
            InvertApplication.SignalEvent<IShowContextMenu>(_ => _.Show(mouseEvent, DiagramViewModel));
        }

        public void ShowContextMenu(MouseEvent mouseEvent)
        {
            InvertApplication.SignalEvent<IShowContextMenu>(_ => _.Show(mouseEvent, DiagramViewModel.SelectedNodes.Cast<object>().ToArray()));
        }

        public void ShowItemContextMenu(MouseEvent mouseEvent)
        {
            InvertApplication.SignalEvent<IShowContextMenu>(_ => _.Show(mouseEvent, DiagramViewModel.SelectedNodeItem));
        }

        protected override void DataContextChanged()
        {
            base.DataContextChanged();
            DiagramViewModel.GraphItems.CollectionChanged += GraphItemsOnCollectionChangedWith;
        }



        protected virtual void OnSelectionChanged(IDiagramNode olddata, IDiagramNode newdata)
        {
            SelectionChangedEventArgs handler = SelectionChanged;
            if (handler != null) handler(olddata, newdata);
        }

        private static void DrawHelp()
        {
            // TODO implement platform stuff
#if UNITY_EDITOR
            //if (InvertGraphEditor.Settings.ShowHelp)
            //{
            //    var rect = new Rect(Screen.width - 275f, 10f, 250f, InvertGraphEditor.KeyBindings.Length * 20f);
            //    GUI.Box(rect, string.Empty);

            //    GUILayout.BeginArea(rect);
            //    foreach (var keyBinding in InvertGraphEditor.KeyBindings.Select(p => p.Name + ": " + p.ToString()).Distinct())
            //    {
            //        EditorGUILayout.LabelField(keyBinding);
            //    }
            //    EditorGUILayout.LabelField("Open Code: Ctrl+Click");
            //    GUILayout.EndArea();

            //}
#endif
        }

        private void DrawErrors()
        {
#if UNITY_EDITOR
            if (DiagramViewModel.HasErrors)
            {
                GUI.Box(Rect, DiagramViewModel.Errors.Message + Environment.NewLine + DiagramViewModel.Errors.StackTrace);
            }
#endif
        }

        public SelectionRectHandler SelectionRectHandler
        {
            get { return mSelectionRectHandler ?? (mSelectionRectHandler = new SelectionRectHandler(DiagramViewModel)); }
            set { mSelectionRectHandler = value; }
        }

        public static bool IsEditingField { get; set; }

        public IEnumerable<IDrawer> CachedChildren
        {
            get
            {
                return mCachedChildren;
            }
        }

        private void GraphItemsOnCollectionChangedWith(object sender, MVVM.NotifyCollectionChangedEventArgs e)
        {
            GraphItemsOnCollectionChangedWith(e);
        }
        private void GraphItemsOnCollectionChangedWith(MVVM.NotifyCollectionChangedEventArgs changeArgs)
        {
            if (changeArgs.NewItems != null)
                foreach (var item in changeArgs.NewItems.OfType<ViewModel>())
                {
            
                    if (item == null) InvertApplication.Log("Graph Item is null");
                 
                    var drawer = InvertGraphEditor.Container.CreateDrawer<IDrawer>(item);
                    
                    if (drawer == null) InvertApplication.Log("Drawer is null");
       
                    Children.Add(drawer);

                    mCachedChildren = Children.OrderBy(p => p.ZOrder).ToArray();
                    drawer.Refresh((IPlatformDrawer)InvertGraphEditor.PlatformDrawer);
                
                }
            if (changeArgs.OldItems != null && changeArgs.OldItems.Count > 0)
            {
                var c = Children.Count;
                Children.RemoveAll(p => changeArgs.OldItems.Contains(p.ViewModelObject));
                var d = Children.Count;
                if (c != d)
                {
                    mCachedChildren = Children.OrderBy(p => p.ZOrder).ToArray();
                }
            }
        }
#if UNITY_EDITOR
        private bool UpgradeOldProject()
        {
            if (DiagramViewModel.NeedsUpgrade)
            {
                var rect = new Rect(50f, 50f, 200f, 75f);
                GUI.Box(rect, string.Empty);
                GUILayout.BeginArea(rect);
                GUILayout.Label("You need to upgrade to the new " + Environment.NewLine +
                                "file format for future compatability.");
                if (GUILayout.Button("Upgrade Now"))
                {
                    DiagramViewModel.UpgradeProject();
                    return true;
                }
                GUILayout.EndArea();
            }
            return false;
        }
#endif
    }
}