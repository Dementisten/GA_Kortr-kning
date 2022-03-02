using static Suites;
using static Face;

public enum Suites{
        Hjärter,
        Ruter,
        Spader,
        Klöver,
    }

    public enum Face
    {
        Ess,
        Två,
        Tre,
        Fyra,
        Fem,
        Sex,
        Sju,
        Åtta,
        Nio,
        Tio,
        Knekt,
        Dam,
        Kung
    }

class Kort{
    public Kort(Suites färg, Face face)
    {
        Face = face;
        Färg = färg;

        switch (Face)
            {
                case Tio:
                case Knekt:
                case Dam:
                case Kung:
                    Valör = 10;
                    break;
                case Ess:
                    Valör = 11;
                    break;
                default:
                    Valör = (int)Face + 1;
                    break;
            }
    }

    
    public int Valör{get; set;}
    public Face Face {get;}

    public Suites Färg{get; set;}

    public void SkrivKort(){
        Console.WriteLine(Färg + " " + Face);
    }

    

}