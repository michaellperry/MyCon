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
        private const string ThisIndividual = "FacetedWorlds.MyCon.Individual.this";
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private Community _community;
        private Independent<Individual> _individual = new Independent<Individual>();
        private Independent<Conference> _conference = new Independent<Conference>();

        public async void Initialize()
        {
            var storage = new FileStreamStorageStrategy();
            var http = new HTTPConfigurationProvider();
            var communication = new BinaryHTTPAsynchronousCommunicationStrategy(http);

            _community = new Community(storage);
            _community.AddAsynchronousCommunicationStrategy(communication);
            _community.Register<CorrespondenceModel>();
            _community.Subscribe(() => _individual.Value);
            _community.Subscribe(() => _conference.Value);

            // Synchronize periodically.
            DispatcherTimer timer = new DispatcherTimer();
            int timeoutSeconds = Math.Min(http.Configuration.TimeoutSeconds, 30);
            timer.Interval = TimeSpan.FromSeconds(5 * timeoutSeconds);
            timer.Tick += delegate(object sender, object e)
            {
                Synchronize();
            };
            timer.Start();

            Individual individual = await _community.LoadFactAsync<Individual>(ThisIndividual);
            if (individual == null)
            {
                string randomId = Punctuation.Replace(Guid.NewGuid().ToString(), String.Empty).ToLower();
                individual = await _community.AddFactAsync(new Individual(randomId));
                await _community.SetFactAsync(ThisIndividual, individual);
            }
            var conference = await _community.AddFactAsync(new Conference(CommonSettings.ConferenceID));
            lock (this)
            {
                _individual.Value = individual;
                _conference.Value = conference;
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

        public Conference Conference
        {
            get
            {
                lock (this)
                {
                    return _conference;
                }
            }
        }

        public Individual Individual
        {
            get
            {
                lock (this)
                {
                    return _individual;
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
