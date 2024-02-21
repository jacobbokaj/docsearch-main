using System.Collections.Generic;
using Shared.Model;

namespace ConsoleSearch
{
    public interface IDatabase
    {
        List<int> GetWordIds(string[] query, out List<string> outIgnored);
        List<BEDocument> GetDocDetails(List<int> docIds);
        List<KeyValuePair<int, int>> GetDocuments(List<int> wordIds);
        List<int> getMissing(int docId, List<int> wordIds);
        List<string> WordsFromIds(List<int> wordIds);
    }
}