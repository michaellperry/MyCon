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
    EnableToastNotification -> Identity
    DisableToastNotification -> EnableToastNotification [label="  *"]
    Conference__name -> Conference [color="red"]
    Conference__name -> Conference__name [label="  *"]
    Conference__conferenceSurvey -> Conference [color="red"]
    Conference__conferenceSurvey -> Conference__conferenceSurvey [label="  *"]
    Conference__conferenceSurvey -> Survey
    Conference__mapUrl -> Conference [color="red"]
    Conference__mapUrl -> Conference__mapUrl [label="  *"]
    ConferenceSessionSurvey -> Conference [color="red"]
    ConferenceSessionSurvey -> Survey
    ConferenceSessionSurvey -> ConferenceSessionSurvey [label="  *"]
    Attendee -> Identity
    Attendee -> Conference
    Day -> Conference [color="red"]
    Time -> Day
    TimeDelete -> Time
    TimeUndelete -> TimeDelete
    Slot -> Attendee
    Slot -> Time [color="red"]
    Room -> Conference
    Room__roomNumber -> Room
    Room__roomNumber -> Room__roomNumber [label="  *"]
    Track -> Conference [color="red"]
    Speaker -> Conference [color="red"]
    Speaker__imageUrl -> Speaker
    Speaker__imageUrl -> Speaker__imageUrl [label="  *"]
    Speaker__contact -> Speaker
    Speaker__contact -> Speaker__contact [label="  *"]
    Speaker__bio -> Speaker
    Speaker__bio -> Speaker__bio [label="  *"]
    Speaker__bio -> DocumentSegment [label="  *"]
    ConferenceNotice -> Conference [color="red"]
    Place -> Time
    Place -> Room
    Session -> Conference [color="red"]
    Session -> Speaker
    Session -> Track [label="  ?"]
    Session__name -> Session
    Session__name -> Session__name [label="  *"]
    Session__description -> Session
    Session__description -> Session__description [label="  *"]
    Session__description -> DocumentSegment [label="  *"]
    Session__level -> Session
    Session__level -> Session__level [label="  *"]
    Session__level -> Level
    SessionDelete -> Session
    SessionUndelete -> SessionDelete
    SessionNotice -> Session [color="red"]
    SessionPlace -> Session
    SessionPlace -> Place
    SessionPlace -> SessionPlace [label="  *"]
    Schedule -> Slot
    Schedule -> SessionPlace [color="red"]
    ScheduleRemove -> Schedule
    Survey -> RatingQuestion [label="  *"]
    Survey -> EssayQuestion [label="  *"]
    SessionEvaluation -> Schedule
    SessionEvaluation -> Survey
    SessionEvaluationCompleted -> ConferenceSessionSurvey [color="red"]
    SessionEvaluationCompleted -> SessionEvaluation
    SessionEvaluationCompleted -> SessionEvaluationRatingAnswer [label="  *"]
    SessionEvaluationCompleted -> SessionEvaluationEssayAnswer [label="  *"]
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
        public static Query QueryIsToastNotificationEnabled = new Query()
            .JoinSuccessors(EnableToastNotification.RoleIdentity, Condition.WhereIsEmpty(EnableToastNotification.QueryIsDisabled)
            )
            ;

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<EnableToastNotification> _isToastNotificationEnabled;

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
            _isToastNotificationEnabled = new Result<EnableToastNotification>(this, QueryIsToastNotificationEnabled);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public IEnumerable<EnableToastNotification> IsToastNotificationEnabled
        {
            get { return _isToastNotificationEnabled; }
        }

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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				EnableToastNotification fact = (EnableToastNotification)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
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
        public static Role RoleIdentity = new Role(new RoleMemento(
			_correspondenceFactType,
			"identity",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Identity", 1),
			false));

        // Queries
        public static Query QueryIsDisabled = new Query()
            .JoinSuccessors(DisableToastNotification.RoleEnable)
            ;

        // Predicates
        public static Condition IsDisabled = Condition.WhereIsNotEmpty(QueryIsDisabled);

        // Predecessors
        private PredecessorObj<Identity> _identity;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public EnableToastNotification(
            Identity identity
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _identity = new PredecessorObj<Identity>(this, RoleIdentity, identity);
        }

        // Hydration constructor
        private EnableToastNotification(FactMemento memento)
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
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				DisableToastNotification fact = (DisableToastNotification)obj;
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
        public static Role RoleEnable = new Role(new RoleMemento(
			_correspondenceFactType,
			"enable",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.EnableToastNotification", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorList<EnableToastNotification> _enable;

        // Fields

        // Results

        // Business constructor
        public DisableToastNotification(
            IEnumerable<EnableToastNotification> enable
            )
        {
            InitializeResults();
            _enable = new PredecessorList<EnableToastNotification>(this, RoleEnable, enable);
        }

        // Hydration constructor
        private DisableToastNotification(FactMemento memento)
        {
            InitializeResults();
            _enable = new PredecessorList<EnableToastNotification>(this, RoleEnable, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public IEnumerable<EnableToastNotification> Enable
        {
            get { return _enable; }
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
            .JoinSuccessors(Conference__name.RoleConference, Condition.WhereIsEmpty(Conference__name.QueryIsCurrent)
            )
            ;
        public static Query QueryConferenceSurvey = new Query()
            .JoinSuccessors(Conference__conferenceSurvey.RoleConference, Condition.WhereIsEmpty(Conference__conferenceSurvey.QueryIsCurrent)
            )
            ;
        public static Query QueryMapUrl = new Query()
            .JoinSuccessors(Conference__mapUrl.RoleConference, Condition.WhereIsEmpty(Conference__mapUrl.QueryIsCurrent)
            )
            ;
        public static Query QueryDays = new Query()
            .JoinSuccessors(Day.RoleConference, Condition.WhereIsNotEmpty(Day.QueryHasTimes)
            )
            ;
        public static Query QueryAllTracks = new Query()
            .JoinSuccessors(Track.RoleConference)
            ;
        public static Query QueryTracks = new Query()
            .JoinSuccessors(Track.RoleConference, Condition.WhereIsNotEmpty(Track.QueryHasSessions)
            )
            ;
        public static Query QuerySessions = new Query()
            .JoinSuccessors(Session.RoleConference, Condition.WhereIsEmpty(Session.QueryIsDeleted)
            )
            ;
        public static Query QueryUnscheduledSessions = new Query()
            .JoinSuccessors(Session.RoleConference, Condition.WhereIsEmpty(Session.QueryIsDeleted)
                .And().IsEmpty(Session.QueryIsScheduled)
            )
            ;
        public static Query QuerySpeakers = new Query()
            .JoinSuccessors(Speaker.RoleConference)
            ;
        public static Query QueryNotices = new Query()
            .JoinSuccessors(ConferenceNotice.RoleConference)
            ;
        public static Query QueryCurrentSessionSurveys = new Query()
            .JoinSuccessors(ConferenceSessionSurvey.RoleConference, Condition.WhereIsEmpty(ConferenceSessionSurvey.QueryIsCurrent)
            )
            ;
        public static Query QueryAllSessionSurveys = new Query()
            .JoinSuccessors(ConferenceSessionSurvey.RoleConference)
            ;
        public static Query QueryRooms = new Query()
            .JoinSuccessors(Room.RoleConference)
            ;

        // Predicates

        // Predecessors

        // Fields
        private string _id;

        // Results
        private Result<Conference__name> _name;
        private Result<Conference__conferenceSurvey> _conferenceSurvey;
        private Result<Conference__mapUrl> _mapUrl;
        private Result<Day> _days;
        private Result<Track> _allTracks;
        private Result<Track> _tracks;
        private Result<Session> _sessions;
        private Result<Session> _unscheduledSessions;
        private Result<Speaker> _speakers;
        private Result<ConferenceNotice> _notices;
        private Result<ConferenceSessionSurvey> _currentSessionSurveys;
        private Result<ConferenceSessionSurvey> _allSessionSurveys;
        private Result<Room> _rooms;

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
            _name = new Result<Conference__name>(this, QueryName);
            _conferenceSurvey = new Result<Conference__conferenceSurvey>(this, QueryConferenceSurvey);
            _mapUrl = new Result<Conference__mapUrl>(this, QueryMapUrl);
            _days = new Result<Day>(this, QueryDays);
            _allTracks = new Result<Track>(this, QueryAllTracks);
            _tracks = new Result<Track>(this, QueryTracks);
            _sessions = new Result<Session>(this, QuerySessions);
            _unscheduledSessions = new Result<Session>(this, QueryUnscheduledSessions);
            _speakers = new Result<Speaker>(this, QuerySpeakers);
            _notices = new Result<ConferenceNotice>(this, QueryNotices);
            _currentSessionSurveys = new Result<ConferenceSessionSurvey>(this, QueryCurrentSessionSurveys);
            _allSessionSurveys = new Result<ConferenceSessionSurvey>(this, QueryAllSessionSurveys);
            _rooms = new Result<Room>(this, QueryRooms);
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
        public IEnumerable<Track> AllTracks
        {
            get { return _allTracks; }
        }
        public IEnumerable<Track> Tracks
        {
            get { return _tracks; }
        }
        public IEnumerable<Session> Sessions
        {
            get { return _sessions; }
        }
        public IEnumerable<Session> UnscheduledSessions
        {
            get { return _unscheduledSessions; }
        }
        public IEnumerable<Speaker> Speakers
        {
            get { return _speakers; }
        }
        public IEnumerable<ConferenceNotice> Notices
        {
            get { return _notices; }
        }
        public IEnumerable<ConferenceSessionSurvey> CurrentSessionSurveys
        {
            get { return _currentSessionSurveys; }
        }
        public IEnumerable<ConferenceSessionSurvey> AllSessionSurveys
        {
            get { return _allSessionSurveys; }
        }
        public IEnumerable<Room> Rooms
        {
            get { return _rooms; }
        }

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_name.Count() != 1 || !object.Equals(_name.Single().Value, value.Value))
				{
					Community.AddFact(new Conference__name(this, _name, value.Value));
				}
			}
        }
        public Disputable<string> MapUrl
        {
            get { return _mapUrl.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_mapUrl.Count() != 1 || !object.Equals(_mapUrl.Single().Value, value.Value))
				{
					Community.AddFact(new Conference__mapUrl(this, _mapUrl, value.Value));
				}
			}
        }

        public Disputable<Survey> ConferenceSurvey
        {
            get { return _conferenceSurvey.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_conferenceSurvey.Count() != 1 || !object.Equals(_conferenceSurvey.Single().Value, value.Value))
				{
					Community.AddFact(new Conference__conferenceSurvey(this, _conferenceSurvey, value.Value));
				}
			}
        }
    }
    
    public partial class Conference__name : CorrespondenceFact
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
				Conference__name newFact = new Conference__name(memento);

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
				Conference__name fact = (Conference__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Conference__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			true));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference__name", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Conference__name.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<Conference__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Conference__name(
            Conference conference
            ,IEnumerable<Conference__name> prior
            ,string value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<Conference__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Conference__name(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<Conference__name>(this, RolePrior, memento);
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
        public IEnumerable<Conference__name> Prior
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
    
    public partial class Conference__conferenceSurvey : CorrespondenceFact
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
				Conference__conferenceSurvey newFact = new Conference__conferenceSurvey(memento);

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
				Conference__conferenceSurvey fact = (Conference__conferenceSurvey)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Conference__conferenceSurvey", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			true));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference__conferenceSurvey", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Survey", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Conference__conferenceSurvey.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<Conference__conferenceSurvey> _prior;
        private PredecessorObj<Survey> _value;

        // Fields

        // Results

        // Business constructor
        public Conference__conferenceSurvey(
            Conference conference
            ,IEnumerable<Conference__conferenceSurvey> prior
            ,Survey value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<Conference__conferenceSurvey>(this, RolePrior, prior);
            _value = new PredecessorObj<Survey>(this, RoleValue, value);
        }

        // Hydration constructor
        private Conference__conferenceSurvey(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<Conference__conferenceSurvey>(this, RolePrior, memento);
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
        public IEnumerable<Conference__conferenceSurvey> Prior
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
    
    public partial class Conference__mapUrl : CorrespondenceFact
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
				Conference__mapUrl newFact = new Conference__mapUrl(memento);

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
				Conference__mapUrl fact = (Conference__mapUrl)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Conference__mapUrl", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			true));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference__mapUrl", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Conference__mapUrl.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorList<Conference__mapUrl> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Conference__mapUrl(
            Conference conference
            ,IEnumerable<Conference__mapUrl> prior
            ,string value
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _prior = new PredecessorList<Conference__mapUrl>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Conference__mapUrl(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _prior = new PredecessorList<Conference__mapUrl>(this, RolePrior, memento);
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
        public IEnumerable<Conference__mapUrl> Prior
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
			true));
        public static Role RoleSessionSurvey = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionSurvey",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Survey", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.ConferenceSessionSurvey", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ConferenceSessionSurvey.RolePrior)
            ;
        public static Query QueryCompleted = new Query()
            .JoinSuccessors(SessionEvaluationCompleted.RoleSessionSurvey)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorObj<Survey> _sessionSurvey;
        private PredecessorList<ConferenceSessionSurvey> _prior;

        // Fields

        // Results
        private Result<SessionEvaluationCompleted> _completed;

        // Business constructor
        public ConferenceSessionSurvey(
            Conference conference
            ,Survey sessionSurvey
            ,IEnumerable<ConferenceSessionSurvey> prior
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _sessionSurvey = new PredecessorObj<Survey>(this, RoleSessionSurvey, sessionSurvey);
            _prior = new PredecessorList<ConferenceSessionSurvey>(this, RolePrior, prior);
        }

        // Hydration constructor
        private ConferenceSessionSurvey(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, memento);
            _sessionSurvey = new PredecessorObj<Survey>(this, RoleSessionSurvey, memento);
            _prior = new PredecessorList<ConferenceSessionSurvey>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _completed = new Result<SessionEvaluationCompleted>(this, QueryCompleted);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }
        public Survey SessionSurvey
        {
            get { return _sessionSurvey.Fact; }
        }
        public IEnumerable<ConferenceSessionSurvey> Prior
        {
            get { return _prior; }
        }
     
        // Field access

        // Query result access
        public IEnumerable<SessionEvaluationCompleted> Completed
        {
            get { return _completed; }
        }

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
        public static Query QueryAllSchedules = new Query()
            .JoinSuccessors(Slot.RoleAttendee)
            .JoinSuccessors(Schedule.RoleSlot)
            ;
        public static Query QueryScheduledSessions = new Query()
            .JoinSuccessors(Slot.RoleAttendee)
            .JoinSuccessors(Schedule.RoleSlot, Condition.WhereIsEmpty(Schedule.QueryIsCurrent)
            )
            .JoinPredecessors(Schedule.RoleSessionPlace)
            .JoinPredecessors(SessionPlace.RoleSession)
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Identity> _identity;
        private PredecessorObj<Conference> _conference;

        // Fields

        // Results
        private Result<Schedule> _currentSchedules;
        private Result<Schedule> _allSchedules;
        private Result<Session> _scheduledSessions;

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
            _allSchedules = new Result<Schedule>(this, QueryAllSchedules);
            _scheduledSessions = new Result<Session>(this, QueryScheduledSessions);
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
        public IEnumerable<Schedule> AllSchedules
        {
            get { return _allSchedules; }
        }
        public IEnumerable<Session> ScheduledSessions
        {
            get { return _scheduledSessions; }
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
			true));

        // Queries
        public static Query QueryTimes = new Query()
            .JoinSuccessors(Time.RoleDay, Condition.WhereIsEmpty(Time.QueryIsDeleted)
            )
            ;
        public static Query QueryHasTimes = new Query()
            .JoinSuccessors(Time.RoleDay, Condition.WhereIsEmpty(Time.QueryIsDeleted)
            )
            ;

        // Predicates
        public static Condition HasTimes = Condition.WhereIsNotEmpty(QueryHasTimes);

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
                .And().IsEmpty(SessionPlace.QueryIsDeleted)
            )
            ;
        public static Query QueryDeletes = new Query()
            .JoinSuccessors(TimeDelete.RoleDeleted, Condition.WhereIsEmpty(TimeDelete.QueryIsUndeleted)
            )
            ;
        public static Query QueryIsDeleted = new Query()
            .JoinSuccessors(TimeDelete.RoleDeleted, Condition.WhereIsEmpty(TimeDelete.QueryIsUndeleted)
            )
            ;

        // Predicates
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(QueryIsDeleted);

        // Predecessors
        private PredecessorObj<Day> _day;

        // Fields
        private DateTime _start;

        // Results
        private Result<SessionPlace> _availableSessions;
        private Result<TimeDelete> _deletes;

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
            _deletes = new Result<TimeDelete>(this, QueryDeletes);
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
        public IEnumerable<TimeDelete> Deletes
        {
            get { return _deletes; }
        }

        // Mutable property access

    }
    
    public partial class TimeDelete : CorrespondenceFact
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
				TimeDelete newFact = new TimeDelete(memento);

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
				TimeDelete fact = (TimeDelete)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.TimeDelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDeleted = new Role(new RoleMemento(
			_correspondenceFactType,
			"deleted",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Time", 1),
			false));

        // Queries
        public static Query QueryIsUndeleted = new Query()
            .JoinSuccessors(TimeUndelete.RoleUndeleted)
            ;

        // Predicates
        public static Condition IsUndeleted = Condition.WhereIsNotEmpty(QueryIsUndeleted);

        // Predecessors
        private PredecessorObj<Time> _deleted;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public TimeDelete(
            Time deleted
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _deleted = new PredecessorObj<Time>(this, RoleDeleted, deleted);
        }

        // Hydration constructor
        private TimeDelete(FactMemento memento)
        {
            InitializeResults();
            _deleted = new PredecessorObj<Time>(this, RoleDeleted, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Time Deleted
        {
            get { return _deleted.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class TimeUndelete : CorrespondenceFact
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
				TimeUndelete newFact = new TimeUndelete(memento);

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
				TimeUndelete fact = (TimeUndelete)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.TimeUndelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleUndeleted = new Role(new RoleMemento(
			_correspondenceFactType,
			"undeleted",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.TimeDelete", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<TimeDelete> _undeleted;

        // Fields

        // Results

        // Business constructor
        public TimeUndelete(
            TimeDelete undeleted
            )
        {
            InitializeResults();
            _undeleted = new PredecessorObj<TimeDelete>(this, RoleUndeleted, undeleted);
        }

        // Hydration constructor
        private TimeUndelete(FactMemento memento)
        {
            InitializeResults();
            _undeleted = new PredecessorObj<TimeDelete>(this, RoleUndeleted, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public TimeDelete Undeleted
        {
            get { return _undeleted.Fact; }
        }

        // Field access

        // Query result access

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
			true));

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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Room fact = (Room)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
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
        public static Query QueryRoomNumber = new Query()
            .JoinSuccessors(Room__roomNumber.RoleRoom, Condition.WhereIsEmpty(Room__roomNumber.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Room__roomNumber> _roomNumber;

        // Business constructor
        public Room(
            Conference conference
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
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
            _roomNumber = new Result<Room__roomNumber>(this, QueryRoomNumber);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public Disputable<string> RoomNumber
        {
            get { return _roomNumber.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_roomNumber.Count() != 1 || !object.Equals(_roomNumber.Single().Value, value.Value))
				{
					Community.AddFact(new Room__roomNumber(this, _roomNumber, value.Value));
				}
			}
        }

    }
    
    public partial class Room__roomNumber : CorrespondenceFact
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
				Room__roomNumber newFact = new Room__roomNumber(memento);

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
				Room__roomNumber fact = (Room__roomNumber)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Room__roomNumber", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleRoom = new Role(new RoleMemento(
			_correspondenceFactType,
			"room",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Room", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Room__roomNumber", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Room__roomNumber.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Room> _room;
        private PredecessorList<Room__roomNumber> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Room__roomNumber(
            Room room
            ,IEnumerable<Room__roomNumber> prior
            ,string value
            )
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, RoleRoom, room);
            _prior = new PredecessorList<Room__roomNumber>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Room__roomNumber(FactMemento memento)
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, RoleRoom, memento);
            _prior = new PredecessorList<Room__roomNumber>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Room Room
        {
            get { return _room.Fact; }
        }
        public IEnumerable<Room__roomNumber> Prior
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
			true));

        // Queries
        public static Query QueryCurrentSessionPlaces = new Query()
            .JoinSuccessors(Session.RoleTrack, Condition.WhereIsEmpty(Session.QueryIsDeleted)
            )
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;
        public static Query QueryHasSessions = new Query()
            .JoinSuccessors(Session.RoleTrack, Condition.WhereIsEmpty(Session.QueryIsDeleted)
            )
            ;

        // Predicates
        public static Condition HasSessions = Condition.WhereIsNotEmpty(QueryHasSessions);

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
			true));

        // Queries
        public static Query QueryImageUrl = new Query()
            .JoinSuccessors(Speaker__imageUrl.RoleSpeaker, Condition.WhereIsEmpty(Speaker__imageUrl.QueryIsCurrent)
            )
            ;
        public static Query QueryContact = new Query()
            .JoinSuccessors(Speaker__contact.RoleSpeaker, Condition.WhereIsEmpty(Speaker__contact.QueryIsCurrent)
            )
            ;
        public static Query QueryBio = new Query()
            .JoinSuccessors(Speaker__bio.RoleSpeaker, Condition.WhereIsEmpty(Speaker__bio.QueryIsCurrent)
            )
            ;
        public static Query QueryAvailableSessions = new Query()
            .JoinSuccessors(Session.RoleSpeaker, Condition.WhereIsEmpty(Session.QueryIsDeleted)
            )
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private string _name;

        // Results
        private Result<Speaker__imageUrl> _imageUrl;
        private Result<Speaker__contact> _contact;
        private Result<Speaker__bio> _bio;
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
            _imageUrl = new Result<Speaker__imageUrl>(this, QueryImageUrl);
            _contact = new Result<Speaker__contact>(this, QueryContact);
            _bio = new Result<Speaker__bio>(this, QueryBio);
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
				if (_imageUrl.Count() != 1 || !object.Equals(_imageUrl.Single().Value, value.Value))
				{
					Community.AddFact(new Speaker__imageUrl(this, _imageUrl, value.Value));
				}
			}
        }
        public Disputable<string> Contact
        {
            get { return _contact.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_contact.Count() != 1 || !object.Equals(_contact.Single().Value, value.Value))
				{
					Community.AddFact(new Speaker__contact(this, _contact, value.Value));
				}
			}
        }

        public Disputable<IEnumerable<DocumentSegment>> Bio
        {
            get { return _bio.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_bio.Count() != 1 || !object.Equals(_bio.Single().Value, value.Value))
				{
					Community.AddFact(new Speaker__bio(this, _bio, value.Value));
				}
			}
        }
    }
    
    public partial class Speaker__imageUrl : CorrespondenceFact
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
				Speaker__imageUrl newFact = new Speaker__imageUrl(memento);

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
				Speaker__imageUrl fact = (Speaker__imageUrl)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__imageUrl", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker__imageUrl", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Speaker__imageUrl.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<Speaker__imageUrl> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Speaker__imageUrl(
            Speaker speaker
            ,IEnumerable<Speaker__imageUrl> prior
            ,string value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<Speaker__imageUrl>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Speaker__imageUrl(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<Speaker__imageUrl>(this, RolePrior, memento);
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
        public IEnumerable<Speaker__imageUrl> Prior
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
    
    public partial class Speaker__contact : CorrespondenceFact
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
				Speaker__contact newFact = new Speaker__contact(memento);

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
				Speaker__contact fact = (Speaker__contact)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__contact", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker__contact", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Speaker__contact.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<Speaker__contact> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Speaker__contact(
            Speaker speaker
            ,IEnumerable<Speaker__contact> prior
            ,string value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<Speaker__contact>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Speaker__contact(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<Speaker__contact>(this, RolePrior, memento);
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
        public IEnumerable<Speaker__contact> Prior
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
    
    public partial class Speaker__bio : CorrespondenceFact
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
				Speaker__bio newFact = new Speaker__bio(memento);

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
				Speaker__bio fact = (Speaker__bio)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__bio", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Speaker__bio", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.DocumentSegment", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Speaker__bio.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<Speaker__bio> _prior;
        private PredecessorList<DocumentSegment> _value;

        // Fields

        // Results

        // Business constructor
        public Speaker__bio(
            Speaker speaker
            ,IEnumerable<Speaker__bio> prior
            ,IEnumerable<DocumentSegment> value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _prior = new PredecessorList<Speaker__bio>(this, RolePrior, prior);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, value);
        }

        // Hydration constructor
        private Speaker__bio(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, memento);
            _prior = new PredecessorList<Speaker__bio>(this, RolePrior, memento);
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
        public IEnumerable<Speaker__bio> Prior
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
    
    public partial class ConferenceNotice : CorrespondenceFact
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
				ConferenceNotice newFact = new ConferenceNotice(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._timeSent = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._text = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceNotice fact = (ConferenceNotice)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._timeSent);
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._text);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceNotice", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleConference = new Role(new RoleMemento(
			_correspondenceFactType,
			"conference",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Conference", 1),
			true));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private DateTime _timeSent;
        private string _text;

        // Results

        // Business constructor
        public ConferenceNotice(
            Conference conference
            ,DateTime timeSent
            ,string text
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _timeSent = timeSent;
            _text = text;
        }

        // Hydration constructor
        private ConferenceNotice(FactMemento memento)
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
        public DateTime TimeSent
        {
            get { return _timeSent; }
        }
        public string Text
        {
            get { return _text; }
        }

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
        public static Query QueryCurrentSessionPlaces = new Query()
            .JoinSuccessors(SessionPlace.RolePlace, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<Time> _placeTime;
        private PredecessorObj<Room> _room;

        // Fields

        // Results
        private Result<SessionPlace> _currentSessionPlaces;

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
            _currentSessionPlaces = new Result<SessionPlace>(this, QueryCurrentSessionPlaces);
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
        public IEnumerable<SessionPlace> CurrentSessionPlaces
        {
            get { return _currentSessionPlaces; }
        }

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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Session fact = (Session)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
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
			true));
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
            .JoinSuccessors(Session__name.RoleSession, Condition.WhereIsEmpty(Session__name.QueryIsCurrent)
            )
            ;
        public static Query QueryDescription = new Query()
            .JoinSuccessors(Session__description.RoleSession, Condition.WhereIsEmpty(Session__description.QueryIsCurrent)
            )
            ;
        public static Query QueryLevel = new Query()
            .JoinSuccessors(Session__level.RoleSession, Condition.WhereIsEmpty(Session__level.QueryIsCurrent)
            )
            ;
        public static Query QueryCurrentSessionPlaces = new Query()
            .JoinSuccessors(SessionPlace.RoleSession, Condition.WhereIsEmpty(SessionPlace.QueryIsCurrent)
            )
            ;
        public static Query QueryNotices = new Query()
            .JoinSuccessors(SessionNotice.RoleSession)
            ;
        public static Query QueryIsDeleted = new Query()
            .JoinSuccessors(SessionDelete.RoleDeleted, Condition.WhereIsEmpty(SessionDelete.QueryIsUndeleted)
            )
            ;
        public static Query QuerySessionDeletes = new Query()
            .JoinSuccessors(SessionDelete.RoleDeleted, Condition.WhereIsEmpty(SessionDelete.QueryIsUndeleted)
            )
            ;
        public static Query QueryIsScheduled = new Query()
            .JoinSuccessors(SessionPlace.RoleSession)
            ;

        // Predicates
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(QueryIsDeleted);
        public static Condition IsScheduled = Condition.WhereIsNotEmpty(QueryIsScheduled);

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorObj<Speaker> _speaker;
        private PredecessorOpt<Track> _track;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Session__name> _name;
        private Result<Session__description> _description;
        private Result<Session__level> _level;
        private Result<SessionPlace> _currentSessionPlaces;
        private Result<SessionNotice> _notices;
        private Result<SessionDelete> _sessionDeletes;

        // Business constructor
        public Session(
            Conference conference
            ,Speaker speaker
            ,Track track
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, RoleConference, conference);
            _speaker = new PredecessorObj<Speaker>(this, RoleSpeaker, speaker);
            _track = new PredecessorOpt<Track>(this, RoleTrack, track);
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
            _name = new Result<Session__name>(this, QueryName);
            _description = new Result<Session__description>(this, QueryDescription);
            _level = new Result<Session__level>(this, QueryLevel);
            _currentSessionPlaces = new Result<SessionPlace>(this, QueryCurrentSessionPlaces);
            _notices = new Result<SessionNotice>(this, QueryNotices);
            _sessionDeletes = new Result<SessionDelete>(this, QuerySessionDeletes);
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
		public Guid Unique { get { return _unique; } }


        // Query result access
        public IEnumerable<SessionPlace> CurrentSessionPlaces
        {
            get { return _currentSessionPlaces; }
        }
        public IEnumerable<SessionNotice> Notices
        {
            get { return _notices; }
        }
        public IEnumerable<SessionDelete> SessionDeletes
        {
            get { return _sessionDeletes; }
        }

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_name.Count() != 1 || !object.Equals(_name.Single().Value, value.Value))
				{
					Community.AddFact(new Session__name(this, _name, value.Value));
				}
			}
        }

        public Disputable<IEnumerable<DocumentSegment>> Description
        {
            get { return _description.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_description.Count() != 1 || !object.Equals(_description.Single().Value, value.Value))
				{
					Community.AddFact(new Session__description(this, _description, value.Value));
				}
			}
        }
        public Disputable<Level> Level
        {
            get { return _level.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				if (_level.Count() != 1 || !object.Equals(_level.Single().Value, value.Value))
				{
					Community.AddFact(new Session__level(this, _level, value.Value));
				}
			}
        }
    }
    
    public partial class Session__name : CorrespondenceFact
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
				Session__name newFact = new Session__name(memento);

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
				Session__name fact = (Session__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session__name", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session__name", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Session__name.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<Session__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Session__name(
            Session session
            ,IEnumerable<Session__name> prior
            ,string value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<Session__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Session__name(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<Session__name>(this, RolePrior, memento);
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
        public IEnumerable<Session__name> Prior
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
    
    public partial class Session__description : CorrespondenceFact
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
				Session__description newFact = new Session__description(memento);

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
				Session__description fact = (Session__description)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session__description", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session__description", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.DocumentSegment", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Session__description.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<Session__description> _prior;
        private PredecessorList<DocumentSegment> _value;

        // Fields

        // Results

        // Business constructor
        public Session__description(
            Session session
            ,IEnumerable<Session__description> prior
            ,IEnumerable<DocumentSegment> value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<Session__description>(this, RolePrior, prior);
            _value = new PredecessorList<DocumentSegment>(this, RoleValue, value);
        }

        // Hydration constructor
        private Session__description(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<Session__description>(this, RolePrior, memento);
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
        public IEnumerable<Session__description> Prior
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
    
    public partial class Session__level : CorrespondenceFact
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
				Session__level newFact = new Session__level(memento);

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
				Session__level fact = (Session__level)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session__level", 1);

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
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session__level", 1),
			false));
        public static Role RoleValue = new Role(new RoleMemento(
			_correspondenceFactType,
			"value",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Level", 1),
			false));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(Session__level.RolePrior)
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<Session__level> _prior;
        private PredecessorObj<Level> _value;

        // Fields

        // Results

        // Business constructor
        public Session__level(
            Session session
            ,IEnumerable<Session__level> prior
            ,Level value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _prior = new PredecessorList<Session__level>(this, RolePrior, prior);
            _value = new PredecessorObj<Level>(this, RoleValue, value);
        }

        // Hydration constructor
        private Session__level(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
            _prior = new PredecessorList<Session__level>(this, RolePrior, memento);
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
        public IEnumerable<Session__level> Prior
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
    
    public partial class SessionDelete : CorrespondenceFact
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
				SessionDelete newFact = new SessionDelete(memento);

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
				SessionDelete fact = (SessionDelete)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionDelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDeleted = new Role(new RoleMemento(
			_correspondenceFactType,
			"deleted",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			false));

        // Queries
        public static Query QueryIsUndeleted = new Query()
            .JoinSuccessors(SessionUndelete.RoleUndeleted)
            ;

        // Predicates
        public static Condition IsUndeleted = Condition.WhereIsNotEmpty(QueryIsUndeleted);

        // Predecessors
        private PredecessorObj<Session> _deleted;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public SessionDelete(
            Session deleted
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _deleted = new PredecessorObj<Session>(this, RoleDeleted, deleted);
        }

        // Hydration constructor
        private SessionDelete(FactMemento memento)
        {
            InitializeResults();
            _deleted = new PredecessorObj<Session>(this, RoleDeleted, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Deleted
        {
            get { return _deleted.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class SessionUndelete : CorrespondenceFact
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
				SessionUndelete newFact = new SessionUndelete(memento);

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
				SessionUndelete fact = (SessionUndelete)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionUndelete", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleUndeleted = new Role(new RoleMemento(
			_correspondenceFactType,
			"undeleted",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionDelete", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<SessionDelete> _undeleted;

        // Fields

        // Results

        // Business constructor
        public SessionUndelete(
            SessionDelete undeleted
            )
        {
            InitializeResults();
            _undeleted = new PredecessorObj<SessionDelete>(this, RoleUndeleted, undeleted);
        }

        // Hydration constructor
        private SessionUndelete(FactMemento memento)
        {
            InitializeResults();
            _undeleted = new PredecessorObj<SessionDelete>(this, RoleUndeleted, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public SessionDelete Undeleted
        {
            get { return _undeleted.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionNotice : CorrespondenceFact
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
				SessionNotice newFact = new SessionNotice(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._timeSent = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._text = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionNotice fact = (SessionNotice)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._timeSent);
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._text);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionNotice", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSession = new Role(new RoleMemento(
			_correspondenceFactType,
			"session",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Session", 1),
			true));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Session> _session;

        // Fields
        private DateTime _timeSent;
        private string _text;

        // Results

        // Business constructor
        public SessionNotice(
            Session session
            ,DateTime timeSent
            ,string text
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, session);
            _timeSent = timeSent;
            _text = text;
        }

        // Hydration constructor
        private SessionNotice(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, RoleSession, memento);
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

        // Field access
        public DateTime TimeSent
        {
            get { return _timeSent; }
        }
        public string Text
        {
            get { return _text; }
        }

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
        public static Query QueryIsDeleted = new Query()
            .JoinPredecessors(SessionPlace.RoleSession)
            .JoinSuccessors(SessionDelete.RoleDeleted, Condition.WhereIsEmpty(SessionDelete.QueryIsUndeleted)
            )
            ;

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(QueryIsDeleted);

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
			true));

        // Queries
        public static Query QueryIsCurrent = new Query()
            .JoinSuccessors(ScheduleRemove.RoleSchedule)
            ;
        public static Query QueryCompletedEvaluations = new Query()
            .JoinSuccessors(SessionEvaluation.RoleSchedule, Condition.WhereIsNotEmpty(SessionEvaluation.QueryIsCompleted)
            )
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
        private Result<SessionEvaluation> _completedEvaluations;

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
            _completedEvaluations = new Result<SessionEvaluation>(this, QueryCompletedEvaluations);
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
        public IEnumerable<SessionEvaluation> CompletedEvaluations
        {
            get { return _completedEvaluations; }
        }

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
        public static Query QueryRatingAnswers = new Query()
            .JoinSuccessors(SessionEvaluationRating.RoleEvaluation)
            .JoinSuccessors(SessionEvaluationRatingAnswer.RoleRating, Condition.WhereIsEmpty(SessionEvaluationRatingAnswer.QueryIsCurrent)
            )
            ;
        public static Query QueryEssayAnswers = new Query()
            .JoinSuccessors(SessionEvaluationEssay.RoleEvaluation)
            .JoinSuccessors(SessionEvaluationEssayAnswer.RoleEssay, Condition.WhereIsEmpty(SessionEvaluationEssayAnswer.QueryIsCurrent)
            )
            ;
        public static Query QueryIsCompleted = new Query()
            .JoinSuccessors(SessionEvaluationCompleted.RoleSessionEvaluation)
            ;

        // Predicates
        public static Condition IsCompleted = Condition.WhereIsNotEmpty(QueryIsCompleted);

        // Predecessors
        private PredecessorObj<Schedule> _schedule;
        private PredecessorObj<Survey> _survey;

        // Fields

        // Results
        private Result<SessionEvaluationRatingAnswer> _ratingAnswers;
        private Result<SessionEvaluationEssayAnswer> _essayAnswers;

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
            _ratingAnswers = new Result<SessionEvaluationRatingAnswer>(this, QueryRatingAnswers);
            _essayAnswers = new Result<SessionEvaluationEssayAnswer>(this, QueryEssayAnswers);
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
        public IEnumerable<SessionEvaluationRatingAnswer> RatingAnswers
        {
            get { return _ratingAnswers; }
        }
        public IEnumerable<SessionEvaluationEssayAnswer> EssayAnswers
        {
            get { return _essayAnswers; }
        }

        // Mutable property access

    }
    
    public partial class SessionEvaluationCompleted : CorrespondenceFact
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
				SessionEvaluationCompleted newFact = new SessionEvaluationCompleted(memento);

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
				SessionEvaluationCompleted fact = (SessionEvaluationCompleted)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionEvaluationCompleted", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleSessionSurvey = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionSurvey",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.ConferenceSessionSurvey", 1),
			true));
        public static Role RoleSessionEvaluation = new Role(new RoleMemento(
			_correspondenceFactType,
			"sessionEvaluation",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluation", 1),
			false));
        public static Role RoleRatingAnswers = new Role(new RoleMemento(
			_correspondenceFactType,
			"ratingAnswers",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationRatingAnswer", 1),
			false));
        public static Role RoleEssayAnswers = new Role(new RoleMemento(
			_correspondenceFactType,
			"essayAnswers",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.SessionEvaluationEssayAnswer", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<ConferenceSessionSurvey> _sessionSurvey;
        private PredecessorObj<SessionEvaluation> _sessionEvaluation;
        private PredecessorList<SessionEvaluationRatingAnswer> _ratingAnswers;
        private PredecessorList<SessionEvaluationEssayAnswer> _essayAnswers;

        // Fields

        // Results

        // Business constructor
        public SessionEvaluationCompleted(
            ConferenceSessionSurvey sessionSurvey
            ,SessionEvaluation sessionEvaluation
            ,IEnumerable<SessionEvaluationRatingAnswer> ratingAnswers
            ,IEnumerable<SessionEvaluationEssayAnswer> essayAnswers
            )
        {
            InitializeResults();
            _sessionSurvey = new PredecessorObj<ConferenceSessionSurvey>(this, RoleSessionSurvey, sessionSurvey);
            _sessionEvaluation = new PredecessorObj<SessionEvaluation>(this, RoleSessionEvaluation, sessionEvaluation);
            _ratingAnswers = new PredecessorList<SessionEvaluationRatingAnswer>(this, RoleRatingAnswers, ratingAnswers);
            _essayAnswers = new PredecessorList<SessionEvaluationEssayAnswer>(this, RoleEssayAnswers, essayAnswers);
        }

        // Hydration constructor
        private SessionEvaluationCompleted(FactMemento memento)
        {
            InitializeResults();
            _sessionSurvey = new PredecessorObj<ConferenceSessionSurvey>(this, RoleSessionSurvey, memento);
            _sessionEvaluation = new PredecessorObj<SessionEvaluation>(this, RoleSessionEvaluation, memento);
            _ratingAnswers = new PredecessorList<SessionEvaluationRatingAnswer>(this, RoleRatingAnswers, memento);
            _essayAnswers = new PredecessorList<SessionEvaluationEssayAnswer>(this, RoleEssayAnswers, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceSessionSurvey SessionSurvey
        {
            get { return _sessionSurvey.Fact; }
        }
        public SessionEvaluation SessionEvaluation
        {
            get { return _sessionEvaluation.Fact; }
        }
        public IEnumerable<SessionEvaluationRatingAnswer> RatingAnswers
        {
            get { return _ratingAnswers; }
        }
             public IEnumerable<SessionEvaluationEssayAnswer> EssayAnswers
        {
            get { return _essayAnswers; }
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
        public static Query QueryCurrentAnswers = new Query()
            .JoinSuccessors(SessionEvaluationRatingAnswer.RoleRating, Condition.WhereIsEmpty(SessionEvaluationRatingAnswer.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<SessionEvaluation> _evaluation;
        private PredecessorObj<RatingQuestion> _question;

        // Fields

        // Results
        private Result<SessionEvaluationRatingAnswer> _currentAnswers;

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
            _currentAnswers = new Result<SessionEvaluationRatingAnswer>(this, QueryCurrentAnswers);
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
        public IEnumerable<SessionEvaluationRatingAnswer> CurrentAnswers
        {
            get { return _currentAnswers; }
        }

        // Mutable property access

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
        public static Role RoleRating = new Role(new RoleMemento(
			_correspondenceFactType,
			"rating",
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
        private PredecessorObj<SessionEvaluationRating> _rating;
        private PredecessorList<SessionEvaluationRatingAnswer> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public SessionEvaluationRatingAnswer(
            SessionEvaluationRating rating
            ,IEnumerable<SessionEvaluationRatingAnswer> prior
            ,int value
            )
        {
            InitializeResults();
            _rating = new PredecessorObj<SessionEvaluationRating>(this, RoleRating, rating);
            _prior = new PredecessorList<SessionEvaluationRatingAnswer>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SessionEvaluationRatingAnswer(FactMemento memento)
        {
            InitializeResults();
            _rating = new PredecessorObj<SessionEvaluationRating>(this, RoleRating, memento);
            _prior = new PredecessorList<SessionEvaluationRatingAnswer>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public SessionEvaluationRating Rating
        {
            get { return _rating.Fact; }
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
        public static Query QueryCurrentAnswers = new Query()
            .JoinSuccessors(SessionEvaluationEssayAnswer.RoleEssay, Condition.WhereIsEmpty(SessionEvaluationEssayAnswer.QueryIsCurrent)
            )
            ;

        // Predicates

        // Predecessors
        private PredecessorObj<SessionEvaluation> _evaluation;
        private PredecessorObj<EssayQuestion> _question;

        // Fields

        // Results
        private Result<SessionEvaluationEssayAnswer> _currentAnswers;

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
            _currentAnswers = new Result<SessionEvaluationEssayAnswer>(this, QueryCurrentAnswers);
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
        public IEnumerable<SessionEvaluationEssayAnswer> CurrentAnswers
        {
            get { return _currentAnswers; }
        }

        // Mutable property access

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
        public static Role RoleEssay = new Role(new RoleMemento(
			_correspondenceFactType,
			"essay",
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
        private PredecessorObj<SessionEvaluationEssay> _essay;
        private PredecessorList<SessionEvaluationEssayAnswer> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public SessionEvaluationEssayAnswer(
            SessionEvaluationEssay essay
            ,IEnumerable<SessionEvaluationEssayAnswer> prior
            ,string value
            )
        {
            InitializeResults();
            _essay = new PredecessorObj<SessionEvaluationEssay>(this, RoleEssay, essay);
            _prior = new PredecessorList<SessionEvaluationEssayAnswer>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private SessionEvaluationEssayAnswer(FactMemento memento)
        {
            InitializeResults();
            _essay = new PredecessorObj<SessionEvaluationEssay>(this, RoleEssay, memento);
            _prior = new PredecessorList<SessionEvaluationEssayAnswer>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public SessionEvaluationEssay Essay
        {
            get { return _essay.Fact; }
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
				Identity.QueryIsToastNotificationEnabled.QueryDefinition);
			community.AddType(
				EnableToastNotification._correspondenceFactType,
				new EnableToastNotification.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { EnableToastNotification._correspondenceFactType }));
			community.AddQuery(
				EnableToastNotification._correspondenceFactType,
				EnableToastNotification.QueryIsDisabled.QueryDefinition);
			community.AddType(
				DisableToastNotification._correspondenceFactType,
				new DisableToastNotification.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { DisableToastNotification._correspondenceFactType }));
			community.AddType(
				Conference._correspondenceFactType,
				new Conference.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference._correspondenceFactType }));
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryName.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryConferenceSurvey.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryMapUrl.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryDays.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryAllTracks.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryTracks.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySessions.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryUnscheduledSessions.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySpeakers.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryNotices.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryCurrentSessionSurveys.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryAllSessionSurveys.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryRooms.QueryDefinition);
			community.AddType(
				Conference__name._correspondenceFactType,
				new Conference__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference__name._correspondenceFactType }));
			community.AddQuery(
				Conference__name._correspondenceFactType,
				Conference__name.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Conference__conferenceSurvey._correspondenceFactType,
				new Conference__conferenceSurvey.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference__conferenceSurvey._correspondenceFactType }));
			community.AddQuery(
				Conference__conferenceSurvey._correspondenceFactType,
				Conference__conferenceSurvey.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Conference__mapUrl._correspondenceFactType,
				new Conference__mapUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference__mapUrl._correspondenceFactType }));
			community.AddQuery(
				Conference__mapUrl._correspondenceFactType,
				Conference__mapUrl.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ConferenceSessionSurvey._correspondenceFactType,
				new ConferenceSessionSurvey.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceSessionSurvey._correspondenceFactType }));
			community.AddQuery(
				ConferenceSessionSurvey._correspondenceFactType,
				ConferenceSessionSurvey.QueryIsCurrent.QueryDefinition);
			community.AddQuery(
				ConferenceSessionSurvey._correspondenceFactType,
				ConferenceSessionSurvey.QueryCompleted.QueryDefinition);
			community.AddType(
				Attendee._correspondenceFactType,
				new Attendee.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Attendee._correspondenceFactType }));
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.QueryCurrentSchedules.QueryDefinition);
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.QueryAllSchedules.QueryDefinition);
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.QueryScheduledSessions.QueryDefinition);
			community.AddType(
				Day._correspondenceFactType,
				new Day.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Day._correspondenceFactType }));
			community.AddQuery(
				Day._correspondenceFactType,
				Day.QueryTimes.QueryDefinition);
			community.AddQuery(
				Day._correspondenceFactType,
				Day.QueryHasTimes.QueryDefinition);
			community.AddType(
				Time._correspondenceFactType,
				new Time.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Time._correspondenceFactType }));
			community.AddQuery(
				Time._correspondenceFactType,
				Time.QueryAvailableSessions.QueryDefinition);
			community.AddQuery(
				Time._correspondenceFactType,
				Time.QueryDeletes.QueryDefinition);
			community.AddQuery(
				Time._correspondenceFactType,
				Time.QueryIsDeleted.QueryDefinition);
			community.AddType(
				TimeDelete._correspondenceFactType,
				new TimeDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { TimeDelete._correspondenceFactType }));
			community.AddQuery(
				TimeDelete._correspondenceFactType,
				TimeDelete.QueryIsUndeleted.QueryDefinition);
			community.AddType(
				TimeUndelete._correspondenceFactType,
				new TimeUndelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { TimeUndelete._correspondenceFactType }));
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
			community.AddQuery(
				Room._correspondenceFactType,
				Room.QueryRoomNumber.QueryDefinition);
			community.AddType(
				Room__roomNumber._correspondenceFactType,
				new Room__roomNumber.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room__roomNumber._correspondenceFactType }));
			community.AddQuery(
				Room__roomNumber._correspondenceFactType,
				Room__roomNumber.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Track._correspondenceFactType,
				new Track.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Track._correspondenceFactType }));
			community.AddQuery(
				Track._correspondenceFactType,
				Track.QueryCurrentSessionPlaces.QueryDefinition);
			community.AddQuery(
				Track._correspondenceFactType,
				Track.QueryHasSessions.QueryDefinition);
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
				Speaker__imageUrl._correspondenceFactType,
				new Speaker__imageUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__imageUrl._correspondenceFactType }));
			community.AddQuery(
				Speaker__imageUrl._correspondenceFactType,
				Speaker__imageUrl.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Speaker__contact._correspondenceFactType,
				new Speaker__contact.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__contact._correspondenceFactType }));
			community.AddQuery(
				Speaker__contact._correspondenceFactType,
				Speaker__contact.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Speaker__bio._correspondenceFactType,
				new Speaker__bio.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__bio._correspondenceFactType }));
			community.AddQuery(
				Speaker__bio._correspondenceFactType,
				Speaker__bio.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ConferenceNotice._correspondenceFactType,
				new ConferenceNotice.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceNotice._correspondenceFactType }));
			community.AddType(
				Place._correspondenceFactType,
				new Place.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Place._correspondenceFactType }));
			community.AddQuery(
				Place._correspondenceFactType,
				Place.QueryCurrentSessionPlaces.QueryDefinition);
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
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryNotices.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryIsDeleted.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QuerySessionDeletes.QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryIsScheduled.QueryDefinition);
			community.AddType(
				Session__name._correspondenceFactType,
				new Session__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session__name._correspondenceFactType }));
			community.AddQuery(
				Session__name._correspondenceFactType,
				Session__name.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Session__description._correspondenceFactType,
				new Session__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session__description._correspondenceFactType }));
			community.AddQuery(
				Session__description._correspondenceFactType,
				Session__description.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Session__level._correspondenceFactType,
				new Session__level.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session__level._correspondenceFactType }));
			community.AddQuery(
				Session__level._correspondenceFactType,
				Session__level.QueryIsCurrent.QueryDefinition);
			community.AddType(
				SessionDelete._correspondenceFactType,
				new SessionDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionDelete._correspondenceFactType }));
			community.AddQuery(
				SessionDelete._correspondenceFactType,
				SessionDelete.QueryIsUndeleted.QueryDefinition);
			community.AddType(
				SessionUndelete._correspondenceFactType,
				new SessionUndelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionUndelete._correspondenceFactType }));
			community.AddType(
				SessionNotice._correspondenceFactType,
				new SessionNotice.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionNotice._correspondenceFactType }));
			community.AddType(
				SessionPlace._correspondenceFactType,
				new SessionPlace.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionPlace._correspondenceFactType }));
			community.AddQuery(
				SessionPlace._correspondenceFactType,
				SessionPlace.QueryIsCurrent.QueryDefinition);
			community.AddQuery(
				SessionPlace._correspondenceFactType,
				SessionPlace.QueryIsDeleted.QueryDefinition);
			community.AddType(
				Schedule._correspondenceFactType,
				new Schedule.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Schedule._correspondenceFactType }));
			community.AddQuery(
				Schedule._correspondenceFactType,
				Schedule.QueryIsCurrent.QueryDefinition);
			community.AddQuery(
				Schedule._correspondenceFactType,
				Schedule.QueryCompletedEvaluations.QueryDefinition);
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
			community.AddQuery(
				SessionEvaluation._correspondenceFactType,
				SessionEvaluation.QueryRatingAnswers.QueryDefinition);
			community.AddQuery(
				SessionEvaluation._correspondenceFactType,
				SessionEvaluation.QueryEssayAnswers.QueryDefinition);
			community.AddQuery(
				SessionEvaluation._correspondenceFactType,
				SessionEvaluation.QueryIsCompleted.QueryDefinition);
			community.AddType(
				SessionEvaluationCompleted._correspondenceFactType,
				new SessionEvaluationCompleted.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationCompleted._correspondenceFactType }));
			community.AddType(
				SessionEvaluationRating._correspondenceFactType,
				new SessionEvaluationRating.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionEvaluationRating._correspondenceFactType }));
			community.AddQuery(
				SessionEvaluationRating._correspondenceFactType,
				SessionEvaluationRating.QueryCurrentAnswers.QueryDefinition);
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
				SessionEvaluationEssay.QueryCurrentAnswers.QueryDefinition);
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
