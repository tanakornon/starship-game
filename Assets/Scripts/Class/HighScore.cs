using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScore
{
    public List<string> Name { get; set; }
    public List<int> Score { get; set; }

    // Constructor
    public HighScore()
    {
        Name = new List<string>();
        Score = new List<int>();

        ReadScore();
    }

    public string GetPlayerName()
    {
        string strName = "";

        foreach (string it in Name)
        {
            strName += it + "\n\n";
        }

        return strName;
    }

    public string GetHighScore()
    {
        string strScore = "";

        foreach (int it in Score)
        {
            strScore += it.ToString() + "\n\n";
        }

        return strScore;
    }

    public void UpdateHighScore(string newName, int newScore)
    {

        bool insert = false;

        for (int i = 0; i < Score.Count; i++)
        {
            if (newScore > Score[i] && !insert)
            {
                Name.Insert(i, newName);
                Score.Insert(i, newScore);
                insert = true;
            }
        }

        if (!insert)
        {
            Name.Add(newName);
            Score.Add(newScore);
        }

        WriteScore();
    }

    void WriteScore()
    {
        StreamWriter writer = new StreamWriter(path, false);

        writer.Write("");

        for (int i = 0; i < Math.Min(Score.Count, 5); i++)
        {
            writer.WriteLine(Name[i] + ':' + Score[i]);
        }

        writer.Close();
    }

    void ReadScore()
    {
        StreamReader reader = new StreamReader(path);

        Name.Clear();
        Score.Clear();

        string line = "";
        while ((line = reader.ReadLine()) != null)
        {

            string[] temp = line.Split(':');
            Name.Add(temp[0]);
            Score.Add(Int32.Parse(temp[1]));
        }

        reader.Close();
    }

















    public void ClearScore()
    {
        Name.Clear();
        Score.Clear();
        WriteScore();
    }
    public bool CheckForUpdate(int newScore)
    {

        if (Score.Count < 5)
        {
            return true;
        }

        foreach (var curScore in Score)
        {
            if (newScore > curScore)
            {
                return true;
            }
        }

        return false;
    }

    private static string path = Application.dataPath + "/Resources/score.txt";
}