using System.Collections.Generic;
using System.IO;
using System.Linq;
using Graphs;
namespace MetroHelper
{
    public class GrandeStation
    {
        private List<Station_de_metro> stations;
        private int latitude;
        private int longitude;

        public GrandeStation(List<Station_de_metro> stations,  int latitude, int longitude)
        {
            this.stations = stations;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public List<Station_de_metro> Stations
        {
            get { return stations; }
            set { stations = value; }
        }

        public int Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public int Longitude
        {
            get { return longitude; } 
            set { longitude = value; }
        }
    }
}