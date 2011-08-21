using System;
using System.Linq;
using System.Threading;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.FileStream;
using UpdateControls.Correspondence.POXClient;
using System.IO;

namespace FacetedWorlds.MyCon.SurveyDump
{
    class Program
    {
        private static Conference _conference;
        private const string ConferenceID = "3796A5094AC64949B9FB286CBD521FD9";

        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FacetedWorlds", "MyConSurveys");
            Community community = new Community(FileStreamStorageStrategy.Load(path))
                .AddAsynchronousCommunicationStrategy(new POXAsynchronousCommunicationStrategy(new POXConfigurationProvider()))
                .Register<CorrespondenceModel>()
                .Subscribe(() => _conference)
                .Subscribe(() => _conference.AllSessionSurveys);

            _conference = community.AddFact(new Conference(ConferenceID));

            Console.WriteLine("Receiving facts.");
            community.BeginReceiving();
            WaitWhileSynchronizing(community);

            var completedSurveys =
                from survey in _conference.AllSessionSurveys
                from completed in survey.Completed
                select new
                {
                    SessionName = completed.SessionEvaluation.Schedule.SessionPlace.Session.Name,
                    SpeakerName = completed.SessionEvaluation.Schedule.SessionPlace.Session.Speaker.Name,
                    Ratings =
                        from ratingAnswer in completed.SessionEvaluation.RatingAnswers
                        select new
                        {
                            Question = ratingAnswer.Rating.Question.Text,
                            Answer = ratingAnswer.Value
                        },
                    Essays =
                        from essayAnswer in completed.SessionEvaluation.EssayAnswers
                        select new
                        {
                            Question = essayAnswer.Essay.Question.Text,
                            Answer = essayAnswer.Value
                        }
                };

            foreach (var completedSurvey in completedSurveys)
            {
                Console.WriteLine(completedSurvey.SessionName);
                Console.WriteLine(completedSurvey.SpeakerName);
                foreach (var rating in completedSurvey.Ratings)
                    Console.WriteLine(String.Format("{0}: {1}", rating.Question, rating.Answer));
                foreach (var essay in completedSurvey.Essays)
                    Console.WriteLine(String.Format("{0}: {1}", essay.Question, essay.Answer));
                Console.WriteLine();
            }

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
