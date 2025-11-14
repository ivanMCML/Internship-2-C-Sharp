using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace PutPutujem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var trips = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"id", 1},
                    {"tripDate", new DateTime(2013, 5, 2)},
                    {"distance", 150},
                    {"gasSpent", 10},
                    {"gasPricePerL", 1.4},
                    {"totalGasPrice", 14}
                },
                new Dictionary<string, object>
                {
                    {"id", 2},
                    {"tripDate", new DateTime(2017, 2, 12)},
                    {"distance", 450},
                    {"gasSpent", 25},
                    {"gasPricePerL", 1.2},
                    {"totalGasPrice", 30}
                },
                new Dictionary<string, object>
                {
                    {"id", 3},
                    {"tripDate", new DateTime(2018, 12, 23)},
                    {"distance", 148},
                    {"gasSpent", 10},
                    {"gasPricePerL", 1.3},
                    {"totalGasPrice", 13}
                },
                new Dictionary<string, object>
                {
                    {"id", 4},
                    {"tripDate", new DateTime(2021, 7, 20)},
                    {"distance", 330},
                    {"gasSpent", 18},
                    {"gasPricePerL", 1.4},
                    {"totalGasPrice", 25.2}
                },
                new Dictionary<string, object>
                {
                    {"id", 5},
                    {"tripDate", new DateTime(2025, 10, 10)},
                    {"distance", 58},
                    {"gasSpent", 3},
                    {"gasPricePerL", 1.45},
                    {"totalGasPrice", 4.35}
                }
            };

            var users = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"id", 4},
                    {"firstName", "Ivan"},
                    {"lastName", "Bebic"},
                    {"birthDate", new DateTime(2005, 2, 27)},
                    {"trips", new List<int>() {1, 2} }
                },
                new Dictionary<string, object>
                {
                    {"id", 10},
                    {"firstName", "Marko"},
                    {"lastName", "Livaja"},
                    {"birthDate", new DateTime(1993, 8, 26)},
                    {"trips", new List<int>() {3, 4} }
                },
                new Dictionary<string, object>
                {
                    {"id", 23},
                    {"firstName", "Filip"},
                    {"lastName", "Krovinovic"},
                    {"birthDate", new DateTime(1995, 8, 29)},
                    {"trips", new List<int>() {5} }
                }
            };

            Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");

            while (true)
            {
                int choice = mainMenu();

                if (choice == 1)
                    aboutUsers(users);
                else if (choice == 2)
                    aboutTrips(trips);
                else return;
            }
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

        static void aboutUsers(List<Dictionary<string, object>> users)
        {
            Console.WriteLine("\n1 - Unos novog korisnika");
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
                        createNewUser(users);
                        break;
                    case 2:
                        deleteUser(users);
                        break;
                    case 3:
                        editUser(users);
                        break;
                    case 4:
                        listAllUsers(users);
                        break;
                }
            }
            else aboutUsers(users);
        }

        static void aboutTrips(List<Dictionary<string, object>> trips)
        {
            Console.WriteLine("\n1 - Unos novog putovanja");
            Console.WriteLine("2 - Brisanje putovanja");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja");
            Console.WriteLine("4 - Pregled svih putovanja");
            Console.WriteLine("5 - Izvještaji i analize");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            Console.Write("\nOdabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 0 && answer < 6)
            {
                //switch (answer)
                //{
                //    case 0:
                //        return;
                //    case 1:
                //        createNewTrip(trips);
                //        break;
                //    case 2:
                //        deleteTrip(trips);
                //        break;
                //    case 3:
                //        editTrip(trips);
                //        break;
                //    case 4:
                //        listAllTrips(trips);
                //        break;
                //    case 5:
                //        getReports(trips);

                //}
            }
            else aboutTrips(trips);
        }


        static void createNewUser(List<Dictionary<string, object>> users)
        {

            string? name;
            do
            {
                Console.Write("Ime: ");
                name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(name));

            string? surname;

            do
            {
                Console.Write("Prezime: ");
                surname = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(surname));


            var date = getDate("rodjenja");

            int i = 0;
            int id;
            while (true)
            {
                i++;
                if (users.Any(u => (int)u["id"] == i))
                    continue;
                else
                {
                    id = i;
                    break;
                }
            }

            var newUser = new Dictionary<string, object> {
                {"id", id},
                {"firstName", name},
                {"lastName", surname},
                {"birthDate", date },
                {"trips", new List<int>()}
            };

            Console.WriteLine("Korisnik uspjesno dodan!");
        }



        static DateTime getDate(string target)
        {
            var today = DateTime.Now;

            Console.Write($"Godina {target}: ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                return getDate(target);
            }

            Console.Write($"Mjesec {target}: ");
            if (!int.TryParse(Console.ReadLine(), out int month))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                return getDate(target);
            }

            Console.Write($"Dan {target}: ");
            if (!int.TryParse(Console.ReadLine(), out int day))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                return getDate(target);
            }

            DateTime date;
            try
            {
                date = new DateTime(year, month, day);
            }
            catch
            {
                Console.WriteLine("Datum ne postoji! Ponovi");
                return getDate(target);
            }

            if (date > today)
            {
                Console.WriteLine("Datum ne moze biti u buducnosti! Ponovi");
                return getDate(target);
            }

            return date;
        }

        static void deleteUser(List<Dictionary<string, object>> users)
        {
            Console.WriteLine("1 - Brisanje po ID-u");
            Console.WriteLine("2 - Brisanje po imenu i prezimenu");
            if (!int.TryParse(Console.ReadLine(), out int answer) || !(answer == 1 || answer == 2))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                deleteUser(users);
                return;
            }

            int listLength = users.Count;

            if (answer == 1)
            {
                Console.Write("ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    users.RemoveAll(u => (int)u["id"] == id);
                }
            }
            else
            {
                string? name;
                do
                {
                    Console.Write("Ime: ");
                    name = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(name));

                string? surname;
                do
                {
                    Console.Write("Prezime: ");
                    surname = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(surname));


                users.RemoveAll(u => (string)u["firstName"] == name && (string)u["lastName"] == surname);
            }

            if (listLength == users.Count)
                Console.WriteLine("Korisnik nije pronadjen.");
            else Console.WriteLine("Korisnik uspjesno izbrisan.");
        }

        static void editUser(List<Dictionary<string, object>> users)
        {
            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                editUser(users);
                return;
            }

            var user = users.Find(u => (int)u["id"] == id);
            
            if (user == null)
            {
                Console.WriteLine("Korisnik nije pronadjen!");
                return;
            }
            
            Console.WriteLine("1 - Promjena imena");
            Console.WriteLine("2 - Promjena prezimena");
            Console.WriteLine("3 - Promjena datuma rodjenja");
            if (int.TryParse(Console.ReadLine(), out int answer) && answer > 0 && answer < 4)
            {
                string? check;
                switch (answer)
                {
                    case 1:
                        string? name;
                        do
                        {
                            Console.Write("Ime: ");
                            name = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(name));

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti ime korisnika?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");
                        
                        if (check == "da")
                        {
                            user["firstName"] = name;
                            Console.WriteLine("Ime uspjesno promijenjeno!");
                        }
                        else return;
                        break;

                    case 2:
                        string? surname;
                        do
                        {
                            Console.Write("Prezime: ");
                            surname = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(surname));

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti prezime korisnika?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");
                        
                        if (check == "da")
                        {
                            user["firstName"] = surname;
                            Console.WriteLine("Prezime uspjesno promijenjeno!");
                        }
                        else return;
                        break;

                    case 3:
                        var date = getDate("rodjenja");

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti datum rodjenja korisnika?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");

                        if (check == "da")
                        {
                            user["birthDate"] = date;
                            Console.WriteLine("Datum rodjenja uspjesno promijenjen!");
                        }
                        else return;
                        break;
                }
            }

        }

        static void listAllUsers(List<Dictionary<string, object>> users)
        {

        }

    }
}