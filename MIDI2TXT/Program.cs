using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

string name = "The Brave";
MidiFile midiFile = MidiFile.Read($@"D:\{name}.mid");

string path = $@"D:\{name}.txt";
Note[] notes = midiFile.GetNotes().ToArray();
var tempoMap = midiFile.GetTempoMap();

File.WriteAllText(path, string.Empty);

long defaultTempoMicroseconds = tempoMap.GetTempoAtTime((MidiTimeSpan)0).MicrosecondsPerQuarterNote;

long quaterNoteMs = (long)defaultTempoMicroseconds / 1000;
var firstMs = (int)TimeConverter.ConvertTo<MetricTimeSpan>(notes[0].Time, tempoMap).TotalMilliseconds;
File.AppendAllText(path, $"QuaterNoteMs {quaterNoteMs}\n");
File.AppendAllText(path, $"FirstNoteMs {firstMs}\n");

for (int i = 0; i < notes.Length; i++)
{
    long startTimeInTicks = notes[i].Time;
    long endTimeInTicks = notes[i].EndTime;

    long startTimeInMs = (long)TimeConverter.ConvertTo<MetricTimeSpan>(startTimeInTicks, tempoMap).TotalMilliseconds;
    long endTimeInMs = (long)TimeConverter.ConvertTo<MetricTimeSpan>(endTimeInTicks, tempoMap).TotalMilliseconds;
    int type = 0;
    string str;
    int index = 0;
    
    
    switch (notes[i].NoteName.ToString())
    {
        case "F": index = 0; break;
        case "E": index = 1; break;
        case "DSharp": index = 2; break;
        case "D": index = 3; break;
        case "CSharp": index = 4; break;
        case "C": index = 5; break;
    }
    if((int)(endTimeInMs - startTimeInMs) <= (int)quaterNoteMs / 3.8f)
    {
        type = 1; str = type.ToString();
    }
    else if(endTimeInMs - startTimeInMs <= quaterNoteMs / 1.8f)
    {
        type = 0; str = type.ToString();
    }
    else
    {
        type = 2; str = $"{type} {endTimeInMs}";
    }
    File.AppendAllText(path, $"{(int)TimeConverter.ConvertTo<MetricTimeSpan>(notes[i].Time, tempoMap).TotalMilliseconds} {index} {str}\n");
}