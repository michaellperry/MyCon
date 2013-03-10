using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FacetedWorlds.MyCon.SampleData
{
    public class SampleSessionDataSource
    {
        public class SessionData
        {
            public string Title { get; set; }
            public ImageSource Image { get; set; }
            public string Day { get; set; }
            public string Time { get; set; }
            public string Room { get; set; }
            public string Track { get; set; }
            public string Description { get; set; }
            public string Speaker { get; set; }
            public string SpeakerBio { get; set; }
            public Visibility AddVisible { get; set; }
            public Visibility RemoveVisible { get; set; }
            public Brush StatusBrush { get; set; }
        }

        public SampleSessionDataSource()
        {
            Session = new SessionData
            {
                Title = "The Awesomeness Factor: Taking your Greatness to 11",
                //Image = new BitmapImage(new Uri("http://qedcode.com/extras/Perry_Headshot_Medium.jpg", UriKind.Absolute)),
                Day = "Sat",
                Time = "2:30",
                Room = "Room: Vestibule on the Square",
                Track = "Architecture",
                Description = "When you are this awesome, it can't get any better, right?\n\nWrong!\n\nYou can always get more awesome! We'll show you how. You can multiply your greatness by the awesome factor. This will make you even more awesome than you already are. We'll give you three actionable recommendations for cranking up your greatness to 11.\n\nGo team awesome!",
                Speaker = "Michael L Perry",
                SpeakerBio = "Software is math. Michael L Perry has built upon the works of mathematicians like Bertrand Meyer, James Rumbaugh, and Donald Knuth to develop a mathematical system for software development. He has captured this system in a set of open source projects, Update Controls and Correspondence. As a Principal Consultant at Improving Enterprises, he applies mathematical concepts to building scalable and robust enterprise systems. You can find out more at qedcode.com.",
                AddVisible = Visibility.Visible,
                RemoveVisible = Visibility.Collapsed
                //StatusBrush = Application.Current.Resources["UnscheduledStatusBrush"] as Brush
            };
        }

        public SessionData Session { get; private set; }

        public static object MySchedule
        {
            get
            {
                return new
                {
                    Test = "Hello!",
                    Days = new object[]
                    {
                        new
                        {
                            Day = "Saturday, March 16",
                            Slots = new object[]
                            {
                                new
                                {
                                    Time = "8:30",
                                    Schedules = new object[]
                                    {
                                        new
                                        {
                                            Title = "The Awesomeness Factor: Taking your Greatness to 11",
                                            Speaker = "Michael L Perry",
                                            Room = "Room: Vestibule on the Square",
                                            Scheduled = true,
                                            Overbooked = false,
                                            IsSelected = Visibility.Visible
                                        }
                                    }
                                },
                                new
                                {
                                    Time = "10:00",
                                    Schedules = new object[]
                                    {
                                        new
                                        {
                                            Title = "Another Awesome Session",
                                            Speaker = "Caleb Jenkins",
                                            Room = "Room: The Slarty Bart Fast",
                                            Scheduled = true,
                                            Overbooked = false,
                                            IsSelected = Visibility.Collapsed
                                        }
                                    }
                                },
                                new
                                {
                                    Time = "11:30",
                                    Schedules = new object[]
                                    {
                                        new
                                        {
                                            Title = "Lunch",
                                            Speaker = "Lunch",
                                            Room = "Room: The lunch room",
                                            Scheduled = false,
                                            Overbooked = false,
                                            IsSelected = Visibility.Collapsed
                                        }
                                    }
                                },
                                new
                                {
                                    Time = "1:00",
                                    Schedules = new object[]
                                    {
                                        new
                                        {
                                            Title = "Awesome Choice The First",
                                            Speaker = "Teresa Burger",
                                            Room = "Room: The OMG!",
                                            Scheduled = true,
                                            Overbooked = true,
                                            IsSelected = Visibility.Collapsed
                                        },
                                        new
                                        {
                                            Title = "Awesome Choice The Second",
                                            Speaker = "Robert Burger",
                                            Room = "Room: The WAT!",
                                            Scheduled = true,
                                            Overbooked = true,
                                            IsSelected = Visibility.Collapsed
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }

        public static object AvailableSessions
        {
            get
            {
                return new
                {
                    Sessions = new object[]
                    {
                        new
                        {
                            Title = "The Awesomeness Factor: Taking your Greatness to 11",
                            Speaker = "Michael L Perry",
                            Room = "Room: Vestibule on the Square",
                            StatusBrush = Application.Current.Resources["ScheduledStatusBrush"] as Brush
                        },
                        new
                        {
                            Title = "Some Other Session",
                            Speaker = "That Guy",
                            Room = "Room: Somewhere Else",
                            StatusBrush = Application.Current.Resources["UnscheduledStatusBrush"] as Brush
                        },
                        new
                        {
                            Title = "Where Do They Get These People?!?",
                            Speaker = "OMG Stanley",
                            Room = "Room: The Other Side",
                            StatusBrush = Application.Current.Resources["UnscheduledStatusBrush"] as Brush
                        }
                    }
                };
            }
        }
    }
}
