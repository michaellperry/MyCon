using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionEvaluationViewModel
    {
        private readonly Schedule _schedule;
        private readonly Survey _survey;
        private readonly ImageCache _imageCache;

        public SessionEvaluationViewModel(Schedule schedule, Survey survey, ImageCache imageCache)
        {
            _schedule = schedule;
            _survey = survey;
            _imageCache = imageCache;
        }
    }
}
