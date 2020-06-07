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

                Song prevSong = new Song();
                prevSong.SongID = "";

                

                int parseInt;
                int parseInt2;
                List<int> peakList = new List<int>();
                List<int> weeksList = new List<int>();

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    int sameSongInt = prevSong.SongID.CompareTo(values[5]);
                    //Console.WriteLine("prevSong: " + prevSong.SongID);
                    //Console.WriteLine("Value: " + values[5]);
                    //Console.WriteLine(sameSongInt);

                    bool sameSong;
                    if (sameSongInt != 0) sameSong = false;
                    else sameSong = true;

                    if (sameSong) continue;
                    else
                    { 
                        Song song = new Song();
                        song.SongID = values[5];
                        //song.SongTitle = values[3];
                        //song.Performer = values[4];
                        //if (int.TryParse(values[8], out parseInt))
                        //{
                        //    int peak = parseInt;
                        //    song.Peak = peak;
                        //    peakList.Add(peak);
                        //}
                        //if (int.TryParse(values[9], out parseInt2))
                        //{
                        //    int weeks = parseInt;
                        //    song.WeeksOnChart = weeks;
                        //    weeksList.Add(weeks);
                        //}

                        //song.Peak = peakList.Max();
                        //song.WeeksOnChart = weeksList.Max();

                    
                        results.Add(song);
                        prevSong.SongID = song.SongID;
                    }
                    
                }
            }
            return results;
        }
    }
}
