using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.BinaryHTTPClient.Notification;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private Community _community;
        private Individual _individual;

        public void Initialize()
        {
            var storage = IsolatedStorageStorageStrategy.Load();
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);
            var notification = new WindowsPhoneNotificationStrategy(http);
            communication.SetNotificationStrategy(notification);

            _community = new Community(storage);
            //_community.AddAsynchronousCommunicationStrategy(communication);
            _community.Register<CorrespondenceModel>();
            _community.Subscribe(() => _individual);

            _individual = _community.AddFact(new Individual(GetAnonymousUserId()));
            http.Individual = _individual;

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

        public Individual Individual
        {
            get { return _individual; }
        }

        public void Synchronize()
        {
            _community.BeginSending();
            _community.BeginReceiving();
        }

        private static string GetAnonymousUserId()
        {
            string anid = null;
            try
            {
                anid = UserExtendedProperties.GetValue("ANID") as string;
            }
            catch (NotSupportedException ex)
            {
                anid = null;
            }
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
