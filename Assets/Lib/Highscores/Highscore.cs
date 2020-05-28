namespace Lib.Highscores
{
    public class Highscore
    {
        public string Playername { get; set; }
        public int Time { get; set; }

        public Highscore(string playername, int time)
        {
            Playername = playername;
            Time = time;
        }

        public string AsHumanFormat()
        {
            return Playername + ": " + Time + "Seconds";
        }
    }
}