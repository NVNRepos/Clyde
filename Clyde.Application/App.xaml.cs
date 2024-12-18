﻿using System;
using System.Threading;
using System.Windows;

namespace Clyde.App
{
    public partial class App : Application
    {
        #region Fields

        private static ApplicationTheme _theme;

        private readonly Mutex _mutex = new Mutex(true, Clyde.Domain.ClydeDomain.APPLICATION_NAME);

        #endregion

        #region Properties

        /// <summary> The requested application color theme </summary>
        public static ApplicationTheme RequestedTheme => _theme;

        public bool IsFirstInstance
            => _mutex.WaitOne(0, false);

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            if(!IsFirstInstance)
                Shutdown();

            _theme = Win32Facade.GetSystemTheme();
#if DEBUG
            Clyde.App.Resources.Language.Culture = Globalization.ClydeCulture.English;
            _theme = ApplicationTheme.Light;
#endif
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Styles.xaml", uriKind: UriKind.Relative) });
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri($"Resources/Themes/{RequestedTheme}.xaml", uriKind: UriKind.Relative) });
            base.OnStartup(e);
        }
    }
}
