using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        ListOfRooms(roomRepo);
                        break;
                    case ("Search for room"):
                        SearchForRoom(roomRepo);
                        break;
                    case ("Add a room"):
                        AddARoom(roomRepo);
                        break;
                    case ("Show all chores"):
                        ListOfChores(choreRepo);
                        break;
                    case ("Search for chore"):
                        SearchForChores(choreRepo);
                        break;
                    case ("Search for roommate"):
                        SearchForRoommates(roommateRepo);
                        break;
                    case ("Add a chore"):
                        AddAChore(choreRepo);
                        break;
                    case ("Unassigned Chores"):
                        UnassignedChores(choreRepo);
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }
        static void ListOfChores(ChoreRepository choreRepo)
        {
            List<Chore> chores = choreRepo.GetAll();
            foreach (Chore r in chores)
            {
                Console.WriteLine($"{r.Id} - {r.Name}");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }
        static void SearchForChores(ChoreRepository choreRepo)
        {
            Console.Write("Room Id: ");
            string response = Console.ReadLine();
            int id;
            if (!int.TryParse(response, out id))
            {
                Console.WriteLine("That is not a valid choice.");
                SearchForChores(choreRepo);
            }
            Chore chore = choreRepo.GetById(id);

            Console.WriteLine($"{chore.Id} - {chore.Name}");
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }
        static void AddAChore(ChoreRepository choreRepo)
        {
            Console.Write("Chore Name: ");
            string name = Console.ReadLine();
            Chore choreToAdd = new Chore { Name = name };
            choreRepo.Insert(choreToAdd);
        }
        static void UnassignedChores(ChoreRepository choreRepo)
        {
            List<Chore> unassigned = choreRepo.GetUnassignedChores();
            Console.WriteLine("Unassigned Chores: ");
            foreach (Chore chore in unassigned)
            {
                Console.WriteLine($"({chore.Id}) {chore.Name}");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }
        static void ListOfRooms(RoomRepository roomRepo)
        {
            List<Room> rooms = roomRepo.GetAll();
            foreach (Room r in rooms)
            {
                Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }
        static void SearchForRoom(RoomRepository roomRepo)
        {
            Console.Write("Room Id: ");
            string response = Console.ReadLine();
            int id;
            if (!int.TryParse(response, out id))
            {
                Console.WriteLine("That is not a valid choice.");
                SearchForRoom(roomRepo);
            }
            Room room = roomRepo.GetById(id);

            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void SearchForRoommates(RoommateRepository roommateRepo)
        {
            Console.Write("Roommate Id: ");
            string response = Console.ReadLine();
            int id;
            if (int.TryParse(response, out id))
            {
                Roommate room = roommateRepo.GetById(id);

                Console.WriteLine($"[{room.Id}] - {room.FirstName} Rent Portion: {room.RentPortion} in {room.Room.Name} ");
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }
        }

        static void AddARoom(RoomRepository roomRepo)
        {
            Console.Write("Room name: ");
            string name = Console.ReadLine();

            Console.Write("Max occupancy: ");
            int max = int.Parse(Console.ReadLine());

            Room roomToAdd = new Room()
            {
                Name = name,
                MaxOccupancy = max
            };

            roomRepo.Insert(roomToAdd);

            Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Search for roommate",
            "Show all chores",
            "Search for chore",
            "Add a chore",
            "Unassigned Chores",
            "Exit"
        };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }

        }
    }
}
