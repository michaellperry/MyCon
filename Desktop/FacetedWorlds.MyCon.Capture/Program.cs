using System;
using System.IO;
using System.Threading;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.FileStream;
using UpdateControls.Correspondence.POXClient;

namespace FacetedWorlds.MyCon.Capture
{
    class Program
    {
        private static Conference conference;
        private const string ConferenceID = "09BD46B786CA49BAB443CC212F6DB5EA";

        static void Main(string[] args)
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FacetedWorlds",
                "MyCon");
            Community community = new Community(FileStreamStorageStrategy.Load(folderPath))
                .AddAsynchronousCommunicationStrategy(new POXAsynchronousCommunicationStrategy(new POXConfigurationProvider()))
                .Register<CorrespondenceModel>()
                .Subscribe(() => conference);

            conference = community.AddFact(new Conference(ConferenceID));

            Console.WriteLine("Receiving facts.");
            community.BeginReceiving();
            WaitWhileSynchronizing(community);

            Console.WriteLine("Updating conference data.");
            DataLoader dataLoader = new DataLoader();
            dataLoader.Load(conference);

            Console.WriteLine("Sending updates.");
            community.BeginSending();
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
