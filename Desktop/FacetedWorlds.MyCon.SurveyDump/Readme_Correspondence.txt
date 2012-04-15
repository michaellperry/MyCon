Create a ViewModelLocator in App.xaml:

<Application ...
             xmlns:vm="clr-namespace:FacetedWorlds.MyCon.SurveyDump.ViewModels">

    <Application.Resources>
        <vm:ViewModelLocator x:Key="Locator"/>
    </Application.Resources>

Reverence it in MainWindow.xaml:

<Window 
        ...
        DataContext="{Binding Main, Source={StaticResource Locator}}">
