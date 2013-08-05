using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using UpdateControls.Correspondence.Tasks;
using System;
using System.IO;
using System.Threading;

/**
/ For use with http://graphviz.org/
digraph "FacetedWorlds.MyCon.Model"
{
    rankdir=BT
    IndividualProfile -> Individual [color="red"]
    IndividualProfile -> Profile
    Attendee -> Conference [color="red"]
    Attendee -> Profile [color="red"]
    AttendeeInactive -> Attendee
    AttendeeActive -> AttendeeInactive
    ConferenceHeader -> Catalog [color="red"]
    ConferenceHeader -> Conference
    ConferenceHeader__name -> ConferenceHeader
    ConferenceHeader__name -> ConferenceHeader__name [label="  *"]
    ConferenceHeader__imageUrl -> ConferenceHeader
    ConferenceHeader__imageUrl -> ConferenceHeader__imageUrl [label="  *"]
    ConferenceHeader__startDate -> ConferenceHeader
    ConferenceHeader__startDate -> ConferenceHeader__startDate [label="  *"]
    ConferenceHeader__endDate -> ConferenceHeader
    ConferenceHeader__endDate -> ConferenceHeader__endDate [label="  *"]
    ConferenceHeader__address -> ConferenceHeader
    ConferenceHeader__address -> ConferenceHeader__address [label="  *"]
    ConferenceHeader__city -> ConferenceHeader
    ConferenceHeader__city -> ConferenceHeader__city [label="  *"]
    ConferenceHeader__homePageUrl -> ConferenceHeader
    ConferenceHeader__homePageUrl -> ConferenceHeader__homePageUrl [label="  *"]
    ConferenceHeader__description -> ConferenceHeader
    ConferenceHeader__description -> ConferenceHeader__description [label="  *"]
    ConferenceHeader__description -> DocumentSegment [label="  *"]
    ConferenceHeaderDelete -> ConferenceHeader
    Speaker -> Conference [color="red"]
    Speaker__name -> Speaker
    Speaker__name -> Speaker__name [label="  *"]
    Speaker__imageUrl -> Speaker
    Speaker__imageUrl -> Speaker__imageUrl [label="  *"]
    Speaker__contact -> Speaker
    Speaker__contact -> Speaker__contact [label="  *"]
    Speaker__bio -> Speaker
    Speaker__bio -> Speaker__bio [label="  *"]
    Speaker__bio -> DocumentSegment [label="  *"]
    Session -> Speaker
    Session__title -> Session
    Session__title -> Session__title [label="  *"]
    Session__description -> Session
    Session__description -> Session__description [label="  *"]
    Session__description -> DocumentSegment [label="  *"]
    Track -> Conference [color="red"]
    Track__name -> Track
    Track__name -> Track__name [label="  *"]
    SessionTrack -> Session
    SessionTrack -> Track
    SessionTrack -> SessionTrack [label="  *"]
    Time -> Conference [color="red"]
    Time__startTime -> Time
    Time__startTime -> Time__startTime [label="  *"]
    Room -> Conference [color="red"]
    Room__roomNumber -> Room
    Room__roomNumber -> Room__roomNumber [label="  *"]
    Slot -> Time
    Slot -> Room
    SessionSlot -> Session
    SessionSlot -> Slot
    SessionSlot -> SessionSlot [label="  *"]
}
**/

namespace FacetedWorlds.MyCon.Model
{
    public partial class Individual : CorrespondenceFact
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
				Individual newFact = new Individual(memento);

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
				Individual fact = (Individual)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Individual.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Individual.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Individual", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Individual GetUnloadedInstance()
        {
            return new Individual((FactMemento)null) { IsLoaded = false };
        }

        public static Individual GetNullInstance()
        {
            return new Individual((FactMemento)null) { IsNull = true };
        }

