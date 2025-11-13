using Microsoft.VisualBasic.FileIO;
using System.Reflection.Metadata;

namespace PutPutujem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");

            int choice = mainMenu();

            if (choice == 1)
                users();
            else if (choice == 2)
                trips();
            else return;

        }


        static int mainMenu()
        {
            Console.WriteLine("1 - Korisnici");
            Console.WriteLine("2 - Putovanja");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            Console.Write("\nOdabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 0 && answer < 3)
                return answer;
            else return mainMenu();
        }

        static void users()
        {
            Console.WriteLine("1 - Unos novog korisnika");
            Console.WriteLine("2 - Brisanje korisnika");
            Console.WriteLine("3 - Uređivanje korisnika");
            Console.WriteLine("4 - Pregled svih korisnika");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            Console.Write("\nOdabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 0 && answer < 5)
            {
                switch (answer)
                {
                    case 0:
                        return;
                    case 1:
                        createNewUser();
                        break;
                    case 2:
                        deleteUser();
                        break;
                    case 3:
                        editUser();
                        break;
                    case 4:
                        listAllUsers();
                        break;
                }
            }
            else users();
        }

        static void trips()
        {
            Console.WriteLine("1 - Unos novog putovanja");
            Console.WriteLine("2 - Brisanje putovanja");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja");
            Console.WriteLine("4 - Pregled svih putovanja");
            Console.WriteLine("5 - Izvještaji i analize");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            Console.Write("\nOdabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 0 && answer < 6)
            {
                switch (answer)
                {
                    case 0:
                        return;
                    case 1:
                        createNewTrip();
                        break;
                    case 2:
                        deleteTrip();
                        break;
                    case 3:
                        editTrip();
                        break;
                    case 4:
                        listAllTrips();
                        break;
                    case 5:
                        getReports();

                }
            }
            else users();
        }
    }
}
