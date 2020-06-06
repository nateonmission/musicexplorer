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
                string line = "";
                reader.ReadLine();
                reader.ReadLine();

                Song oldSong = new Song();
                oldSong.SongID = "";
                oldSong.SongTitle = "";
                oldSong.Performer = "";

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (!(oldSong.SongID == values[5]))
                    {
                        var song = new Song();
                        song.SongID = values[5];
                        song.SongTitle = values[3];
                        song.Performer = values[4];
                        results.Add(song);
                        oldSong = song;
                    }

                }

            }
            return results;

        }
    }
}
