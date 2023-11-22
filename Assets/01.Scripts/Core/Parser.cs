using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoSingleton<Parser>
{
    public Sheet sheet;

    public void ReadInfo()
    {
        sheet = new Sheet();
        string str = null;
        using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\Assets\99.Sheets\" + LevelManager.Instance.levelSO.name + ".txt"))
        {
            while ((str = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(str))
                    break;
                if (str.StartsWith("QuaterNoteMs"))
                {
                    sheet.quaterNoteMs = int.Parse(str.Split()[1].Trim());
                    continue;
                }
                if(str.StartsWith("FirstNoteMs"))
                {
                    sheet.firstNoteMs = int.Parse(str.Split()[1].Trim());
                    continue;
                }    
                string[] s = str.Split(' ');
                if (s.Length == 1)
                {
                    sheet.quaterNoteMs = int.Parse(s[0].Trim());
                    continue;
                }
                int time = int.Parse(s[0].Trim());
                int line = int.Parse(s[1].Trim());
                int type = int.Parse(s[2].Trim());
                int tail = -1;
                if (s.Length >= 4)
                {
                    tail = int.Parse(s[3].Trim());
                }
                sheet.notes.Add(new Note(line, time, (NoteType)type, tail));
            }
        }
        GameManager.Instance.sheet = sheet;
    }
}
