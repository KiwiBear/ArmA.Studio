﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmA.Studio.Plugin
{
    public interface ISplashActivityPlugin : IPlugin
    {
        /// <summary>
        /// Function to perform activity while the splash screen is displayed.
        /// Function will not be executed on main thread.
        /// </summary>
        /// <param name="SetIndeterminate">Used to set the progress bar into a state where progress cannot be told in percent.</param>
        /// <param name="SetDisplayText">Sets the current display text.</param>
        /// <param name="SetProgress">Updates the progress. Range is from [0.0 - 1.0].</param>
        /// <param name="stlThreadDispatcher">Use this to create things that require MainThread "permissions". Example: MessageBox</param>
        /// <param name="terminate">Determines if the programm should terminate and restart afterwards or not.</param>
        void PerformSplashActivity(System.Windows.Threading.Dispatcher stlThreadDispatcher, Action<bool> SetIndeterminate, Action<string> SetDisplayText, Action<double> SetProgress, out bool terminate);
    }
}
