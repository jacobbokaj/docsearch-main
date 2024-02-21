using System;
using Shared;

namespace Renamer
{
	public class App
	{
		public void Run() {

            Crawler renamer = new Crawler();
            renamer.Crawl(new DirectoryInfo(Paths.FOLDER), RenameFile);
            Console.WriteLine("Done with");
            Console.WriteLine("Folders: " + renamer.CountFolders);
            Console.WriteLine("Files:   " + CountFiles);
        }

        void RenameFile(FileInfo f)
        {
            Console.WriteLine($"Behandler {f.FullName}");

            if (f.FullName.EndsWith(".txt")) return;

            if (f.Name.StartsWith('.')) return;



            var ending = f.FullName.EndsWith(".") ? "txt" : ".txt";

            File.Move(f.FullName, f.FullName + ending, true);

            CountFiles++;
        }

        public int CountFiles { get; private set; }
    }
}

