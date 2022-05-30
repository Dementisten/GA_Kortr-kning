class Spelare{
    public int Chips {get; set;} = 10000;
    public int Bet {get; set;}
    public int SpeladeHänder {get; set;}
    public int VunnaHänder {get; set;}
    public int Highscore {get; set;}

    public List<Kort> Hand {get; set;}

    //Bettet blir det man skriver in i metoden och det subtraheras från spelarens chips
    public void Betta(int bet){
        Bet += bet;
        Chips -= bet;
    }

    //Återställer bettet
    public void ClearBet(){
        Bet = 0;
    }

    //När rundan blir lika så ska bettet returneras, då anropas denna metoden
    public void ReturnBet(){
        Chips += Bet;
        ClearBet();
    }
    //Vid blackjack ska vinsten vara högre, därför måste det kollas innan vinsten betalas ut och skrivs med när metoden tillkallas
    public int Vinst(bool blackjack){
        int ChipsWon;
        if (blackjack){
            ChipsWon = (int)(Bet * 2.5);
        }
        else{
            ChipsWon = (Bet * 2);
        }

        Chips += ChipsWon;
        ClearBet();
        return ChipsWon;
    }

    public int HandSumma(){
        int summa = 0;
        foreach(Kort kort in Hand){
            summa += kort.Valör;
        }
        return summa;
    }
    //Skriver ut information om spelet och även spelarens hand i rundan
    public void WriteHand()
        {
            Console.Write("Bet: ");
            Console.Write(Bet + "  ");
            Console.Write("Chips: ");
            Console.Write(Chips + "  ");
            Console.Write("Rundor körda: ");
            Console.Write(SpeladeHänder + " ");
            Console.Write("Rundor vunna: ");
            Console.Write(VunnaHänder + " ");

            Console.WriteLine();
            Console.WriteLine("Din hand: " + HandSumma());
            foreach (Kort kort in Hand) {
                kort.SkrivKort();
            }
            Console.WriteLine();
        }

}