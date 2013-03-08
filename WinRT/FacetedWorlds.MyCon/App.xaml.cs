﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace FacetedWorlds.MyCon
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogUnhandledException(e.Exception);
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogUnhandledException(e.Exception);
        }                                                                                           

        public static async void LogUnhandledException(Exception e)
        {
            var logsFolder = await ApplicationData.Current.LocalFolder
                .CreateFolderAsync("Logs", CreationCollisionOption.OpenIfExists);
            var file = await logsFolder.CreateFileAsync(String.Format("{0}.log", DateTime.Now.Ticks));
            var stream = await file.OpenStreamForWriteAsync();
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteLineAsync(e.Message);
                if (e.StackTrace != null)
                    await writer.WriteLineAsync(e.StackTrace);
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            InitializeAnalytics();

            Frame rootFrame = ActivateRootFrame(args);
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            SettingsPane.GetForCurrentView().CommandsRequested += DisplayPrivacyPolicy;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SettingsPane.GetForCurrentView().CommandsRequested -= DisplayPrivacyPolicy;

            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void DisplayPrivacyPolicy(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand privacyPolicyCommand = new SettingsCommand("privacyPolicy", "Privacy Policy", (uiCommand) => { LaunchPrivacyPolicyUrl(); });
            args.Request.ApplicationCommands.Add(privacyPolicyCommand);
        }

        async void LaunchPrivacyPolicyUrl()
        {
            Uri privacyPolicyUrl = new Uri("<<Your privacy policy URL here>>");
            var result = await Windows.System.Launcher.LaunchUriAsync(privacyPolicyUrl);
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            // TODO: Register the Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().QuerySubmitted
            // event in OnWindowCreated to speed up searches once the application is already running

            InitializeAnalytics();
            var rootFrame = ActivateRootFrame(args);

            rootFrame.Navigate(typeof(MainPage), args.QueryText);
        }

        private static Frame ActivateRootFrame(IActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            // Ensure the current window is active
            Window.Current.Activate();
            return rootFrame;
        }

        private void InitializeAnalytics()
        {
            //MarkedUp.AnalyticClient.Initialize("Get your own key.");
            //this.UnhandledException += (s, e) =>
            //    MarkedUp.AnalyticClient.LogLastChanceException(e);
            //TaskScheduler.UnobservedTaskException += (s, e) =>
            //    MarkedUp.AnalyticClient.Error("UnobservedTaskException", e.Exception);
        }
    }
}
