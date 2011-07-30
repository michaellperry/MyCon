using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacetedWorlds.MyCon.Model;

namespace FacetedWorlds.MyCon.Capture
{
    class DataLoader
    {
        private Dictionary<string, Time> _timeById = new Dictionary<string, Time>();
        private Dictionary<string, Speaker> _speakerById = new Dictionary<string, Speaker>();
        private Dictionary<string, Track> _trackById = new Dictionary<string, Track>();
        private Dictionary<string, Room> _roomById = new Dictionary<string, Room>();

        public void Load(Conference conference)
        {
        }
    }
}
