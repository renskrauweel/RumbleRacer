namespace Lib.Highscores
{
    public class Highscore
    {
        public string Circuit { get; set; }
        public string Playername { get; set; }
        public double Time { get; set; }

        public Highscore(string circuit, string playername, double time)
        {
            Circuit = circuit;
            Playername = playername;
            Time = time;
        }

        public string AsHumanFormat()
        {
            return Playername + ": " + Time + "S";
        }
    }
}