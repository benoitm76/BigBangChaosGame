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

        /* More score variables */
        HighScoreData data;
        public string HighScoresFilename = "highscores.dat";
        public StorageDevice device { get; set; }

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

                SaveHighScores2(data, HighScoresFilename, device);
            }
        }



        /* Save highscores */
        public static void SaveHighScores2(HighScoreData data, string filename, StorageDevice device)
        {
            FileStream stream;
            if (!File.Exists(filename))
            {
                stream = File.Open(filename, FileMode.OpenOrCreate);
            }
            else
            {
                stream = File.Open(filename, FileMode.Truncate);
            }
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



        }

        /* Load highscores */
        public HighScoreData LoadHighScores(string filename)
        {
            HighScoreData data;

            // Open the file
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
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
        }


        /* Save player highscore when game ends */
        public void SaveHighScore(int score, string Name)
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

                data.PlayerName[scoreIndex] = Name; //Retrieve User Name Here
                data.Score[scoreIndex] = score; // Retrieve score here

                SaveHighScores2(data, HighScoresFilename, device);
            }
        }


        /* Iterate through data if highscore is called and make the string to be saved*/
        public string makeHighScoreString()
        {
            // Create the data to save
            return makeHighScoreString(LoadHighScores(HighScoresFilename));
        }

        /* Iterate through data if highscore is called and make the string to be saved*/
        public string makeHighScoreString(HighScoreData data2)
        {
            // Create scoreBoardString
            string scoreBoardString = "Highscores:\n\n";
            int classement = 0;
            for (int i = 0; i < 5; i++) // this part was missing (5 means how many in the list/array/Counter)
            {
                classement = i + 1;
                scoreBoardString = scoreBoardString + classement + " - " + data2.PlayerName[i] + " - " + data2.Score[i] + " Km" + "\n";
            }
            return scoreBoardString;
        }

        /* Iterate through data if highscore is called and make the string to be saved*/
        public string makeHighScoreString2()
        {
            // Create the data to save
            return makeHighScoreString2(LoadHighScores(HighScoresFilename));
        }

        /* Iterate through data if highscore is called and make the string to be saved*/
        public string makeHighScoreString2(HighScoreData data2)
        {
            // Create scoreBoardString
            string scoreBoardString = "";
            int classement = 0;
            for (int i = 0; i < 5; i++) // this part was missing (5 means how many in the list/array/Counter)
            {
                classement = i + 1;
                scoreBoardString = scoreBoardString + classement + " - " + data2.PlayerName[i] + " - " + data2.Score[i] + " Km" + "\n";
            }
            return scoreBoardString;
        }
    }

}
