using System;
using System.Linq;
using System.Windows;
using FacetedWorlds.MyCon.Model;
using UpdateControls.Correspondence;

namespace FacetedWorlds.MyCon.ViewModels
{
    public class MainViewModel
    {
        private Community _community;
        private Individual _individual;

        public MainViewModel(Community community, Individual individual)
        {
            _community = community;
            _individual = individual;
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
