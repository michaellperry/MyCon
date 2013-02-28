using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacetedWorlds.MyCon.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace FacetedWorlds.MyCon.Tests
{
    [TestClass]
    public class ModelTest
    {
        private Community _communityFlynn;
        private Community _communityAlan;
        private Individual _individualFlynn;
        private Individual _individualAlan;

        [TestInitialize]
        public async Task Initialize()
        {
            var sharedCommunication = new MemoryCommunicationStrategy();
            _communityFlynn = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualFlynn)
				;
            _communityAlan = new Community(new MemoryStorageStrategy())
                .AddCommunicationStrategy(sharedCommunication)
                .Register<CorrespondenceModel>()
                .Subscribe(() => _individualAlan)
                ;

            _individualFlynn = await _communityFlynn.AddFactAsync(new Individual("flynn"));
            _individualAlan = await _communityAlan.AddFactAsync(new Individual("alan"));
        }

        private async Task Synchronize()
        {
            while (await _communityFlynn.SynchronizeAsync() || await _communityAlan.SynchronizeAsync()) ;
        }
	}
}
