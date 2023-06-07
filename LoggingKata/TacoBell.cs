using System;
namespace LoggingKata
{
	public class TacoBell : ITrackable
	{
		public TacoBell(string name, double lat, double lon)
		{
			Name = name;
			Location = new Point(lat, lon);
		}

        public string Name { get; set; }
        public Point Location { get; set; }
    }
}

