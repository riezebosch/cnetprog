using System.Collections.Generic;

namespace SeriesApi.Model
{
    public class Serie
    {
        public string Title { get; set; }
        public IList<Season> Seasons { get; set; }
        public int Id { get; set; }
    }
}