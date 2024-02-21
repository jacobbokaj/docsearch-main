using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using indexer;
using Shared.Model;

namespace Indexer
{
    public class Crawler
    {
        private readonly char[] separators = " \\\n\t\"$'!,?;.:-_**+=)([]{}<>/@&%€#".ToCharArray();
        /* Will be used to spilt text into words. So a word is a maximal sequence of
         * chars that does not contain any char from separators */

        private Dictionary<string, int> words = new Dictionary<string, int>();

        private List<WordWithFrequrency> wordsWithFrequrencyList = new List<WordWithFrequrency>();
        
        /* Will contain all words from files during indexing - thet key is the 
         * value of the word and the value is its id in the database */

        private int documentCounter = 0;
        /* Will count the number of documents indexed during indexing */

        IDatabase mdatabase;

        public Crawler(IDatabase db) { mdatabase = db; }

        //Return a dictionary containing all words (as the key)in the file
        // [f] and the value is the number of occurrences of the key in file.
        private ISet<string> ExtractWordsInFile(FileInfo f)
        {
            ISet<string> res = new HashSet<string>();
            var content = File.ReadAllLines(f.FullName);
            foreach (var line in content)
            {
                foreach (var aWord in line.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                {
                    res.Add(aWord);
                }
            }

            return res;
        }

        private ISet<int> GetWordIdFromWords(ISet<string> src)
        {
            ISet<int> res = new HashSet<int>();

            foreach (var p in src)
            {
                res.Add(words[p]);
            }
            return res;
        }

        private ISet<int> GetWordWithFreduncyIdFromWords(ISet<string> src)
        {
            ISet<int> res = new HashSet<int>();

            for (int i = 0; i < src.Count; i++)
            {
                res.Add(wordsWithFrequrencyList[i].Index);
            }


            //foreach (var p in src)
            //{
            //    res.Add(wordsWithFrequrencyList.[p]);
            //}
            return res;
        }



        // Return a dictionary of all the words (the key) in the files contained
        // in the directory [dir]. Only files with an extension in
        // [extensions] is read. The value part of the return value is
        // the number of occurrences of the key.
        public void IndexFilesIn(DirectoryInfo dir, List<string> extensions)
        {

            Console.WriteLine($"Crawling {dir.FullName}");

            foreach (var file in dir.EnumerateFiles())
                if (extensions.Contains(file.Extension))
                {
                    documentCounter++;
                    BEDocument newDoc = new BEDocument
                    {
                        mId = documentCounter,
                        mUrl = file.FullName,
                        mIdxTime = DateTime.Now.ToString(),
                        mCreationTime = file.CreationTime.ToString()
                    };

                    mdatabase.InsertDocument(newDoc);
                    Dictionary<string, int> newWords = new Dictionary<string, int>();
                    ISet<string> wordsInFile = ExtractWordsInFile(file);
                    foreach (var aWord in wordsInFile)
                    {
                        if (!words.ContainsKey(aWord))
                        {
                            words.Add(aWord, words.Count + 1);
                            newWords.Add(aWord, words[aWord]);
                        }
                    }
                    mdatabase.InsertAllWords(newWords);

                    mdatabase.InsertAllOcc(newDoc.mId, GetWordIdFromWords(wordsInFile));


                }
            foreach (var d in dir.EnumerateDirectories())
                IndexFilesIn(d, extensions);
        }


        public void IndexFilesWithListStartLooper(DirectoryInfo dir, List<string> extensions)
        {
            if (dir.Exists)
            {
                wordsWithFrequrencyList = new List<WordWithFrequrency>();
                documentCounter = 0;
                foreach (var file in dir.EnumerateFiles())
                {
                    //      Console.WriteLine("Running to file: " + dir.EnumerateFiles()  + "   wordsWIthFrequrencyList: " + wordsWithFrequrencyList.Count);
                    if (extensions.Contains(file.Extension))
                    {

                        IndexFilesInWithListLooper(file, wordsWithFrequrencyList);
                      //  mdatabase.InsertAllOcc(documentCounter, GetWordWithFreduncyIdFromWords(wordsWithFrequrencyList));
                    //    break;
                    }
                }
                //mdatabase.InsertAllWordsWithFrequrencies(wordsWithFrequrencyList);
                // Her skal du få den til at kunne tilføje alle dokumenterne til sidst, fordi at "words" kan blive opdateret undervejs, som gør de først bliver "insert" til sidst.
            }
        }



        private void IndexFilesInWithListLooper(FileInfo file, List<WordWithFrequrency> wordWithFrequrencies)
        {
            documentCounter++;
            BEDocument newDoc = new BEDocument
            {
                mId = documentCounter,
                mUrl = file.FullName,
                mIdxTime = DateTime.Now.ToString(),
                mCreationTime = file.CreationTime.ToString()
            };

            mdatabase.InsertDocument(newDoc);

            List<WordWithFrequrency> newWords = new List<WordWithFrequrency>();
            ISet<string> wordsInFile = ExtractWordsInFile(file);
            foreach (var aWord in wordsInFile)
            {
                WordWithFrequrency target = wordWithFrequrencies.FirstOrDefault(x => x.Word.Equals(aWord, StringComparison.OrdinalIgnoreCase));

                if (target == default(WordWithFrequrency))
                {
                    var newId = wordWithFrequrencies.Count + 1;
                    wordWithFrequrencies.Add(new WordWithFrequrency(newId, aWord));
                    newWords.Add(new WordWithFrequrency(newId, aWord));
                }
                else
                {
                    target.FrequrencyOld = target.Frequrency;
                    target.Frequrency += 1;

                }
            }
            mdatabase.InsertAllWordsWithFrequrencies(wordsWithFrequrencyList);
         //      mdatabase.InsertAllOcc(newDoc.mId, GetWordWithFreduncyIdFromWords(wordsInFile));
        }


        public void IndexFilesInWithList(DirectoryInfo dir, List<string> extensions, int looperIndex)
        {


            foreach (var file in dir.EnumerateFiles())
                if (extensions.Contains(file.Extension))
                {
                    documentCounter++;
                    BEDocument newDoc = new BEDocument
                    {
                        mId = documentCounter,
                        mUrl = file.FullName,
                        mIdxTime = DateTime.Now.ToString(),
                        mCreationTime = file.CreationTime.ToString()
                    };




                    mdatabase.InsertDocument(newDoc);
                    List<WordWithFrequrency> newWords = new List<WordWithFrequrency>();
                    ISet<string> wordsInFile = ExtractWordsInFile(file);
                    Console.WriteLine("wordWithFre: " + wordsWithFrequrencyList.Count);
                    foreach (var aWord in wordsInFile)
                    {

                        // Console.WriteLine(aWord.ToString());
                        WordWithFrequrency target = wordsWithFrequrencyList.FirstOrDefault(x => x.Word == aWord);
                        // WordWithFrequrency target = newWords.FirstOrDefault(x => x.Word == aWord);
                        // Console.WriteLine(target == default(WordWithFrequrency));
                        // Console.WriteLine("aWord:    " + aWord);
                        if (target == default(WordWithFrequrency))
                        {
                            var newId = wordsWithFrequrencyList.Count + 1 + looperIndex;
                            wordsWithFrequrencyList.Add(new WordWithFrequrency(newId, aWord));
                            newWords.Add(new WordWithFrequrency(newId, aWord));
                        }
                        else
                        {
                            target.Frequrency += 1;
                        }
                    }
                    //Console.WriteLine(newWords.Count);

                    //   mdatabase.InsertAllOcc(newDoc.mId, GetWordWithFreduncyIdFromWords(wordsInFile));

                }
            int i = 0;
            foreach (var d in dir.EnumerateDirectories())
            {
                Console.WriteLine("Running rekursiv: " + i);
                IndexFilesInWithList(d, extensions, wordsWithFrequrencyList.Count);
                i++;

            }
            //    mdatabase.InsertAllWordsWithFrequrencies(wordsWithFrequrencyList);
        }
    }
}
