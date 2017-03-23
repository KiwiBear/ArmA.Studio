﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel;

namespace ArmA.Studio.Dialogs
{
    public partial class WorkspaceSelectorDialog
    {
        /// <summary>
        /// Creates a new WorkspaceSelectorDialog to allow the user to select a workspace.
        /// </summary>
        /// <returns>new workspace path or empty string</returns>
        public static string GetWorkspacePath(string selectedPath = null)
        {
            var dlgDc = new WorkspaceSelectorDialogDataContext();
            if (!string.IsNullOrEmpty(selectedPath))
            {
                dlgDc.CurrentPath = selectedPath;
            }
            var dlg = new WorkspaceSelectorDialog(dlgDc);
            var dlgResult = dlg.ShowDialog();
            if (!dlgResult.HasValue || !dlgResult.Value || dlgDc.CurrentPath.Equals(selectedPath, StringComparison.InvariantCultureIgnoreCase))
            {
                return string.Empty;
            }
            var workspace = dlgDc.CurrentPath;
            return workspace;
        }
    }
}
