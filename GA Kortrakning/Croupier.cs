class Croupier{
    public static List<Kort> GömdaKort { get; set; } = new List<Kort>();
    public static List<Kort> VisadeKort { get; set; } = new List<Kort>();

    public static void RevealCard()
        {
            VisadeKort.Add(GömdaKort[0]);
            GömdaKort.RemoveAt(0);
        }

    public static int HandSumma()
        {
            int summa = 0;
            foreach (Kort kort in VisadeKort)
            {
                summa += kort.Valör;
            }
            return summa;
        }

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