using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    public class Commands
    {
        public void addNewAnniversary()
        {
            Console.WriteLine("Adding new anniversary");
        }

        public void listUpcomingAnniversary()
        {
            Console.WriteLine("Upcoming anniversary");
        }

        public void quit()
        {
            Console.WriteLine("Quit");
        }


        public void printCommandOptions()
        {
            Console.WriteLine("\nPress # from above list to entry.");
            Console.WriteLine("Press N to add a new anniversary.");
            Console.WriteLine("Press U to list upcoming anniversary.");
            Console.WriteLine("Press X to quit.");
        }

        public string getUserInput()
        {
            string inputCommand = Console.ReadLine();

            return inputCommand;
        }
    }
}
