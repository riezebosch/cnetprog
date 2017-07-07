using System.Collections.Generic;

namespace SeriesApi.Model
{
    public class Season
    {
        public int Year { get; set; }
        public IList<Episode> Episodes { get; set; }
        public int Id { get; set; }
    }
}