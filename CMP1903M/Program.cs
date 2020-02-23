using System;
using System.Collections.Generic;
using System.IO;

namespace CMP1903M
{
    class Country
    {
        private string Name;
        public double Population;
        public string Vote;
        private double VoteWorth;

        public Country(string CountryName, string CountryPopulation)
        {
            Name = CountryName;
            Population = Convert.ToDouble(CountryPopulation);
            Vote = "Abstain";
            VoteWorth = 0;
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

            double PopVotes = 0.00;

        }
        
        static void Menu(List<Country> CountryObjects)
        {
            Console.WriteLine("\n\nEU Voting Calculator");
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
                CheckVoteStatus(CountryObjects);
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

            Console.WriteLine("Member States\nMinimum “Yes” required for adoption: (55%) 15");
            Console.WriteLine("Yes: " + TotalCountryVotes + "\nNo: " + (27-TotalCountryVotes));
            Console.WriteLine("\nFinal Result: " + VoteResult);

            Console.WriteLine("\n\n% Population\nMinimum “Yes” required for adoption: 65%");
            Console.WriteLine("Yes: " + TotalPopulationVotes + "\nNo: " + (100 - TotalPopulationVotes));
            Console.WriteLine("\nFinal Result: " + VoteResult);

            Menu(CountryObjects);
        }
    }
}
