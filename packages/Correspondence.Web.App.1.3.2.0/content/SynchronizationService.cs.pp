using System;
using System.Configuration;
using System.Timers;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.SQL;
using UpdateControls.Collections;
using System.Collections.Generic;
using System.Linq;

namespace $rootnamespace$
{
    public class SynchronizationService
    {
        private Community _community;
        private Domain _theDomain;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            string correspondenceConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            _community = new Community(new SQLStorageStrategy(correspondenceConnectionString).UpgradeDatabase())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _theDomain)
                ;
            _community.ClientApp = false;
            _theDomain = _community.AddFact(new Domain());

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

        public Exception LastException
        {
            get { return _community.LastException; }
        }

        public Identity GetIdentity(string userName)
        {
            return _community.AddFact(new Identity(userName));
        }

        public MessageBoard GetMessageBoard(string topic)
        {
            return _community.AddFact(new MessageBoard(topic));
        }
    }
}
