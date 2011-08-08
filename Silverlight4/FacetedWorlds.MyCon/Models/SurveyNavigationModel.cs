using System;
using System.Linq;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Models
{
    public class SurveyNavigationModel
    {
        private Independent<string> _selectedRatingQuestion = new Independent<string>();
        private Independent<string> _selectedEssayQuestion = new Independent<string>();
        private Independent<string> _newQuestion = new Independent<string>();

        public string SelectedRatingQuestion
        {
            get { return _selectedRatingQuestion; }
            set { _selectedRatingQuestion.Value = value; }
        }

        public string SelectedEssayQuestion
        {
            get { return _selectedEssayQuestion; }
            set { _selectedEssayQuestion.Value = value; }
        }

        public string NewQuestion
        {
            get { return _newQuestion; }
            set { _newQuestion.Value = value; }
        }
    }
}
