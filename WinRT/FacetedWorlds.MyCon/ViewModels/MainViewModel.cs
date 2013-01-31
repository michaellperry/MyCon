using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;
using UpdateControls.XAML;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private Community _community;
        private Identity _identity;

        public MainViewModel(Community community, Identity identity)
        {
            _community = community;
            _identity = identity;
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public string LastException
        {
            get
            {
                return _community.LastException == null
                    ? String.Empty
                    : _community.LastException.Message;
            }
        }
    }
}
