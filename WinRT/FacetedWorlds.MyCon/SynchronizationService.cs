using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.FileStream;
using UpdateControls.Fields;
using Windows.Storage;
using Windows.UI.Xaml;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private const string ThisIdentity = "FacetedWorlds.MyCon.Identity.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Identity> _identity = new Independent<Identity>();
        private Independent<Attendee> _attendee = new Independent<Attendee>();

        public async void Initialize()
        {
            var storage = new FileStreamStorageStrategy();
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);

            _community = new Community(storage);
            _community.AddAsynchronousCommunicationStrategy(communication);
            _community.Register<CorrespondenceModel>();
            _community.Subscribe(() => _identity.Value);

            // Synchronize periodically.
            DispatcherTimer timer = new DispatcherTimer();
            int timeoutSeconds = Math.Min(http.Configuration.TimeoutSeconds, 30);
            timer.Interval = TimeSpan.FromSeconds(5 * timeoutSeconds);
            timer.Tick += delegate(object sender, object e)
            {
                Synchronize();
            };
            timer.Start();

            Identity identity = await _community.LoadFactAsync<Identity>(ThisIdentity);
            if (identity == null)
            {
                string randomId = Punctuation.Replace(Guid.NewGuid().ToString(), String.Empty).ToLower();
                identity = await _community.AddFactAsync(new Identity(randomId));
                await _community.SetFactAsync(ThisIdentity, identity);
            }
            var conference = await _community.AddFactAsync(new Conference(CommonSettings.ConferenceID));
            var attendee = await _community.AddFactAsync(new Attendee(identity, conference));
            lock (this)
            {
                _identity.Value = identity;
                _attendee.Value = attendee;
            }

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
            get
            {
                lock (this)
                {
                    return _identity;
                }
            }
        }

        public Attendee Attendee
        {
            get
            {
                lock (this)
                {
                    return _attendee;
                }
            }
        }

        public void Synchronize()
        {
            _community.BeginSending();
            _community.BeginReceiving();
        }
    }
}
