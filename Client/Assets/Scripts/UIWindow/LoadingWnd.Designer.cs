//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SYJ
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    public partial class LoadingWnd
    {
        
        public const string NAME = "LoadingWnd";
        
        [SerializeField()]
        public UnityEngine.UI.Text txtTips;
        
        [SerializeField()]
        public UnityEngine.UI.Image loadingfg;
        
        [SerializeField()]
        public UnityEngine.UI.Image imgPoint;
        
        [SerializeField()]
        public UnityEngine.UI.Text txtPrg;
        
        private LoadingWndData mPrivateData = null;
        
        public LoadingWndData mData
        {
            get
            {
                return mPrivateData ?? (mPrivateData = new LoadingWndData());
            }
            set
            {
                mUIData = value;
                mPrivateData = value;
            }
        }
        
        protected override void ClearUIComponents()
        {
            txtTips = null;
            loadingfg = null;
            imgPoint = null;
            txtPrg = null;
            mData = null;
        }
    }
}