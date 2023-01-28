using System;
using System.Collections.Generic;
using System.IO;

namespace Journal
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> prompts = new List<string>()
            {
                "who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
            Journal journal = new Journal();
        

            while (true)
            {

                showMenu();

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        string prompt = GetRandomPrompt(prompts);
                        Console.WriteLine($"{prompt}");
                        string response = Console.ReadLine();
                        Entry entry = new Entry(prompt, response);
                        journal.AddEntry(entry);
                        break;

                    case 2:
                        journal.DisplayEntries();
                        break;

                    case 3:
                        Console.WriteLine("Enter a filename:");
                        string filename = Console.ReadLine();

                        journal.Save(filename);
                        break;
                    case 4:
                        Console.WriteLine("Enter a filename:");
                        filename = Console.ReadLine();
                        journal.Load(filename);
                        break;
                    case 5:
                        return;
                }
            }
        }

        static void showMenu()
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
        }

        static string GetRandomPrompt(List<string> prompts)
        {
            Random random = new Random();
            int index = random.Next(0, prompts.Count);
            return prompts[index];
        }    
    }
}

namespace Journal
{
    class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }
        
        public Entry(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToString("d");
        }
    }
}

namespace Journal
{
    class Journal
    {
        List<Entry> entries;

        public Journal()
        {
            entries = new List<Entry>();
        }
        
        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }
        
        public void DisplayEntries()
        {
            foreach (Entry entry in entries)
            {
                Console.WriteLine($"{entry.Date} - {entry.Prompt}");
                Console.WriteLine($"{entry.Response}");
                Console.WriteLine();
            }
        }

        public void Save(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    sw.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
        }

        public void Load(string filename)
        {
            entries.Clear();
            
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split('|');
                    
                    Entry entry = new Entry(parts[1], parts[2]);
                    entry.Date = parts[0];
                    
                    entries.Add(entry);
                }
            }
        }
    }
}