using System;
using System.Collections.Generic;
using System.IO;

namespace NationalParksMenuApp
{
    internal class Program
    {
        // Main declarations that should be scoped program-wide
        public static string TheParksFileOLD = @"F:\\Development\\NationalParksMenuApp\\ParkData.txt";
        public static string TheParksFile = Path.Combine(Directory.GetCurrentDirectory(), @"ParkData.txt");
        public static List<clsPark> TheParks = new List<clsPark>();

        private static void Main(string[] args)
        {
            bool loopFlag = true;
            ConsoleKeyInfo optionSelect;

            //Menu loop
            while (loopFlag)
            {
                Console.Clear();
                Console.WriteLine("      National Parks Directory\n");
                Console.WriteLine("  ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine("  Press select an option:\n");
                Console.WriteLine("  1 - Park list");
                Console.WriteLine("  2 - Add park");
                Console.WriteLine("  3 - Quit\n");
                optionSelect = Console.ReadKey();
                Console.WriteLine("\n\n");

                switch (optionSelect.KeyChar)
                {
                    case '1':
                        break;

                    case '2':
                        break;

                    case '3':
                        break;
                }

                //Pressing 3 breaks loop and closes program
                if (optionSelect.KeyChar == '3')
                {
                    loopFlag = false;
                }
                else if (optionSelect.KeyChar == '1')
                {
                    //Output 1
                    Console.Clear();
                    Console.WriteLine("  List of National Parks:");
                    Console.WriteLine("  ───────────────────────\n");
                    Output1();
                }
                else if (optionSelect.KeyChar == '2')
                {
                    //Output 2
                    Console.Clear();
                    Console.WriteLine("  Add National Park to Directory:");
                    Console.WriteLine("  ───────────────────────────────\n");
                    Output2();
                }
                else
                {
                    //Output if invalid
                    Console.Clear();
                    Console.WriteLine("  Please select a valid option.");
                }

                //Wait for user to press ENTER
                Console.WriteLine("  ────────────────────────────\n");
                Console.WriteLine("    Press ENTER to continue.");
                Console.ReadLine();
            }
        }

        private static void Output1()
        {
            string line;

            //Opens path to parks list txt file
            StreamReader sr = new StreamReader("F:\\Development\\NationalParksMenuApp\\ParkData.txt");
            line = sr.ReadLine();

            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
        }

        private static void Output2()
        {
            string line;

            //Allows user to add new park.
            using (StreamWriter file = new StreamWriter(@"F:\\Development\\NationalParksMenuApp\\ParkData.txt", true))
            {
                line = Console.ReadLine();
                file.WriteLine("  " + line + "\n");
            }
        }


        // ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
        // ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀

        //  Below here is MY code to read and write your data file, to/from a collection called
        //  'TheParks'. I only changed your data file to identiy when we had a location vs. park
        //  itself. Once loaded, you can do all kinds of stuff with the data to the screen - for
        //  example, I wrote ONE way of doing it below, to show you how you could build it...

        /// <summary>
        /// Method to read the data file into the List collection for managing by the application.
        /// </summary>
        private static void ReadParkData()
        {
            string line;
            string LastLocation = string.Empty;
            clsPark aPark = null;

            // Open the file and ready it to begin processing the data; 
            //   - If the line begins with a '+', then the entry is a location
            //   - If the line begins with a '-', it is a park associated with the most recently 
            //     read location
            // NOTE: All other lines are ignored!
            StreamReader sr = new StreamReader(TheParksFile);
            line = sr.ReadLine();
            while (line != null)              // Continue to read until you reach end of file
            {
                line = line.Trim();           // Remove an spaces prefixed or trailing the line
                if (line.Length > 0)          // Ignore blank lines
                {
                    if (line.Substring(0, 1) == "+")         // If line describes a new location
                    {
                        LastLocation = line.Substring(1);   // Preserve the location
                    }
                    else if (line.Substring(0, 1) == "-")   // Else if line contains a park name
                    {
                        // Add the new new location and park name to the collection and continue
                        aPark = new clsPark();
                        aPark.Location = LastLocation;
                        aPark.ParkName = line.Substring(1);
                        TheParks.Add(aPark);
                    }
                }

                line = sr.ReadLine();
            }

            // Close and dispose of the streamwriter resource
            sr.Close();
            sr.Dispose();
        }

        /// <summary>
        /// Method to write the data file from the List collection back to the data file.
        /// </summary>
        private static void WriteParkData()
        {
            string CurrentLocation = string.Empty;
            string tmpStr;

            // Allows user to add new park.
            using (StreamWriter file = new StreamWriter(TheParksFile, false))
            {
                // Loop through the collection
                for (int xx = 0; xx < TheParks.Count; xx++)
                {
                    // Determine if we need a new section header and then generate it if so
                    if (CurrentLocation != TheParks[xx].Location)
                    {
                        // If NOT the first location, we want to at a couple of separator lines
                        if (CurrentLocation != string.Empty)
                        {
                            file.WriteLine();
                            file.WriteLine();
                        }

                        // Write the location and underline
                        file.WriteLine("+" + TheParks[xx].Location);
                        tmpStr = string.Empty;
                        for (int yy = 0; yy < TheParks[xx].Location.Length; yy++)
                            tmpStr = tmpStr + "▀";
                        file.WriteLine(" " + tmpStr);

                        // Set and store the current location so we can use it for comparison as
                        //   we loop through the list
                        CurrentLocation = TheParks[xx].Location;
                    }

                    // Now begin outputting the park names
                    file.WriteLine("-" + TheParks[xx].ParkName);
                }
            }
        }

        private static void ShowAllData()
        {
            string CurrentLocation = string.Empty;
            string tmpStr;

            // Clear the screen
            Console.Clear();

            // Loop the entire collection of parks
            for (int xx = 0; xx < TheParks.Count; xx++)
            {
                // If the location changes, generate a header line in the display
                if (TheParks[xx].Location != CurrentLocation)
                {
                    // Add blank line for previous output, but avoid first one
                    if (CurrentLocation != string.Empty)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    // Generate the header
                    Console.WriteLine(TheParks[xx].Location);
                    tmpStr = string.Empty;
                    for (int yy = 0; yy < TheParks[xx].Location.Length; yy++)
                        tmpStr = tmpStr + "─";
                    Console.WriteLine(tmpStr);

                    // Set current location in memory so we can keep up when we need a new header
                    CurrentLocation = TheParks[xx].Location;
                }

                // Now write the park name out
                Console.WriteLine("  " + TheParks[xx].ParkName);
            }
        }

        /// <summary>
        /// Secondary method to show all data, but in a more compact format (I think).
        /// </summary>
        private static void ShowAllData2()
        {
            string CurrentLocation = string.Empty;
            string tmpParksStr = string.Empty;
            string tmpStr;

            // Clear the screen and generate a header
            Console.Clear();
            Console.WriteLine("┌────────────────────────┐");
            Console.WriteLine("│   Current Parks Data   │");
            Console.WriteLine("└────────────────────────┘\n");

            // Loop the entire collection of parks
            for (int xx = 0; xx < TheParks.Count; xx++)
            {
                // If the location changes, generate a header line in the display
                if (TheParks[xx].Location != CurrentLocation)
                {
                    // Generate current list to the screen before adding a blank line
                    if (CurrentLocation != string.Empty)
                    {
                        // Remove the last comma in the string before writing it on the console
                        tmpParksStr = tmpParksStr.Substring(0, tmpParksStr.Length - 2);
                        Console.WriteLine("   " + tmpParksStr); ;
                        Console.WriteLine();
                    }

                    // Generate the header
                    Console.WriteLine(TheParks[xx].Location);
                    tmpStr = string.Empty;
                    for (int yy = 0; yy < TheParks[xx].Location.Length; yy++)
                        tmpStr = tmpStr + "─";
                    Console.WriteLine(tmpStr);

                    // Set current location in memory so we can keep up when we need a new header
                    CurrentLocation = TheParks[xx].Location;
                    tmpParksStr = string.Empty;
                }

                // Now write the park name out
                tmpParksStr = tmpParksStr + TheParks[xx].ParkName + ", ";
            }

            // Render the final string now, since we are at the END of the collection
            tmpParksStr = tmpParksStr.Substring(0, tmpParksStr.Length - 2);
            Console.WriteLine("   " + tmpParksStr); ;
            Console.WriteLine();
        }

        private static void ShowOnlyLocations()
        {
            string CurrentLocation = string.Empty;

            // Clear the screen and generate a header
            Console.Clear();
            Console.WriteLine("┌────────────────────────────┐");
            Console.WriteLine("│   Current Park Locations   │");
            Console.WriteLine("└────────────────────────────┘\n");

            // Loop the entire collection of parks
            for (int xx = 0; xx < TheParks.Count; xx++)
            {
                if (TheParks[xx].Location != CurrentLocation)
                {
                    Console.WriteLine(TheParks[xx].Location);
                    CurrentLocation = TheParks[xx].Location;
                }
            }
        }

        /// <summary>
        /// Method to show all the parks for a specified location.
        /// </summary>
        /// <param name="pLocation">Location to be filtered by.</param>
        private static void ShowParksForALocation(string pLocation)
        {
            string tmpStr = string.Empty;
            string headerStr;

            // Build a box line equal to the length of the location name before rendering the header
            headerStr = "Current Parks for Location: " + pLocation;
            for (int xx = 0; xx < headerStr.Length; xx++)
                tmpStr = tmpStr + "─";

            // Clear the screen and generate a header
            Console.Clear();
            Console.WriteLine("┌───" + tmpStr + "───┐");
            Console.WriteLine("│   " + headerStr + "   │");
            Console.WriteLine("└───" + tmpStr + "───┘\n");

            // Loop the entire collection of parks
            for (int xx = 0; xx < TheParks.Count; xx++)
            {
                if (TheParks[xx].Location == pLocation)
                {
                    Console.WriteLine(TheParks[xx].ParkName);
                }
            }
        }



    }
}