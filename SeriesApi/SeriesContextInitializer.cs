using SeriesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriesApi
{
    public static class SeriesContextInitializer
    {
        public static SeriesContext InitializeData(this SeriesContext context)
        {
            context.Series.Add(new Serie
            {
                Id = 0,
                Title = "Sherlock Holmes",
                Seasons = new List<Season>
                {
                    new Season
                    {
                        Id = 0,
                        Year = 2010,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "Study in Pink"
                            },
                            new Episode
                            {
                                Title = "The Blind Banker"
                            },
                            new Episode
                            {
                                Title = "The Great Game"
                            },
                        }
                    },
                    new Season
                    {
                        Id = 0,
                        Year = 2012,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "A Scandal in Belgravia"
                            },
                            new Episode
                            {
                                Title = "The Hounds of Baskerville"
                            },
                            new Episode
                            {
                                Title = "The Reichenbach Fall"
                            },
                        }
                    },
                    new Season
                    {
                        Id = 0,
                        Year = 2014,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "Many Happy Returns"
                            },
                            new Episode
                            {
                                Title = "The Empty Hearse"
                            },
                            new Episode
                            {
                                Title = "The Sign of Three"
                            },
                            new Episode
                            {
                                Title = "His Last Vow"
                            },

                        }
                    },
                    new Season
                    {
                        Year = 2016,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "The Abominable Bride"
                            }
                        }
                    },
                    new Season
                    {
                        Id = 0,
                        Year = 2017,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "The Six Thatchers"
                            },
                            new Episode
                            {
                                Title = "The Lying Detective"
                            },
                            new Episode
                            {
                                Title = "The Final Problem"
                            },
                        }
                    },
                }
            });

            context.Series.Add(new Serie
            {
                Id = 0,
                Title = "Elementary",
                Seasons = new List<Season>
                {
                    new Season
                    {
                        Id = 0,
                        Year = 2012,
                        Episodes = new List<Episode>
                        {
                            new Episode
                            {
                                Title = "Pilot"
                            },
                            new Episode
                            {
                                Title = "While You Were Sleeping"
                            },
                            new Episode
                            {
                                Title = "Child Predator"
                            },
                            new Episode
                            {
                                Title = "Rat Race"
                            },
                            new Episode
                            {
                                Title = "Lesser Evils"
                            },
                            new Episode
                            {
                                Title = "Flight Risk"
                            },
                            new Episode
                            {
                                Title = "One Way to Get Off"
                            },
                        }
                    }
                }
            });

            context.SaveChanges();
            return context;
        }
    }
}
