using System;
using System.Collections.Generic;

namespace GA
{
    class Program
    {
        static void Main(string[] args)
        {
            Kortlek kortlek = new Kortlek();

            kortlek.FillDeck();
            kortlek.FillDeck();
            kortlek.PrintDeck();
        }
    }
}
