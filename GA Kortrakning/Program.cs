// See https://aka.ms/new-console-template for more information
class Program
    {

        private static Kortlek kortlek = new Kortlek();
        private static Spelare spelare = new Spelare();

        private enum Resultat
        {
            Lika,
            SpelareVinner,
            SpelareTjock,
            SpelareBlackjack,
            DealerVinner,
            FelBet
        }

        public bool IsHandBlackjack(List<Kort> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.Ess && hand[1].Valör == 10) return true;
                else if (hand[1].Face == Face.Ess && hand[0].Valör == 10) return true;
            }
            return false;
        }

        static void Text(){
            Console.Clear();

            Console.WriteLine("Antal kort kvar: " + kortlek.Count());
            spelare.WriteHand();
            Croupier.WriteHand();
        }

        static void Action(){
            {
            string action;
            do
            {
                Console.Clear();
                spelare.WriteHand();
                Croupier.WriteHand();

                Console.Write("Vad vill du göra? ");
                action = Console.ReadLine();

                switch (action.ToUpper())
                {
                    case "HIT":
                        spelare.Hand.Add(kortlek.TakeCard());
                        break;
                    case "STAND":
                        break;
                    case "DOUBLE":
                        if (spelare.Chips >= spelare.Bet)
                        {
                            spelare.Chips -= spelare.Bet;
                            spelare.Bet = spelare.Bet * 2;
                        }
                        
                        spelare.Hand.Add(kortlek.TakeCard());
                        break;
                    default:
                        Console.WriteLine("Hit, Stand, Double");
                        Console.ReadKey();
                        break;
                }

                if (spelare.HandSumma() > 21)
                {
                    foreach (Kort kort in spelare.Hand)
                    {
                        if (kort.Valör == 11)
                        {
                            kort.Valör = 1;
                            break;
                        }
                    }
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE")
                && spelare.HandSumma() <= 21);
        }
        }

        static void AvslutaRunda(Resultat resultat){
            switch (resultat){
                case Resultat.Lika:
                    spelare.ReturnBet();
                    Console.WriteLine("Lika");
                    break;
                case Resultat.SpelareVinner:
                    spelare.Vinst(false);
                    Console.WriteLine("Spelare vann ");
                    break;
                case Resultat.SpelareTjock:
                    spelare.ClearBet();
                    Console.WriteLine("Spelare tjock");
                    break;
                case Resultat.SpelareBlackjack:
                    spelare.Vinst(true);
                    Console.WriteLine("Spelare vann med blackjack ");
                    break;
                case Resultat.DealerVinner:
                    spelare.ClearBet();
                    Console.WriteLine("Croupieren vann");
                    break;
            }

            if (spelare.Chips <= 0){
                Console.WriteLine("Spelare bankrupt efter " + spelare.SpeladeHänder + " rundor.");

                spelare = new Spelare();
            }

            Console.WriteLine("Tryck för att fortsätta.");
            Console.ReadLine();
            StartaRunda();

        }
        
        static void StartaRunda(){
            kortlek.Shuffle();
            spelare.Hand = kortlek.TakeHand();
            
            Croupier.GömdaKort = kortlek.TakeHand();
            Croupier.VisadeKort = new List<Kort>();

            if (spelare.Hand[0].Face == Face.Ess && spelare.Hand[1].Face == Face.Ess)
            {
                spelare.Hand[1].Valör = 1;
            }

            if (Croupier.GömdaKort[0].Face == Face.Ess && Croupier.GömdaKort[1].Face == Face.Ess)
            {
                Croupier.GömdaKort[1].Valör = 1;
            }
            Croupier.RevealCard();

            //Betta
            Console.WriteLine("Chips: " + spelare.Chips);
            Console.WriteLine("Lägg ett bet för att spela: ");
            int b = int.Parse(Console.ReadLine());

            if (b <= spelare.Chips)
                spelare.Betta(b);

            else{
                Console.WriteLine("Felaktigt bet!");
                StartaRunda();
            }
            

            //Visa Kort
            Text();

            //Blackjack?
            if (spelare.HandSumma() == 21){
                AvslutaRunda(Resultat.SpelareBlackjack);
            }

            //Spela
            Action();

            //Visa Kort igen
            Croupier.RevealCard();
            Text();

            //Checka om spelare är tjock
            if (spelare.HandSumma() > 21){
                AvslutaRunda(Resultat.SpelareTjock);
            }

            //Croupier spelar
            while (Croupier.HandSumma() <= 16){
                Croupier.VisadeKort.Add(kortlek.TakeCard());
            }

            if (Croupier.HandSumma() > 21){
                AvslutaRunda(Resultat.SpelareVinner);
            }

            //Kolla vem som vann
            if (spelare.HandSumma() > Croupier.HandSumma()){
                AvslutaRunda(Resultat.SpelareVinner);
            }
            else if (spelare.HandSumma() == Croupier.HandSumma()){
                AvslutaRunda(Resultat.Lika);
            }








        }
        
        static void Main(string[] args)
        {
            kortlek.FillDeck();
            kortlek.FillDeck();
            kortlek.FillDeck();
            kortlek.FillDeck();
            kortlek.FillDeck();
            kortlek.FillDeck();
            StartaRunda();
        }
    }
