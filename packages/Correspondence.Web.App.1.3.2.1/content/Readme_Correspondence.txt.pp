Create a SynchronizationService in Global.asax.cs:

    public class MvcApplication : System.Web.HttpApplication
    {
        private static SynchronizationService _synchronizationService = new SynchronizationService();

        public static SynchronizationService SynchronizationService
        {
            get { return _synchronizationService; }
        }

		...

        protected void Application_Start()
        {
            ...
            SynchronizationService.Initialize();
        }
    }
