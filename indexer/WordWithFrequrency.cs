using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indexer
{
    public class WordWithFrequrency
    {
        private int index;
        private string word;
        private int frequrency;


        public int Index { get => index; }
        public string Word { get => word; }
        public int Frequrency { get => frequrency; set => frequrency = value; }

        private int frequrencyOld;
        public int FrequrencyOld { get => frequrencyOld; set => frequrencyOld = value; }

        public WordWithFrequrency(int index, string word)
        {
            this.index = index;
            this.word = word;
            frequrency = 1;
            frequrencyOld = 0;
        }

        public WordWithFrequrency(int index, string word, int frequrency)
        {
            this.index = index;
            this.word = word;
            this.frequrency = frequrency;
        }
        public override string ToString()
        {
            return "<" + Index + ", " + Word + "> " + Frequrency;
        }
    }
}
