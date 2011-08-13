using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Presentation.ViewModels
{
    public class FactsViewModel : IPresentationViewModel
    {
        public enum StateId
        {
            NewIdentityMike,
            NewIdentityRussell,
            NewCloud1,
            NewCloud2,
            NewThought3,
            NewThought3Text,
            NewThought4,
            NewThought4Text,
            NewLink,
            QueryNeighbors,
            NewShare,
            QueryClouds,
            PublishThoughts
        }

        private Independent<StateId> _state = new Independent<StateId>();

        public StateId State
        {
            get { return _state; }
        }

        public bool Forward()
        {
            if (_state.Value != StateId.PublishThoughts)
            {
                _state.Value = _state.Value + 1;
                return true;
            }
            else
                return false;
        }

        public bool Backward()
        {
            if (_state.Value != StateId.NewIdentityMike)
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
                    case StateId.NewIdentityMike:
                    case StateId.NewIdentityRussell:
                        return "fact Identity {\nkey:\n    string userName;\n}";
                    case StateId.NewCloud1:
                    case StateId.NewCloud2:
                        return "fact Cloud {\nkey:\n    unique;\n}";
                    case StateId.NewThought3:
                        return "fact Thought {\nkey:\n    unique;\n    Cloud cloud;\n}";
                    case StateId.NewThought3Text:
                    case StateId.NewThought4:
                    case StateId.NewThought4Text:
                        return "fact Thought {\nkey:\n    unique;\n    Cloud cloud;\n\nmutable:\n    string text;\n}";
                    case StateId.NewLink:
                        return "fact Link {\nkey:\n    Thought* thoughts;\n}";
                    case StateId.QueryNeighbors:
                        return "fact Thought {\n...\nquery:\n    Thought* neighbors {\n        Link l : l.thoughts = this\n        Thought t : l.thoughts = t\n    }\n}";
                    case StateId.NewShare:
                        return "fact Share {\nkey:\n    publish Identity recipient;\n    Cloud cloud;\n}";
                    case StateId.QueryClouds:
                        return "fact Identity {\n...\nquery:\n    Cloud* sharedClouds {\n        Share s : s.recipient = this\n        Cloud c : s.cloud = c\n    }\n}";
                    case StateId.PublishThoughts:
                        return "fact Thought {\nkey:\n    unique;\n    publish Cloud cloud;\n\nmutable:\n    string text;\n}";
                }
                return string.Empty;
            }
        }
    }
}
