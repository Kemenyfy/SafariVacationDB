using System;
using safari_vacation.Models;

namespace safari_vacation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display all animals the user has seen
            Console.WriteLine("So far you've seen:");

            var db = new SafariVacationContext();

            var animals = db.SeenAnimalsTable;

            foreach (var animal in animals)
            {
                System.Console.WriteLine($"{animal.Species}");
            }

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(A)dd");
            Console.WriteLine("(U)pdate");
            Console.WriteLine("(R)emove");

            var playerSelects = Console.ReadLine();

            if (playerSelects == "A")
            {
                AddAnimal();
            }
            else if (playerSelects == "U")
            {
                // UpdateAnimal();
            }
            else if (playerSelects == "R")
            {
                // RemoveAnimal();
            }
            else
            {

            }



            // Update the CountOfTimesSeen and LocationOfLastSeen for an animal
            // Console.WriteLine("What animal do you want to upgrade?");
            // var animalToUpdate = db.SeenAnimalsTable.FirstOrDefault(animal => animal.id);

        }

        public static void AddAnimal()
        {
            // Add a new animal you've just seen
            Console.WriteLine("What animal have you just seen?");

            var animalSpecies = Console.ReadLine();

            var newAnimal = new SeenAnimals
            {
                Species = animalSpecies
            };

            var db = new SafariVacationContext();

            if (animalSpecies == "")
            {
                Console.WriteLine("Nothing to Add");
            }
            else
            {
                db.SeenAnimalsTable.Add(newAnimal);
                db.SaveChanges();
            }
        }
    }
}
