Version 1: 12-2-2024

A seachengine that consist of an indexer and a search program.

The indexer will crawl a folder (in depth) and create a reverse index
in a database. It will only index text files with .txt as extension.

The search program is a console program that offers a query-based search
in the reverse index. It is in the ConsoleSearch project.

The class library Shared contains classes that are used by the indexer
and the ConsoleSearch. It contains:

- Paths containing static paths for 1) files to index and, 2) a path for
  the database
- BEDocument (BE for Business Entity) - a class representing a document.

The project Renamer is a console program used to rename all files in a
folder. Current version will rename all files with no extension to have
.txt as extension.


