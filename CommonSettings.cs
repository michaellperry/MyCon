using System;
using System.Text.RegularExpressions;

namespace FacetedWorlds.MyCon
{
    public static class CommonSettings
    {
        private const string _conferenceID = "<<Your conference ID>>";

        public static string ConferenceID
        {
            get
            {
                string conferenceID = _conferenceID;
                if (conferenceID == "<<Your conference ID>>")
                {
                    Regex punctuation = new Regex("[{}-]");
                    throw new InvalidOperationException(String.Format("Set the conference ID to \"{0}\".",
                        punctuation.Replace(Guid.NewGuid().ToString().ToUpper(), "")));
                }
                return conferenceID;
            }
        }
    }
}
