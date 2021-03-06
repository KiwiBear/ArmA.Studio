﻿using System.Windows.Input;
using System.Collections.ObjectModel;
using ArmA.Studio.Data.UI;
using ArmA.Studio.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArmA.Studio.Data.UI.Commands;
using System.Threading;
using System.Linq;
using System.Windows;

namespace ArmA.Studio.DataContext
{
    public class BreakpointsPane : PanelBase
    {
        public override string Title => Properties.Localization.PanelDisplayName_Breakpoints;

        public ICommand CmdEntryOnDoubleClick => new RelayCommand((p) =>
        {
            if (!(p is BreakpointInfo))
                return;
            var bp = (BreakpointInfo)p;
            var doc = Workspace.Instance.CreateOrFocusDocument(bp.FileRef);
            if (doc is TextEditorBaseDataContext)
            {
                var textDoc = doc as TextEditorBaseDataContext;
                textDoc.GetEditorInstanceAsync().ContinueWith((t) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        t.Result.TextArea.Caret.Line = bp.Line;
                        t.Result.ScrollToLine(bp.Line);
                        t.Result.TextArea.Caret.Show();
                    });
                });
            }
        });

        public ICommand CmdBreakPointInfoUpdated => new RelayCommand((p) =>
        {
            if(p is BreakpointInfo)
            {
                var bpi = (BreakpointInfo)p;
                Workspace.Instance.BreakpointManager.SetBreakpoint(bpi);
                var doc = Workspace.Instance.GetCurrentDocument();
                if (doc.FileReference.Equals(bpi.FileRef))
                {
                    doc.RefreshVisuals();
                }
            }
        });
        public IEnumerable<BreakpointInfo> Breakpoints { get { return this._Breakpoints; } set { this._Breakpoints = value;
            this.RaisePropertyChanged(); } }
        private IEnumerable<BreakpointInfo> _Breakpoints;

        public BreakpointsPane()
        {
            Task.Run(() =>
            {
                SpinWait.SpinUntil(() => Workspace.Instance != null);
                SpinWait.SpinUntil(() => Workspace.Instance.BreakpointManager != null);
                Workspace.Instance.BreakpointManager.OnBreakPointsChanged += this.BreakpointManager_OnBreakPointsChanged;
                this.Breakpoints = Workspace.Instance.BreakpointManager;
            });
        }

        private void BreakpointManager_OnBreakPointsChanged(object sender, BreakpointManager.BreakPointsChangedEventArgs e)
        {
            this.Breakpoints = Workspace.Instance.BreakpointManager.ToList();
        }
    }
}