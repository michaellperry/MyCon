using System;
using System.Configuration;
using System.IO;
using System.Timers;
using System.Web.Hosting;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.FileStream;

namespace FacetedWorlds.MyCon.Web
{
    public class SynchronizationService
    {
        private Community _community;
        private Conference _conference;

        public void Initialize()
        {
            HTTPConfigurationProvider configurationProvider = new HTTPConfigurationProvider();
            string path = Path.Combine(HostingEnvironment.MapPath("~/App_Data"), "Correspondence");
            _community = new Community(FileStreamStorageStrategy.Load(path))
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _conference)
                ;
            _community.ClientApp = false;

            string conferenceId = ConfigurationManager.AppSettings["ConferenceID"];
            if (string.IsNullOrEmpty(conferenceId))
                conferenceId = CommonSettings.ConferenceID;
            _conference = _community.AddFact(new Conference(conferenceId));

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
    }
}
