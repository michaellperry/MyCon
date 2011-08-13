using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Presentation.ViewModels
{
    public class FactsViewModel : IPresentationViewModel
    {
        public enum StateId
        {
            Start,
            CreateConference1,
            CreateConference2,
            CreateConference3,
            Track,
            CreateTrack1,
            CreateTrack2,
            CreateTrack3,
            Tracks,
            ViewModel,
            Publish,
            Subscribe,
            Mutable,
            Property,
            PropertyOverwrite,
            End,
        }

        private Independent<StateId> _state = new Independent<StateId>();

        public StateId State
        {
            get { return _state; }
        }

        public bool Forward()
        {
            if (_state.Value != StateId.End)
            {
                _state.Value = _state.Value + 1;
                return true;
            }
            else
                return false;
        }

        public bool Backward()
        {
            if (_state.Value != StateId.Start)
            {
                _state.Value = _state.Value - 1;
                return true;
            }
            else
                return false;
        }

        public string Code
        {
            get
            {
                switch (_state.Value)
                {
                    case StateId.Start:
                        return "fact Conference {\nkey:\n    string id;\n}";
                    case StateId.CreateConference1:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));";
                    case StateId.CreateConference2:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Conference con2 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));";
                    case StateId.CreateConference3:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Conference con2 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Conference con3 = Community.AddFact(\n    new Conference(\"86CBD521FD9\"));";
                    case StateId.Track:
                        return "fact Track {\nkey:\n    Conference conference;\n    string name;\n}";
                    case StateId.CreateTrack1:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Track track1 = Community.AddFact(\n    new Track(con1, \"Developers\"));";
                    case StateId.CreateTrack2:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Track track1 = Community.AddFact(\n    new Track(con1, \"Developers\"));\n" +
                            "Conference con2 = Community.AddFact(\n    new Conference(\"86CBD521FD9\"));\n" +
                            "Track track2 = Community.AddFact(\n    new Track(con2, \"Developers\"));";
                    case StateId.CreateTrack3:
                        return
                            "Conference con1 = Community.AddFact(\n    new Conference(\"3796A5094AC\"));\n" +
                            "Track track1 = Community.AddFact(\n    new Track(con1, \"Developers\"));\n" +
                            "Conference con2 = Community.AddFact(\n    new Conference(\"86CBD521FD9\"));\n" +
                            "Track track2 = Community.AddFact(\n    new Track(con2, \"Developers\"));\n" +
                            "Track track3 = Community.AddFact(\n    new Track(con1, \"Project Managers\"));";
                    case StateId.Tracks:
                        return "fact Conference {\nkey:\n    string id;\n\nquery:\n    Track* tracks {\n        Track t : t.conference = this;\n    }\n}";
                    case StateId.ViewModel:
                        return "public class ConferenceViewModel\n{\n    public IEnumerable<string> Tracks\n    {\n        return\n            from track in _conference.Tracks\n            orderby track.Name\n            select track.Name;\n    }\n}";
                    case StateId.Publish:
                        return "fact Track {\nkey:\n    publish Conference conference;\n    string name;\n}";
                    case StateId.Subscribe:
                        return "Community.Subscribe(() => _conference);";
                    case StateId.Mutable:
                        return "fact Conference {\nkey:\n    string id;\n\nmutable:\n    publish string name;\n\nquery:\n    Track* tracks { ... }\n}";
                    case StateId.Property:
                        return "con1 = \"Dallas TechFest\";";
                    case StateId.PropertyOverwrite:
                        return "con1 = \"Dallas TechFest 2011\";";
                }
                return string.Empty;
            }
        }
    }
}
