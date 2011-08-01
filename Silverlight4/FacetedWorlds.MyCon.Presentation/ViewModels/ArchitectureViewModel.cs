using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Presentation.ViewModels
{
    public class ArchitectureViewModel : IPresentationViewModel
    {
        public enum StateId
        {
            Start,
            MVVM,
            IsolatedStorage,
            Cloud,
            Database,
            DataAccess,
            WebService,
            ServiceProxy,
            Conventional,
            DatabaseGlow,
            DataAccessGlow,
            WebServiceGlow,
            ServiceProxyGlow,
            ServiceAgentGlow,
            ViewModelGlow,
            ModelGlow,
            IsolatedStorageGlow,
            ViewGlow,
            SchemalessDatabase,
            StorageStrategy,
            SynchronizationServer,
            CommunicationStrategy,
            Community,
            Correspondence
        }

        private Independent<StateId> _state = new Independent<StateId>();

        public StateId State
        {
            get { return _state; }
        }

        public bool Forward()
        {
            if (_state.Value != StateId.Correspondence)
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

        public string Title
        {
            get
            {
                switch (_state.Value)
                {
                    case StateId.Start:
                        return "Architecture";
                    case StateId.MVVM:
                        return "Model View View Model";
                    case StateId.IsolatedStorage:
                        return "Isolated Storage";
                    case StateId.Cloud:
                        return "Internet";
                    case StateId.Database:
                        return "Database";
                    case StateId.DataAccess:
                        return "Data Access Layer";
                    case StateId.WebService:
                        return "Web Service";
                    case StateId.ServiceProxy:
                        return "Service Proxy";
                    case StateId.Conventional:
                        return "Service Agent";
                    case StateId.DatabaseGlow:
                        return "Database Schema";
                    case StateId.DataAccessGlow:
                        return "Object Relational Mapping";
                    case StateId.WebServiceGlow:
                        return "Web Service Methods";
                    case StateId.ServiceProxyGlow:
                        return "Generate Service Proxy";
                    case StateId.ServiceAgentGlow:
                        return "Service Agent Methods";
                    case StateId.ViewModelGlow:
                        return "Interaction Logic";
                    case StateId.ModelGlow:
                        return "Domain Logic";
                    case StateId.IsolatedStorageGlow:
                        return "XML Schema";
                    case StateId.ViewGlow:
                        return "User Interface";
                    case StateId.SchemalessDatabase:
                        return "Schemaless Database";
                    case StateId.StorageStrategy:
                        return "Storage Strategy";
                    case StateId.SynchronizationServer:
                        return "Synchronization Server";
                    case StateId.CommunicationStrategy:
                        return "Communication Strategy";
                    case StateId.Community:
                        return "Community";
                    case StateId.Correspondence:
                        return "Correspondence";
                }
                return string.Empty;
            }
        }
    }
}
