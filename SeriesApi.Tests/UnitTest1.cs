using Microsoft.EntityFrameworkCore;
using SeriesApi.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace SeriesApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<SeriesContext>()
                .UseSqlite(@"File=.\data.db")
                .Options;
            using (var context = new SeriesContext(options))
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
                            Year = 2014,
                            Episodes = new List<Episode>
                            {
                                new Episode
                                {
                                    Id = 0,
                                    Title = "Study in Pink"
                                }
                            }
                        }
                    }
                });
            }
        }
    }
}
