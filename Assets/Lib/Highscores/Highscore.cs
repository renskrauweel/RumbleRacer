namespace Lib.Highscores
{
    public class Highscore
    {
        public string Playername { get; set; }
        public int TimeMs { get; set; }

        public Highscore(string playername, int timeMs)
        {
            Playername = playername;
            TimeMs = timeMs;
        }

        public string AsHumanFormat()
        {
            return Playername + ": " + TimeMs + "MS";
        }
    }
}