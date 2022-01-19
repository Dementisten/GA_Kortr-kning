using System;
using System.Collections.Generic;
class Kortlek{
    public List<Kort> kortlek = new List<Kort>();
    public void FillDeck(){
        for (int i = 0; i < 52; i++)
          {
             Kort.Suites suite = (Kort.Suites)(Math.Floor((decimal)i/13));
             //Add 2 to value as a cards start a 2
             int val = i%13 + 2;
             kortlek.Add(new Kort(val, suite));
          }
    }

    public void PrintDeck(){
        foreach(Kort element in kortlek){
            Console.WriteLine(element.Namn);
        }
    }
}