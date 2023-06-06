using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("Error, 0 lines");
            }

            if (lines.Length == 1)
            {
                logger.LogWarning("Warning, Only one line");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double testDistance = 0;
            double finalDistance = 0;
            var coorA = new GeoCoordinate();
            var coorB = new GeoCoordinate();

            for (int i = 0; i < locations.Length; i++)
            {
                coorA.Latitude = locations[i].Location.Latitude;
                coorA.Longitude = locations[i].Location.Longitude;

                for (int j = 1; j < locations.Length; j++)
                {
                    coorB.Latitude = locations[j].Location.Latitude;
                    coorB.Longitude = locations[j].Location.Longitude;

                    testDistance = coorB.GetDistanceTo(coorA);

                    if (finalDistance < testDistance)
                    {
                        finalDistance = testDistance;
                        tacoBell1 = locations[i];
                        tacoBell2 = locations[j];
                        Console.WriteLine($"The distance between {tacoBell1.Name} and {tacoBell2.Name} is {testDistance * 0.00062137}miles.");
                    }
                }
            }

            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart and the distance is {finalDistance} meters or {finalDistance * 0.00062137} miles.");
            Console.WriteLine();
        }
    }
}
