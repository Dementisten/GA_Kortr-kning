using System;
using System.Collections.Generic;

class Kortlek{
    public List<Kort> kortlek = new List<Kort>();

    public int RunningCount {get; set;}

    public void FillDeck(){
        //Loopar 6 gånger för att blackjack ska ha 6 kortlekar
        for(int a = 0; a < 6; a++)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    kortlek.Add(new Kort((Suites)j, (Face)i));
                }
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

    //Tar ett kort ur kortleken och räknar även korten åt spelaren. 10 eller över blir -1 och 6 eller mindre blir +1 returnerar ett kort
    public Kort TakeCard(){
        Kort k = kortlek[0];
        kortlek.RemoveAt(0);

        if (k.Valör >= 10){
            RunningCount--;
        }
        else if (k.Valör <= 6){
            RunningCount++;
        }
        return k;
    }

    public List<Kort> TakeHand(){
        //Samma som TakeCard() fast det returneras i en lista med 2 kort för spelarens hand
        List<Kort> hand = new List<Kort>();
        hand.Add(TakeCard());
        hand.Add(TakeCard());
        return hand;
    }

    public void PrintDeck(){
        //Funktion för att se till att alla kort funkar som dem ska
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