using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1903M
{
    class Country
    {
        string name;
        double population;
        string vote;

        public Country(string CountryName, string CountryPopulation)
        {
            name = CountryName;
            population = Convert.ToDouble(CountryPopulation);
            vote = "Abstain";
        }

        public List<String> GetCountryInfo()
        {
            string StringPopulation = Convert.ToString(population);
            List<string> CountryInfo = new List<string>() { name, StringPopulation, vote };
            return CountryInfo;
        }
    }

    class MainClass
    {
        static void Main()
        {
            List<Country> CountryObjects = SetupCountryObjects();

            Console.WriteLine("EU Voting Calculator");
            Console.WriteLine("1:\tView Countries, Population and Votes\n2:\tChange a Countries Vote\n3:\tCheck Vote Status\n\nEnter your option:");
            string UserOption = Console.ReadLine();

            if (UserOption == "1")
            {
                DisplayCountries(CountryObjects);
            }
            else if (UserOption == "2")
            {

            }
            else if (UserOption == "3")
            {

            }
            else
            {
                Console.WriteLine("Please enter a valid option");
                Main();
            }

        }
        
         
        static List<Country> SetupCountryObjects()
        {
            string line;

            List<Country> CountryObjects = new List<Country>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willz\Desktop\OOP Assignment 1\CMP1903M\CMP1903M\Countries.txt");
            //System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "\\Countries.txt")); 
            //System.IO.StreamReader file = new System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Countries.txt"));

            //List<String> line_strings = new List<String>();

            while ((line = file.ReadLine()) != null)
            {
                //line_strings.Add(line);
                string[] CountryDetails = line.Split(",");
                CountryObjects.Add(new Country(CountryDetails[0], CountryDetails[1]));
            }

            return CountryObjects;
        }
        

        static void DisplayCountries(List<Country> CountryObjects)
        {
            Console.WriteLine("\nCountry" + " ".PadLeft(13) + "Population" + " ".PadLeft(5) + "Vote");
            foreach (Country x in CountryObjects)
            {
                Console.WriteLine(x.GetCountryInfo()[0] + " ".PadLeft(20 - x.GetCountryInfo()[0].Length) + x.GetCountryInfo()[1] + " ".PadLeft(15 - x.GetCountryInfo()[1].Length) + x.GetCountryInfo()[2]);
            }
        }
    }
}