        public Individual Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Individual fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Individual)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries
        private static Query _cacheQueryProfiles;

        public static Query GetQueryProfiles()
		{
            if (_cacheQueryProfiles == null)
            {
			    _cacheQueryProfiles = new Query()
		    		.JoinSuccessors(IndividualProfile.GetRoleIndividual())
		    		.JoinPredecessors(IndividualProfile.GetRoleProfile())
                ;
            }
            return _cacheQueryProfiles;
		}
        private static Query _cacheQueryActiveAttendees;

        public static Query GetQueryActiveAttendees()
		{
            if (_cacheQueryActiveAttendees == null)
            {
			    _cacheQueryActiveAttendees = new Query()
		    		.JoinSuccessors(IndividualProfile.GetRoleIndividual())
		    		.JoinPredecessors(IndividualProfile.GetRoleProfile())
    				.JoinSuccessors(Attendee.GetRoleProfile(), Condition.WhereIsEmpty(Attendee.GetQueryIsActive())
				)
                ;
            }
            return _cacheQueryActiveAttendees;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Profile> _profiles;
        private Result<Attendee> _activeAttendees;

        // Business constructor
        public Individual(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Individual(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _profiles = new Result<Profile>(this, GetQueryProfiles(), Profile.GetUnloadedInstance, Profile.GetNullInstance);
            _activeAttendees = new Result<Attendee>(this, GetQueryActiveAttendees(), Attendee.GetUnloadedInstance, Attendee.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Profile> Profiles
        {
            get { return _profiles; }
        }
        public Result<Attendee> ActiveAttendees
        {
            get { return _activeAttendees; }
        }

        // Mutable property access

    }
    
    public partial class Profile : CorrespondenceFact
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
				Profile newFact = new Profile(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._profileId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Profile fact = (Profile)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._profileId);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Profile.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Profile.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Profile", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Profile GetUnloadedInstance()
        {
            return new Profile((FactMemento)null) { IsLoaded = false };
        }

        public static Profile GetNullInstance()
        {
            return new Profile((FactMemento)null) { IsNull = true };
        }

        public Profile Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Profile fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Profile)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _profileId;

        // Results

        // Business constructor
        public Profile(
            string profileId
            )
        {
            InitializeResults();
            _profileId = profileId;
        }

        // Hydration constructor
        private Profile(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string ProfileId
        {
            get { return _profileId; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class IndividualProfile : CorrespondenceFact
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
				IndividualProfile newFact = new IndividualProfile(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				IndividualProfile fact = (IndividualProfile)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return IndividualProfile.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return IndividualProfile.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.IndividualProfile", 1770677284);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static IndividualProfile GetUnloadedInstance()
        {
            return new IndividualProfile((FactMemento)null) { IsLoaded = false };
        }

        public static IndividualProfile GetNullInstance()
        {
            return new IndividualProfile((FactMemento)null) { IsNull = true };
        }

        public IndividualProfile Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                IndividualProfile fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (IndividualProfile)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleIndividual;
        public static Role GetRoleIndividual()
        {
            if (_cacheRoleIndividual == null)
            {
                _cacheRoleIndividual = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "individual",
			        Individual._correspondenceFactType,
			        true));
            }
            return _cacheRoleIndividual;
        }
        private static Role _cacheRoleProfile;
        public static Role GetRoleProfile()
        {
            if (_cacheRoleProfile == null)
            {
                _cacheRoleProfile = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profile",
			        Profile._correspondenceFactType,
			        false));
            }
            return _cacheRoleProfile;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Individual> _individual;
        private PredecessorObj<Profile> _profile;

        // Fields

        // Results

        // Business constructor
        public IndividualProfile(
            Individual individual
            ,Profile profile
            )
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), individual);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), profile);
        }

        // Hydration constructor
        private IndividualProfile(FactMemento memento)
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), memento, Individual.GetUnloadedInstance, Individual.GetNullInstance);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), memento, Profile.GetUnloadedInstance, Profile.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Individual Individual
        {
            get { return IsNull ? Individual.GetNullInstance() : _individual.Fact; }
        }
        public Profile Profile
        {
            get { return IsNull ? Profile.GetNullInstance() : _profile.Fact; }
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Attendee fact = (Attendee)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Attendee.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Attendee.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Attendee", 1640206880);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Attendee GetUnloadedInstance()
        {
            return new Attendee((FactMemento)null) { IsLoaded = false };
        }

        public static Attendee GetNullInstance()
        {
            return new Attendee((FactMemento)null) { IsNull = true };
        }

        public Attendee Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Attendee fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Attendee)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        true));
            }
            return _cacheRoleConference;
        }
        private static Role _cacheRoleProfile;
        public static Role GetRoleProfile()
        {
            if (_cacheRoleProfile == null)
            {
                _cacheRoleProfile = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profile",
			        Profile._correspondenceFactType,
			        true));
            }
            return _cacheRoleProfile;
        }

        // Queries
        private static Query _cacheQueryInactives;

        public static Query GetQueryInactives()
		{
            if (_cacheQueryInactives == null)
            {
			    _cacheQueryInactives = new Query()
    				.JoinSuccessors(AttendeeInactive.GetRoleAttendee(), Condition.WhereIsEmpty(AttendeeInactive.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryInactives;
		}
        private static Query _cacheQueryIsActive;

        public static Query GetQueryIsActive()
		{
            if (_cacheQueryIsActive == null)
            {
			    _cacheQueryIsActive = new Query()
    				.JoinSuccessors(AttendeeInactive.GetRoleAttendee(), Condition.WhereIsEmpty(AttendeeInactive.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryIsActive;
		}

        // Predicates
        public static Condition IsActive = Condition.WhereIsEmpty(GetQueryIsActive());

        // Predecessors
        private PredecessorObj<Conference> _conference;
        private PredecessorObj<Profile> _profile;

        // Fields

        // Results
        private Result<AttendeeInactive> _inactives;

        // Business constructor
        public Attendee(
            Conference conference
            ,Profile profile
            )
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), profile);
        }

        // Hydration constructor
        private Attendee(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), memento, Profile.GetUnloadedInstance, Profile.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _inactives = new Result<AttendeeInactive>(this, GetQueryInactives(), AttendeeInactive.GetUnloadedInstance, AttendeeInactive.GetNullInstance);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }
        public Profile Profile
        {
            get { return IsNull ? Profile.GetNullInstance() : _profile.Fact; }
        }

        // Field access

        // Query result access
        public Result<AttendeeInactive> Inactives
        {
            get { return _inactives; }
        }

        // Mutable property access

    }
    
    public partial class AttendeeInactive : CorrespondenceFact
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
				AttendeeInactive newFact = new AttendeeInactive(memento);

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
				AttendeeInactive fact = (AttendeeInactive)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return AttendeeInactive.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return AttendeeInactive.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.AttendeeInactive", -1617690182);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static AttendeeInactive GetUnloadedInstance()
        {
            return new AttendeeInactive((FactMemento)null) { IsLoaded = false };
        }

        public static AttendeeInactive GetNullInstance()
        {
            return new AttendeeInactive((FactMemento)null) { IsNull = true };
        }

        public AttendeeInactive Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                AttendeeInactive fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (AttendeeInactive)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleAttendee;
        public static Role GetRoleAttendee()
        {
            if (_cacheRoleAttendee == null)
            {
                _cacheRoleAttendee = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "attendee",
			        Attendee._correspondenceFactType,
			        false));
            }
            return _cacheRoleAttendee;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(AttendeeActive.GetRoleAttendeeInactive())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Attendee> _attendee;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public AttendeeInactive(
            Attendee attendee
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _attendee = new PredecessorObj<Attendee>(this, GetRoleAttendee(), attendee);
        }

        // Hydration constructor
        private AttendeeInactive(FactMemento memento)
        {
            InitializeResults();
            _attendee = new PredecessorObj<Attendee>(this, GetRoleAttendee(), memento, Attendee.GetUnloadedInstance, Attendee.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Attendee Attendee
        {
            get { return IsNull ? Attendee.GetNullInstance() : _attendee.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class AttendeeActive : CorrespondenceFact
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
				AttendeeActive newFact = new AttendeeActive(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				AttendeeActive fact = (AttendeeActive)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return AttendeeActive.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return AttendeeActive.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.AttendeeActive", 198765784);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static AttendeeActive GetUnloadedInstance()
        {
            return new AttendeeActive((FactMemento)null) { IsLoaded = false };
        }

        public static AttendeeActive GetNullInstance()
        {
            return new AttendeeActive((FactMemento)null) { IsNull = true };
        }

        public AttendeeActive Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                AttendeeActive fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (AttendeeActive)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleAttendeeInactive;
        public static Role GetRoleAttendeeInactive()
        {
            if (_cacheRoleAttendeeInactive == null)
            {
                _cacheRoleAttendeeInactive = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "attendeeInactive",
			        AttendeeInactive._correspondenceFactType,
			        false));
            }
            return _cacheRoleAttendeeInactive;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<AttendeeInactive> _attendeeInactive;

        // Fields

        // Results

        // Business constructor
        public AttendeeActive(
            AttendeeInactive attendeeInactive
            )
        {
            InitializeResults();
            _attendeeInactive = new PredecessorObj<AttendeeInactive>(this, GetRoleAttendeeInactive(), attendeeInactive);
        }

        // Hydration constructor
        private AttendeeActive(FactMemento memento)
        {
            InitializeResults();
            _attendeeInactive = new PredecessorObj<AttendeeInactive>(this, GetRoleAttendeeInactive(), memento, AttendeeInactive.GetUnloadedInstance, AttendeeInactive.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public AttendeeInactive AttendeeInactive
        {
            get { return IsNull ? AttendeeInactive.GetNullInstance() : _attendeeInactive.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Catalog : CorrespondenceFact
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
				Catalog newFact = new Catalog(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._domain = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Catalog fact = (Catalog)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._domain);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Catalog.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Catalog.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Catalog", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Catalog GetUnloadedInstance()
        {
            return new Catalog((FactMemento)null) { IsLoaded = false };
        }

        public static Catalog GetNullInstance()
        {
            return new Catalog((FactMemento)null) { IsNull = true };
        }

        public Catalog Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Catalog fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Catalog)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries
        private static Query _cacheQueryConferenceHeaders;

        public static Query GetQueryConferenceHeaders()
		{
            if (_cacheQueryConferenceHeaders == null)
            {
			    _cacheQueryConferenceHeaders = new Query()
    				.JoinSuccessors(ConferenceHeader.GetRoleCatalog(), Condition.WhereIsEmpty(ConferenceHeader.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryConferenceHeaders;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _domain;

        // Results
        private Result<ConferenceHeader> _conferenceHeaders;

        // Business constructor
        public Catalog(
            string domain
            )
        {
            InitializeResults();
            _domain = domain;
        }

        // Hydration constructor
        private Catalog(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _conferenceHeaders = new Result<ConferenceHeader>(this, GetQueryConferenceHeaders(), ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string Domain
        {
            get { return _domain; }
        }

        // Query result access
        public Result<ConferenceHeader> ConferenceHeaders
        {
            get { return _conferenceHeaders; }
        }

        // Mutable property access

    }
    
    public partial class ConferenceHeader : CorrespondenceFact
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
				ConferenceHeader newFact = new ConferenceHeader(memento);

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
				ConferenceHeader fact = (ConferenceHeader)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader", -615824386);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader GetUnloadedInstance()
        {
            return new ConferenceHeader((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader GetNullInstance()
        {
            return new ConferenceHeader((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleCatalog;
        public static Role GetRoleCatalog()
        {
            if (_cacheRoleCatalog == null)
            {
                _cacheRoleCatalog = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "catalog",
			        Catalog._correspondenceFactType,
			        true));
            }
            return _cacheRoleCatalog;
        }
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        false));
            }
            return _cacheRoleConference;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(ConferenceHeader__name.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}
        private static Query _cacheQueryImageUrl;

        public static Query GetQueryImageUrl()
		{
            if (_cacheQueryImageUrl == null)
            {
			    _cacheQueryImageUrl = new Query()
    				.JoinSuccessors(ConferenceHeader__imageUrl.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__imageUrl.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryImageUrl;
		}
        private static Query _cacheQueryStartDate;

        public static Query GetQueryStartDate()
		{
            if (_cacheQueryStartDate == null)
            {
			    _cacheQueryStartDate = new Query()
    				.JoinSuccessors(ConferenceHeader__startDate.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__startDate.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryStartDate;
		}
        private static Query _cacheQueryEndDate;

        public static Query GetQueryEndDate()
		{
            if (_cacheQueryEndDate == null)
            {
			    _cacheQueryEndDate = new Query()
    				.JoinSuccessors(ConferenceHeader__endDate.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__endDate.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryEndDate;
		}
        private static Query _cacheQueryAddress;

        public static Query GetQueryAddress()
		{
            if (_cacheQueryAddress == null)
            {
			    _cacheQueryAddress = new Query()
    				.JoinSuccessors(ConferenceHeader__address.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__address.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryAddress;
		}
        private static Query _cacheQueryCity;

        public static Query GetQueryCity()
		{
            if (_cacheQueryCity == null)
            {
			    _cacheQueryCity = new Query()
    				.JoinSuccessors(ConferenceHeader__city.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__city.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryCity;
		}
        private static Query _cacheQueryHomePageUrl;

        public static Query GetQueryHomePageUrl()
		{
            if (_cacheQueryHomePageUrl == null)
            {
			    _cacheQueryHomePageUrl = new Query()
    				.JoinSuccessors(ConferenceHeader__homePageUrl.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__homePageUrl.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryHomePageUrl;
		}
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(ConferenceHeader__description.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeaderDelete.GetRoleConferenceHeader())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Catalog> _catalog;
        private PredecessorObj<Conference> _conference;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<ConferenceHeader__name> _name;
        private Result<ConferenceHeader__imageUrl> _imageUrl;
        private Result<ConferenceHeader__startDate> _startDate;
        private Result<ConferenceHeader__endDate> _endDate;
        private Result<ConferenceHeader__address> _address;
        private Result<ConferenceHeader__city> _city;
        private Result<ConferenceHeader__homePageUrl> _homePageUrl;
        private Result<ConferenceHeader__description> _description;

        // Business constructor
        public ConferenceHeader(
            Catalog catalog
            ,Conference conference
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _catalog = new PredecessorObj<Catalog>(this, GetRoleCatalog(), catalog);
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
        }

        // Hydration constructor
        private ConferenceHeader(FactMemento memento)
        {
            InitializeResults();
            _catalog = new PredecessorObj<Catalog>(this, GetRoleCatalog(), memento, Catalog.GetUnloadedInstance, Catalog.GetNullInstance);
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<ConferenceHeader__name>(this, GetQueryName(), ConferenceHeader__name.GetUnloadedInstance, ConferenceHeader__name.GetNullInstance);
            _imageUrl = new Result<ConferenceHeader__imageUrl>(this, GetQueryImageUrl(), ConferenceHeader__imageUrl.GetUnloadedInstance, ConferenceHeader__imageUrl.GetNullInstance);
            _startDate = new Result<ConferenceHeader__startDate>(this, GetQueryStartDate(), ConferenceHeader__startDate.GetUnloadedInstance, ConferenceHeader__startDate.GetNullInstance);
            _endDate = new Result<ConferenceHeader__endDate>(this, GetQueryEndDate(), ConferenceHeader__endDate.GetUnloadedInstance, ConferenceHeader__endDate.GetNullInstance);
            _address = new Result<ConferenceHeader__address>(this, GetQueryAddress(), ConferenceHeader__address.GetUnloadedInstance, ConferenceHeader__address.GetNullInstance);
            _city = new Result<ConferenceHeader__city>(this, GetQueryCity(), ConferenceHeader__city.GetUnloadedInstance, ConferenceHeader__city.GetNullInstance);
            _homePageUrl = new Result<ConferenceHeader__homePageUrl>(this, GetQueryHomePageUrl(), ConferenceHeader__homePageUrl.GetUnloadedInstance, ConferenceHeader__homePageUrl.GetNullInstance);
            _description = new Result<ConferenceHeader__description>(this, GetQueryDescription(), ConferenceHeader__description.GetUnloadedInstance, ConferenceHeader__description.GetNullInstance);
        }

        // Predecessor access
        public Catalog Catalog
        {
            get { return IsNull ? Catalog.GetNullInstance() : _catalog.Fact; }
        }
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<ConferenceHeader__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__name(this, _name, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__imageUrl, string> ImageUrl
        {
            get { return _imageUrl.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _imageUrl.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__imageUrl(this, _imageUrl, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__startDate, DateTime> StartDate
        {
            get { return _startDate.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _startDate.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__startDate(this, _startDate, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__endDate, DateTime> EndDate
        {
            get { return _endDate.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _endDate.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__endDate(this, _endDate, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__address, string> Address
        {
            get { return _address.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _address.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__address(this, _address, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__city, string> City
        {
            get { return _city.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _city.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__city(this, _city, value.Value));
				}
			}
        }
        public TransientDisputable<ConferenceHeader__homePageUrl, string> HomePageUrl
        {
            get { return _homePageUrl.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _homePageUrl.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__homePageUrl(this, _homePageUrl, value.Value));
				}
			}
        }

        public TransientDisputable<ConferenceHeader__description, IEnumerable<DocumentSegment>> Description
        {
            get { return _description.AsTransientDisputable(fact => (IEnumerable<DocumentSegment>)fact.Value); }
			set
			{
				var current = _description.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__description(this, _description, value.Value));
				}
			}
        }
    }
    
    public partial class ConferenceHeader__name : CorrespondenceFact
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
				ConferenceHeader__name newFact = new ConferenceHeader__name(memento);

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
				ConferenceHeader__name fact = (ConferenceHeader__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__name", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__name GetUnloadedInstance()
        {
            return new ConferenceHeader__name((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__name GetNullInstance()
        {
            return new ConferenceHeader__name((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__name Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__name fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__name)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__name(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__name> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__name(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__name>(this, GetRolePrior(), memento, ConferenceHeader__name.GetUnloadedInstance, ConferenceHeader__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__name> Prior
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
    
    public partial class ConferenceHeader__imageUrl : CorrespondenceFact
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
				ConferenceHeader__imageUrl newFact = new ConferenceHeader__imageUrl(memento);

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
				ConferenceHeader__imageUrl fact = (ConferenceHeader__imageUrl)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__imageUrl.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__imageUrl.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__imageUrl", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__imageUrl GetUnloadedInstance()
        {
            return new ConferenceHeader__imageUrl((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__imageUrl GetNullInstance()
        {
            return new ConferenceHeader__imageUrl((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__imageUrl Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__imageUrl fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__imageUrl)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__imageUrl._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__imageUrl.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__imageUrl> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__imageUrl(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__imageUrl> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__imageUrl>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__imageUrl(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__imageUrl>(this, GetRolePrior(), memento, ConferenceHeader__imageUrl.GetUnloadedInstance, ConferenceHeader__imageUrl.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__imageUrl> Prior
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
    
    public partial class ConferenceHeader__startDate : CorrespondenceFact
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
				ConferenceHeader__startDate newFact = new ConferenceHeader__startDate(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceHeader__startDate fact = (ConferenceHeader__startDate)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__startDate.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__startDate.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__startDate", 1696361360);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__startDate GetUnloadedInstance()
        {
            return new ConferenceHeader__startDate((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__startDate GetNullInstance()
        {
            return new ConferenceHeader__startDate((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__startDate Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__startDate fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__startDate)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__startDate._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__startDate.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__startDate> _prior;

        // Fields
        private DateTime _value;

        // Results

        // Business constructor
        public ConferenceHeader__startDate(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__startDate> prior
            ,DateTime value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__startDate>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__startDate(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__startDate>(this, GetRolePrior(), memento, ConferenceHeader__startDate.GetUnloadedInstance, ConferenceHeader__startDate.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__startDate> Prior
        {
            get { return _prior; }
        }

        // Field access
        public DateTime Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ConferenceHeader__endDate : CorrespondenceFact
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
				ConferenceHeader__endDate newFact = new ConferenceHeader__endDate(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceHeader__endDate fact = (ConferenceHeader__endDate)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__endDate.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__endDate.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__endDate", 1696361360);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__endDate GetUnloadedInstance()
        {
            return new ConferenceHeader__endDate((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__endDate GetNullInstance()
        {
            return new ConferenceHeader__endDate((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__endDate Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__endDate fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__endDate)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__endDate._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__endDate.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__endDate> _prior;

        // Fields
        private DateTime _value;

        // Results

        // Business constructor
        public ConferenceHeader__endDate(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__endDate> prior
            ,DateTime value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__endDate>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__endDate(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__endDate>(this, GetRolePrior(), memento, ConferenceHeader__endDate.GetUnloadedInstance, ConferenceHeader__endDate.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__endDate> Prior
        {
            get { return _prior; }
        }

        // Field access
        public DateTime Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ConferenceHeader__address : CorrespondenceFact
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
				ConferenceHeader__address newFact = new ConferenceHeader__address(memento);

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
				ConferenceHeader__address fact = (ConferenceHeader__address)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__address.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__address.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__address", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__address GetUnloadedInstance()
        {
            return new ConferenceHeader__address((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__address GetNullInstance()
        {
            return new ConferenceHeader__address((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__address Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__address fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__address)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__address._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__address.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__address> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__address(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__address> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__address>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__address(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__address>(this, GetRolePrior(), memento, ConferenceHeader__address.GetUnloadedInstance, ConferenceHeader__address.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__address> Prior
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
    
    public partial class ConferenceHeader__city : CorrespondenceFact
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
				ConferenceHeader__city newFact = new ConferenceHeader__city(memento);

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
				ConferenceHeader__city fact = (ConferenceHeader__city)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__city.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__city.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__city", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__city GetUnloadedInstance()
        {
            return new ConferenceHeader__city((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__city GetNullInstance()
        {
            return new ConferenceHeader__city((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__city Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__city fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__city)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__city._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__city.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__city> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__city(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__city> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__city>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__city(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__city>(this, GetRolePrior(), memento, ConferenceHeader__city.GetUnloadedInstance, ConferenceHeader__city.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__city> Prior
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
    
    public partial class ConferenceHeader__homePageUrl : CorrespondenceFact
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
				ConferenceHeader__homePageUrl newFact = new ConferenceHeader__homePageUrl(memento);

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
				ConferenceHeader__homePageUrl fact = (ConferenceHeader__homePageUrl)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__homePageUrl.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__homePageUrl.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__homePageUrl", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__homePageUrl GetUnloadedInstance()
        {
            return new ConferenceHeader__homePageUrl((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__homePageUrl GetNullInstance()
        {
            return new ConferenceHeader__homePageUrl((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__homePageUrl Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__homePageUrl fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__homePageUrl)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__homePageUrl._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__homePageUrl.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__homePageUrl> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__homePageUrl(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__homePageUrl> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__homePageUrl>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__homePageUrl(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__homePageUrl>(this, GetRolePrior(), memento, ConferenceHeader__homePageUrl.GetUnloadedInstance, ConferenceHeader__homePageUrl.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__homePageUrl> Prior
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
    
    public partial class ConferenceHeader__description : CorrespondenceFact
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
				ConferenceHeader__description newFact = new ConferenceHeader__description(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceHeader__description fact = (ConferenceHeader__description)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__description", 676114496);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__description GetUnloadedInstance()
        {
            return new ConferenceHeader__description((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__description GetNullInstance()
        {
            return new ConferenceHeader__description((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__description Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__description fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__description)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ConferenceHeader__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }
        private static Role _cacheRoleValue;
        public static Role GetRoleValue()
        {
            if (_cacheRoleValue == null)
            {
                _cacheRoleValue = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "value",
			        DocumentSegment._correspondenceFactType,
			        false));
            }
            return _cacheRoleValue;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ConferenceHeader__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__description> _prior;
        private PredecessorList<DocumentSegment> _value;

        // Fields

        // Results

        // Business constructor
        public ConferenceHeader__description(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__description> prior
            ,IEnumerable<DocumentSegment> value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__description>(this, GetRolePrior(), prior);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), value);
        }

        // Hydration constructor
        private ConferenceHeader__description(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__description>(this, GetRolePrior(), memento, ConferenceHeader__description.GetUnloadedInstance, ConferenceHeader__description.GetNullInstance);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), memento, DocumentSegment.GetUnloadedInstance, DocumentSegment.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
        }
        public PredecessorList<ConferenceHeader__description> Prior
        {
            get { return _prior; }
        }
        public PredecessorList<DocumentSegment> Value
        {
            get { return _value; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class ConferenceHeaderDelete : CorrespondenceFact
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
				ConferenceHeaderDelete newFact = new ConferenceHeaderDelete(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ConferenceHeaderDelete fact = (ConferenceHeaderDelete)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeaderDelete.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeaderDelete.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeaderDelete", -289631808);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeaderDelete GetUnloadedInstance()
        {
            return new ConferenceHeaderDelete((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeaderDelete GetNullInstance()
        {
            return new ConferenceHeaderDelete((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeaderDelete Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeaderDelete fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeaderDelete)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConferenceHeader;
        public static Role GetRoleConferenceHeader()
        {
            if (_cacheRoleConferenceHeader == null)
            {
                _cacheRoleConferenceHeader = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conferenceHeader",
			        ConferenceHeader._correspondenceFactType,
			        false));
            }
            return _cacheRoleConferenceHeader;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;

        // Fields

        // Results

        // Business constructor
        public ConferenceHeaderDelete(
            ConferenceHeader conferenceHeader
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
        }

        // Hydration constructor
        private ConferenceHeaderDelete(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ConferenceHeader ConferenceHeader
        {
            get { return IsNull ? ConferenceHeader.GetNullInstance() : _conferenceHeader.Fact; }
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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Conference fact = (Conference)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Conference.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Conference.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Conference", 2);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Conference GetUnloadedInstance()
        {
            return new Conference((FactMemento)null) { IsLoaded = false };
        }

        public static Conference GetNullInstance()
        {
            return new Conference((FactMemento)null) { IsNull = true };
        }

        public Conference Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Conference fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Conference)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles

        // Queries
        private static Query _cacheQueryConferenceHeaders;

        public static Query GetQueryConferenceHeaders()
		{
            if (_cacheQueryConferenceHeaders == null)
            {
			    _cacheQueryConferenceHeaders = new Query()
		    		.JoinSuccessors(ConferenceHeader.GetRoleConference())
                ;
            }
            return _cacheQueryConferenceHeaders;
		}
        private static Query _cacheQueryTimes;

        public static Query GetQueryTimes()
		{
            if (_cacheQueryTimes == null)
            {
			    _cacheQueryTimes = new Query()
		    		.JoinSuccessors(Time.GetRoleConference())
                ;
            }
            return _cacheQueryTimes;
		}

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<ConferenceHeader> _conferenceHeaders;
        private Result<Time> _times;

        // Business constructor
        public Conference(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Conference(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _conferenceHeaders = new Result<ConferenceHeader>(this, GetQueryConferenceHeaders(), ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _times = new Result<Time>(this, GetQueryTimes(), Time.GetUnloadedInstance, Time.GetNullInstance);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<ConferenceHeader> ConferenceHeaders
        {
            get { return _conferenceHeaders; }
        }
        public Result<Time> Times
        {
            get { return _times; }
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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Speaker fact = (Speaker)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Speaker.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Speaker.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker", -1711482154);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Speaker GetUnloadedInstance()
        {
            return new Speaker((FactMemento)null) { IsLoaded = false };
        }

        public static Speaker GetNullInstance()
        {
            return new Speaker((FactMemento)null) { IsNull = true };
        }

        public Speaker Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Speaker fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Speaker)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        true));
            }
            return _cacheRoleConference;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Speaker__name.GetRoleSpeaker(), Condition.WhereIsEmpty(Speaker__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}
        private static Query _cacheQueryImageUrl;

        public static Query GetQueryImageUrl()
		{
            if (_cacheQueryImageUrl == null)
            {
			    _cacheQueryImageUrl = new Query()
    				.JoinSuccessors(Speaker__imageUrl.GetRoleSpeaker(), Condition.WhereIsEmpty(Speaker__imageUrl.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryImageUrl;
		}
        private static Query _cacheQueryContact;

        public static Query GetQueryContact()
		{
            if (_cacheQueryContact == null)
            {
			    _cacheQueryContact = new Query()
    				.JoinSuccessors(Speaker__contact.GetRoleSpeaker(), Condition.WhereIsEmpty(Speaker__contact.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryContact;
		}
        private static Query _cacheQueryBio;

        public static Query GetQueryBio()
		{
            if (_cacheQueryBio == null)
            {
			    _cacheQueryBio = new Query()
    				.JoinSuccessors(Speaker__bio.GetRoleSpeaker(), Condition.WhereIsEmpty(Speaker__bio.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryBio;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Speaker__name> _name;
        private Result<Speaker__imageUrl> _imageUrl;
        private Result<Speaker__contact> _contact;
        private Result<Speaker__bio> _bio;

        // Business constructor
        public Speaker(
            Conference conference
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
        }

        // Hydration constructor
        private Speaker(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Speaker__name>(this, GetQueryName(), Speaker__name.GetUnloadedInstance, Speaker__name.GetNullInstance);
            _imageUrl = new Result<Speaker__imageUrl>(this, GetQueryImageUrl(), Speaker__imageUrl.GetUnloadedInstance, Speaker__imageUrl.GetNullInstance);
            _contact = new Result<Speaker__contact>(this, GetQueryContact(), Speaker__contact.GetUnloadedInstance, Speaker__contact.GetNullInstance);
            _bio = new Result<Speaker__bio>(this, GetQueryBio(), Speaker__bio.GetUnloadedInstance, Speaker__bio.GetNullInstance);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Speaker__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Speaker__name(this, _name, value.Value));
				}
			}
        }
        public TransientDisputable<Speaker__imageUrl, string> ImageUrl
        {
            get { return _imageUrl.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _imageUrl.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Speaker__imageUrl(this, _imageUrl, value.Value));
				}
			}
        }
        public TransientDisputable<Speaker__contact, string> Contact
        {
            get { return _contact.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _contact.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Speaker__contact(this, _contact, value.Value));
				}
			}
        }

        public TransientDisputable<Speaker__bio, IEnumerable<DocumentSegment>> Bio
        {
            get { return _bio.AsTransientDisputable(fact => (IEnumerable<DocumentSegment>)fact.Value); }
			set
			{
				var current = _bio.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Speaker__bio(this, _bio, value.Value));
				}
			}
        }
    }
    
    public partial class Speaker__name : CorrespondenceFact
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
				Speaker__name newFact = new Speaker__name(memento);

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
				Speaker__name fact = (Speaker__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Speaker__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Speaker__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__name", 443104904);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Speaker__name GetUnloadedInstance()
        {
            return new Speaker__name((FactMemento)null) { IsLoaded = false };
        }

        public static Speaker__name GetNullInstance()
        {
            return new Speaker__name((FactMemento)null) { IsNull = true };
        }

        public Speaker__name Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Speaker__name fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Speaker__name)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSpeaker;
        public static Role GetRoleSpeaker()
        {
            if (_cacheRoleSpeaker == null)
            {
                _cacheRoleSpeaker = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "speaker",
			        Speaker._correspondenceFactType,
			        false));
            }
            return _cacheRoleSpeaker;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Speaker__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Speaker__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Speaker> _speaker;
        private PredecessorList<Speaker__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Speaker__name(
            Speaker speaker
            ,IEnumerable<Speaker__name> prior
            ,string value
            )
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), speaker);
            _prior = new PredecessorList<Speaker__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Speaker__name(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), memento, Speaker.GetUnloadedInstance, Speaker.GetNullInstance);
            _prior = new PredecessorList<Speaker__name>(this, GetRolePrior(), memento, Speaker__name.GetUnloadedInstance, Speaker__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return IsNull ? Speaker.GetNullInstance() : _speaker.Fact; }
        }
        public PredecessorList<Speaker__name> Prior
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Speaker__imageUrl.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Speaker__imageUrl.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__imageUrl", 443104904);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Speaker__imageUrl GetUnloadedInstance()
        {
            return new Speaker__imageUrl((FactMemento)null) { IsLoaded = false };
        }

        public static Speaker__imageUrl GetNullInstance()
        {
            return new Speaker__imageUrl((FactMemento)null) { IsNull = true };
        }

        public Speaker__imageUrl Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Speaker__imageUrl fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Speaker__imageUrl)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSpeaker;
        public static Role GetRoleSpeaker()
        {
            if (_cacheRoleSpeaker == null)
            {
                _cacheRoleSpeaker = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "speaker",
			        Speaker._correspondenceFactType,
			        false));
            }
            return _cacheRoleSpeaker;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Speaker__imageUrl._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Speaker__imageUrl.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), speaker);
            _prior = new PredecessorList<Speaker__imageUrl>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Speaker__imageUrl(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), memento, Speaker.GetUnloadedInstance, Speaker.GetNullInstance);
            _prior = new PredecessorList<Speaker__imageUrl>(this, GetRolePrior(), memento, Speaker__imageUrl.GetUnloadedInstance, Speaker__imageUrl.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return IsNull ? Speaker.GetNullInstance() : _speaker.Fact; }
        }
        public PredecessorList<Speaker__imageUrl> Prior
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Speaker__contact.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Speaker__contact.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__contact", 443104904);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Speaker__contact GetUnloadedInstance()
        {
            return new Speaker__contact((FactMemento)null) { IsLoaded = false };
        }

        public static Speaker__contact GetNullInstance()
        {
            return new Speaker__contact((FactMemento)null) { IsNull = true };
        }

        public Speaker__contact Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Speaker__contact fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Speaker__contact)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSpeaker;
        public static Role GetRoleSpeaker()
        {
            if (_cacheRoleSpeaker == null)
            {
                _cacheRoleSpeaker = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "speaker",
			        Speaker._correspondenceFactType,
			        false));
            }
            return _cacheRoleSpeaker;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Speaker__contact._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Speaker__contact.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), speaker);
            _prior = new PredecessorList<Speaker__contact>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Speaker__contact(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), memento, Speaker.GetUnloadedInstance, Speaker.GetNullInstance);
            _prior = new PredecessorList<Speaker__contact>(this, GetRolePrior(), memento, Speaker__contact.GetUnloadedInstance, Speaker__contact.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return IsNull ? Speaker.GetNullInstance() : _speaker.Fact; }
        }
        public PredecessorList<Speaker__contact> Prior
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Speaker__bio fact = (Speaker__bio)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Speaker__bio.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Speaker__bio.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Speaker__bio", -577141912);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Speaker__bio GetUnloadedInstance()
        {
            return new Speaker__bio((FactMemento)null) { IsLoaded = false };
        }

        public static Speaker__bio GetNullInstance()
        {
            return new Speaker__bio((FactMemento)null) { IsNull = true };
        }

        public Speaker__bio Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Speaker__bio fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Speaker__bio)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSpeaker;
        public static Role GetRoleSpeaker()
        {
            if (_cacheRoleSpeaker == null)
            {
                _cacheRoleSpeaker = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "speaker",
			        Speaker._correspondenceFactType,
			        false));
            }
            return _cacheRoleSpeaker;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Speaker__bio._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }
        private static Role _cacheRoleValue;
        public static Role GetRoleValue()
        {
            if (_cacheRoleValue == null)
            {
                _cacheRoleValue = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "value",
			        DocumentSegment._correspondenceFactType,
			        false));
            }
            return _cacheRoleValue;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Speaker__bio.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), speaker);
            _prior = new PredecessorList<Speaker__bio>(this, GetRolePrior(), prior);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), value);
        }

        // Hydration constructor
        private Speaker__bio(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), memento, Speaker.GetUnloadedInstance, Speaker.GetNullInstance);
            _prior = new PredecessorList<Speaker__bio>(this, GetRolePrior(), memento, Speaker__bio.GetUnloadedInstance, Speaker__bio.GetNullInstance);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), memento, DocumentSegment.GetUnloadedInstance, DocumentSegment.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return IsNull ? Speaker.GetNullInstance() : _speaker.Fact; }
        }
        public PredecessorList<Speaker__bio> Prior
        {
            get { return _prior; }
        }
        public PredecessorList<DocumentSegment> Value
        {
            get { return _value; }
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Session.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Session.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session", -1802727126);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Session GetUnloadedInstance()
        {
            return new Session((FactMemento)null) { IsLoaded = false };
        }

        public static Session GetNullInstance()
        {
            return new Session((FactMemento)null) { IsNull = true };
        }

        public Session Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Session fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Session)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSpeaker;
        public static Role GetRoleSpeaker()
        {
            if (_cacheRoleSpeaker == null)
            {
                _cacheRoleSpeaker = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "speaker",
			        Speaker._correspondenceFactType,
			        false));
            }
            return _cacheRoleSpeaker;
        }

        // Queries
        private static Query _cacheQueryTitle;

        public static Query GetQueryTitle()
		{
            if (_cacheQueryTitle == null)
            {
			    _cacheQueryTitle = new Query()
    				.JoinSuccessors(Session__title.GetRoleSession(), Condition.WhereIsEmpty(Session__title.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryTitle;
		}
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(Session__description.GetRoleSession(), Condition.WhereIsEmpty(Session__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Speaker> _speaker;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Session__title> _title;
        private Result<Session__description> _description;

        // Business constructor
        public Session(
            Speaker speaker
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), speaker);
        }

        // Hydration constructor
        private Session(FactMemento memento)
        {
            InitializeResults();
            _speaker = new PredecessorObj<Speaker>(this, GetRoleSpeaker(), memento, Speaker.GetUnloadedInstance, Speaker.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _title = new Result<Session__title>(this, GetQueryTitle(), Session__title.GetUnloadedInstance, Session__title.GetNullInstance);
            _description = new Result<Session__description>(this, GetQueryDescription(), Session__description.GetUnloadedInstance, Session__description.GetNullInstance);
        }

        // Predecessor access
        public Speaker Speaker
        {
            get { return IsNull ? Speaker.GetNullInstance() : _speaker.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Session__title, string> Title
        {
            get { return _title.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _title.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Session__title(this, _title, value.Value));
				}
			}
        }

        public TransientDisputable<Session__description, IEnumerable<DocumentSegment>> Description
        {
            get { return _description.AsTransientDisputable(fact => (IEnumerable<DocumentSegment>)fact.Value); }
			set
			{
				var current = _description.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Session__description(this, _description, value.Value));
				}
			}
        }
    }
    
    public partial class Session__title : CorrespondenceFact
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
				Session__title newFact = new Session__title(memento);

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
				Session__title fact = (Session__title)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Session__title.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Session__title.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session__title", -1186408944);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Session__title GetUnloadedInstance()
        {
            return new Session__title((FactMemento)null) { IsLoaded = false };
        }

        public static Session__title GetNullInstance()
        {
            return new Session__title((FactMemento)null) { IsNull = true };
        }

        public Session__title Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Session__title fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Session__title)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSession;
        public static Role GetRoleSession()
        {
            if (_cacheRoleSession == null)
            {
                _cacheRoleSession = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "session",
			        Session._correspondenceFactType,
			        false));
            }
            return _cacheRoleSession;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Session__title._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Session__title.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorList<Session__title> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Session__title(
            Session session
            ,IEnumerable<Session__title> prior
            ,string value
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), session);
            _prior = new PredecessorList<Session__title>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Session__title(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), memento, Session.GetUnloadedInstance, Session.GetNullInstance);
            _prior = new PredecessorList<Session__title>(this, GetRolePrior(), memento, Session__title.GetUnloadedInstance, Session__title.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return IsNull ? Session.GetNullInstance() : _session.Fact; }
        }
        public PredecessorList<Session__title> Prior
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Session__description fact = (Session__description)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Session__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Session__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Session__description", 2088311536);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Session__description GetUnloadedInstance()
        {
            return new Session__description((FactMemento)null) { IsLoaded = false };
        }

        public static Session__description GetNullInstance()
        {
            return new Session__description((FactMemento)null) { IsNull = true };
        }

        public Session__description Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Session__description fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Session__description)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSession;
        public static Role GetRoleSession()
        {
            if (_cacheRoleSession == null)
            {
                _cacheRoleSession = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "session",
			        Session._correspondenceFactType,
			        false));
            }
            return _cacheRoleSession;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Session__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }
        private static Role _cacheRoleValue;
        public static Role GetRoleValue()
        {
            if (_cacheRoleValue == null)
            {
                _cacheRoleValue = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "value",
			        DocumentSegment._correspondenceFactType,
			        false));
            }
            return _cacheRoleValue;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Session__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _session = new PredecessorObj<Session>(this, GetRoleSession(), session);
            _prior = new PredecessorList<Session__description>(this, GetRolePrior(), prior);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), value);
        }

        // Hydration constructor
        private Session__description(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), memento, Session.GetUnloadedInstance, Session.GetNullInstance);
            _prior = new PredecessorList<Session__description>(this, GetRolePrior(), memento, Session__description.GetUnloadedInstance, Session__description.GetNullInstance);
            _value = new PredecessorList<DocumentSegment>(this, GetRoleValue(), memento, DocumentSegment.GetUnloadedInstance, DocumentSegment.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return IsNull ? Session.GetNullInstance() : _session.Fact; }
        }
        public PredecessorList<Session__description> Prior
        {
            get { return _prior; }
        }
        public PredecessorList<DocumentSegment> Value
        {
            get { return _value; }
        }

        // Field access

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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Track fact = (Track)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Track.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Track.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Track", -1711482154);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Track GetUnloadedInstance()
        {
            return new Track((FactMemento)null) { IsLoaded = false };
        }

        public static Track GetNullInstance()
        {
            return new Track((FactMemento)null) { IsNull = true };
        }

        public Track Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Track fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Track)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        true));
            }
            return _cacheRoleConference;
        }

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Track__name.GetRoleTrack(), Condition.WhereIsEmpty(Track__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Track__name> _name;

        // Business constructor
        public Track(
            Conference conference
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
        }

        // Hydration constructor
        private Track(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Track__name>(this, GetQueryName(), Track__name.GetUnloadedInstance, Track__name.GetNullInstance);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Track__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _name.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Track__name(this, _name, value.Value));
				}
			}
        }

    }
    
    public partial class Track__name : CorrespondenceFact
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
				Track__name newFact = new Track__name(memento);

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
				Track__name fact = (Track__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Track__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Track__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Track__name", -1855599040);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Track__name GetUnloadedInstance()
        {
            return new Track__name((FactMemento)null) { IsLoaded = false };
        }

        public static Track__name GetNullInstance()
        {
            return new Track__name((FactMemento)null) { IsNull = true };
        }

        public Track__name Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Track__name fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Track__name)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleTrack;
        public static Role GetRoleTrack()
        {
            if (_cacheRoleTrack == null)
            {
                _cacheRoleTrack = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "track",
			        Track._correspondenceFactType,
			        false));
            }
            return _cacheRoleTrack;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Track__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Track__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Track> _track;
        private PredecessorList<Track__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Track__name(
            Track track
            ,IEnumerable<Track__name> prior
            ,string value
            )
        {
            InitializeResults();
            _track = new PredecessorObj<Track>(this, GetRoleTrack(), track);
            _prior = new PredecessorList<Track__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Track__name(FactMemento memento)
        {
            InitializeResults();
            _track = new PredecessorObj<Track>(this, GetRoleTrack(), memento, Track.GetUnloadedInstance, Track.GetNullInstance);
            _prior = new PredecessorList<Track__name>(this, GetRolePrior(), memento, Track__name.GetUnloadedInstance, Track__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Track Track
        {
            get { return IsNull ? Track.GetNullInstance() : _track.Fact; }
        }
        public PredecessorList<Track__name> Prior
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
    
    public partial class SessionTrack : CorrespondenceFact
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
				SessionTrack newFact = new SessionTrack(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionTrack fact = (SessionTrack)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return SessionTrack.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return SessionTrack.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionTrack", -239068600);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static SessionTrack GetUnloadedInstance()
        {
            return new SessionTrack((FactMemento)null) { IsLoaded = false };
        }

        public static SessionTrack GetNullInstance()
        {
            return new SessionTrack((FactMemento)null) { IsNull = true };
        }

        public SessionTrack Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                SessionTrack fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (SessionTrack)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSession;
        public static Role GetRoleSession()
        {
            if (_cacheRoleSession == null)
            {
                _cacheRoleSession = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "session",
			        Session._correspondenceFactType,
			        false));
            }
            return _cacheRoleSession;
        }
        private static Role _cacheRoleTrack;
        public static Role GetRoleTrack()
        {
            if (_cacheRoleTrack == null)
            {
                _cacheRoleTrack = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "track",
			        Track._correspondenceFactType,
			        false));
            }
            return _cacheRoleTrack;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        SessionTrack._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(SessionTrack.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorObj<Track> _track;
        private PredecessorList<SessionTrack> _prior;

        // Fields

        // Results

        // Business constructor
        public SessionTrack(
            Session session
            ,Track track
            ,IEnumerable<SessionTrack> prior
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), session);
            _track = new PredecessorObj<Track>(this, GetRoleTrack(), track);
            _prior = new PredecessorList<SessionTrack>(this, GetRolePrior(), prior);
        }

        // Hydration constructor
        private SessionTrack(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), memento, Session.GetUnloadedInstance, Session.GetNullInstance);
            _track = new PredecessorObj<Track>(this, GetRoleTrack(), memento, Track.GetUnloadedInstance, Track.GetNullInstance);
            _prior = new PredecessorList<SessionTrack>(this, GetRolePrior(), memento, SessionTrack.GetUnloadedInstance, SessionTrack.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return IsNull ? Session.GetNullInstance() : _session.Fact; }
        }
        public Track Track
        {
            get { return IsNull ? Track.GetNullInstance() : _track.Fact; }
        }
        public PredecessorList<SessionTrack> Prior
        {
            get { return _prior; }
        }

        // Field access

        // Query result access

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
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Time fact = (Time)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Time.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Time.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Time", -1711482154);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Time GetUnloadedInstance()
        {
            return new Time((FactMemento)null) { IsLoaded = false };
        }

        public static Time GetNullInstance()
        {
            return new Time((FactMemento)null) { IsNull = true };
        }

        public Time Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Time fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Time)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        true));
            }
            return _cacheRoleConference;
        }

        // Queries
        private static Query _cacheQueryStartTime;

        public static Query GetQueryStartTime()
		{
            if (_cacheQueryStartTime == null)
            {
			    _cacheQueryStartTime = new Query()
    				.JoinSuccessors(Time__startTime.GetRoleTime(), Condition.WhereIsEmpty(Time__startTime.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryStartTime;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Conference> _conference;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Time__startTime> _startTime;

        // Business constructor
        public Time(
            Conference conference
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
        }

        // Hydration constructor
        private Time(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _startTime = new Result<Time__startTime>(this, GetQueryStartTime(), Time__startTime.GetUnloadedInstance, Time__startTime.GetNullInstance);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Time__startTime, DateTime> StartTime
        {
            get { return _startTime.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _startTime.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new Time__startTime(this, _startTime, value.Value));
				}
			}
        }

    }
    
    public partial class Time__startTime : CorrespondenceFact
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
				Time__startTime newFact = new Time__startTime(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Time__startTime fact = (Time__startTime)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Time__startTime.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Time__startTime.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Time__startTime", 1575759140);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Time__startTime GetUnloadedInstance()
        {
            return new Time__startTime((FactMemento)null) { IsLoaded = false };
        }

        public static Time__startTime GetNullInstance()
        {
            return new Time__startTime((FactMemento)null) { IsNull = true };
        }

        public Time__startTime Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Time__startTime fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Time__startTime)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleTime;
        public static Role GetRoleTime()
        {
            if (_cacheRoleTime == null)
            {
                _cacheRoleTime = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "time",
			        Time._correspondenceFactType,
			        false));
            }
            return _cacheRoleTime;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Time__startTime._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Time__startTime.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Time> _time;
        private PredecessorList<Time__startTime> _prior;

        // Fields
        private DateTime _value;

        // Results

        // Business constructor
        public Time__startTime(
            Time time
            ,IEnumerable<Time__startTime> prior
            ,DateTime value
            )
        {
            InitializeResults();
            _time = new PredecessorObj<Time>(this, GetRoleTime(), time);
            _prior = new PredecessorList<Time__startTime>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Time__startTime(FactMemento memento)
        {
            InitializeResults();
            _time = new PredecessorObj<Time>(this, GetRoleTime(), memento, Time.GetUnloadedInstance, Time.GetNullInstance);
            _prior = new PredecessorList<Time__startTime>(this, GetRolePrior(), memento, Time__startTime.GetUnloadedInstance, Time__startTime.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Time Time
        {
            get { return IsNull ? Time.GetNullInstance() : _time.Fact; }
        }
        public PredecessorList<Time__startTime> Prior
        {
            get { return _prior; }
        }

        // Field access
        public DateTime Value
        {
            get { return _value; }
        }

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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Room.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Room.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Room", -1711482154);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Room GetUnloadedInstance()
        {
            return new Room((FactMemento)null) { IsLoaded = false };
        }

        public static Room GetNullInstance()
        {
            return new Room((FactMemento)null) { IsNull = true };
        }

        public Room Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Room fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Room)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleConference;
        public static Role GetRoleConference()
        {
            if (_cacheRoleConference == null)
            {
                _cacheRoleConference = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "conference",
			        Conference._correspondenceFactType,
			        true));
            }
            return _cacheRoleConference;
        }

        // Queries
        private static Query _cacheQueryRoomNumber;

        public static Query GetQueryRoomNumber()
		{
            if (_cacheQueryRoomNumber == null)
            {
			    _cacheQueryRoomNumber = new Query()
    				.JoinSuccessors(Room__roomNumber.GetRoleRoom(), Condition.WhereIsEmpty(Room__roomNumber.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryRoomNumber;
		}

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
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), conference);
        }

        // Hydration constructor
        private Room(FactMemento memento)
        {
            InitializeResults();
            _conference = new PredecessorObj<Conference>(this, GetRoleConference(), memento, Conference.GetUnloadedInstance, Conference.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _roomNumber = new Result<Room__roomNumber>(this, GetQueryRoomNumber(), Room__roomNumber.GetUnloadedInstance, Room__roomNumber.GetNullInstance);
        }

        // Predecessor access
        public Conference Conference
        {
            get { return IsNull ? Conference.GetNullInstance() : _conference.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Room__roomNumber, string> RoomNumber
        {
            get { return _roomNumber.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _roomNumber.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Room__roomNumber.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Room__roomNumber.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Room__roomNumber", -968433032);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Room__roomNumber GetUnloadedInstance()
        {
            return new Room__roomNumber((FactMemento)null) { IsLoaded = false };
        }

        public static Room__roomNumber GetNullInstance()
        {
            return new Room__roomNumber((FactMemento)null) { IsNull = true };
        }

        public Room__roomNumber Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Room__roomNumber fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Room__roomNumber)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleRoom;
        public static Role GetRoleRoom()
        {
            if (_cacheRoleRoom == null)
            {
                _cacheRoleRoom = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "room",
			        Room._correspondenceFactType,
			        false));
            }
            return _cacheRoleRoom;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Room__roomNumber._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Room__roomNumber.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), room);
            _prior = new PredecessorList<Room__roomNumber>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Room__roomNumber(FactMemento memento)
        {
            InitializeResults();
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), memento, Room.GetUnloadedInstance, Room.GetNullInstance);
            _prior = new PredecessorList<Room__roomNumber>(this, GetRolePrior(), memento, Room__roomNumber.GetUnloadedInstance, Room__roomNumber.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Room Room
        {
            get { return IsNull ? Room.GetNullInstance() : _room.Fact; }
        }
        public PredecessorList<Room__roomNumber> Prior
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Slot fact = (Slot)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Slot.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Slot.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.Slot", -1604894840);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Slot GetUnloadedInstance()
        {
            return new Slot((FactMemento)null) { IsLoaded = false };
        }

        public static Slot GetNullInstance()
        {
            return new Slot((FactMemento)null) { IsNull = true };
        }

        public Slot Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                Slot fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (Slot)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSlotTime;
        public static Role GetRoleSlotTime()
        {
            if (_cacheRoleSlotTime == null)
            {
                _cacheRoleSlotTime = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "slotTime",
			        Time._correspondenceFactType,
			        false));
            }
            return _cacheRoleSlotTime;
        }
        private static Role _cacheRoleRoom;
        public static Role GetRoleRoom()
        {
            if (_cacheRoleRoom == null)
            {
                _cacheRoleRoom = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "room",
			        Room._correspondenceFactType,
			        false));
            }
            return _cacheRoleRoom;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Time> _slotTime;
        private PredecessorObj<Room> _room;

        // Fields

        // Results

        // Business constructor
        public Slot(
            Time slotTime
            ,Room room
            )
        {
            InitializeResults();
            _slotTime = new PredecessorObj<Time>(this, GetRoleSlotTime(), slotTime);
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), room);
        }

        // Hydration constructor
        private Slot(FactMemento memento)
        {
            InitializeResults();
            _slotTime = new PredecessorObj<Time>(this, GetRoleSlotTime(), memento, Time.GetUnloadedInstance, Time.GetNullInstance);
            _room = new PredecessorObj<Room>(this, GetRoleRoom(), memento, Room.GetUnloadedInstance, Room.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Time SlotTime
        {
            get { return IsNull ? Time.GetNullInstance() : _slotTime.Fact; }
        }
        public Room Room
        {
            get { return IsNull ? Room.GetNullInstance() : _room.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class SessionSlot : CorrespondenceFact
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
				SessionSlot newFact = new SessionSlot(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				SessionSlot fact = (SessionSlot)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return SessionSlot.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return SessionSlot.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.SessionSlot", 1543110592);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static SessionSlot GetUnloadedInstance()
        {
            return new SessionSlot((FactMemento)null) { IsLoaded = false };
        }

        public static SessionSlot GetNullInstance()
        {
            return new SessionSlot((FactMemento)null) { IsNull = true };
        }

        public SessionSlot Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                SessionSlot fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (SessionSlot)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
        }

        // Roles
        private static Role _cacheRoleSession;
        public static Role GetRoleSession()
        {
            if (_cacheRoleSession == null)
            {
                _cacheRoleSession = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "session",
			        Session._correspondenceFactType,
			        false));
            }
            return _cacheRoleSession;
        }
        private static Role _cacheRoleSlot;
        public static Role GetRoleSlot()
        {
            if (_cacheRoleSlot == null)
            {
                _cacheRoleSlot = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "slot",
			        Slot._correspondenceFactType,
			        false));
            }
            return _cacheRoleSlot;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        SessionSlot._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(SessionSlot.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Session> _session;
        private PredecessorObj<Slot> _slot;
        private PredecessorList<SessionSlot> _prior;

        // Fields

        // Results

        // Business constructor
        public SessionSlot(
            Session session
            ,Slot slot
            ,IEnumerable<SessionSlot> prior
            )
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), session);
            _slot = new PredecessorObj<Slot>(this, GetRoleSlot(), slot);
            _prior = new PredecessorList<SessionSlot>(this, GetRolePrior(), prior);
        }

        // Hydration constructor
        private SessionSlot(FactMemento memento)
        {
            InitializeResults();
            _session = new PredecessorObj<Session>(this, GetRoleSession(), memento, Session.GetUnloadedInstance, Session.GetNullInstance);
            _slot = new PredecessorObj<Slot>(this, GetRoleSlot(), memento, Slot.GetUnloadedInstance, Slot.GetNullInstance);
            _prior = new PredecessorList<SessionSlot>(this, GetRolePrior(), memento, SessionSlot.GetUnloadedInstance, SessionSlot.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Session Session
        {
            get { return IsNull ? Session.GetNullInstance() : _session.Fact; }
        }
        public Slot Slot
        {
            get { return IsNull ? Slot.GetNullInstance() : _slot.Fact; }
        }
        public PredecessorList<SessionSlot> Prior
        {
            get { return _prior; }
        }

        // Field access

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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return DocumentSegment.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return DocumentSegment.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.DocumentSegment", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static DocumentSegment GetUnloadedInstance()
        {
            return new DocumentSegment((FactMemento)null) { IsLoaded = false };
        }

        public static DocumentSegment GetNullInstance()
        {
            return new DocumentSegment((FactMemento)null) { IsNull = true };
        }

        public DocumentSegment Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                DocumentSegment fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (DocumentSegment)t.Result;
                    loaded.Set();
                });
                loaded.WaitOne();
                return fact;
            }
            else
                return this;
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
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.GetQueryProfiles().QueryDefinition);
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.GetQueryActiveAttendees().QueryDefinition);
			community.AddType(
				Profile._correspondenceFactType,
				new Profile.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Profile._correspondenceFactType }));
			community.AddType(
				IndividualProfile._correspondenceFactType,
				new IndividualProfile.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { IndividualProfile._correspondenceFactType }));
			community.AddType(
				Attendee._correspondenceFactType,
				new Attendee.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Attendee._correspondenceFactType }));
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.GetQueryInactives().QueryDefinition);
			community.AddQuery(
				Attendee._correspondenceFactType,
				Attendee.GetQueryIsActive().QueryDefinition);
			community.AddType(
				AttendeeInactive._correspondenceFactType,
				new AttendeeInactive.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { AttendeeInactive._correspondenceFactType }));
			community.AddQuery(
				AttendeeInactive._correspondenceFactType,
				AttendeeInactive.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				AttendeeActive._correspondenceFactType,
				new AttendeeActive.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { AttendeeActive._correspondenceFactType }));
			community.AddType(
				Catalog._correspondenceFactType,
				new Catalog.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Catalog._correspondenceFactType }));
			community.AddQuery(
				Catalog._correspondenceFactType,
				Catalog.GetQueryConferenceHeaders().QueryDefinition);
			community.AddType(
				ConferenceHeader._correspondenceFactType,
				new ConferenceHeader.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryName().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryImageUrl().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryStartDate().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryEndDate().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryAddress().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryCity().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryHomePageUrl().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				ConferenceHeader._correspondenceFactType,
				ConferenceHeader.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__name._correspondenceFactType,
				new ConferenceHeader__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__name._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__name._correspondenceFactType,
				ConferenceHeader__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__imageUrl._correspondenceFactType,
				new ConferenceHeader__imageUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__imageUrl._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__imageUrl._correspondenceFactType,
				ConferenceHeader__imageUrl.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__startDate._correspondenceFactType,
				new ConferenceHeader__startDate.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__startDate._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__startDate._correspondenceFactType,
				ConferenceHeader__startDate.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__endDate._correspondenceFactType,
				new ConferenceHeader__endDate.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__endDate._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__endDate._correspondenceFactType,
				ConferenceHeader__endDate.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__address._correspondenceFactType,
				new ConferenceHeader__address.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__address._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__address._correspondenceFactType,
				ConferenceHeader__address.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__city._correspondenceFactType,
				new ConferenceHeader__city.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__city._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__city._correspondenceFactType,
				ConferenceHeader__city.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__homePageUrl._correspondenceFactType,
				new ConferenceHeader__homePageUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__homePageUrl._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__homePageUrl._correspondenceFactType,
				ConferenceHeader__homePageUrl.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeader__description._correspondenceFactType,
				new ConferenceHeader__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__description._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__description._correspondenceFactType,
				ConferenceHeader__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeaderDelete._correspondenceFactType,
				new ConferenceHeaderDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeaderDelete._correspondenceFactType }));
			community.AddType(
				Conference._correspondenceFactType,
				new Conference.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference._correspondenceFactType }));
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.GetQueryConferenceHeaders().QueryDefinition);
			community.AddQuery(
				Conference._correspondenceFactType,
				Conference.GetQueryTimes().QueryDefinition);
			community.AddType(
				Speaker._correspondenceFactType,
				new Speaker.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker._correspondenceFactType }));
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.GetQueryName().QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.GetQueryImageUrl().QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.GetQueryContact().QueryDefinition);
			community.AddQuery(
				Speaker._correspondenceFactType,
				Speaker.GetQueryBio().QueryDefinition);
			community.AddType(
				Speaker__name._correspondenceFactType,
				new Speaker__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__name._correspondenceFactType }));
			community.AddQuery(
				Speaker__name._correspondenceFactType,
				Speaker__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Speaker__imageUrl._correspondenceFactType,
				new Speaker__imageUrl.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__imageUrl._correspondenceFactType }));
			community.AddQuery(
				Speaker__imageUrl._correspondenceFactType,
				Speaker__imageUrl.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Speaker__contact._correspondenceFactType,
				new Speaker__contact.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__contact._correspondenceFactType }));
			community.AddQuery(
				Speaker__contact._correspondenceFactType,
				Speaker__contact.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Speaker__bio._correspondenceFactType,
				new Speaker__bio.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Speaker__bio._correspondenceFactType }));
			community.AddQuery(
				Speaker__bio._correspondenceFactType,
				Speaker__bio.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Session._correspondenceFactType,
				new Session.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session._correspondenceFactType }));
			community.AddQuery(
				Session._correspondenceFactType,
				Session.GetQueryTitle().QueryDefinition);
			community.AddQuery(
				Session._correspondenceFactType,
				Session.GetQueryDescription().QueryDefinition);
			community.AddType(
				Session__title._correspondenceFactType,
				new Session__title.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session__title._correspondenceFactType }));
			community.AddQuery(
				Session__title._correspondenceFactType,
				Session__title.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Session__description._correspondenceFactType,
				new Session__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Session__description._correspondenceFactType }));
			community.AddQuery(
				Session__description._correspondenceFactType,
				Session__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Track._correspondenceFactType,
				new Track.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Track._correspondenceFactType }));
			community.AddQuery(
				Track._correspondenceFactType,
				Track.GetQueryName().QueryDefinition);
			community.AddType(
				Track__name._correspondenceFactType,
				new Track__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Track__name._correspondenceFactType }));
			community.AddQuery(
				Track__name._correspondenceFactType,
				Track__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				SessionTrack._correspondenceFactType,
				new SessionTrack.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionTrack._correspondenceFactType }));
			community.AddQuery(
				SessionTrack._correspondenceFactType,
				SessionTrack.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Time._correspondenceFactType,
				new Time.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Time._correspondenceFactType }));
			community.AddQuery(
				Time._correspondenceFactType,
				Time.GetQueryStartTime().QueryDefinition);
			community.AddType(
				Time__startTime._correspondenceFactType,
				new Time__startTime.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Time__startTime._correspondenceFactType }));
			community.AddQuery(
				Time__startTime._correspondenceFactType,
				Time__startTime.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Room._correspondenceFactType,
				new Room.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room._correspondenceFactType }));
			community.AddQuery(
				Room._correspondenceFactType,
				Room.GetQueryRoomNumber().QueryDefinition);
			community.AddType(
				Room__roomNumber._correspondenceFactType,
				new Room__roomNumber.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Room__roomNumber._correspondenceFactType }));
			community.AddQuery(
				Room__roomNumber._correspondenceFactType,
				Room__roomNumber.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Slot._correspondenceFactType,
				new Slot.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Slot._correspondenceFactType }));
			community.AddType(
				SessionSlot._correspondenceFactType,
				new SessionSlot.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { SessionSlot._correspondenceFactType }));
			community.AddQuery(
				SessionSlot._correspondenceFactType,
				SessionSlot.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				DocumentSegment._correspondenceFactType,
				new DocumentSegment.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { DocumentSegment._correspondenceFactType }));
		}
	}
}
