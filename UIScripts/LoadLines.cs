using UnityEngine;
using System.Collections;

public class LoadLines : MonoBehaviour {

    public string[] lines;

    //string filePath = "C:\\Users\\Gustavo\\Documents\\004 - Game\\Island Survivor\\Assets\\Database\\NarratorLines.txt";
    string filePath = "C:\\Users\\Inmetrics\\Documents\\_trabalhos\\Island Survivor\\Assets\\Database\\NarratorLines.txt";

    // Load narrator lines in a txt
    void Awake () {

        lines = new string[10];

        readFile(filePath);   

    }

    void readFile(string filePath)
    {
        int counter = 0;
        string line;

        // Read the file and display it line by line.
        System.IO.StreamReader file =
           new System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding("iso-8859-1"));

        while ((line = file.ReadLine()) != null)
        {
            if(counter > 0)
                lines[counter-1] = line; 


            counter++;
        }
        file.Close();
    }
}
