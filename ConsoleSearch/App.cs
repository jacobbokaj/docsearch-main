﻿using System;
using System.Collections.Generic;

namespace ConsoleSearch
{
    public class App
    {
        public App()
        {
        }

        public void Run()
        {

            SearchLogic mSearchLogic = new SearchLogic(new Database());
            CommandOptions commandOptions = new CommandOptions();

            Console.WriteLine("Console Search");

            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                commandOptions.Commands();
                string input = Console.ReadLine();
                commandOptions.Run(input);



                if (input.Equals("q")) { break; }

                if (input.Contains('/') == false)
                {







                    var query = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);


                    var result = mSearchLogic.Search(query, 10, commandOptions.CasesensitiveFlag);

                    if (result.Ignored.Count > 0)
                    {
                        Console.WriteLine($"Ignored: {string.Join(',', result.Ignored)}");
                    }

                    //int idx = 1;
                    //foreach (var doc in result.DocumentHits)
                    //{
                    //    Console.WriteLine($"{idx} : {doc.Document.mUrl} -- contains {doc.NoOfHits} search terms");
                    //    Console.WriteLine("Index time: " + doc.Document.mIdxTime);
                    //    Console.WriteLine($"Missing: {ArrayAsString(doc.Missing.ToArray())}");
                    //    idx++;
                    //}
                    Console.WriteLine("Documents: " + result.Hits + ". Time: " + result.TimeUsed.TotalMilliseconds);
                }
            }
        }

        string ArrayAsString(string[] s)
        {
            return s.Length == 0 ? "[]" : $"[{String.Join(',', s)}]";
            //foreach (var str in s)
            //    res += str + ", ";
            //return res.Substring(0, res.Length - 2) + "]";
        }
    }
}
