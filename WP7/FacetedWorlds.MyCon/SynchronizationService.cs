using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Model;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.POXClient;
using FacetedWorlds.MyCon.DallasTechFest;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private const string ConferenceID = "09BD46B786CA49BAB443CC212F6DB5EA";

        private Community _community;
        private Attendee _attendee;

        public void Initialize()
        {
            POXConfigurationProvider configurationProvider = new POXConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new POXAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _attendee.Conference)
                .Subscribe(() => _attendee.ScheduledSessions)
                ;

            Identity identity = _community.AddFact(new Identity(GetAnonymousUserId()));
            Conference conference = _community.AddFact(new Conference(ConferenceID));
            _attendee = _community.AddFact(new Attendee(identity, conference));
            configurationProvider.Identity = identity;

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

            //DataLoader.Load(conference);
        }

        public Community Community
        {
            get { return _community; }
        }

        public Attendee Attendee
        {
            get { return _attendee; }
        }

        public void Synchronize()
        {
            try
            {
                _community.BeginSending();
                _community.BeginReceiving();
            }
            catch (Exception ex)
            {
                // TODO: Ignore
            }
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
