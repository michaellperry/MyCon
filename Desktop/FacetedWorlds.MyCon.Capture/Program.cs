using System;
using System.IO;
using System.Threading;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.BinaryHTTPClient;
using UpdateControls.Correspondence.FileStream;

namespace FacetedWorlds.MyCon.Capture
{
    class Program
    {
        private static Conference _conference;

        static void Main(string[] args)
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FacetedWorlds",
                "MyCon");
            Community community = new Community(FileStreamStorageStrategy.Load(folderPath))
                .AddAsynchronousCommunicationStrategy(new BinaryHTTPAsynchronousCommunicationStrategy(new HTTPConfigurationProvider()))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _conference);

            _conference = community.AddFact(new Conference(CommonSettings.ConferenceID));

            Console.WriteLine("Receiving facts.");
            community.BeginReceiving();
            WaitWhileSynchronizing(community);

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }

        private static void WaitWhileSynchronizing(Community community)
        {
            while (community.Synchronizing)
                Thread.Sleep(1000);

            if (community.LastException != null)
                Console.WriteLine(community.LastException.Message);
        }
    }
}
