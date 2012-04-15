using System;
using System.Configuration;
using System.Timers;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.SQL;
using UpdateControls.Collections;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Web
{
    public class SynchronizationService
    {
        private Community _community;
        private Conference _conference;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            string correspondenceConnectionString = ConfigurationManager.ConnectionStrings["Correspondence"].ConnectionString;
            _community = new Community(new SQLStorageStrategy(correspondenceConnectionString).UpgradeDatabase())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _conference)
                ;
            _community.ClientApp = false;

            _conference = _community.AddFact(new Conference(CommonSettings.ConferenceID));

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                _community.BeginSending();
            };

            // Resume in 5 minutes if there is an error.
            Timer synchronizeTimer = new Timer();
            synchronizeTimer.Elapsed += delegate
            {
                _community.BeginSending();
                _community.BeginReceiving();
            };
            synchronizeTimer.Interval = 5.0 * 60.0 * 1000.0;
            synchronizeTimer.Start();

            // And synchronize on startup.
            _community.BeginSending();
            _community.BeginReceiving();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Conference Conference
        {
            get { return _conference; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }

        public Identity GetIdentity(string userName)
        {
            return _community.AddFact(new Identity(userName));
        }
    }
}