namespace LoggingKata
{
    /// Parses a POI file to locate all the Taco Bells
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin Parsing . . .");
            if(line == null)
            {
                logger.LogWarning("null line");
                return null;
            }

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("not enough info");

                return null;
            }

            var latitude = double.Parse(cells[0]);
            var longitude = double.Parse(cells[1]);
            var name = cells[2];

            var tacoBell = new TacoBell(name, latitude, longitude);
            //tacoBell.Name = name;

            //var point = new Point();
            //point.Longitude = longitude;
            //point.Latitude = latitude;
            //tacoBell.Location = point;

            logger.LogInfo($"Name: {tacoBell.Name} | Lat: {tacoBell.Location.Latitude} | Long: {tacoBell.Location.Longitude}");

            return tacoBell;
        }
    }
}