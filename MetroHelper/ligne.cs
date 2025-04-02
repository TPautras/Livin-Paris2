using System.Collections.Generic;

namespace MetroHelper
{
    public class ligne
    {
        private string numeroligne;
        private List<Station_de_metro> stations;

        public ligne(string numeroligne, List<Station_de_metro> stations)
        {
            this.numeroligne = numeroligne;
        
            this.stations = stations;
        }
    
    }
}