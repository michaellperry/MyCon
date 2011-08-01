using System;

namespace FacetedWorlds.MyCon.Presentation.ViewModels
{
    public class BulletViewModel
    {
        public enum StateId
        {
            Hidden,
            Visible
        }

        private string _text;
        private Func<bool> _isVisible;

        public BulletViewModel(string text, Func<bool> isVisible)
        {
            _text = text;
            _isVisible = isVisible;
        }

        public string Text
        {
            get { return _text; }
        }

        public StateId State
        {
            get { return _isVisible() ? StateId.Visible : StateId.Hidden; }
        }
    }
}
