﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FacetedWorlds.MyCon.Model
{
    public partial class Conference
    {
        public Time GetTime(DateTime startTime)
        {
            Day day = Community.AddFact(new Day(this, startTime.Date));
            return Community.AddFact(new Time(day, startTime));
        }

        public Speaker GetSpeaker(string speakerName)
        {
            return Community.AddFact(new Speaker(this, speakerName));
        }

        public Speaker NewSpeaker(string speakerName, string contact, string bio, string imageUrl)
        {
            Speaker speaker = GetSpeaker(speakerName);
            if (speaker.ImageUrl.Value != imageUrl)
                speaker.ImageUrl = imageUrl;
            List<DocumentSegment> bioSegments = DocumentSegments(bio);
            if (!SegmentsEqual(speaker.Bio.Value, bioSegments))
                speaker.Bio = bioSegments;
            if (speaker.Contact != contact && !String.IsNullOrEmpty(contact))
                speaker.Contact = contact;
            return speaker;
        }

        public Session NewSession(string sessionId, string sessionName, string trackName, Speaker speaker, string level, string description)
        {
            Track track = trackName == null ? null : Community.AddFact(new Track(this, trackName));
            Session session = Community.AddFact(new Session(this, speaker, track, sessionId));
            if (session.Name.Value != sessionName)
                session.Name = sessionName;
            var descriptionSegments = DocumentSegments(description);
            if (!SegmentsEqual(session.Description.Value, descriptionSegments))
                session.Description = descriptionSegments;
            if (!String.IsNullOrEmpty(level))
            {
                Level l = Community.AddFact(new Level(level));
                if (session.Level.Value != l)
                    session.Level = l;
            }
            return session;
        }

        public void NewSessionPlace(string sessionId, string sessionName, string description, string speakerName, string contact, string bio, string imageUrl, string trackName, DateTime startTime, string roomNumber)
        {
            Speaker speaker = NewSpeaker(speakerName, contact, bio, imageUrl);
            Session session = NewSession(sessionId, sessionName, trackName, speaker, null, description);
            Time time = GetTime(startTime);
            Room room = Community.AddFact(new Room(this, roomNumber));
            Place place = Community.AddFact(new Place(time, room));
            Community.AddFact(new SessionPlace(session, place, new List<SessionPlace>()));
        }

        public void NewGeneralSessionPlace(string sessionId, string sessionName, Time time, string roomNumber)
        {
            Speaker speaker = Community.AddFact(new Speaker(this, string.Empty));
            Session session = Community.AddFact(new Session(this, speaker, null, sessionId));
            if (session.Name.Value != sessionName)
                session.Name = sessionName;
            Room room = Community.AddFact(new Room(this, roomNumber));
            Place place = Community.AddFact(new Place(time, room));
            Community.AddFact(new SessionPlace(session, place, new List<SessionPlace>()));
        }

        public List<DocumentSegment> DocumentSegments(string text)
        {
            List<DocumentSegment> segments = new List<DocumentSegment>();
            while (!String.IsNullOrEmpty(text))
            {
                int segmentLength = Math.Min(512, text.Length);
                segments.Add(Community.AddFact(new DocumentSegment(text.Substring(0, segmentLength))));
                text = text.Substring(segmentLength);
            }
            return segments;
        }

        public bool SegmentsEqual(IEnumerable<DocumentSegment> a, IEnumerable<DocumentSegment> b)
        {
            if (a == null)
                return b == null;
            if (b == null)
                return false;

            IEnumerator<DocumentSegment> aEnum = a.GetEnumerator();
            IEnumerator<DocumentSegment> bEnum = b.GetEnumerator();
            bool aNext = aEnum.MoveNext();
            bool bNext = bEnum.MoveNext();
            while (aNext && bNext)
            {
                if (aEnum.Current != bEnum.Current)
                    return false;
                aNext = aEnum.MoveNext();
                bNext = bEnum.MoveNext();
            }
            if ((aNext && !bNext) || bNext && !aNext)
                return false;
            return true;
        }

        public void NewSurvey(List<string> ratingQuestionsText, List<string> essayQuestionsText)
        {
            List<RatingQuestion> ratingQuestions = ratingQuestionsText
                .Select(text => Community.AddFact(new RatingQuestion(text)))
                .ToList();
            List<EssayQuestion> essayQuestions = essayQuestionsText
                .Select(text => Community.AddFact(new EssayQuestion(text)))
                .ToList();
            Survey survey = Community.AddFact(new Survey(ratingQuestions, essayQuestions));
            List<ConferenceSessionSurvey> currentSessionSurveys = CurrentSessionSurveys.ToList();
            if (currentSessionSurveys.Count == 1 && currentSessionSurveys.Single().SessionSurvey != survey)
            {
                Community.AddFact(new ConferenceSessionSurvey(this, survey, currentSessionSurveys));
            }
        }
    }
}
