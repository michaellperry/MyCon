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
    ConferenceHeader__location -> ConferenceHeader
    ConferenceHeader__location -> ConferenceHeader__location [label="  *"]
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

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results

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
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

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
        private static Query _cacheQueryLocation;

        public static Query GetQueryLocation()
		{
            if (_cacheQueryLocation == null)
            {
			    _cacheQueryLocation = new Query()
    				.JoinSuccessors(ConferenceHeader__location.GetRoleConferenceHeader(), Condition.WhereIsEmpty(ConferenceHeader__location.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryLocation;
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
        private Result<ConferenceHeader__location> _location;

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
            _location = new Result<ConferenceHeader__location>(this, GetQueryLocation(), ConferenceHeader__location.GetUnloadedInstance, ConferenceHeader__location.GetNullInstance);
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
        public TransientDisputable<ConferenceHeader__location, string> Location
        {
            get { return _location.AsTransientDisputable(fact => fact.Value); }
			set
			{
				var current = _location.Ensure().ToList();
				if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
				{
					Community.AddFact(new ConferenceHeader__location(this, _location, value.Value));
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
    
    public partial class ConferenceHeader__location : CorrespondenceFact
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
				ConferenceHeader__location newFact = new ConferenceHeader__location(memento);

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
				ConferenceHeader__location fact = (ConferenceHeader__location)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ConferenceHeader__location.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ConferenceHeader__location.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"FacetedWorlds.MyCon.Model.ConferenceHeader__location", 1696361312);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ConferenceHeader__location GetUnloadedInstance()
        {
            return new ConferenceHeader__location((FactMemento)null) { IsLoaded = false };
        }

        public static ConferenceHeader__location GetNullInstance()
        {
            return new ConferenceHeader__location((FactMemento)null) { IsNull = true };
        }

        public ConferenceHeader__location Ensure()
        {
            if (_loadedTask != null)
            {
                ManualResetEvent loaded = new ManualResetEvent(false);
                ConferenceHeader__location fact = null;
                _loadedTask.ContinueWith(delegate(Task<CorrespondenceFact> t)
                {
                    fact = (ConferenceHeader__location)t.Result;
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
			        ConferenceHeader__location._correspondenceFactType,
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
		    		.JoinSuccessors(ConferenceHeader__location.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ConferenceHeader> _conferenceHeader;
        private PredecessorList<ConferenceHeader__location> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ConferenceHeader__location(
            ConferenceHeader conferenceHeader
            ,IEnumerable<ConferenceHeader__location> prior
            ,string value
            )
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), conferenceHeader);
            _prior = new PredecessorList<ConferenceHeader__location>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ConferenceHeader__location(FactMemento memento)
        {
            InitializeResults();
            _conferenceHeader = new PredecessorObj<ConferenceHeader>(this, GetRoleConferenceHeader(), memento, ConferenceHeader.GetUnloadedInstance, ConferenceHeader.GetNullInstance);
            _prior = new PredecessorList<ConferenceHeader__location>(this, GetRolePrior(), memento, ConferenceHeader__location.GetUnloadedInstance, ConferenceHeader__location.GetNullInstance);
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
        public PredecessorList<ConferenceHeader__location> Prior
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
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
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
				ConferenceHeader.GetQueryLocation().QueryDefinition);
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
				ConferenceHeader__location._correspondenceFactType,
				new ConferenceHeader__location.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeader__location._correspondenceFactType }));
			community.AddQuery(
				ConferenceHeader__location._correspondenceFactType,
				ConferenceHeader__location.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ConferenceHeaderDelete._correspondenceFactType,
				new ConferenceHeaderDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ConferenceHeaderDelete._correspondenceFactType }));
			community.AddType(
				Conference._correspondenceFactType,
				new Conference.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Conference._correspondenceFactType }));
		}
	}
}
