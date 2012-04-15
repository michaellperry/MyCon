using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using FacetedWorlds.MyCon.Model;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.IsolatedStorage;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private Community _community;
        private Attendee _attendee;

        public void Initialize()
        {
            InitializeData();

            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _attendee.Conference)
                .Subscribe(() => _attendee.ScheduledSessions)
                ;

            Identity identity = _community.AddFact(new Identity(GetAnonymousUserId()));
            Conference conference = _community.AddFact(new Conference(CommonSettings.ConferenceID));
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
            catch (Exception)
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

        private void InitializeData()
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                ExtractFile(store, "FactTree.bin");
                ExtractFile(store, "FactTypeTable.bin");
                ExtractFile(store, "IncomingTimestampTable.bin");
                ExtractFile(store, "Index.bin");
                ExtractFile(store, "MessageTable.bin");
                ExtractFile(store, "PeerTable.bin");
                ExtractFile(store, "RoleTable.bin");
            }
        }

        private static void ExtractFile(IsolatedStorageFile store, string baseFileName)
        {
            if (!store.FileExists(baseFileName))
            {
                using (Stream sourceStream = Application.GetResourceStream(new Uri("Data/" + baseFileName, UriKind.Relative)).Stream)
                {
                    using (IsolatedStorageFileStream targetStream = store.CreateFile(baseFileName))
                    {
                        byte[] buffer = new byte[1024];
                        int length;
                        while ((length = sourceStream.Read(buffer, 0, 1024)) > 0)
                            targetStream.Write(buffer, 0, length);
                        targetStream.Close();
                    }
                    sourceStream.Close();
                }
            }
        }
    }
}
