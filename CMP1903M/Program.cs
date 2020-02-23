﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1903M
{
    class Country
    {
        private string Name;
        public double Population;
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
            Console.WriteLine("Enter the vote of this country (Yes/No/Abstain) or enter C to cancel the vote change:");
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
            else if (CountryVote == "c")
            {
                return;
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
            Console.WriteLine("\n\nEU Voting Calculator");
            Console.WriteLine("1:\tView Countries, Population and Votes\n2:\tChange a Countries Vote\n3:\tCheck Vote Status\n4:\tExit the Program\n\nEnter your option:");
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
                CheckVoteStatus(CountryObjects);
            }
            else if (UserOption == "4")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please enter a valid option");
                Menu(CountryObjects);
            }
        }
         
        static List<Country> SetupCountryObjects()
        {
            List<Country> CountryObjects = new List<Country>();

            string directory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string path = Path.GetFullPath(Path.Combine(directory, @"..\"));

            System.IO.StreamReader file = new System.IO.StreamReader(path + @"/Countries.txt");
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
                }
            }
            Menu(CountryObjects);
        }

        static void CheckVoteStatus(List<Country> CountryObjects)
        {
            double TotalPopulationVotes = 0;
            int TotalCountryVotes = 0;
            string VoteResult = "Rejected";

            foreach (Country x in CountryObjects)
            {
                if (x.GetCountryInfo()[2] == "Yes")
                {
                    TotalPopulationVotes += x.Population;
                    TotalCountryVotes += 1;
                }
            }

            if (TotalPopulationVotes > 65)
            {
                VoteResult = "Approved";
            }
            else if (TotalCountryVotes > 15)
            {
                VoteResult = "Approved";
            }

            Console.WriteLine("\nMember States\nMinimum “Yes” required for adoption: (55%) 15");
            Console.WriteLine("Yes: " + TotalCountryVotes + "\nNo: " + (27-TotalCountryVotes));

            Console.WriteLine("\n% Population\nMinimum “Yes” required for adoption: 65%");
            Console.WriteLine("Yes: " + TotalPopulationVotes + "\nNo: " + (100 - TotalPopulationVotes));

            Console.WriteLine("\n\nFinal Result: " + VoteResult);

            Menu(CountryObjects);
        }
    }
}
