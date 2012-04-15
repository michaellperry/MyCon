using System;
using System.Text.RegularExpressions;

namespace FacetedWorlds.MyCon
{
    public static class CommonSettings
    {
        private const string _conferenceID = "B313BE8C72EC4F06BD55E332F491FB34";

        public static string ConferenceID
        {
            get
            {
                if (_conferenceID == "<<Your conference ID>>")
                {
                    Regex punctuation = new Regex("[{}-]");
                    throw new InvalidOperationException(String.Format("Set the conference ID to \"{0}\".",
                        punctuation.Replace(Guid.NewGuid().ToString().ToUpper(), "")));
                }
                return _conferenceID;
            }
        }
    }
}
