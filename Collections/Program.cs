using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* ListsMain *-*-*-*-*-");
            Lists.ListsMain();

            Console.WriteLine("\n-*-*-*-*-* ArrayListsMain *-*-*-*-*-");
            ArrayLists.ArrayListsMain();

            Console.WriteLine("\n-*-*-*-*-* DictionariesMain *-*-*-*-*-");
            Dictionaries.DictionariesMain();

            Console.WriteLine("\n-*-*-*-*-* SortedListsMain *-*-*-*-*-");
            SortedLists.SortedListsMain();

            Console.WriteLine("\n-*-*-*-*-* HashTablesMain *-*-*-*-*-");
            HashTables.HashTablesMain();

            Console.WriteLine("\n-*-*-*-*-* TuplesMain *-*-*-*-*-");
            Tuples.TuplesMain();



            Console.ReadLine();
        }
    }

    internal struct Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Grade { get; set; }
    }

    internal static class Lists
    {
        /*
        Only excepts one specific type
        */
        static public void ListsMain()
        {
            Console.WriteLine("-*-*-*-*-* Lists1 *-*-*-*-*-");
            Lists1();
            Console.WriteLine("-*-*-*-*-* Lists2 *-*-*-*-*-");
            Lists2();
        }
        static public void Lists1()
        {
            var students = new List<Student>
            {
                new Student{ Id = 1, Name = "Bali", Grade = 66 },
                new Student{ Id = 2, Name = "Kali", Grade = 55 },
                new Student{ Id = 3, Name = "Mali", Grade = 77 },
            };
            Console.WriteLine("-*-*-*-*-* Before AddRange *-*-*-*-*-");
            students.ForEach(x => Console.WriteLine($"{x.Id} {x.Name} {x.Grade} "));

            var students2 = new List<Student>
            {
                new Student{ Id = 4, Name = "Balık", Grade = 60 },
                new Student{ Id = 5, Name = "Kalın", Grade = 30 },
                new Student{ Id = 6, Name = "Nazik", Grade = 90 },
            };

            students.AddRange(students2);

            Console.WriteLine("-*-*-*-*-* After AddRange *-*-*-*-*-");
            students.ForEach(x=>Console.WriteLine($"{x.Id} {x.Name} {x.Grade} "));

            Console.WriteLine("-*-*-*-*-* Other Functions *-*-*-*-*-");

            var st = new Student { Id = 3, Name = "Mali", Grade = 77 };
            var isContains = students.Contains(st);
            var isContains2 = students.Contains(new Student { Id = 3, Name = "Mali", Grade = 77 });
            Console.WriteLine(isContains2);


        }

        static public void Lists2()
        {
            var numbers = new List<int>() { 10, 20, 30, 40, 10 };
            numbers.Remove(10); // removes the first 10 from a list

            numbers.RemoveAt(2); //removes the 3rd element (index starts from 0)

            //numbers.RemoveAt(10); //throws ArgumentOutOfRangeException

            numbers.Insert(1, 999);

            foreach (var el in numbers)
                Console.Write($"{el}, "); //prints 20 30
        }
    }
    internal static class ArrayLists
    {
        /*
        Excepts one or more types
        */
        static public void ArrayListsMain()
        {
            Console.WriteLine("-*-*-*-*-* ArrayLists1 *-*-*-*-*-");
            ArrayLists1();
        }
        static public void ArrayLists1()
        {
            var arlist1 = new ArrayList
            {
                1,
                ' ',
                " Mustafa ",
                3.14M,
                true,
                4.5,
                null
            };

            var myQueue = new Queue();
            myQueue.Enqueue("Mustafa");
            myQueue.Enqueue(20);

            arlist1.AddRange(myQueue);

            arlist1.Insert(1, "Second Item");

            ArrayList arlist2 = new ArrayList()
            {
                300, 400, 500
            };
            arlist1.InsertRange(3, arlist2);

            Console.WriteLine("-*-*-*-*-* Before Remove *-*-*-*-*-");
            for (var i = 0; i<=arlist1.Count-1; i++)
                Console.WriteLine(arlist1[i]);

            Console.WriteLine("-*-*-*-*-* After Remove *-*-*-*-*-");
            arlist1.Remove(null); //Removes first occurance of null
            arlist1.RemoveAt(4); //Removes element at index 4
            arlist1.RemoveRange(0, 2);//Removes two elements starting from 1st item (0 index)
            foreach (var item in arlist1)
                Console.WriteLine(item);

            Console.WriteLine(arlist1.Contains(300)); // true
            Console.WriteLine(arlist1.Contains("Mustafa")); // true
            Console.WriteLine(arlist1.Contains(10)); // false
            Console.WriteLine(arlist1.Contains("Bazı Bazı")); // false

            arlist1.Clear();
        }
    }

    internal static class Dictionaries
    {
        /*
        Unsorted (KEY <-> Value) pairs
        */
        static public void DictionariesMain()
        {
            Console.WriteLine("\n-*-*-*-*-* Dictionaries1 *-*-*-*-*-");
            Dictionaries1();
        }

        static public void Dictionaries1()
        {
            var numberNames = new Dictionary<int, string>()
            {
                { 0, "Zero"},
                { 1, "One"},
                { 2, "Two"},
            };
            foreach (KeyValuePair<int, string> item in numberNames)
                Console.WriteLine($"{item.Key}: {item.Value}");

            var cities = new Dictionary<string, string>(){
                {"TR", "İstanbul, Ankara, İzmir"},
                {"UK", "London, Manchester, Birmingham"},
                {"USA", "Chicago, New York, Washington"},
                {"India", "Mumbai, New Delhi, Pune"},
            };
            Console.WriteLine(cities["TR"]);
            Console.WriteLine(cities["USA"]);

            //Console.WriteLine(cities["France"]);  //System.Collections.Generic.KeyNotFoundException
            if (cities.ContainsKey("France"))
            {
                Console.WriteLine(cities["France"]);
            }

            Console.WriteLine(cities.ContainsValue("Mumbai, New Delhi, Pune"));

            string result;
            if (cities.TryGetValue("Germany", out result))
            {
                Console.WriteLine(result);
            }

            // Get element by index.
            for (int i = 0; i < cities.Count; i++)
            {
                Console.WriteLine($"Key: {cities.ElementAt(i).Key}, Value: {cities.ElementAt(i).Value}");
            }

            cities.Clear();
        }
    }

    internal static class SortedLists
    {
        /*
        Sorted (KEY <-> Value) pairs
        */
        static public void SortedListsMain()
        {
            Console.WriteLine("\n-*-*-*-*-* SortedLists1 *-*-*-*-*-");
            SortedLists1();
        }

        static public void SortedLists1()
        {
            SortedList<int, string> numberNames = new SortedList<int, string>
            {
                { 3, "Three" },
                { 1, "One" },
                { 2, "Two" },
                { 4, null },
                { 10, "Ten" },
                { 5, "Five" }
            };
            //numberNames.Add(11, "eleven");

            Console.WriteLine("\n-*-*-*-*-* Before *-*-*-*-*-");
            PrintKeyValuePairs(numberNames);


            if (numberNames.ContainsKey(4))
            {
                numberNames[4] = "four";
            }

            if (numberNames.TryGetValue(4, out string result))
                Console.WriteLine("Key: {0}, Value: {1}", 4, result);

            numberNames.Remove(1);//removes key 1 pair
            numberNames.Remove(10);//removes key 1 pair, no error if not exists

            numberNames.RemoveAt(0);//removes key-value pair from index 0
                                    //numberNames.RemoveAt(10);//run-time exception: ArgumentOutOfRangeException
            Console.WriteLine("\n-*-*-*-*-* After *-*-*-*-*-");
            PrintKeyValuePairs(numberNames);
        }

        private static void PrintKeyValuePairs<TKey, TValue>(ICollection<KeyValuePair<TKey, TValue>> numberNames)
        {
            foreach (var item in numberNames)
                Console.WriteLine("key: {0}, value: {1}", item.Key, item.Value);
        }
    }

    internal static class HashTables
    {
        /*
        The Hashtable is a non-generic collection that stores key-value pairs, similar to generic Dictionary<TKey, TValue> collection. It optimizes lookups by computing the hash code of each key and stores it in a different bucket internally and then matches the hash code of the specified key at the time of accessing values.


        - Hashtable stores key-value pairs.
        - Comes under System.Collection namespace.
        - Implements IDictionary interface.
        - Keys must be unique and cannot be null.
        - Values can be null or duplicate.
        - Values can be accessed by passing associated key in the indexer e.g. myHashtable[key]
        - Elements are stored as DictionaryEntry objects.
        */
        static public void HashTablesMain()
        {
            Console.WriteLine("\n-*-*-*-*-* HashTables1 *-*-*-*-*-");
            HashTables1();
        }

        static public void HashTables1()
        {
            Hashtable numberNames = new Hashtable
            {
                { 3, "Three" },
                { 1, "One" },
                { 2, "Two" },
            };

            var dict = new Dictionary<int, string>
            {
                { 3, "Three" },
                { 1, "One" },
                { 2, "Two" },
            };
            var numberNames_fromDict = new Hashtable(dict);

            foreach (DictionaryEntry de in numberNames_fromDict)
                Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);

            var cities = new Hashtable(){
                {"UK", "London, Manchester, Birmingham"},
                {"USA", "Chicago, New York, Washington"},
                {"India", "Mumbai, New Delhi, Pune"},
                {"France", ""}
            };

            string citiesOfUK = cities["UK"].ToString(); //cast to string
            string citiesOfUSA = cities["USA"].ToString(); //cast to string

            Console.WriteLine(citiesOfUK);
            Console.WriteLine(citiesOfUSA);

            cities["UK"] = "Liverpool, Bristol"; // update value of UK key
            cities["USA"] = "Los Angeles, Boston"; // update value of USA key
                                                   //cities["France"] = "Paris"; //throws run-time exception: KeyNotFoundException

            if (cities.ContainsKey("France"))
            {
                cities["France"] = "Paris";
            }

            cities.Remove("UK"); // removes UK
                                 //cities.Remove("France"); //throws run-time exception: KeyNotFoundException

            if (cities.ContainsKey("France"))
            { // check key before removing it
                cities.Remove("France");
            }

            cities.Clear(); //removes all elements

            Console.WriteLine("-*-*-* After Clear *-*-*-");
            foreach (DictionaryEntry de in cities)
                Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);
        }
    }

    internal static class Tuples
    {
        static public void TuplesMain()
        {
            Console.WriteLine("\n-*-*-*-*-* Tuples1 *-*-*-*-*-");
            Tuples1();
            Console.WriteLine("\n-*-*-*-*-* ValueTuples1 *-*-*-*-*-");
            ValueTuples1();
        }

        static public void Tuples1()
        {
            // The Tuple<T> class was introduced in .NET Framework 4.0.
            // The Tuple is a reference type and not a value type.

            /*
            The Tuple is a reference type and not a value type. It allocates on heap and could result in CPU intensive operations.
            The Tuple is limited to include eight elements. You need to use nested tuples if you need to store more elements. However, this may result in ambiguity.
            The Tuple elements can be accessed using properties with a name pattern Item<elementNumber>, which does not make sense.
            */
            var person = Tuple.Create(1, "Steve", "Jobs");
            Console.WriteLine(person.Item1); // returns 1
            Console.WriteLine(person.Item2); // returns "Steve"
            Console.WriteLine(person.Item3); // returns "Jobs"


            var numbers = Tuple.Create("One", 2, 3, "Four", 5, "Six", 7, 8);
            Console.WriteLine(numbers.Item1); // returns "One"
            Console.WriteLine(numbers.Item2); // returns 2
            Console.WriteLine(numbers.Item3); // returns 3
            Console.WriteLine(numbers.Item4); // returns "Four"
            Console.WriteLine(numbers.Item5); // returns 5
            Console.WriteLine(numbers.Item6); // returns "Six"
            Console.WriteLine(numbers.Item7); // returns 7
            // Rest -> last element as a Tuple
            Console.WriteLine(numbers.Rest); // returns (8)
            Console.WriteLine(numbers.Rest.Item1); // returns 8

            // nested tuples
            Console.WriteLine("-*-*-* nested tuples *-*-*-");
            var numbers2 = Tuple.Create(1, 2, Tuple.Create(3, 4, 5, 6, 7, 8), 9, 10, 11, 12, 13);
            Console.WriteLine(numbers2.Item1); // returns 1
            Console.WriteLine(numbers2.Item2); // returns 2
            Console.WriteLine(numbers2.Item3); // returns (3, 4, 5, 6, 7,  8)
            Console.WriteLine(numbers2.Item3.Item1); // returns 3
            Console.WriteLine(numbers2.Item4); // returns 9
            Console.WriteLine(numbers2.Rest.Item1); //returns 13
        }

        static public void ValueTuples1()
        {
            // C# 7.0 (.NET Framework 4.7)(.net Standart 2.0) introduced the ValueTuple

            // create a values tuple.
            // equivalent Tuple
            //var person = Tuple.Create(1, "Bill", "Gates");
            //var person = (1, "Bill", "Gates");
            //ValueTuple<int, string, string> person = (1, "Bill", "Gates");
            (int, string, string) person = (1, "Bill", "Gates");

            Console.WriteLine(person.Item1);  // returns 1
            Console.WriteLine(person.Item2);   // returns "Bill"
            Console.WriteLine(person.Item3);   // returns "Gates"

            // Unlike Python tuple, .net Tuples require at least 2 values.
            var number = (1);  // int type, NOT a tuple
            var numbers = (1, 2); //valid tuple
            var numbers2 = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);

            // Tuple takes default Item1, Item2, Item3, ... properties.
            // We can assign names to the ValueTuple properties instead of having the default property

            (int Id, string FirstName, string LastName) insan = (0, "Mustafa", "Selçuk");
            Console.WriteLine($"{insan.Id}, {insan.FirstName}, {insan.LastName}.");

            //Also in easy way.
            (int, string, string) insansı = (Id: 47, FirstName: "Yeti", LastName: "MountainChild");

            var insansı2 = (Id: 0, FirstName: "Mustafa", LastName: "Selçuk");
            Console.WriteLine($"{insansı2.Id}, {insansı2.FirstName}, {insansı2.LastName}.");

            // Deconstruction pattern
            // "_" means; Discard it, i don't care about the value.
            (var id, var FName, _) = GetPerson();
            Console.WriteLine(FName);
        }
        static (int, string, string) GetPerson()
        {
            return (Id: 1, FirstName: "Bill", LastName: "Gates");
        }
    }
}
