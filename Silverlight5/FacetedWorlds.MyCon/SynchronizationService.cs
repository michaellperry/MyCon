using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.BinaryHTTPClient;
using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.Model;
using System.Reflection;
using System.IO;
using System.Xml;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private NavigationModel _navigationModel;
        private Community _community;
        private Conference _conference;

        public SynchronizationService(NavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _conference)
                ;

            _conference = _community.AddFact(new Conference(CommonSettings.ConferenceID));

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

            InitializeData();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Conference Conference
        {
            get { return _conference; }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }

        private void InitializeData()
        {
            //DataLoader.GenerateCode();
        }
    }
}
