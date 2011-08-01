using System;
using System.Reflection;
using System.Windows.Controls;
using FacetedWorlds.MyCon.Presentation;
using UpdateControls.XAML;
using UpdateControls.XAML.Wrapper;

namespace FacetedWorlds.MyCon.Presentation.Navigation
{
    public class NavigationController
    {
        private MainPage _mainPage;
        private IObjectInstance _viewModel;

        public bool Initialized
        {
            get { return _viewModel != null; }
        }

        public void BeginMainPage(MainPage mainPage)
        {
            _mainPage = mainPage;
        }

        public void NavigateTo(object rawViewModel)
        {
            IObjectInstance viewModel = ForView.Wrap(rawViewModel);
            _viewModel = viewModel;
            DisplayView(viewModel);
        }

        private void DisplayView(IObjectInstance viewModel)
        {
            if (_mainPage != null)
            {
                UserControl view = CreateView(viewModel.WrappedObject.GetType());
                view.DataContext = viewModel;

                // TODO: Begin exit animation. Remove the child when animation completes.
                _mainPage.LayoutRoot.Children.Clear();

                _mainPage.LayoutRoot.Children.Add(view);
                // TODO: Begin enter animation.
            }
        }

        private static UserControl CreateView(Type viewModelType)
        {
            string viewModelTypeName = viewModelType.FullName;
            string viewTypeName = viewModelTypeName.Replace("ViewModel", "View");
            Assembly viewAssembly = viewModelType.Assembly;
            UserControl view = (UserControl)viewAssembly.CreateInstance(viewTypeName);
            if (view == null)
                throw new InvalidOperationException(String.Format("Define a view of type {0}.", viewTypeName));
            return view;
        }
    }
}
