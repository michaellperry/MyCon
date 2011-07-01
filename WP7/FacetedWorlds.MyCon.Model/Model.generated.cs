using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

/**
/ For use with http://graphviz.org/
digraph "FacetedWorlds.MyCon.Model"
{
    rankdir=BT
    DisableToastNotification -> Identity
    EnableToastNotification -> DisableToastNotification [label="  *"]
    ConferenceName -> Conference
    ConferenceName -> ConferenceName [label="  *"]
    ConferenceSessionSurvey -> Conference
    ConferenceSessionSurvey -> ConferenceSessionSurvey [label="  *"]
    ConferenceSessionSurvey -> Survey
    ConferenceConferenceSurvey -> Conference
    ConferenceConferenceSurvey -> ConferenceConferenceSurvey [label="  *"]
    ConferenceConferenceSurvey -> Survey
    Attendee -> Identity
    Attendee -> Conference
    Day -> Conference
    Time -> Day
    Slot -> Attendee
    Slot -> Time
    Room -> Conference
    Track -> Conference
    Speaker -> Conference
    SpeakerImageUrl -> Speaker
    SpeakerImageUrl -> SpeakerImageUrl [label="  *"]
    SpeakerContact -> Speaker
    SpeakerContact -> SpeakerContact [label="  *"]
    SpeakerBio -> Speaker
    SpeakerBio -> SpeakerBio [label="  *"]
    SpeakerBio -> DocumentSegment [label="  *"]
    Place -> Time
    Place -> Room
    Session -> Conference
    Session -> Speaker
    Session -> Track [label="  ?"]
    SessionName -> Session
    SessionName -> SessionName [label="  *"]
    SessionDescription -> Session
    SessionDescription -> SessionDescription [label="  *"]
    SessionDescription -> DocumentSegment [label="  *"]
    SessionLevel -> Session
    SessionLevel -> SessionLevel [label="  *"]
    SessionLevel -> Level
    SessionPlace -> Session
    SessionPlace -> Place
    SessionPlace -> SessionPlace [label="  *"]
    Schedule -> Slot
    Schedule -> SessionPlace
    ScheduleRemove -> Schedule
    Survey -> RatingQuestion [label="  *"]
    Survey -> EssayQuestion [label="  *"]
    SessionEvaluation -> Schedule
    SessionEvaluation -> Survey
    SessionEvaluationRating -> SessionEvaluation
    SessionEvaluationRating -> RatingQuestion
    SessionEvaluationRatingAnswer -> SessionEvaluationRating
    SessionEvaluationRatingAnswer -> SessionEvaluationRatingAnswer [label="  *"]
    SessionEvaluationEssay -> SessionEvaluation
    SessionEvaluationEssay -> EssayQuestion
    SessionEvaluationEssayAnswer -> SessionEvaluationEssay
    SessionEvaluationEssayAnswer -> SessionEvaluationEssayAnswer [label="  *"]
}
**/

namespace FacetedWorlds.MyCon.Model
{
    public partial class Identity : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Identity newFact = new Identity(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Identity fact = (Identity)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Identity", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query QueryIsToastNotificationDisabled = new Query()
            .JoinSuccessors(DisableToastNotification.RoleIdentity, Condition.WhereIsEmpty(DisableToastNotification.QueryIsReenabled)
            )
            ;

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<DisableToastNotification> _isToastNotificationDisabled;

        // Business constructor
        public Identity(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Identity(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _isToastNotificationDisabled = new Result<DisableToastNotification>(this, QueryIsToastNotificationDisabled);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public IEnumerable<DisableToastNotification> IsToastNotificationDisabled
        {
            get { return _isToastNotificationDisabled; }
        }

        // Mutable property access

    }
    
    public partial class DisableToastNotification : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				DisableToastNotification newFact = new DisableToastNotification(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				DisableToastNotification fact = (DisableToastNotification)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.DisableToastNotification", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleIdentity = new Role(new RoleMemento(
			_correspondenceFactType,
			"identity",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Identity", 1),
			false));

        // Queries
        public static Query QueryIsReenabled = new Query()
            .JoinSuccessors(EnableToastNotification.RoleDisable)
            ;

        // Predicates
        public static Condition IsReenabled = Condition.WhereIsNotEmpty(QueryIsReenabled);

        // Predecessors
        private PredecessorObj<Identity> _identity;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public DisableToastNotification(
            Identity identity
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, identity);
        }

