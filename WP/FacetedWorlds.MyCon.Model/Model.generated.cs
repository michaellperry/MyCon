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

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results

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
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


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
			community.AddType(
				DocumentSegment._correspondenceFactType,
				new DocumentSegment.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { DocumentSegment._correspondenceFactType }));
		}
	}
}
