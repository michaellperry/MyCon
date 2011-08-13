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

            _viewModels.Add(new TitleViewModel());
            _viewModels.Add(new BulletPointViewModel("Michael L Perry")
                .AddBullet("qedcode.com")
                .AddBullet("@MichaelLPerry")
                .AddBullet("Update Controls")
                .AddBullet("Correspondence"));
            _viewModels.Add(new BulletPointViewModel("Occasionally-connected clients Windows Phone")
                .AddBullet("Why?")
                .AddBullet("Architecture")
                .AddBullet("Correspondence")
                .AddBullet("Next steps"));
            _viewModels.Add(new BulletPointViewModel("Problems with phone apps")
                .AddBullet("Long delays waiting for data")
                .AddBullet("Error messages on failure to connect")
                .AddBullet("Refresh buttons"));
            _viewModels.Add(new BulletPointViewModel("Occasionally-connected solutions")
                .AddBullet("Local storage")
                .AddBullet("Change queue")
                .AddBullet("Push notification"));
            _viewModels.Add(new ArchitectureViewModel());
            _viewModels.Add(new BulletPointViewModel("The Community is ...")
                .AddBullet("Users")
                .AddBullet("Devices")
                .AddBullet("Data")
                .AddBullet("Services"));
            _viewModels.Add(new BulletPointViewModel("The Community ...")
                .AddBullet("Calls strategies")
                .AddBullet("Caches facts")
                .AddBullet("Subscribes to queues")
                .AddBullet("Is model agnostic"));
            _viewModels.Add(new FactsViewModel());
            _viewModels.Add(new BulletPointViewModel("Next Steps")
                .AddBullet("qedcode.com/correspondence")
                .AddBullet("NuGet Correspondence. WindowsPhone.AllInOne")
                .AddBullet("Correspondence.CodePlex.com"));
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
