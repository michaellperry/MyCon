using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.BinaryHTTPClient;

namespace $rootnamespace$
{
    public class SynchronizationService
    {
        private Community _community;
        private Identity _identity;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _identity)
                ;

            _identity = _community.AddFact(new Identity(GetAnonymousUserId()));
            configurationProvider.Identity = _identity;

            // Synchronize whenever the user has something to send.
            _community.FactAdded += delegate
            {
                Synchronize();
            };

            // Synchronize when the network becomes available.
            System.Net.NetworkInformation.NetworkChange.NetworkAddressChanged += (sender, e) =>
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                    Synchronize();
            };

            // And synchronize on startup or resume.
            Synchronize();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Identity Identity
        {
            get { return _identity; }
        }

        public void Synchronize()
        {
            _community.BeginSending();
            _community.BeginReceiving();
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }

        private static string GetAnonymousUserId()
        {
            string anid = UserExtendedProperties.GetValue("ANID") as string;
            string anonymousUserId = String.IsNullOrEmpty(anid)
                ? "test:user1"
                : "liveid:" + ParseAnonymousId(anid);
            return anonymousUserId;
        }

        private static string ParseAnonymousId(string anid)
        {
            string[] parts = anid.Split('&');
            IEnumerable<string[]> pairs = parts.Select(part => part.Split('='));
            string id = pairs
                .Where(pair => pair.Length == 2 && pair[0] == "A")
                .Select(pair => pair[1])
                .FirstOrDefault();
            return id;
        }
    }
}
