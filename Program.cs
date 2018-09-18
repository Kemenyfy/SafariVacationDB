using System;
using System.Linq;
using System.Collections;
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
                System.Console.WriteLine($"{animal.Species} {animal.CountOfTimeSeen} Times. Last time at {animal.LocationOfLastSeen}");
            }
            Greeting();
        }

        public static void Greeting()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("What else do you want to do?");
            Console.WriteLine("(A)dd");
            Console.WriteLine("(U)pdate");
            Console.WriteLine("(R)emove");
            Console.WriteLine("(O)ther Stuff that Mark asked for");

            var playerSelects = Console.ReadLine();

            if (playerSelects == "A")
            {
                AddAnimal();
            }
            else if (playerSelects == "U")
            {
                UpdateAnimal();
            }
            else if (playerSelects == "R")
            {
                RemoveAnimal();
            }
            else if (playerSelects == "O")
            {
                RandomStuffAskedByMark();
            }
            else
            {
                Console.WriteLine("Next time pick one of the options! Grrrrrr");
            }

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
                Console.WriteLine("Keep it up, nice job!");
                Greeting();
            }
        }

        public static void UpdateAnimal()
        {
            // Update the CountOfTimesSeen and LocationOfLastSeen for an animal
            Console.WriteLine("What animal do you want to upgrade?");

            var db = new SafariVacationContext();

            var picked = Console.ReadLine();
            var animalToUpdate = db.SeenAnimalsTable.FirstOrDefault(f => f.Species == picked);

            Console.WriteLine($"Ok, you picked {picked}. What do you want to update?");
            Console.WriteLine($"(S)pecies");
            Console.WriteLine($"(C)ount of Times Seen");
            Console.WriteLine($"(L)ocation of Last Seen");

            var playerSelects = Console.ReadLine();

            if (playerSelects == "S")
            {
                Console.WriteLine("What is the new species name?");
                var speciesName = Console.ReadLine();
                animalToUpdate.Species = speciesName;
                Console.WriteLine("Cool, what else?");
                Greeting();
            }
            else if (playerSelects == "C")
            {
                Console.WriteLine("How many times have you seen this species?");
                var speciesCount = int.Parse(Console.ReadLine());
                animalToUpdate.CountOfTimeSeen = speciesCount;
                Console.WriteLine("Great, what else?");
                Greeting();
            }
            else if (playerSelects == "L")
            {
                Console.WriteLine("Where did you last see this species?");
                var speciesLocation = Console.ReadLine();
                animalToUpdate.LocationOfLastSeen = speciesLocation;
                Console.WriteLine("Awesome, what else?");
                Greeting();
            }
            else
            {
                Console.WriteLine("Next time pick one of the three! Grrrrrr");
            }
            db.SaveChanges();

        }

        public static void RemoveAnimal()
        {
            // Remove Animal
            Console.WriteLine("Which animal do you want to remove?");

            var db = new SafariVacationContext();
            var picked = Console.ReadLine();
            var AnimalToRemove = db.SeenAnimalsTable.FirstOrDefault(f => f.Species == picked);

            Console.WriteLine($"Bye bye {picked}");
            db.SeenAnimalsTable.Remove(AnimalToRemove);


            db.SaveChanges();
            Greeting();
        }

        public static void RandomStuffAskedByMark()
        {
            // Display all animals seen in the Jungle
            Console.WriteLine("What do you want to do?");
            Console.WriteLine($"(D)isplay all animals seen in the Jungle");
            Console.WriteLine($"(R)emove all animals that I have seen in the Desert");
            Console.WriteLine($"(A)dd all the CountOfTimeSeen and get a total number of animals seen");
            Console.WriteLine($"(G)et the CountOfTimeSeen of lions, tigers and bears");

            var playerSelects = Console.ReadLine();
            if (playerSelects == "D")
            {
                Console.WriteLine("The animals you've seen in the jungle are:");
                var db = new SafariVacationContext();
                var animals = db.SeenAnimalsTable.Where(f => f.LocationOfLastSeen == "Jungle");

                foreach (var animal in animals)
                {
                    Console.WriteLine($"{animal.Species}");
                }
                Greeting();
            }
            else if (playerSelects == "R")
            {
                Console.WriteLine("The animals you've seen in the desert are:");
                var db = new SafariVacationContext();
                var animals = db.SeenAnimalsTable.Where(f => f.LocationOfLastSeen == "Desert");

                foreach (var animal in animals)
                {
                    Console.WriteLine($"{animal.Species}");
                    db.SeenAnimalsTable.Remove(animal);
                }
                Console.WriteLine("And now they are gone... Sad, but true...");
                db.SaveChanges();
                Greeting();
            }
            else if (playerSelects == "A")
            {
                var db = new SafariVacationContext();
                var animals = db.SeenAnimalsTable.Where(f => f.CountOfTimeSeen >= 1);
                var numberOfRecords = 0;
                var numberOfAnimalsSeen = 0;

                foreach (var animal in animals)
                {
                    numberOfRecords++;
                    numberOfAnimalsSeen += animal.CountOfTimeSeen;
                }

                Console.WriteLine("The total of Animals seen is: " + numberOfRecords.ToString());
                Console.WriteLine($"You have seen a total of {numberOfAnimalsSeen} animals");
            }
            else if (playerSelects == "G")
            {
                var db = new SafariVacationContext();
                var animals = db.SeenAnimalsTable.Where(f => f.CountOfTimeSeen >= 1);

                var numberOfSpecialAnimalsSeen = 0;
                var LionsTigersBears = db.SeenAnimalsTable.Where(animal => animal.Species == "Lion" || animal.Species == "Tiger" || animal.Species == "Bear");

                foreach (var animal in LionsTigersBears)
                {
                    numberOfSpecialAnimalsSeen += animal.CountOfTimeSeen;
                }

                Console.WriteLine($"You have seen a total of {numberOfSpecialAnimalsSeen} Lions, Tigers and Bears");
            }
            else
            {
                Console.WriteLine("Common, pick carefully next time.");
            }
        }
    }
}