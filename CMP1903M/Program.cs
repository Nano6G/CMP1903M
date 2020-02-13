using System;

namespace CMP1903M
{
    class Country
    {
        string name;
        float population;

        public void SetCountry(string CountryName, float CountryPopulation)
        {
            name = CountryName;
            population = CountryPopulation;
        }

        public string GetCountry()
        {
            return name;
        }
    }
}
