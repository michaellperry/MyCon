using System;
using UpdateControls;

namespace FacetedWorlds.MyCon
{
    public class Update : IUpdatable
    {
        private Dependent _dependent;

        private Update(Dependent dependent)
        {
            _dependent = dependent;
        }

        public void UpdateNow()
        {
            _dependent.OnGet();
        }

        public static Dependent WhenNecessary(Action updateMethod)
        {
            var dependent = new Dependent(updateMethod);
            Update update = new Update(dependent);
            dependent.Invalidated += delegate
            {
                UpdateScheduler.ScheduleUpdate(update);
            };
            dependent.OnGet();
            return dependent;
        }
    }
}
