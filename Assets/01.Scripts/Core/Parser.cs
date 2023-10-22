using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using UnityEditor.Search;

public class Parser : MonoSingleton<Parser>
{
    public Sheet sheet;

    public void ReadInfo()
    {
        int j = 0;
        sheet = new Sheet();
        string str = null;
        using (StreamReader sr = new StreamReader(@"D:\test.txt"))
        {
            while ((str = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(str))
                    break;

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
                sheet.notes.Add(new Note(line, time, (NoteType)type, tail));
                j++;
            }
        }
    }
}
