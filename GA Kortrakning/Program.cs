using System.IO;
// See https://aka.ms/new-console-template for more information
class Program
    {

        private static Kortlek kortlek = new Kortlek();
        private static Spelare spelare = new Spelare();

        int Highscore = 1;

        //Olika enum som beskriver de olika resultaten som kan bli av en runda
        private enum Resultat
        {
            Lika,
            SpelareVinner,
            SpelareTjock,
            SpelareBlackjack,
            DealerVinner,
            FelBet
        }
        //Kollar om handen är blackjack eller inte och returnerar en bool true eller false
        public bool IsHandBlackjack(List<Kort> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.Ess && hand[1].Valör == 10) return true;
                else if (hand[1].Face == Face.Ess && hand[0].Valör == 10) return true;
            }
            return false;
        }
        //Enkel metod som skriver ut lite information om rundan
        static void Text(){
            Console.Clear();
            Console.WriteLine("Antal kort kvar: " + kortlek.Count());
            spelare.WriteHand();
            Croupier.WriteHand();
        }
        //Action är den metod som anropas när spelaren ska göra ett val i rundan, den består av en loop som loopas tills spelaren väljer Stand eller Double eller har över 21
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

                //Byter det till stora bokstäver för att alla kombinationer av stora och små ska funka för användaren
                switch (action.ToUpper())
                {
                    //För varje val finns ett case som gör olika saker
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
                        Console.WriteLine("Hit, Stand eller Double");
                        Console.ReadKey();
                        break;
                }

                //Om spelarens handsumma är över 21 så kollar man om ett av spelarens kort är ett ess och såfall konverterar man det till en etta enligt blackjack regler
                //Annars bryts loopen
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
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE") && spelare.HandSumma() <= 21);
        }
        }
        //Här kommer de enum från början in igen och för varje resultat så betalas olika summor ut och statistiken uppdateras
        static void AvslutaRunda(Resultat resultat){
            switch (resultat){
                case Resultat.Lika:
                    spelare.ReturnBet();
                    Console.WriteLine("Lika");
                    spelare.SpeladeHänder++;
                    break;
                case Resultat.SpelareVinner:
                    spelare.Vinst(false);
                    Console.WriteLine("Spelare vann ");
                    spelare.SpeladeHänder++;
                    spelare.VunnaHänder++;
                    break;
                case Resultat.SpelareTjock:
                    spelare.ClearBet();
                    Console.WriteLine("Spelare tjock");
                    spelare.SpeladeHänder++;
                    break;
                case Resultat.SpelareBlackjack:
                    spelare.Vinst(true);
                    Console.WriteLine("Spelare vann med blackjack ");
                    spelare.SpeladeHänder++;
                    spelare.VunnaHänder++;
                    break;
                case Resultat.DealerVinner:
                    spelare.ClearBet();
                    Console.WriteLine("Croupieren vann");
                    spelare.SpeladeHänder++;
                    break;
            }
            
            //Efter varje vinst eller förlust kollas spelarens chips och jämförs med highscore, om det är högre än highscore blir highscoret mängden chips spelaren har
            if(spelare.Chips > spelare.Highscore)
                spelare.Highscore = spelare.Chips;

            if (spelare.Chips <= 0){
                Console.WriteLine("Spelare bankrupt efter " + spelare.SpeladeHänder + " rundor och " + spelare.VunnaHänder + " vunna händer.");
                Console.WriteLine("Dina chips har återställts.");

                //När spelaren blir bankrupt startar spelet om och chipsen återställs. Innan det så skrivs highscore ut i Textfil.txt
                StreamWriter sw = new StreamWriter("Textfil.txt", true);
                sw.WriteLine("Highscore: " + spelare.Highscore + " chips");
                sw.Close();
                spelare = new Spelare();
            }
            

            Console.WriteLine("Tryck för att fortsätta.");
            Console.ReadLine();
            StartaRunda();

        }
        
        static void StartaRunda(){
            
            //Betta
            Console.Write("Chips: " + spelare.Chips);
            Console.WriteLine("   " + "Running count: " + kortlek.RunningCount);
            Console.WriteLine("Antal kort kvar: " + kortlek.Count());
            Console.WriteLine("Lägg ett bet för att spela: ");
            try{
                int b = int.Parse(Console.ReadLine());

                if (b <= spelare.Chips)
                    spelare.Betta(b);

                else{
                    Console.WriteLine("Felaktigt bet!");
                    StartaRunda();
                }    
            }
            catch{
                Console.WriteLine("Ange ett heltal");
                StartaRunda();
            }
            

            //Ta kort
            kortlek.Shuffle();
            spelare.Hand = kortlek.TakeHand();
            
            Croupier.GömdaKort = kortlek.TakeHand();
            Croupier.VisadeKort = new List<Kort>();

            //Om båda korten i handen är ess konverteras ett av dem till en etta, för både spelaren och croupieren
            if (spelare.Hand[0].Face == Face.Ess && spelare.Hand[1].Face == Face.Ess)
            {
                spelare.Hand[1].Valör = 1;
            }

            if (Croupier.GömdaKort[0].Face == Face.Ess && Croupier.GömdaKort[1].Face == Face.Ess)
            {
                Croupier.GömdaKort[1].Valör = 1;
            }
            Croupier.RevealCard();

            
            

            //Visa Kort
            Text();

            //Kollar om spelaren har blackjack och avslutar såfall rundan med resultaten SpelareBlackjack
            if (spelare.HandSumma() == 21){
                AvslutaRunda(Resultat.SpelareBlackjack);
            }

            //Här får spelaren göra sina val
            Action();

            //Visa Kort igen
            Croupier.RevealCard();
            Text();

            //Checka om spelare är tjock
            if (spelare.HandSumma() > 21){
                AvslutaRunda(Resultat.SpelareTjock);
            }

            //Croupier spelar
            //Om croupieren får 17 eller över slutar den ta kort
            while (Croupier.HandSumma() <= 16){
                Croupier.VisadeKort.Add(kortlek.TakeCard());

                Thread.Sleep(1000);

                Console.Clear();
                spelare.WriteHand();
                Croupier.WriteHand();
            }
            //Om croupieren har över 21 så vinner spelaren direkt eftersom vi redan kollat så spelaren inte är tjock
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



            //Om spelaren inte vann eller det blev lika ännu så vinner Croupieren
            AvslutaRunda(Resultat.DealerVinner);


        }
        
        static void Main(string[] args)
        {
            //Kortleken läggs till här istället för i StartaRunda() för att det ska vara möjligt att räkna kort. Om man lägger den i StartaRunda kommer den fyllas på varje gång en runda startar och det går inte längre räkna korten
            kortlek.FillDeck();
            StartaRunda();
        }
    }
