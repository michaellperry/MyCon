using System.Collections.Generic;
using System.Linq;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Presentation.ViewModels
{
    public class BulletPointViewModel : IPresentationViewModel
    {
        private string _title;
        private List<string> _bullets = new List<string>();

        private Independent<int> _index = new Independent<int>();

        public BulletPointViewModel(string title)
        {
            _title = title;
        }

        public BulletPointViewModel AddBullet(string bullet)
        {
            _bullets.Add(bullet);
            return this;
        }

        public string Title
        {
            get { return _title; }
        }

        public IEnumerable<BulletViewModel> Bullets
        {
            get
            {
                return _bullets
                    .Select((text, index) => new BulletViewModel(text, () => _index.Value > index));
            }
        }

        public bool Forward()
        {
            if (_index < _bullets.Count)
            {
                _index.Value = _index.Value + 1;
                return true;
            }
            else
                return false;
        }

        public bool Backward()
        {
            if (_index > 0)
            {
                _index.Value = _index.Value - 1;
                return true;
            }
            else
                return false;
        }
    }
}