        // Hydration constructor
        private DisableToastNotification(FactMemento memento)
        {
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Identity Identity
        {
            get { return _identity.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class EnableToastNotification : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				EnableToastNotification newFact = new EnableToastNotification(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				EnableToastNotification fact = (EnableToastNotification)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.EnableToastNotification", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDisable = new Role(new RoleMemento(
			_correspondenceFactType,
			"disable",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.DisableToastNotification", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorList<DisableToastNotification> _disable;

        // Fields

        // Results

        // Business constructor
        public EnableToastNotification(
            IEnumerable<DisableToastNotification> disable
            )
        {
            InitializeResults();
            _disable = new PredecessorList<DisableToastNotification>(this, RoleDisable, disable);
        }

        // Hydration constructor
        private EnableToastNotification(FactMemento memento)
        {
            InitializeResults();
            _disable = new PredecessorList<DisableToastNotification>(this, RoleDisable, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public IEnumerable<DisableToastNotification> Disable
        {
            get { return _disable; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Conference : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Conference newFact = new Conference(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._id = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Conference fact = (Conference)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._id);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Conference", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query QueryName = new Query()
            .JoinSuccessors(ConferenceName.RoleConference, Condition.WhereIsEmpty(ConferenceName.QueryIsCurrent)
            )
            ;
        public static Query QuerySessionSurvey = new Query()
            .JoinSuccessors(ConferenceSessionSurvey.RoleConference, Condition.WhereIsEmpty(ConferenceSessionSurvey.QueryIsCurrent)
            )
            ;
        public static Query QueryConferenceSurvey = new Query()
            .JoinSuccessors(ConferenceConferenceSurvey.RoleConference, Condition.WhereIsEmpty(ConferenceConferenceSurvey.QueryIsCurrent)
            )
            ;
        public static Query QueryDays = new Query()
            .JoinSuccessors(Day.RoleConference)
            ;
        public static Query QueryTracks = new Query()
            .JoinSuccessors(Track.RoleConference)
            ;
        public static Query QuerySessions = new Query()
            .JoinSuccessors(Session.RoleConference)
            ;
        public static Query QuerySpeakers = new Query()
            .JoinSuccessors(Speaker.RoleConference)
            ;

        // Predicates

        // Predecessors

        // Fields
        private string _id;

        // Results
        private Result<ConferenceName> _name;
        private Result<ConferenceSessionSurvey> _sessionSurvey;
        private Result<ConferenceConferenceSurvey> _conferenceSurvey;
        private Result<Day> _days;
        private Result<Track> _tracks;
        private Result<Session> _sessions;
        private Result<Speaker> _speakers;

        // Business constructor
        public Conference(
            string id
            )
        {
            InitializeResults();
            _id = id;
        }

        // Hydration constructor
        private Conference(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<ConferenceName>(this, QueryName);
            _sessionSurvey = new Result<ConferenceSessionSurvey>(this, QuerySessionSurvey);
            _conferenceSurvey = new Result<ConferenceConferenceSurvey>(this, QueryConferenceSurvey);
            _days = new Result<Day>(this, QueryDays);
            _tracks = new Result<Track>(this, QueryTracks);
            _sessions = new Result<Session>(this, QuerySessions);
            _speakers = new Result<Speaker>(this, QuerySpeakers);
        }

        // Predecessor access

        // Field access
        public string Id
        {
            get { return _id; }
        }

        // Query result access
        public IEnumerable<Day> Days
        {
            get { return _days; }
        }
        public IEnumerable<Track> Tracks
        {
            get { return _tracks; }
        }
        public IEnumerable<Session> Sessions
        {
            get { return _sessions; }
        }
        public IEnumerable<Speaker> Speakers
        {
            get { return _speakers; }
        }

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new ConferenceName(this, _name, value.Value));
			}
        }

        public Disputable<Survey> SessionSurvey
        {
            get { return _sessionSurvey.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new ConferenceSessionSurvey(this, _sessionSurvey, value.Value));
			}
        }
        public Disputable<Survey> ConferenceSurvey
        {
            get { return _conferenceSurvey.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new ConferenceConferenceSurvey(this, _conferenceSurvey, value.Value));
			}
        }
    }
    
    public partial class ConferenceName : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ConferenceName newFact = new ConferenceName(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceName fact = (ConferenceName)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceName", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.ConferenceName", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ConferenceName.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<ConferenceName> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceName(
            Conference conference
            ,IEnumerable<ConferenceName> prior
            ,string value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<ConferenceName>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceName(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<ConferenceName>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }
        public IEnumerable<ConferenceName> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ConferenceSessionSurvey : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ConferenceSessionSurvey newFact = new ConferenceSessionSurvey(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceSessionSurvey fact = (ConferenceSessionSurvey)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceSessionSurvey", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.ConferenceSessionSurvey", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Survey", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ConferenceSessionSurvey.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<ConferenceSessionSurvey> _prior;
        private PredecessorObj<Survey> _value;

        // Fields

        // Results

        // Business constructor
        public ConferenceSessionSurvey(
            Conference conference
            ,IEnumerable<ConferenceSessionSurvey> prior
            ,Survey value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<ConferenceSessionSurvey>(this, RolePrior, prior);
            _value = new PredecessorObj<Survey>(this, RoleValue, value);
        }

        // Hydration constructor
        private ConferenceSessionSurvey(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<ConferenceSessionSurvey>(this, RolePrior, memento);
            _value = new PredecessorObj<Survey>(this, RoleValue, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }
        public IEnumerable<ConferenceSessionSurvey> Prior
        {
            get { return _prior; }
        }
             public Survey Value
        {
            get { return _value.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class ConferenceConferenceSurvey : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ConferenceConferenceSurvey newFact = new ConferenceConferenceSurvey(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceConferenceSurvey fact = (ConferenceConferenceSurvey)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceConferenceSurvey", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.ConferenceConferenceSurvey", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Survey", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ConferenceConferenceSurvey.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<ConferenceConferenceSurvey> _prior;
        private PredecessorObj<Survey> _value;

        // Fields

        // Results

        // Business constructor
        public ConferenceConferenceSurvey(
            Conference conference
            ,IEnumerable<ConferenceConferenceSurvey> prior
            ,Survey value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<ConferenceConferenceSurvey>(this, RolePrior, prior);
            _value = new PredecessorObj<Survey>(this, RoleValue, value);
        }

        // Hydration constructor
        private ConferenceConferenceSurvey(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<ConferenceConferenceSurvey>(this, RolePrior, memento);
            _value = new PredecessorObj<Survey>(this, RoleValue, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }
        public IEnumerable<ConferenceConferenceSurvey> Prior
        {
            get { return _prior; }
        }
             public Survey Value
        {
            get { return _value.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Attendee : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Attendee newFact = new Attendee(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Attendee fact = (Attendee)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Attendee", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleIdentity = new Role(new RoleMemento(
			_correspondenceFactType,
			"identity",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Identity", 1),
			false));
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));

        // Queries
        public static Query QueryCurrentSchedules = new Query()
            .JoinSuccessors(Slot.RoleAttendee)
            .JoinSuccessors(Schedule.RoleSlot, Condition.WhereIsEmpty(Schedule.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Identity> _identity;
        private PredecessorObj<Conference> _conference;

        // Fields

        // Results
        private Result<Schedule> _currentSchedules;

        // Business constructor
        public Attendee(
            Identity identity
            ,Conference conference
            )
        {
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, identity);
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
        }

        // Hydration constructor
        private Attendee(FactMemento memento)
        {
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, memento);
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentSchedules = new Result<Schedule>(this, QueryCurrentSchedules);
        }

        // Predecessor access
        public Identity Identity
        {
            get { return _identity.Fact; }
        }
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access

        // Query result access
        public IEnumerable<Schedule> CurrentSchedules
        {
            get { return _currentSchedules; }
        }

        // Mutable property access

    }
    
    public partial class Day : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Day newFact = new Day(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._conferenceDate = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Day fact = (Day)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._conferenceDate);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Day", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));

        // Queries
        public static Query QueryTimes = new Query()
            .JoinSuccessors(Time.RoleDay)
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private DateTime _conferenceDate;

        // Results
        private Result<Time> _times;

        // Business constructor
        public Day(
            Conference conference
            ,DateTime conferenceDate
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _conferenceDate = conferenceDate;
        }

        // Hydration constructor
        private Day(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _times = new Result<Time>(this, QueryTimes);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access
        public DateTime ConferenceDate
        {
            get { return _conferenceDate; }
        }

        // Query result access
        public IEnumerable<Time> Times
        {
            get { return _times; }
        }

        // Mutable property access

    }
    
    public partial class Time : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Time newFact = new Time(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._start = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Time fact = (Time)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._start);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Time", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDay = new Role(new RoleMemento(
			_correspondenceFactType,
			"day",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Day", 1),
			false));

        // Queries
        public static Query QueryAvailableSessions = new Query()
            .JoinSuccessors(Place.RolePlaceTime)
            .JoinSuccessors(SessionPlace.RolePlace, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Day> _day;

        // Fields
        private DateTime _start;

        // Results
        private Result<SessionPlace> _availableSessions;

        // Business constructor
        public Time(
            Day day
            ,DateTime start
            )
        {
            InitializeResults();
            _day = new PredecessorObj<Day>(this, RoleDay, day);
            _start = start;
        }

        // Hydration constructor
        private Time(FactMemento memento)
        {
            InitializeResults();
            _day = new PredecessorObj<Day>(this, RoleDay, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _availableSessions = new Result<SessionPlace>(this, QueryAvailableSessions);
        }

        // Predecessor access
        public Day Day
        {
            get { return _day.Fact; }
        }

        // Field access
        public DateTime Start
        {
            get { return _start; }
        }

        // Query result access
        public IEnumerable<SessionPlace> AvailableSessions
        {
            get { return _availableSessions; }
        }

        // Mutable property access

    }
    
    public partial class Slot : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Slot newFact = new Slot(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Slot fact = (Slot)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Slot", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleAttendee = new Role(new RoleMemento(
			_correspondenceFactType,
			"attendee",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Attendee", 1),
			false));
        public static Role RoleSlotTime = new Role(new RoleMemento(
			_correspondenceFactType,
			"slotTime",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Time", 1),
			false));

        // Queries
        public static Query QueryCurrentSchedules = new Query()
            .JoinSuccessors(Schedule.RoleSlot, Condition.WhereIsEmpty(Schedule.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Attendee> _attendee;
        private PredecessorObj<Time> _slotTime;

        // Fields

        // Results
        private Result<Schedule> _currentSchedules;

        // Business constructor
        public Slot(
            Attendee attendee
            ,Time slotTime
            )
        {
            InitializeResults();
            _attendee = new PredecessorObj<Attendee>(this, RoleAttendee, attendee);
            _slotTime = new PredecessorObj<Time>(this, RoleSlotTime, slotTime);
        }

        // Hydration constructor
        private Slot(FactMemento memento)
        {
            InitializeResults();
            _attendee = new PredecessorObj<Attendee>(this, RoleAttendee, memento);
            _slotTime = new PredecessorObj<Time>(this, RoleSlotTime, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentSchedules = new Result<Schedule>(this, QueryCurrentSchedules);
        }

        // Predecessor access
        public Attendee Attendee
        {
            get { return _attendee.Fact; }
        }
        public Time SlotTime
        {
            get { return _slotTime.Fact; }
        }

        // Field access

        // Query result access
        public IEnumerable<Schedule> CurrentSchedules
        {
            get { return _currentSchedules; }
        }

        // Mutable property access

    }
    
    public partial class Room : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Room newFact = new Room(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._roomNumber = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Room fact = (Room)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._roomNumber);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Room", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private string _roomNumber;

        // Results

        // Business constructor
        public Room(
            Conference conference
            ,string roomNumber
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _roomNumber = roomNumber;
        }

        // Hydration constructor
        private Room(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access
        public string RoomNumber
        {
            get { return _roomNumber; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Track : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Track newFact = new Track(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._name = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Track fact = (Track)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._name);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Track", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));

        // Queries
        public static Query QueryCurrentSessionPlaces = new Query()
            .JoinSuccessors(Session.RoleTrack)
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private string _name;

        // Results
        private Result<SessionPlace> _currentSessionPlaces;

        // Business constructor
        public Track(
            Conference conference
            ,string name
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _name = name;
        }

        // Hydration constructor
        private Track(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _currentSessionPlaces = new Result<SessionPlace>(this, QueryCurrentSessionPlaces);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access
        public IEnumerable<SessionPlace> CurrentSessionPlaces
        {
            get { return _currentSessionPlaces; }
        }

        // Mutable property access

    }
    
    public partial class Speaker : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Speaker newFact = new Speaker(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._name = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Speaker fact = (Speaker)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._name);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));

        // Queries
        public static Query QueryImageUrl = new Query()
            .JoinSuccessors(SpeakerImageUrl.RoleSpeaker, Condition.WhereIsEmpty(SpeakerImageUrl.QueryIsCurrent)
            )
            ;
        public static Query QueryContact = new Query()
            .JoinSuccessors(SpeakerContact.RoleSpeaker, Condition.WhereIsEmpty(SpeakerContact.QueryIsCurrent)
            )
            ;
        public static Query QueryBio = new Query()
            .JoinSuccessors(SpeakerBio.RoleSpeaker, Condition.WhereIsEmpty(SpeakerBio.QueryIsCurrent)
            )
            ;
        public static Query QueryAvailableSessions = new Query()
            .JoinSuccessors(Session.RoleSpeaker)
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private string _name;

        // Results
        private Result<SpeakerImageUrl> _imageUrl;
        private Result<SpeakerContact> _contact;
        private Result<SpeakerBio> _bio;
        private Result<SessionPlace> _availableSessions;

        // Business constructor
        public Speaker(
            Conference conference
            ,string name
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _name = name;
        }

        // Hydration constructor
        private Speaker(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _imageUrl = new Result<SpeakerImageUrl>(this, QueryImageUrl);
            _contact = new Result<SpeakerContact>(this, QueryContact);
            _bio = new Result<SpeakerBio>(this, QueryBio);
            _availableSessions = new Result<SessionPlace>(this, QueryAvailableSessions);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access
        public IEnumerable<SessionPlace> AvailableSessions
        {
            get { return _availableSessions; }
        }

        // Mutable property access
        public Disputable<string> ImageUrl
        {
            get { return _imageUrl.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SpeakerImageUrl(this, _imageUrl, value.Value));
			}
        }
        public Disputable<string> Contact
        {
            get { return _contact.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SpeakerContact(this, _contact, value.Value));
			}
        }

        public Disputable<IEnumerable<DocumentSegment>> Bio
        {
            get { return _bio.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SpeakerBio(this, _bio, value.Value));
			}
        }
    }
    
    public partial class SpeakerImageUrl : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SpeakerImageUrl newFact = new SpeakerImageUrl(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SpeakerImageUrl fact = (SpeakerImageUrl)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SpeakerImageUrl", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSpeaker = new Role(new RoleMemento(
			_correspondenceFactType,
			"speaker",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SpeakerImageUrl", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SpeakerImageUrl.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<SpeakerImageUrl> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public SpeakerImageUrl(
            Speaker speaker
            ,IEnumerable<SpeakerImageUrl> prior
            ,string value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<SpeakerImageUrl>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SpeakerImageUrl(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<SpeakerImageUrl>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return _speaker.Fact; }
        }
        public IEnumerable<SpeakerImageUrl> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class SpeakerContact : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SpeakerContact newFact = new SpeakerContact(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SpeakerContact fact = (SpeakerContact)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SpeakerContact", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSpeaker = new Role(new RoleMemento(
			_correspondenceFactType,
			"speaker",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SpeakerContact", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SpeakerContact.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<SpeakerContact> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public SpeakerContact(
            Speaker speaker
            ,IEnumerable<SpeakerContact> prior
            ,string value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<SpeakerContact>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SpeakerContact(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<SpeakerContact>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return _speaker.Fact; }
        }
        public IEnumerable<SpeakerContact> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class SpeakerBio : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SpeakerBio newFact = new SpeakerBio(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SpeakerBio fact = (SpeakerBio)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SpeakerBio", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSpeaker = new Role(new RoleMemento(
			_correspondenceFactType,
			"speaker",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SpeakerBio", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.DocumentSegment", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SpeakerBio.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<SpeakerBio> _prior;
        private PredecessorList<DocumentSegment> _value;

        // Fields

        // Results

        // Business constructor
        public SpeakerBio(
            Speaker speaker
            ,IEnumerable<SpeakerBio> prior
            ,IEnumerable<DocumentSegment> value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<SpeakerBio>(this, RolePrior, prior);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, value);
        }

        // Hydration constructor
        private SpeakerBio(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<SpeakerBio>(this, RolePrior, memento);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return _speaker.Fact; }
        }
        public IEnumerable<SpeakerBio> Prior
        {
            get { return _prior; }
        }
             public IEnumerable<DocumentSegment> Value
        {
            get { return _value; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Place : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Place newFact = new Place(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Place fact = (Place)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Place", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RolePlaceTime = new Role(new RoleMemento(
			_correspondenceFactType,
			"placeTime",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Time", 1),
			false));
        public static Role RoleRoom = new Role(new RoleMemento(
			_correspondenceFactType,
			"room",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Room", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Time> _placeTime;
        private PredecessorObj<Room> _room;

        // Fields

        // Results

        // Business constructor
        public Place(
            Time placeTime
            ,Room room
            )
        {
            InitializeResults();
            _placeTime = new PredecessorObj<Time>(this, RolePlaceTime, placeTime);
            _room = new PredecessorObj<Room>(this, RoleRoom, room);
        }

        // Hydration constructor
        private Place(FactMemento memento)
        {
            InitializeResults();
            _placeTime = new PredecessorObj<Time>(this, RolePlaceTime, memento);
            _room = new PredecessorObj<Room>(this, RoleRoom, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Time PlaceTime
        {
            get { return _placeTime.Fact; }
        }
        public Room Room
        {
            get { return _room.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Level : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Level newFact = new Level(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._name = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Level fact = (Level)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._name);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Level", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _name;

        // Results

        // Business constructor
        public Level(
            string name
            )
        {
            InitializeResults();
            _name = name;
        }

        // Hydration constructor
        private Level(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Name
        {
            get { return _name; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Session : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Session newFact = new Session(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._id = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Session fact = (Session)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._id);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			false));
        public static Role RoleSpeaker = new Role(new RoleMemento(
			_correspondenceFactType,
			"speaker",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker", 1),
			false));
        public static Role RoleTrack = new Role(new RoleMemento(
			_correspondenceFactType,
			"track",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Track", 1),
			false));

        // Queries
        public static Query QueryName = new Query()
            .JoinSuccessors(SessionName.RoleSession, Condition.WhereIsEmpty(SessionName.QueryIsCurrent)
            )
            ;
        public static Query QueryDescription = new Query()
            .JoinSuccessors(SessionDescription.RoleSession, Condition.WhereIsEmpty(SessionDescription.QueryIsCurrent)
            )
            ;
        public static Query QueryLevel = new Query()
            .JoinSuccessors(SessionLevel.RoleSession, Condition.WhereIsEmpty(SessionLevel.QueryIsCurrent)
            )
            ;
        public static Query QueryCurrentSessionPlaces = new Query()
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorObj<Speaker> _speaker;
        private PredecessorOpt<Track> _track;

        // Fields
        private string _id;

        // Results
        private Result<SessionName> _name;
        private Result<SessionDescription> _description;
        private Result<SessionLevel> _level;
        private Result<SessionPlace> _currentSessionPlaces;

        // Business constructor
        public Session(
            Conference conference
            ,Speaker speaker
            ,Track track
            ,string id
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _track = new PredecessorOpt<Track>(this, RoleTrack, track);
            _id = id;
        }

        // Hydration constructor
        private Session(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _track = new PredecessorOpt<Track>(this, RoleTrack, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<SessionName>(this, QueryName);
            _description = new Result<SessionDescription>(this, QueryDescription);
            _level = new Result<SessionLevel>(this, QueryLevel);
            _currentSessionPlaces = new Result<SessionPlace>(this, QueryCurrentSessionPlaces);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }
        public Speaker Speaker
        {
            get { return _speaker.Fact; }
        }
        public Track Track
        {
            get { return _track.Fact; }
        }

        // Field access
        public string Id
        {
            get { return _id; }
        }

        // Query result access
        public IEnumerable<SessionPlace> CurrentSessionPlaces
        {
            get { return _currentSessionPlaces; }
        }

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionName(this, _name, value.Value));
			}
        }

        public Disputable<IEnumerable<DocumentSegment>> Description
        {
            get { return _description.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionDescription(this, _description, value.Value));
			}
        }
        public Disputable<Level> Level
        {
            get { return _level.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionLevel(this, _level, value.Value));
			}
        }
    }
    
    public partial class SessionName : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionName newFact = new SessionName(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionName fact = (SessionName)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionName", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSession = new Role(new RoleMemento(
			_correspondenceFactType,
			"session",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionName", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionName.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<SessionName> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public SessionName(
            Session session
            ,IEnumerable<SessionName> prior
            ,string value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<SessionName>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SessionName(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<SessionName>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return _session.Fact; }
        }
        public IEnumerable<SessionName> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionDescription : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionDescription newFact = new SessionDescription(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionDescription fact = (SessionDescription)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionDescription", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSession = new Role(new RoleMemento(
			_correspondenceFactType,
			"session",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionDescription", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.DocumentSegment", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionDescription.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<SessionDescription> _prior;
        private PredecessorList<DocumentSegment> _value;

        // Fields

        // Results

        // Business constructor
        public SessionDescription(
            Session session
            ,IEnumerable<SessionDescription> prior
            ,IEnumerable<DocumentSegment> value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<SessionDescription>(this, RolePrior, prior);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, value);
        }

        // Hydration constructor
        private SessionDescription(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<SessionDescription>(this, RolePrior, memento);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return _session.Fact; }
        }
        public IEnumerable<SessionDescription> Prior
        {
            get { return _prior; }
        }
             public IEnumerable<DocumentSegment> Value
        {
            get { return _value; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionLevel : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionLevel newFact = new SessionLevel(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionLevel fact = (SessionLevel)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionLevel", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSession = new Role(new RoleMemento(
			_correspondenceFactType,
			"session",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionLevel", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Level", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionLevel.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<SessionLevel> _prior;
        private PredecessorObj<Level> _value;

        // Fields

        // Results

        // Business constructor
        public SessionLevel(
            Session session
            ,IEnumerable<SessionLevel> prior
            ,Level value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<SessionLevel>(this, RolePrior, prior);
            _value = new PredecessorObj<Level>(this, RoleValue, value);
        }

        // Hydration constructor
        private SessionLevel(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<SessionLevel>(this, RolePrior, memento);
            _value = new PredecessorObj<Level>(this, RoleValue, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return _session.Fact; }
        }
        public IEnumerable<SessionLevel> Prior
        {
            get { return _prior; }
        }
             public Level Value
        {
            get { return _value.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionPlace : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionPlace newFact = new SessionPlace(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionPlace fact = (SessionPlace)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionPlace", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSession = new Role(new RoleMemento(
			_correspondenceFactType,
			"session",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			false));
        public static Role RolePlace = new Role(new RoleMemento(
			_correspondenceFactType,
			"place",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Place", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionPlace", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionPlace.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorObj<Place> _place;
        private PredecessorList<SessionPlace> _prior;

        // Fields

        // Results

        // Business constructor
        public SessionPlace(
            Session session
            ,Place place
            ,IEnumerable<SessionPlace> prior
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _place = new PredecessorObj<Place>(this, RolePlace, place);
            _prior = new PredecessorList<SessionPlace>(this, RolePrior, prior);
        }

        // Hydration constructor
        private SessionPlace(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _place = new PredecessorObj<Place>(this, RolePlace, memento);
            _prior = new PredecessorList<SessionPlace>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return _session.Fact; }
        }
        public Place Place
        {
            get { return _place.Fact; }
        }
        public IEnumerable<SessionPlace> Prior
        {
            get { return _prior; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Schedule : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Schedule newFact = new Schedule(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Schedule fact = (Schedule)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Schedule", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSlot = new Role(new RoleMemento(
			_correspondenceFactType,
			"slot",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Slot", 1),
			false));
        public static Role RoleSessionPlace = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionPlace",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionPlace", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ScheduleRemove.RoleSchedule)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Slot> _slot;
        private PredecessorObj<SessionPlace> _sessionPlace;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public Schedule(
            Slot slot
            ,SessionPlace sessionPlace
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _slot = new PredecessorObj<Slot>(this, RoleSlot, slot);
            _sessionPlace = new PredecessorObj<SessionPlace>(this, RoleSessionPlace, sessionPlace);
        }

        // Hydration constructor
        private Schedule(FactMemento memento)
        {
            InitializeResults();
            _slot = new PredecessorObj<Slot>(this, RoleSlot, memento);
            _sessionPlace = new PredecessorObj<SessionPlace>(this, RoleSessionPlace, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Slot Slot
        {
            get { return _slot.Fact; }
        }
        public SessionPlace SessionPlace
        {
            get { return _sessionPlace.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class ScheduleRemove : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ScheduleRemove newFact = new ScheduleRemove(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ScheduleRemove fact = (ScheduleRemove)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ScheduleRemove", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSchedule = new Role(new RoleMemento(
			_correspondenceFactType,
			"schedule",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Schedule", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Schedule> _schedule;

        // Fields

        // Results

        // Business constructor
        public ScheduleRemove(
            Schedule schedule
            )
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, RoleSchedule, schedule);
        }

        // Hydration constructor
        private ScheduleRemove(FactMemento memento)
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, RoleSchedule, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Schedule Schedule
        {
            get { return _schedule.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class RatingQuestion : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				RatingQuestion newFact = new RatingQuestion(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._text = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				RatingQuestion fact = (RatingQuestion)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._text);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.RatingQuestion", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _text;

        // Results

        // Business constructor
        public RatingQuestion(
            string text
            )
        {
            InitializeResults();
            _text = text;
        }

        // Hydration constructor
        private RatingQuestion(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Text
        {
            get { return _text; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class EssayQuestion : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				EssayQuestion newFact = new EssayQuestion(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._text = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				EssayQuestion fact = (EssayQuestion)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._text);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.EssayQuestion", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _text;

        // Results

        // Business constructor
        public EssayQuestion(
            string text
            )
        {
            InitializeResults();
            _text = text;
        }

        // Hydration constructor
        private EssayQuestion(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Text
        {
            get { return _text; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Survey : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Survey newFact = new Survey(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Survey fact = (Survey)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Survey", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleRatingQuestions = new Role(new RoleMemento(
			_correspondenceFactType,
			"ratingQuestions",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.RatingQuestion", 1),
			false));
        public static Role RoleEssayQuestions = new Role(new RoleMemento(
			_correspondenceFactType,
			"essayQuestions",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.EssayQuestion", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorList<RatingQuestion> _ratingQuestions;
        private PredecessorList<EssayQuestion> _essayQuestions;

        // Fields

        // Results

        // Business constructor
        public Survey(
            IEnumerable<RatingQuestion> ratingQuestions
            ,IEnumerable<EssayQuestion> essayQuestions
            )
        {
            InitializeResults();
            _ratingQuestions = new PredecessorList<RatingQuestion>(this, RoleRatingQuestions, ratingQuestions);
            _essayQuestions = new PredecessorList<EssayQuestion>(this, RoleEssayQuestions, essayQuestions);
        }

        // Hydration constructor
        private Survey(FactMemento memento)
        {
            InitializeResults();
            _ratingQuestions = new PredecessorList<RatingQuestion>(this, RoleRatingQuestions, memento);
            _essayQuestions = new PredecessorList<EssayQuestion>(this, RoleEssayQuestions, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public IEnumerable<RatingQuestion> RatingQuestions
        {
            get { return _ratingQuestions; }
        }
             public IEnumerable<EssayQuestion> EssayQuestions
        {
            get { return _essayQuestions; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionEvaluation : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionEvaluation newFact = new SessionEvaluation(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionEvaluation fact = (SessionEvaluation)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluation", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSchedule = new Role(new RoleMemento(
			_correspondenceFactType,
			"schedule",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Schedule", 1),
			false));
        public static Role RoleSurvey = new Role(new RoleMemento(
			_correspondenceFactType,
			"survey",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Survey", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Schedule> _schedule;
        private PredecessorObj<Survey> _survey;

        // Fields

        // Results

        // Business constructor
        public SessionEvaluation(
            Schedule schedule
            ,Survey survey
            )
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, RoleSchedule, schedule);
            _survey = new PredecessorObj<Survey>(this, RoleSurvey, survey);
        }

        // Hydration constructor
        private SessionEvaluation(FactMemento memento)
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, RoleSchedule, memento);
            _survey = new PredecessorObj<Survey>(this, RoleSurvey, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Schedule Schedule
        {
            get { return _schedule.Fact; }
        }
        public Survey Survey
        {
            get { return _survey.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionEvaluationRating : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionEvaluationRating newFact = new SessionEvaluationRating(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionEvaluationRating fact = (SessionEvaluationRating)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluationRating", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleEvaluation = new Role(new RoleMemento(
			_correspondenceFactType,
			"evaluation",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluation", 1),
			false));
        public static Role RoleQuestion = new Role(new RoleMemento(
			_correspondenceFactType,
			"question",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.RatingQuestion", 1),
			false));

        // Queries
        public static Query QueryAnswer = new Query()
            .JoinSuccessors(SessionEvaluationRatingAnswer.RoleSessionEvaluationRating, Condition.WhereIsEmpty(SessionEvaluationRatingAnswer.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<SessionEvaluation> _evaluation;
        private PredecessorObj<RatingQuestion> _question;

        // Fields

        // Results
        private Result<SessionEvaluationRatingAnswer> _answer;

        // Business constructor
        public SessionEvaluationRating(
            SessionEvaluation evaluation
            ,RatingQuestion question
            )
        {
            InitializeResults();
            _evaluation = new PredecessorObj<SessionEvaluation>(this, RoleEvaluation, evaluation);
            _question = new PredecessorObj<RatingQuestion>(this, RoleQuestion, question);
        }

        // Hydration constructor
        private SessionEvaluationRating(FactMemento memento)
        {
            InitializeResults();
            _evaluation = new PredecessorObj<SessionEvaluation>(this, RoleEvaluation, memento);
            _question = new PredecessorObj<RatingQuestion>(this, RoleQuestion, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _answer = new Result<SessionEvaluationRatingAnswer>(this, QueryAnswer);
        }

        // Predecessor access
        public SessionEvaluation Evaluation
        {
            get { return _evaluation.Fact; }
        }
        public RatingQuestion Question
        {
            get { return _question.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access
        public Disputable<int> Answer
        {
            get { return _answer.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionEvaluationRatingAnswer(this, _answer, value.Value));
			}
        }

    }
    
    public partial class SessionEvaluationRatingAnswer : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionEvaluationRatingAnswer newFact = new SessionEvaluationRatingAnswer(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionEvaluationRatingAnswer fact = (SessionEvaluationRatingAnswer)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluationRatingAnswer", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSessionEvaluationRating = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionEvaluationRating",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationRating", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationRatingAnswer", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionEvaluationRatingAnswer.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<SessionEvaluationRating> _sessionEvaluationRating;
        private PredecessorList<SessionEvaluationRatingAnswer> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public SessionEvaluationRatingAnswer(
            SessionEvaluationRating sessionEvaluationRating
            ,IEnumerable<SessionEvaluationRatingAnswer> prior
            ,int value
            )
        {
            InitializeResults();
            _sessionEvaluationRating = new PredecessorObj<SessionEvaluationRating>(this, RoleSessionEvaluationRating, sessionEvaluationRating);
            _prior = new PredecessorList<SessionEvaluationRatingAnswer>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SessionEvaluationRatingAnswer(FactMemento memento)
        {
            InitializeResults();
            _sessionEvaluationRating = new PredecessorObj<SessionEvaluationRating>(this, RoleSessionEvaluationRating, memento);
            _prior = new PredecessorList<SessionEvaluationRatingAnswer>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public SessionEvaluationRating SessionEvaluationRating
        {
            get { return _sessionEvaluationRating.Fact; }
        }
        public IEnumerable<SessionEvaluationRatingAnswer> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public int Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionEvaluationEssay : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionEvaluationEssay newFact = new SessionEvaluationEssay(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionEvaluationEssay fact = (SessionEvaluationEssay)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluationEssay", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleEvaluation = new Role(new RoleMemento(
			_correspondenceFactType,
			"evaluation",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluation", 1),
			false));
        public static Role RoleQuestion = new Role(new RoleMemento(
			_correspondenceFactType,
			"question",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.EssayQuestion", 1),
			false));

        // Queries
        public static Query QueryAnswer = new Query()
            .JoinSuccessors(SessionEvaluationEssayAnswer.RoleSessionEvaluationEssay, Condition.WhereIsEmpty(SessionEvaluationEssayAnswer.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<SessionEvaluation> _evaluation;
        private PredecessorObj<EssayQuestion> _question;

        // Fields

        // Results
        private Result<SessionEvaluationEssayAnswer> _answer;

        // Business constructor
        public SessionEvaluationEssay(
            SessionEvaluation evaluation
            ,EssayQuestion question
            )
        {
            InitializeResults();
            _evaluation = new PredecessorObj<SessionEvaluation>(this, RoleEvaluation, evaluation);
            _question = new PredecessorObj<EssayQuestion>(this, RoleQuestion, question);
        }

        // Hydration constructor
        private SessionEvaluationEssay(FactMemento memento)
        {
            InitializeResults();
            _evaluation = new PredecessorObj<SessionEvaluation>(this, RoleEvaluation, memento);
            _question = new PredecessorObj<EssayQuestion>(this, RoleQuestion, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _answer = new Result<SessionEvaluationEssayAnswer>(this, QueryAnswer);
        }

        // Predecessor access
        public SessionEvaluation Evaluation
        {
            get { return _evaluation.Fact; }
        }
        public EssayQuestion Question
        {
            get { return _question.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access
        public Disputable<string> Answer
        {
            get { return _answer.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionEvaluationEssayAnswer(this, _answer, value.Value));
			}
        }

    }
    
    public partial class SessionEvaluationEssayAnswer : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				SessionEvaluationEssayAnswer newFact = new SessionEvaluationEssayAnswer(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionEvaluationEssayAnswer fact = (SessionEvaluationEssayAnswer)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluationEssayAnswer", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSessionEvaluationEssay = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionEvaluationEssay",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationEssay", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationEssayAnswer", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(SessionEvaluationEssayAnswer.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<SessionEvaluationEssay> _sessionEvaluationEssay;
        private PredecessorList<SessionEvaluationEssayAnswer> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public SessionEvaluationEssayAnswer(
            SessionEvaluationEssay sessionEvaluationEssay
            ,IEnumerable<SessionEvaluationEssayAnswer> prior
            ,string value
            )
        {
            InitializeResults();
            _sessionEvaluationEssay = new PredecessorObj<SessionEvaluationEssay>(this, RoleSessionEvaluationEssay, sessionEvaluationEssay);
            _prior = new PredecessorList<SessionEvaluationEssayAnswer>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SessionEvaluationEssayAnswer(FactMemento memento)
        {
            InitializeResults();
            _sessionEvaluationEssay = new PredecessorObj<SessionEvaluationEssay>(this, RoleSessionEvaluationEssay, memento);
            _prior = new PredecessorList<SessionEvaluationEssayAnswer>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public SessionEvaluationEssay SessionEvaluationEssay
        {
            get { return _sessionEvaluationEssay.Fact; }
        }
        public IEnumerable<SessionEvaluationEssayAnswer> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class DocumentSegment : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				DocumentSegment newFact = new DocumentSegment(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._text = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				DocumentSegment fact = (DocumentSegment)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._text);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.DocumentSegment", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _text;

        // Results

        // Business constructor
        public DocumentSegment(
            string text
            )
        {
            InitializeResults();
            _text = text;
        }

        // Hydration constructor
        private DocumentSegment(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Text
        {
            get { return _text; }
        }

        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Identity._correspondenceFactType,
				new Identity.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Identity._correspondenceFactType }));
			community.AddQuery(
				Identity._correspondenceFactType,
				Identity.QueryIsToastNotificationDisabled.QueryDefinition);
			community.AddType(
				DisableToastNotification._correspondenceFactType,
				new DisableToastNotification.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { DisableToastNotification._correspondenceFactType }));
			community.AddQuery(
				DisableToastNotification._correspondenceFactType,
				DisableToastNotification.QueryIsReenabled.QueryDefinition);
			community.AddType(
				EnableToastNotification._correspondenceFactType,
				new EnableToastNotification.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { EnableToastNotification._correspondenceFactType }));
			community.AddType(
				Conference._correspondenceFactType,
				new Conference.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference._correspondenceFactType }));
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryName.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySessionSurvey.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryConferenceSurvey.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryDays.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryTracks.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySessions.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySpeakers.QueryDefinition);
			community.AddType(
				ConferenceName._correspondenceFactType,
				new ConferenceName.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceName._correspondenceFactType }));
			community.AddQuery(
				ConferenceName._correspondenceFactType,
				ConferenceName.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ConferenceSessionSurvey._correspondenceFactType,
				new ConferenceSessionSurvey.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceSessionSurvey._correspondenceFactType }));
			community.AddQuery(
				ConferenceSessionSurvey._correspondenceFactType,
				ConferenceSessionSurvey.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ConferenceConferenceSurvey._correspondenceFactType,
				new ConferenceConferenceSurvey.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceConferenceSurvey._correspondenceFactType }));
			community.AddQuery(
				ConferenceConferenceSurvey._correspondenceFactType,
				ConferenceConferenceSurvey.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Attendee._correspondenceFactType,
				new Attendee.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Attendee._correspondenceFactType }));
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.QueryCurrentSchedules.QueryDefinition);
			community.AddType(
				Day._correspondenceFactType,
				new Day.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Day._correspondenceFactType }));
			community.AddQuery(
				Day._correspondenceFactType,
				Day.QueryTimes.QueryDefinition);
			community.AddType(
				Time._correspondenceFactType,
				new Time.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Time._correspondenceFactType }));
			community.AddQuery(
				Time._correspondenceFactType,
				Time.QueryAvailableSessions.QueryDefinition);
			community.AddType(
				Slot._correspondenceFactType,
				new Slot.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Slot._correspondenceFactType }));
			community.AddQuery(
				Slot._correspondenceFactType,
				Slot.QueryCurrentSchedules.QueryDefinition);
			community.AddType(
				Room._correspondenceFactType,
				new Room.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room._correspondenceFactType }));
			community.AddType(
				Track._correspondenceFactType,
				new Track.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Track._correspondenceFactType }));
			community.AddQuery(
				Track._correspondenceFactType,
				Track.QueryCurrentSessionPlaces.QueryDefinition);
			community.AddType(
				Speaker._correspondenceFactType,
				new Speaker.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker._correspondenceFactType }));
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.QueryImageUrl.QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.QueryContact.QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.QueryBio.QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.QueryAvailableSessions.QueryDefinition);
			community.AddType(
				SpeakerImageUrl._correspondenceFactType,
				new SpeakerImageUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SpeakerImageUrl._correspondenceFactType }));
			community.AddQuery(
				SpeakerImageUrl._correspondenceFactType,
				SpeakerImageUrl.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SpeakerContact._correspondenceFactType,
				new SpeakerContact.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SpeakerContact._correspondenceFactType }));
			community.AddQuery(
				SpeakerContact._correspondenceFactType,
				SpeakerContact.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SpeakerBio._correspondenceFactType,
				new SpeakerBio.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SpeakerBio._correspondenceFactType }));
			community.AddQuery(
				SpeakerBio._correspondenceFactType,
				SpeakerBio.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Place._correspondenceFactType,
				new Place.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Place._correspondenceFactType }));
			community.AddType(
				Level._correspondenceFactType,
				new Level.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Level._correspondenceFactType }));
			community.AddType(
				Session._correspondenceFactType,
				new Session.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session._correspondenceFactType }));
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryName.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryDescription.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryLevel.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryCurrentSessionPlaces.QueryDefinition);
			community.AddType(
				SessionName._correspondenceFactType,
				new SessionName.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionName._correspondenceFactType }));
			community.AddQuery(
				SessionName._correspondenceFactType,
				SessionName.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SessionDescription._correspondenceFactType,
				new SessionDescription.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionDescription._correspondenceFactType }));
			community.AddQuery(
				SessionDescription._correspondenceFactType,
				SessionDescription.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SessionLevel._correspondenceFactType,
				new SessionLevel.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionLevel._correspondenceFactType }));
			community.AddQuery(
				SessionLevel._correspondenceFactType,
				SessionLevel.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SessionPlace._correspondenceFactType,
				new SessionPlace.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionPlace._correspondenceFactType }));
			community.AddQuery(
				SessionPlace._correspondenceFactType,
				SessionPlace.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Schedule._correspondenceFactType,
				new Schedule.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Schedule._correspondenceFactType }));
			community.AddQuery(
				Schedule._correspondenceFactType,
				Schedule.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ScheduleRemove._correspondenceFactType,
				new ScheduleRemove.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ScheduleRemove._correspondenceFactType }));
			community.AddType(
				RatingQuestion._correspondenceFactType,
				new RatingQuestion.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { RatingQuestion._correspondenceFactType }));
			community.AddType(
				EssayQuestion._correspondenceFactType,
				new EssayQuestion.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { EssayQuestion._correspondenceFactType }));
			community.AddType(
				Survey._correspondenceFactType,
				new Survey.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Survey._correspondenceFactType }));
			community.AddType(
				SessionEvaluation._correspondenceFactType,
				new SessionEvaluation.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluation._correspondenceFactType }));
			community.AddType(
				SessionEvaluationRating._correspondenceFactType,
				new SessionEvaluationRating.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationRating._correspondenceFactType }));
			community.AddQuery(
				SessionEvaluationRating._correspondenceFactType,
				SessionEvaluationRating.QueryAnswer.QueryDefinition);
			community.AddType(
				SessionEvaluationRatingAnswer._correspondenceFactType,
				new SessionEvaluationRatingAnswer.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationRatingAnswer._correspondenceFactType }));
			community.AddQuery(
				SessionEvaluationRatingAnswer._correspondenceFactType,
				SessionEvaluationRatingAnswer.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SessionEvaluationEssay._correspondenceFactType,
				new SessionEvaluationEssay.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationEssay._correspondenceFactType }));
			community.AddQuery(
				SessionEvaluationEssay._correspondenceFactType,
				SessionEvaluationEssay.QueryAnswer.QueryDefinition);
			community.AddType(
				SessionEvaluationEssayAnswer._correspondenceFactType,
				new SessionEvaluationEssayAnswer.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationEssayAnswer._correspondenceFactType }));
			community.AddQuery(
				SessionEvaluationEssayAnswer._correspondenceFactType,
				SessionEvaluationEssayAnswer.QueryIsCurrent.QueryDefinition);
			community.AddType(
				DocumentSegment._correspondenceFactType,
				new DocumentSegment.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { DocumentSegment._correspondenceFactType }));
		}
	}
}
