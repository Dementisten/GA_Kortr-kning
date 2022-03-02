using System;
using System.Collections.Generic;

class Kortlek{
    public List<Kort> kortlek = new List<Kort>();
    public void FillDeck(){
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                kortlek.Add(new Kort((Suites)j, (Face)i));
            }
        }
    }

    static Random r = new Random();
    public void Shuffle(){
        for (int n = kortlek.Count - 1; n > 0; --n)
        {
            int k = r.Next(n+1);
            Kort temp = kortlek[n];
            kortlek[n] = kortlek[k];
            kortlek[k] = temp;
        }
    }

    public Kort TakeCard(){
        Kort k = kortlek[0];
        kortlek.RemoveAt(0);
        return k;
    }

    public List<Kort> TakeHand(){
        List<Kort> hand = new List<Kort>();
        hand.Add(kortlek[0]);
        hand.Add(kortlek[1]);
        kortlek.RemoveRange(0, 2);
        return hand;
    }

    public void PrintDeck(){
        foreach(Kort element in kortlek){
            element.SkrivKort();
        }
    }

    public int Count(){
        int c;
        c = kortlek.Count;
        return c;
    }
}