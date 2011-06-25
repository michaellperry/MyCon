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
    Attendee -> Identity
    Attendee -> Conference
    Day -> Conference
    Time -> Day
    Slot -> Attendee
    Slot -> Time
    Room -> Conference
    Track -> Conference
    Speaker -> Conference
    Place -> Time
    Place -> Room
    Session -> Conference
    Session -> Speaker
    Session -> Track [label="  ?"]
    SessionName -> Session
    SessionName -> SessionName [label="  *"]
    SessionPlace -> Session
    SessionPlace -> Place
    SessionPlace -> SessionPlace [label="  *"]
    Schedule -> Slot
    Schedule -> SessionPlace
    Schedule -> Schedule [label="  *"]
    Evaluation -> Schedule
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
        public static Query QueryDays = new Query()
            .JoinSuccessors(Day.RoleConference)
            ;
        public static Query QueryTracks = new Query()
            .JoinSuccessors(Track.RoleConference)
            ;
        public static Query QuerySessions = new Query()
            .JoinSuccessors(Session.RoleConference)
            ;

        // Predicates

        // Predecessors

        // Fields
        private string _id;

        // Results
        private Result<ConferenceName> _name;
        private Result<Day> _days;
        private Result<Track> _tracks;
        private Result<Session> _sessions;

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
            _days = new Result<Day>(this, QueryDays);
            _tracks = new Result<Track>(this, QueryTracks);
            _sessions = new Result<Session>(this, QuerySessions);
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

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new ConferenceName(this, _name, value.Value));
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

        // Predicates

        // Predecessors
        private PredecessorObj<Identity> _identity;
        private PredecessorObj<Conference> _conference;

        // Fields

        // Results

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

        // Predicates

        // Predecessors
        private PredecessorObj<Attendee> _attendee;
        private PredecessorObj<Time> _slotTime;

        // Fields

        // Results

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

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Fields
        private string _name;

        // Results

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

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorObj<Speaker> _speaker;
        private PredecessorOpt<Track> _track;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<SessionName> _name;

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
            _name = new Result<SessionName>(this, QueryName);
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

        // Mutable property access
        public Disputable<string> Name
        {
            get { return _name.Select(fact => fact.Value).AsDisputable(); }
			set
			{
				Community.AddFact(new SessionName(this, _name, value.Value));
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
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Schedule fact = (Schedule)obj;
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
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("FacetedWorlds.MyCon.Model.Schedule", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Slot> _slot;
        private PredecessorObj<SessionPlace> _sessionPlace;
        private PredecessorList<Schedule> _prior;

        // Fields

        // Results

        // Business constructor
        public Schedule(
            Slot slot
            ,SessionPlace sessionPlace
            ,IEnumerable<Schedule> prior
            )
        {
            InitializeResults();
            _slot = new PredecessorObj<Slot>(this, RoleSlot, slot);
            _sessionPlace = new PredecessorObj<SessionPlace>(this, RoleSessionPlace, sessionPlace);
            _prior = new PredecessorList<Schedule>(this, RolePrior, prior);
        }

        // Hydration constructor
        private Schedule(FactMemento memento)
        {
            InitializeResults();
            _slot = new PredecessorObj<Slot>(this, RoleSlot, memento);
            _sessionPlace = new PredecessorObj<SessionPlace>(this, RoleSessionPlace, memento);
            _prior = new PredecessorList<Schedule>(this, RolePrior, memento);
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
        public IEnumerable<Schedule> Prior
        {
            get { return _prior; }
        }
     
        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Evaluation : CorrespondenceFact
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
				Evaluation newFact = new Evaluation(memento);

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
				Evaluation fact = (Evaluation)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Evaluation", 1);

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
        public Evaluation(
            Schedule schedule
            )
        {
            InitializeResults();
            _schedule = new PredecessorObj<Schedule>(this, RoleSchedule, schedule);
        }

        // Hydration constructor
        private Evaluation(FactMemento memento)
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
				Conference.QueryDays.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QueryTracks.QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.QuerySessions.QueryDefinition);
			community.AddType(
				ConferenceName._correspondenceFactType,
				new ConferenceName.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceName._correspondenceFactType }));
			community.AddQuery(
				ConferenceName._correspondenceFactType,
				ConferenceName.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Attendee._correspondenceFactType,
				new Attendee.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Attendee._correspondenceFactType }));
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
			community.AddType(
				Place._correspondenceFactType,
				new Place.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Place._correspondenceFactType }));
			community.AddType(
				Session._correspondenceFactType,
				new Session.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session._correspondenceFactType }));
			community.AddQuery(
				Session._correspondenceFactType,
				Session.QueryName.QueryDefinition);
			community.AddType(
				SessionName._correspondenceFactType,
				new SessionName.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionName._correspondenceFactType }));
			community.AddQuery(
				SessionName._correspondenceFactType,
				SessionName.QueryIsCurrent.QueryDefinition);
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
			community.AddType(
				Evaluation._correspondenceFactType,
				new Evaluation.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Evaluation._correspondenceFactType }));
		}
	}
}
