using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.Presentation.ViewModels;

namespace FacetedWorlds.MyCon.Presentation.Navigation
{
    public class NavigationGraph
    {
        private NavigationController _controller;
        private List<IPresentationViewModel> _viewModels = new List<IPresentationViewModel>();
        private int _index = 0;

        public NavigationGraph(NavigationController controller)
        {
            _controller = controller;

            _viewModels.Add(new BulletPointViewModel("Occasionally Connected Clients")
                .AddBullet("View data off-line")
                .AddBullet("Make changes off-line")
                .AddBullet("Work with a subset of data")
                .AddBullet("Store and forward")
                .AddBullet("Propogate changes to the user"));
            _viewModels.Add(new BulletPointViewModel("Correspondence")
                .AddBullet("Local storage")
                .AddBullet("Change queue")
                .AddBullet("Synchronization service")
                .AddBullet("Push notification")
                .AddBullet("Conflict detection"));
            _viewModels.Add(new BulletPointViewModel("Correspondence is Not")
                .AddBullet("Object oriented")
                .AddBullet("Relational")
                .AddBullet("An ORM"));
            _viewModels.Add(new BulletPointViewModel("Key Concepts")
                .AddBullet("Facts")
                .AddBullet("Storage strategy")
                .AddBullet("Communication strategy")
                .AddBullet("Community"));
            _viewModels.Add(new FactsViewModel());
            _viewModels.Add(new BulletPointViewModel("Storage Strategy")
                .AddBullet("Stores facts")
                .AddBullet("Executes queries")
                .AddBullet("Manages queues")
                .AddBullet("Model agnostic"));
            _viewModels.Add(new BulletPointViewModel("Communication Strategy")
                .AddBullet("Sends facts")
                .AddBullet("Receives facts")
                .AddBullet("Listens for push notifications")
                .AddBullet("Model agnostic"));
            _viewModels.Add(new BulletPointViewModel("Community")
                .AddBullet("Calls strategies")
                .AddBullet("Caches facts")
                .AddBullet("Subscribes to queues")
                .AddBullet("Model agnostic"));
            _viewModels.Add(new ArchitectureViewModel());
            _viewModels.Add(new BulletPointViewModel("Roadmap")
                .AddBullet("Model browser")
                .AddBullet("Security")
                .AddBullet("Self hosting")
                .AddBullet("Tiered service plans")
                .AddBullet("Enterprise tools"));
            _viewModels.Add(new BulletPointViewModel("Next Steps")
                .AddBullet("NuGet Correspondence. Silverlight.AllInOne")
                .AddBullet("http://Correspondence. CodePlex.com")
                .AddBullet("mperry@mallardsoft.com")
                .AddBullet("http://HistoricalModeling.com"));
        }

        public void Start()
        {
            _controller.NavigateTo(_viewModels.First());
        }

        public void Forward()
        {
            bool navigated = _viewModels[_index].Forward();
            if (!navigated && _index < _viewModels.Count - 1)
            {
                _index++;
                _controller.NavigateTo(_viewModels[_index]);
            }
        }

        public void Backward()
        {
            bool navigated = _viewModels[_index].Backward();
            if (!navigated && _index > 0)
            {
                _index--;
                _controller.NavigateTo(_viewModels[_index]);
            }
        }
    }
}
