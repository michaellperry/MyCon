using System;
using System.Collections.Generic;
using System.Linq;
using FacetedWorlds.MyCon.ImageUtilities;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class SessionEvaluationViewModel
    {
        private readonly SessionEvaluation _sessionEvaluation;
        private readonly ImageCache _imageCache;
        
        public SessionEvaluationViewModel(SessionEvaluation sessionEvaluation, ImageCache imageCache)
        {
            _sessionEvaluation = sessionEvaluation;
            _imageCache = imageCache;
        }

        public string ImageUrl
        {
            get { return _imageCache.LargeImageUrl(_sessionEvaluation.Schedule.SessionPlace.Session.Speaker.ImageUrl); }
        }

        public string Title
        {
            get { return _sessionEvaluation.Schedule.SessionPlace.Session.Name; }
        }

        public string Speaker
        {
            get { return _sessionEvaluation.Schedule.SessionPlace.Session.Speaker.Name; }
        }

        public IEnumerable<RatingViewModel> Ratings
        {
            get
            {
                return
                    from ratingQuestion in _sessionEvaluation.Survey.RatingQuestions
                    select new RatingViewModel(_sessionEvaluation.Rating(ratingQuestion));
            }
        }

        public IEnumerable<EssayViewModel> Essays
        {
            get
            {
                return
                    from essayQuestion in _sessionEvaluation.Survey.EssayQuestions
                    select new EssayViewModel(_sessionEvaluation.Essay(essayQuestion));
            }
        }
    }
}
