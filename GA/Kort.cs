class Kort{
    public Kort(int valör, Suites färg)
    {
        Valör = valör;
        Färg = färg;
    }

    public enum Suites{
        Hjärter,
        Ruter,
        Spader,
        Klöver,
    }
    
    public int Valör{get; set;}

    public Suites Färg{get; set;}

    public string Namn{
        get{
            return Färg + " " + Valör;
        }
    }

    

}