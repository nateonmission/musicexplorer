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

                Song prevSong = new Song();
                prevSong.SongID = "";
                prevSong.WeeksOnChart = 0;
                prevSong.Peak = 101;

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    Song newSong = GetSongObj(values);
                    
                    int sameSongInt = prevSong.SongID.CompareTo(newSong.SongID);
                    bool sameSong;
                    if (sameSongInt != 0) sameSong = false;
                    else sameSong = true;

                    if (sameSong)
                    {
                        prevSong = newSong;
                    }
                    else
                    {
                        results.Add(prevSong);
                        prevSong = newSong;
                    }
                    
                }
            }
            return results;
        }

        public static Song GetSongObj(String[] values)
        {
            int parseInt;
            
            Song song = new Song();

            song.SongID = values[5];
            song.SongTitle = values[3];
            song.Performer = values[4];

            DateTime parseDate = new DateTime();
            if (DateTime.TryParse(values[1], out parseDate))
            {
                song.LastWeekID = parseDate;
            }

            if (int.TryParse(values[8], out parseInt))
            {
                song.Peak = parseInt;
            }
            if (int.TryParse(values[9], out parseInt))
            {
                song.WeeksOnChart = parseInt;
            }
            song.FirstWeekID = song.LastWeekID.AddDays(-((song.WeeksOnChart - 1) * 7));

            return song;
        }

    }
}
