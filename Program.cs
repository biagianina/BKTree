using System;
using System.IO;

namespace BK
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (Object obj in args)
                {
                    var text = File.ReadLines(obj.ToString());
                    var tree = new BKTree();

                    foreach(var word in text)
                    {
                        tree.Add(word);
                    }
                    Console.WriteLine("Correction suggestions for word \"wat\" are:");

                    foreach (var match in tree.Match("wat", 1))
                    {
                        Console.WriteLine(match);
                    }
                }
                
                
            }

            Console.Read();
        }
    }
}
