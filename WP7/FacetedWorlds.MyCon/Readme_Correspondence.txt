Create a ViewModelLocator in App.xaml:

<Application
    ...
    xmlns:vm="clr-namespace:FacetedWorlds.MyCon.ViewModels">

    <!--Application Resources-->
    <Application.Resources>
        <vm:ViewModelLocator x:Key="Locator"/>
    </Application.Resources>

Reference it in MainPage.xaml:

<phone:PhoneApplicationPage 
    ...
    DataContext="{Binding Main, Source={StaticResource Locator}}">
