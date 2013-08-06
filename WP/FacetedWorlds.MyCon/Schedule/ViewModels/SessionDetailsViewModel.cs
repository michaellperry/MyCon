using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacetedWorlds.MyCon.Schedule.ViewModels
{
    public interface SessionDetailsViewModel
    {
        bool CanAdd { get; set; }

        void Add();

        bool CanRemove { get; set; }

        void Remove();

        bool CanEvaluate { get; set; }

        string SessionEvaluationUri { get; set; }

        string SearchBySpeakerText { get; set; }

        bool CanSearchBySpeaker { get; set; }

        string SpeakerId { get; set; }

        string SearchByTrackText { get; set; }

        bool CanSearchByTrack { get; set; }

        void SearchByTrack();

        string SearchByTimeText { get; set; }

        bool CanSearchByTime { get; set; }

        string SearchByTimeUri { get; set; }

        bool ShouldPromptForPushNotification();

        string GetConferenceName();
    }
}
