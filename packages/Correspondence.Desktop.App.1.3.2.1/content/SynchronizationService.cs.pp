using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.SSCE;

namespace $rootnamespace$
{
    public class SynchronizationService
    {
        private const string ThisIdentity = "$rootnamespace$.Identity.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Identity _identity;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            string correspondenceDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CorrespondenceApp", "$rootnamespace$", "Correspondence.sdf");
            _community = new Community(new SSCEStorageStrategy(correspondenceDatabase))
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _identity)
                ;

            _identity = _community.LoadFact<Identity>(ThisIdentity);
            if (_identity == null)
            {
                string randomId = Punctuation.Replace(Guid.NewGuid().ToString(), String.Empty).ToLower();
                _identity = _community.AddFact(new Identity(randomId));
                _community.SetFact(ThisIdentity, _identity);
            }

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

        public Identity Identity
        {
            get { return _identity; }
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
