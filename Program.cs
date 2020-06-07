using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace musicExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            var fileName = Path.Combine(directory.FullName, "MusicData.csv");
            var fileContents = ReadSongs(fileName);
        }




        public static HashSet<Song> ReadSongs(string fileName)
        {
            var results = new HashSet<Song>();
            using (var reader = new StreamReader(fileName))
            {
                reader.ReadLine();
                reader.ReadLine();

                string line = "";
                string[] values = line.Split(',');

                Song oldSong = new Song();
                oldSong.SongID = null;

                int parseInt;
                List<int> peakList = new List<int>();
                List<int> weeksList = new List<int>();

                
                while ((line = reader.ReadLine()) != null)
                {
                    Song song = new Song();

                    while (oldSong.SongID == values[5])
                    {
                        parseInt = 101;

                        song.SongID = values[5];
                        song.SongTitle = values[3];
                        song.Performer = values[4];
                        
                        if (values[8] == "")
                        {
                            continue;
                        }
                        else if (int.TryParse(values[8], out parseInt))
                        {
                            int peak = parseInt;
                            peakList.Add(peak);
                        }

                        if (values[9] == "")
                        {
                            continue;
                        }
                        else if (int.TryParse(values[9], out parseInt))
                        {
                            int weeks = parseInt;
                            peakList.Add(weeks);
                        }
                        oldSong = song;
                    }
                    
                    
                    song.Peak = peakList.Max();
                    song.WeeksOnChart = weeksList.Max();

                    results.Add(song);

                    
                }
            }
            return results;
        }
    }
}
