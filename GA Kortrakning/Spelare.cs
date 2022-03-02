class Spelare{
    public int Chips {get; set;} = 10000;
    public int Bet {get; set;}
    public int SpeladeHänder {get; set;}

    public List<Kort> Hand {get; set;}

    public void Betta(int bet){
        Bet += bet;
        Chips -= bet;
    }

    public void ClearBet(){
        Bet = 0;
    }

    public void ReturnBet(){
        Chips += Bet;
        ClearBet();
    }

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

    public void WriteHand()
        {
            Console.Write("Bet: ");
            Console.Write(Bet + "  ");
            Console.Write("Chips: ");
            Console.Write(Chips + "  ");
            Console.Write("Rundor körda: ");
            Console.Write(SpeladeHänder + " ");

            Console.WriteLine();
            Console.WriteLine("Din hand: " + HandSumma());
            foreach (Kort kort in Hand) {
                kort.SkrivKort();
            }
            Console.WriteLine();
        }

}