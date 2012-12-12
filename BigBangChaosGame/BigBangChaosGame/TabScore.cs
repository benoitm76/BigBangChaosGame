using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace BigBangChaosGame
{
    // classe des meilleurs scores
    public class TabScore
    {

        [Serializable]
        public struct HighScoreData
        {
            public string[] PlayerName;
            public int[] Score;
            public int Count;

          public HighScoreData(int count)
          {
              PlayerName = new string[count];
              Score = new int[count];

               Count = count;
          }
       }


        public void Ini()
        {
            // Get the path of the save game
            string fullpath = "highscores.dat";

            // Check to see if the save exists
#if WINDOWS
            if (!File.Exists(fullpath))
            {
                //If the file doesn't exist, make a fake one...
                // Create the data to save
                data = new HighScoreData(5);
                data.PlayerName[0] = "botneil";
                data.Score[0] = 20;

                data.PlayerName[1] = "botshawn";
                data.Score[1] = 10;

                data.PlayerName[2] = "botmark";
                data.Score[2] = 9;

                data.PlayerName[3] = "botcindy";
                data.Score[3] = 8;

                data.PlayerName[4] = "botsam";
                data.Score[4] = 2;

                SaveHighScores(data, HighScoresFilename, device);
            }
#elif XBOX
 
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!iso.FileExists(fullpath))
                {
                    //If the file doesn't exist, make a fake one...
                    // Create the data to save
                    data = new HighScoreData(5);
                     data.PlayerName[0] = "botneil";
                data.Score[0] = 20;

                data.PlayerName[1] = "botshawn";
                data.Score[1] = 10;

                data.PlayerName[2] = "botmark";
                data.Score[2] = 9;

                data.PlayerName[3] = "botcindy";
                data.Score[3] = 8;

                data.PlayerName[4] = "botsam";
                data.Score[4] = 2;
 
                    SaveHighScores(data, HighScoresFilename, device);
                }
            }
 
#endif


        }


        /* More score variables */
        HighScoreData data;
        public string HighScoresFilename = "highscores.dat";
        int PlayerScore = 0;
        string PlayerName;
        string scoreboard;
 
        // String for get name
        string cmdString = "Enter your player name and press Enter";
        // String we are going to display – initially an empty string
        string messageString = "";

        /* Save highscores */
        public static void SaveHighScores(HighScoreData data, string filename, StorageDevice device)
        {
            // Get the path of the save game
            string fullpath = "highscores.dat";

#if WINDOWS
            // Open the file, creating it if necessary
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close the file
                stream.Close();
            }

#elif XBOX
 
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                {
               
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fullpath, FileMode.Create, iso))
                    {
 
                        XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                        serializer.Serialize(stream, data);
 
                    }
 
                }
           
#endif
        }




        /* Load highscores */
        public static HighScoreData LoadHighScores(string filename)
        {
            HighScoreData data;

            // Get the path of the save game
            string fullpath = "highscores.dat";

#if WINDOWS

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                data = (HighScoreData)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }


            return (data);

#elif XBOX
 
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fullpath, FileMode.Open,iso))
                {
                    // Read the data from the file
                    XmlSerializer serializer = new XmlSerializer(typeof(HighScoreData));
                    data = (HighScoreData)serializer.Deserialize(stream);
                }
            }
 
            return (data);
 
#endif

        }



        /* Save player highscore when game ends */
        public void SaveHighScore(int score)
        {
            // Create the data to saved
            HighScoreData data = LoadHighScores(HighScoresFilename);

            int scoreIndex = -1;
            for (int i = data.Count - 1; i > -1; i--)
            {
                if (score >= data.Score[i])
                {
                    scoreIndex = i;
                }
            }

            if (scoreIndex > -1)
            {
                //New high score found ... do swaps
                for (int i = data.Count - 1; i > scoreIndex; i--)
                {
                    data.PlayerName[i] = data.PlayerName[i - 1];
                    data.Score[i] = data.Score[i - 1];
                }

                data.PlayerName[scoreIndex] = PlayerName; //Retrieve User Name Here
                data.Score[scoreIndex] = score; // Retrieve score here

                SaveHighScores(data, HighScoresFilename, device);
            }
        }


        /* Iterate through data if highscore is called and make the string to be saved*/
        public string makeHighScoreString()
        {
            // Create the data to save
            HighScoreData data2 = LoadHighScores(HighScoresFilename);

            // Create scoreBoardString
            string scoreBoardString = "Highscores:\n\n";

            for (int i = 0; i < 6; i++) // this part was missing (5 means how many in the list/array/Counter)
            {
                scoreBoardString = scoreBoardString + data2.PlayerName[i] + "-" + data2.Score[i] + "\n";
            }
            return scoreBoardString;
        }


        public StorageDevice device { get; set; }
    }

}
