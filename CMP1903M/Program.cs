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

        public Country(string CountryName, double CountryPopulation, int CountryVote)
        {
            name = CountryName;
            population = CountryPopulation;
            vote = CountryVote;
        }

        public string GetCountry()
        {
            return name;
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

            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
            }
        }
    }
}
