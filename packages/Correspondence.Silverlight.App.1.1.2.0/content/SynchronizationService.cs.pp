using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.POXClient;
using $rootnamespace$.Models;

namespace $rootnamespace$
{
    public class SynchronizationService
    {
        private NavigationModel _navigationModel;
        private Community _community;

        public SynchronizationService(NavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }

        public void Initialize()
        {
            POXConfigurationProvider configurationProvider = new POXConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new POXAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _navigationModel.CurrentUser)
                ;

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                _community.BeginSending();
            };

            // Periodically resume if there is an error.
            DispatcherTimer synchronizeTimer = new DispatcherTimer();
            synchronizeTimer.Tick += delegate
            {
                _community.BeginSending();
                _community.BeginReceiving();
            };
            synchronizeTimer.Interval = TimeSpan.FromSeconds(60.0);
            synchronizeTimer.Start();

            // And synchronize on startup.
            _community.BeginSending();
            _community.BeginReceiving();
        }

        public Community Community
        {
            get { return _community; }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }
    }
}
