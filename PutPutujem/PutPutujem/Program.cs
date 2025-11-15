using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
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
                    {"fuelSpent", 10},
                    {"fuelPricePerL", 1.4f},
                    {"totalFuelPrice", 14}
                },
                new Dictionary<string, object>
                {
                    {"id", 2},
                    {"tripDate", new DateTime(2017, 2, 12)},
                    {"distance", 450},
                    {"fuelSpent", 25},
                    {"fuelPricePerL", 1.2f},
                    {"totalFuelPrice", 30}
                },
                new Dictionary<string, object>
                {
                    {"id", 3},
                    {"tripDate", new DateTime(2018, 12, 23)},
                    {"distance", 148},
                    {"fuelSpent", 10},
                    {"fuelPricePerL", 1.3f},
                    {"totalFuelPrice", 13}
                },
                new Dictionary<string, object>
                {
                    {"id", 4},
                    {"tripDate", new DateTime(2021, 7, 20)},
                    {"distance", 330},
                    {"fuelSpent", 18},
                    {"fuelPricePerL", 1.4f},
                    {"totalFuelPrice", 25.2f}
                },
                new Dictionary<string, object>
                {
                    {"id", 5},
                    {"tripDate", new DateTime(2025, 10, 10)},
                    {"distance", 58},
                    {"fuelSpent", 3},
                    {"fuelPricePerL", 1.45f},
                    {"totalFuelPrice", 4.35f}
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
                    aboutTrips(trips, users);
                else return;
            }
        }


        static int mainMenu()
        {
            Console.WriteLine("\n1 - Korisnici");
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

        static void aboutTrips(List<Dictionary<string, object>> trips, List<Dictionary<string, object>> users)
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
                switch (answer)
                {
                    case 0:
                        return;
                    case 1:
                        createNewTrip(trips, users);
                        break;
                    case 2:
                        deleteTrip(trips, users);
                        break;
                    case 3:
                        editTrip(trips);
                        break;
                    case 4:
                        listAllTrips(trips);
                        break;
                    case 5:
                        getReports(trips, users);
                        break;

                }
            }
            else aboutTrips(trips, users);
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

            var newUser = new Dictionary<string, object> {
                {"id", getId(users)},
                {"firstName", name},
                {"lastName", surname},
                {"birthDate", date },
                {"trips", new List<int>()}
            };

            users.Add(newUser);

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
            Console.WriteLine("\n1 - Brisanje po ID-u");
            Console.WriteLine("2 - Brisanje po imenu i prezimenu");
            Console.Write("Odabir: ");
            if (!int.TryParse(Console.ReadLine(), out int answer) || !(answer == 1 || answer == 2))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                deleteUser(users);
                return;
            }

            int listLength = users.Count;
            string? check;

            if (answer == 1)
            {
                Console.Write("ID: ");
               
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    do
                    {
                        Console.WriteLine("Jesi li siguran da želiš izbirsati korisnika?(da/ne)");
                        check = Console.ReadLine().ToLower();
                    } while (check != "da" && check != "ne");

                    if (check == "da")
                        users.RemoveAll(u => (int)u["id"] == id);
                    else return;
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

                do
                {
                    Console.WriteLine("Jesi li siguran da želiš izbrisati korisnika?(da/ne)");
                    check = Console.ReadLine().ToLower();
                } while (check != "da" && check != "ne");

                if (check == "da")
                    users.RemoveAll(u => (string)u["firstName"] == name && (string)u["lastName"] == surname);
                else return;
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
            
            Console.WriteLine("\n1 - Promjena imena");
            Console.WriteLine("2 - Promjena prezimena");
            Console.WriteLine("3 - Promjena datuma rodjenja");
            Console.Write("Odabir: ");
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
            Console.WriteLine("\n1 - Ispis po prezimenu abecedno");
            Console.WriteLine("2 - Ispis korisnika starijih od 20 godina");
            Console.WriteLine("3 - Ispis korisnika koji imaju barem 2 putovanja");
            Console.Write("Odabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer > 0 && answer < 4)
            {
                var sorted = new List<Dictionary<string, object>>();
                switch (answer)
                {
                    case 1:
                        sorted = users.OrderBy(u => (string)u["lastName"]).ToList();
                        break;
                    
                    case 2:
                        sorted = users.Where(u => (DateTime)u["birthDate"] < DateTime.Now.AddYears(-20)).ToList();
                        break;
                    
                    case 3:
                        sorted = users.Where(u => ((List<int>)u["trips"]).Count >= 2).ToList();
                        break;

                }
                printUsersList(sorted);
            }
        }

        static void printUsersList(List<Dictionary<string,object>> users)
        {
            foreach(var user in users)
                Console.WriteLine($"{user["id"]} - {user["firstName"]} - {user["lastName"]} - {((DateTime)user["birthDate"]).ToString("yyyy-MM-dd")}");
        }



        static void createNewTrip(List<Dictionary<string, object>> trips, List<Dictionary<string, object>> users)
        {

            Console.Write("ID korisnika: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                createNewTrip(trips, users);
                return;
            }

            var user = users.Find(u => (int)u["id"] == id);
            if (user == null)
            {
                Console.WriteLine("Korisnik nije pronadjen!");
                return;
            }

            var date = getDate("putovanja");

            float distance;
            do
            {
                Console.Write("Kilometraza: ");
            }
            while (!float.TryParse(Console.ReadLine(), out distance) || distance <= 0);

            float fuel;
            do
            {
                Console.Write("Potroseno gorivo: ");
            }
            while (!float.TryParse(Console.ReadLine(), out fuel) || fuel <= 0);

            float fuelPrice;
            do
            {
                Console.Write("Cijena goriva po litru: ");
            }
            while (!float.TryParse(Console.ReadLine(), out fuelPrice) || fuelPrice <= 0);

            var newTrip = new Dictionary<string, object> {
                {"id", getId(trips)},
                {"tripDate", date},
                {"distance", distance},
                {"fuelSpent", fuel},
                {"fuelPricePerL", fuelPrice},
                {"totalFuelPrice", fuel * fuelPrice}
            };

            trips.Add(newTrip);

            ((List<int>)user["trips"]).Add((int)newTrip["id"]);

        }

        static void deleteTrip(List<Dictionary<string, object>> trips, List<Dictionary<string, object>> users)
        {
            int listLength = trips.Count;

            Console.WriteLine("\n1 - Brisanje po ID-u");
            Console.WriteLine("2 - Brisanje putovanja skupljih od unesenog iznosa");
            Console.WriteLine("3 - Brisanje putovanja jeftinijih od unesenog iznosa");

            Console.Write("Odabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer > 0 && answer < 4)
            {
                string check;

                switch (answer)
                { 
                    case 1:
                        Console.Write("ID: ");

                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            do
                            {
                                Console.WriteLine("Jesi li siguran da želiš izbirsati putovanje?(da/ne)");
                                check = Console.ReadLine().ToLower();
                            } while (check != "da" && check != "ne");

                            if (check == "da")
                                trips.RemoveAll(t => (int)t["id"] == id);
                            else return;
                        }
                        break;

                    case 2:
                        Console.Write("Iznos: ");

                        if (float.TryParse(Console.ReadLine(), out float amount))
                        {
                            do
                            {
                                Console.WriteLine("Jesi li siguran da želiš izbirsati putovanja?(da/ne)");
                                check = Console.ReadLine().ToLower();
                            } while (check != "da" && check != "ne");

                            if (check == "da")
                                trips.RemoveAll(t => Convert.ToSingle(t["totalFuelPrice"]) > amount);
                            else return;
                        }
                        break;

                    case 3:
                        Console.Write("Iznos: ");

                        if (float.TryParse(Console.ReadLine(), out amount))
                        {
                            do
                            {
                                Console.WriteLine("Jesi li siguran da želiš izbirsati putovanja?(da/ne)");
                                check = Console.ReadLine().ToLower();
                            } while (check != "da" && check != "ne");

                            if (check == "da")
                                trips.RemoveAll(t => Convert.ToSingle(t["totalFuelPrice"]) < amount);
                            else return;
                        }
                        break;
                }

                if (listLength == trips.Count)
                    Console.WriteLine("Putovanje nije pronadjeno.");
                else Console.WriteLine("Uspjesno obrisano.");
            } 
            else Console.WriteLine("Neispravan unos.");
        }

        static void editTrip(List<Dictionary<string, object>> trips)
        {
            Console.Write("ID putovanja: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neispravan unos! Ponovi");
                editTrip(trips);
                return;
            }

            var trip = trips.Find(u => (int)u["id"] == id);

            if (trip == null)
            {
                Console.WriteLine("Putovanje nije pronadjeno!");
                return;
            }

            Console.WriteLine("\n1 - Promjena datuma");
            Console.WriteLine("2 - Promjena kilometraze");
            Console.WriteLine("3 - Promjena potrosenog goriva");
            Console.WriteLine("4 - Promjena cijene goriva");
            Console.Write("Odabir: ");
            if (int.TryParse(Console.ReadLine(), out int answer) && answer > 0 && answer < 5)
            {
                string? check;
                switch (answer)
                {
                    case 1:
                        var date = getDate("putovanja");

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti datum putovanja?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");

                        if (check == "da")
                        {
                            trip["tripDate"] = date;
                            Console.WriteLine("Datum uspjesno promijenjen!");
                        }
                        else return;
                        break;

                    case 2:
                        float distance;
                        do
                        {
                            Console.Write("Kilometraza: ");
                        }
                        while (!float.TryParse(Console.ReadLine(), out distance) || distance <= 0);

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti kilometrazu putovanja?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");

                        if (check == "da")
                        {
                            trip["distance"] = distance;
                            Console.WriteLine("Kilometraza uspjesno promijenjena!");
                        }
                        else return;
                        break;

                    case 3:
                        float fuel;
                        do
                        {
                            Console.Write("Potroseno gorivo: ");
                        }
                        while (!float.TryParse(Console.ReadLine(), out fuel) || fuel <= 0);

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti potroseno gorivo na putovanju?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");

                        if (check == "da")
                        {
                            trip["fuelSpent"] = fuel;
                            Console.WriteLine("Potroseno gorivo uspjesno promijenjeno!");
                        }
                        else return;
                        break;

                    case 4:
                        float price;
                        do
                        {
                            Console.Write("Cijena goriva: ");
                        }
                        while (!float.TryParse(Console.ReadLine(), out price) || price <= 0);

                        do
                        {
                            Console.WriteLine("Jesi li siguran da želiš promijeniti cijenu goriva?(da/ne)");
                            check = Console.ReadLine().ToLower();
                        } while (check != "da" && check != "ne");

                        if (check == "da")
                        {
                            trip["fuelPricePerL"] = price;
                            Console.WriteLine("Cijena goriva uspjesno promijenjena!");
                        }
                        else return;
                        break;
                }

                trip["totalFuelPrice"] = Convert.ToSingle(trip["fuelSpent"]) * Convert.ToSingle(trip["fuelPricePerL"]);
            }

        }

        static void listAllTrips(List<Dictionary<string, object>> trips)
        {
            Console.WriteLine("\n1 - Ispisi po redu");
            Console.WriteLine("2 - Po trosku uzlazno");
            Console.WriteLine("3 - Po trosku silazno");
            Console.WriteLine("4 - Po kilometrazi uzlazno");
            Console.WriteLine("5 - Po kilometrazi silazno");
            Console.WriteLine("6 - Po datumu uzlazno");
            Console.WriteLine("7 - Po datumu silazno");

            Console.Write("Odabir: ");

            if (int.TryParse(Console.ReadLine(), out int answer) && answer > 0 && answer < 8)
            {
                var sorted = new List<Dictionary<string, object>>();
                switch (answer)
                {
                    case 1:
                        sorted = trips;
                        break;

                    case 2:
                        sorted = trips.OrderBy(t => Convert.ToSingle(t["totalFuelPrice"])).ToList();
                        break;

                    case 3:
                        sorted = trips.OrderByDescending(t => Convert.ToSingle(t["totalFuelPrice"])).ToList();
                        break;

                    case 4:
                        sorted = trips.OrderBy(t => Convert.ToSingle(t["distance"])).ToList();
                        break;

                    case 5:
                        sorted = trips.OrderByDescending(t => Convert.ToSingle(t["distance"])).ToList();
                        break;

                    case 6:
                        sorted = trips.OrderBy(t => (DateTime)t["tripDate"]).ToList();
                        break;

                    case 7:
                        sorted = trips.OrderByDescending(t => (DateTime)t["tripDate"]).ToList();
                        break;
                }
                printTripsList(sorted);
            }
        }

        static void getReports(List<Dictionary<string, object>> trips, List<Dictionary<string, object>> users)
        {

        }

        static int getId(List<Dictionary<string, object>> data)
        {
            int i = 0;
            while (true)
            {
                i++;
                if (data.Any(d => (int)d["id"] == i))
                    continue;
                else return i;
            }
        }

        static void printTripsList(List<Dictionary<string, object>> trips)
        {
            foreach (var trip in trips) 
            {
                Console.WriteLine($"\nPutovanje #{trip["id"]}");
                Console.WriteLine($"Datum: {((DateTime)trip["tripDate"]).ToShortDateString()}");
                Console.WriteLine($"Kilometri: {trip["distance"]}");
                Console.WriteLine($"Gorivo: {trip["fuelSpent"]} L");
                Console.WriteLine($"Cijena po litru: {trip["fuelPricePerL"]} EUR");
                Console.WriteLine($"Ukupno: {trip["totalFuelPrice"]} EUR");
            } 
        }


    }
}