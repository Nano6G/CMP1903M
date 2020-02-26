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

        //Constructor for Country class to set the details of each instantiation
        public Country(string CountryName, string CountryPopulation)
        {
            Name = CountryName;
            Population = Convert.ToDouble(CountryPopulation);
            Vote = "Abstain";
        }

        //Function to return all the information about the country
        public List<String> GetCountryInfo()
        {
            string StringPopulation = Convert.ToString(Population);
            List<string> CountryInfo = new List<string>() { Name, StringPopulation, Vote };
            return CountryInfo;
        }

        //Self-explainatory function to change the vote of a country according to user input
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
        //Initial function to setup the country objects and display the main menu
        private static void Main()
        {
            List<Country> CountryObjects = SetupCountryObjects();

            Console.WriteLine("Welcome to the EU Voting Calculator");
            Menu(CountryObjects);

        }
        
        //Menu function to display the options to the user and follows their input
        private static void Menu(List<Country> CountryObjects)
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
        
        private static List<Country> SetupCountryObjects()
        {
            List<Country> CountryObjects = new List<Country>();

            //Gets the current working directory
            string directory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            //Backs up the working directory by one folder
            string path = Path.GetFullPath(Path.Combine(directory, @"..\"));
            //Opens the Countries text file
            System.IO.StreamReader file = new System.IO.StreamReader(path + @"/Countries.txt");

            string Line;

            //Reads through the Countries file line-by-line and creates a new country object for each line, using the information given
            while ((Line = file.ReadLine()) != null)
            {
                //line_strings.Add(line);
                string[] CountryDetails = Line.Split(",");
                CountryObjects.Add(new Country(CountryDetails[0], CountryDetails[1]));
            }

            return CountryObjects;
        }
        
        //Function that iterates through the CoutryObjects list and displays each country and its information to the user
        private static void DisplayCountries(List<Country> CountryObjects)
        {
            Console.WriteLine("\nCountry" + " ".PadLeft(13) + "Population" + " ".PadLeft(6) + "Vote");
            foreach (Country x in CountryObjects)
            {
                Console.WriteLine(x.GetCountryInfo()[0] + " ".PadLeft(20 - x.GetCountryInfo()[0].Length) + x.GetCountryInfo()[1] + "%" + " ".PadLeft(15 - x.GetCountryInfo()[1].Length) + x.GetCountryInfo()[2]);
            }
            Menu(CountryObjects);
        }

        private static void ChangeCountryVote(string CountryName, List<Country> CountryObjects)
        {
            foreach (Country x in CountryObjects)
            {
                if (x.GetCountryInfo()[0].ToLower() == CountryName)
                {
                    //Calls the ChangeVote function in the Country class
                    x.ChangeVote();
                }
            }
            Menu(CountryObjects);
        }

        private static void CheckVoteStatus(List<Country> CountryObjects)
        {
            double TotalPopulationVotes = 0;
            int TotalCountryVotes = 0;
            //VoteResult variable initialised to "Rejected" and change if not the case
            string VoteResult = "Rejected";

            //Counts how many countries voted "yes"
            foreach (Country x in CountryObjects)
            {
                if (x.GetCountryInfo()[2] == "Yes")
                {
                    TotalPopulationVotes += x.Population;
                    TotalCountryVotes += 1;
                }
            }

            //Determines if the current vote is approved or not using the "qualified majority" rule
            if (TotalPopulationVotes > 65)
            {
                VoteResult = "Approved";
            }
            else if (TotalCountryVotes > 15)
            {
                VoteResult = "Approved";
            }

            //Prints the destails and result of the vote to the user
            Console.WriteLine("\nMember States\nMinimum “Yes” required for adoption: (55%) 15");
            Console.WriteLine("Yes: " + TotalCountryVotes + "\nNo: " + (27-TotalCountryVotes));

            Console.WriteLine("\n% Population\nMinimum “Yes” required for adoption: 65%");
            Console.WriteLine("Yes: " + TotalPopulationVotes + "\nNo: " + (100 - TotalPopulationVotes));

            Console.WriteLine("\n\nFinal Result: " + VoteResult);

            Menu(CountryObjects);
        }
    }
}
