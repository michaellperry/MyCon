using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.IsolatedStorage;
using UpdateControls.Correspondence.POXClient;
using FacetedWorlds.MyCon.Models;
using FacetedWorlds.MyCon.Model;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using FacetedWorlds.MyCon.Data;

namespace FacetedWorlds.MyCon
{
    public class SynchronizationService
    {
        private const string ConferenceID = "Conference ID";

        private NavigationModel _navigationModel;
        private Community _community;
        private Conference _conference;

        public SynchronizationService(NavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }

        public void Initialize()
        {
            POXConfigurationProvider configurationProvider = new POXConfigurationProvider();
            _community = new Community(IsolatedStorageStorageStrategy.Load())
                .AddAsynchronousCommunicationStrategy(new POXAsynchronousCommunicationStrategy(configurationProvider))
                .Register<CorrespondenceModel>()
                ;

            _conference = _community.AddFact(new Conference(ConferenceID));

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

            InitializeData();
        }

        public Community Community
        {
            get { return _community; }
        }

        public Conference Conference
        {
            get { return _conference; }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Exception LastException
        {
            get { return _community.LastException; }
        }

        private void InitializeData()
        {
            DataLocator.Load(_conference);

            //string conferenceName = "Dallas TechFest 2011";
            //if (_conference.Name.Value != conferenceName)
            //    _conference.Name = conferenceName;

            //string conferenceMap = "http://img.docstoccdn.com/thumb/orig/10507230.png";
            //if (_conference.MapUrl != conferenceMap)
            //    _conference.MapUrl = conferenceMap;

            //_conference.GetTime(new DateTime(2011, 8, 12, 9, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 12, 10, 30, 0));
            //_conference.GetTime(new DateTime(2011, 8, 12, 12, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 12, 13, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 12, 14, 30, 0));
            //_conference.GetTime(new DateTime(2011, 8, 12, 16, 0, 0));

            //_conference.GetTime(new DateTime(2011, 8, 13, 9, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 13, 10, 30, 0));
            //_conference.GetTime(new DateTime(2011, 8, 13, 12, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 13, 13, 0, 0));
            //_conference.GetTime(new DateTime(2011, 8, 13, 14, 30, 0));
            //_conference.GetTime(new DateTime(2011, 8, 13, 16, 0, 0));
        }
    }
}
