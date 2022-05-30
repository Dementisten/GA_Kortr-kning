class Croupier{
    public static List<Kort> GömdaKort { get; set; } = new List<Kort>();
    public static List<Kort> VisadeKort { get; set; } = new List<Kort>();

    //Byter ett kort från GömdaKort till VisadeKort
    public static void RevealCard()
        {
            VisadeKort.Add(GömdaKort[0]);
            GömdaKort.RemoveAt(0);
        }
    //Skriver ut summan för handen, används för att kolla vem som vinner bland annat
    public static int HandSumma()
        {
            int summa = 0;
            foreach (Kort kort in VisadeKort)
            {
                summa += kort.Valör;
            }
            return summa;
        }
    //Skriver ut information om handen, skriver ut både visade kort och gömda kort och skriver även ut summan för handen 
    public static void WriteHand()
        {
            Console.WriteLine("Dealerns Hand: " + HandSumma());
            foreach (Kort kort in VisadeKort)
            {
                kort.SkrivKort();
            }
            for (int i = 0; i < GömdaKort.Count; i++)
            {
                Console.WriteLine("Gömt");
            }
            Console.WriteLine();
        }
}