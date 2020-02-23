using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1903M
{
    class Country
    {
        private string Name;
        private double Population;
        public string Vote;

        public Country(string CountryName, string CountryPopulation)
        {
            Name = CountryName;
            Population = Convert.ToDouble(CountryPopulation);
            Vote = "Abstain";
        }

        public List<String> GetCountryInfo()
        {
            string StringPopulation = Convert.ToString(Population);
            List<string> CountryInfo = new List<string>() { Name, StringPopulation, Vote };
            return CountryInfo;
        }

        public void ChangeVote()
        {
            Console.WriteLine("Enter the vote of this country (Yes/No/Abstain):");
            string CountryVote = Console.ReadLine().ToLower();
            if (CountryVote == "yes")
            {
                Vote = "Yes";
            }
            else if (CountryVote == "no")
            {
                Vote = "No";
            }
            else if (CountryVote == "abstain")
            {
                Vote = "Abstain";
            }
            else
            {
                Console.WriteLine("Please enter a valid vote. (Yes, No or Abstain)");
                ChangeVote();
            }
        }
    }

    class MainClass
    {
        static void Main()
        {
            List<Country> CountryObjects = SetupCountryObjects();

            Menu(CountryObjects);

        }
        
        static void Menu(List<Country> CountryObjects)
        {
            Console.WriteLine("EU Voting Calculator");
            Console.WriteLine("1:\tView Countries, Population and Votes\n2:\tChange a Countries Vote\n3:\tCheck Vote Status\n\nEnter your option:");
            string UserOption = Console.ReadLine();

            if (UserOption == "1")
            {
                DisplayCountries(CountryObjects);
            }
            else if (UserOption == "2")
            {
                Console.WriteLine("Enter the name of the country you wish to change the vote of:");
                string CountryName = Console.ReadLine();
                ChangeCountryVote(CountryName.ToLower(), CountryObjects);
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
            List<Country> CountryObjects = new List<Country>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willz\Desktop\OOP Assignment 1\CMP1903M\CMP1903M\Countries.txt");
            //System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "\\Countries.txt")); 
            //System.IO.StreamReader file = new System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Countries.txt"));

            string Line;

            while ((Line = file.ReadLine()) != null)
            {
                //line_strings.Add(line);
                string[] CountryDetails = Line.Split(",");
                CountryObjects.Add(new Country(CountryDetails[0], CountryDetails[1]));
            }

            return CountryObjects;
        }
        

        static void DisplayCountries(List<Country> CountryObjects)
        {
            Console.WriteLine("\nCountry" + " ".PadLeft(13) + "Population" + " ".PadLeft(6) + "Vote");
            foreach (Country x in CountryObjects)
            {
                Console.WriteLine(x.GetCountryInfo()[0] + " ".PadLeft(20 - x.GetCountryInfo()[0].Length) + x.GetCountryInfo()[1] + "%" + " ".PadLeft(15 - x.GetCountryInfo()[1].Length) + x.GetCountryInfo()[2]);
            }
            Menu(CountryObjects);
        }

        static void ChangeCountryVote(string CountryName, List<Country> CountryObjects)
        {
            foreach (Country x in CountryObjects)
            {
                if (x.GetCountryInfo()[0].ToLower() == CountryName)
                {
                    x.ChangeVote();
                    Console.WriteLine(x.GetCountryInfo()[2]);
                }
            }
            Menu(CountryObjects);
        }
    }
}
