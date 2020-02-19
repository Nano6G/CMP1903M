using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1903M
{
    class Country
    {
        string name;
        double population;
        int vote;

        public Country(string CountryName, string CountryPopulation)
        {
            name = CountryName;
            population = Convert.ToDouble(CountryPopulation);
            vote = 0;
        }

        public string GetCountry()
        {
            return name;
        }

        public string GetCountryPop()
        {
            return Convert.ToString(population) + "%";
        }
    }

    class MainClass
    {
        static void Main()
        {
            string line;

            List<Country> Countrys = new List<Country>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willz\Desktop\OOP\CMP1903M\CMP1903M\Countries.txt");
            //System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "\\Countries.txt")); 
            //System.IO.StreamReader file = new System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Countries.txt"));

            //List<String> line_strings = new List<String>();

            while ((line = file.ReadLine()) != null)
            {
                //line_strings.Add(line);
                string[] CountryDetails = line.Split(",");
                Countrys.Add(new Country(CountryDetails[0], CountryDetails[1]));
            }

            Console.WriteLine("Country" + " ".PadLeft(13) + "Population");

            foreach(Country x in Countrys)
            {
                Console.WriteLine(x.GetCountry() + " ".PadLeft(20-x.GetCountry().Length) + x.GetCountryPop());
            }
        }
    }
}
