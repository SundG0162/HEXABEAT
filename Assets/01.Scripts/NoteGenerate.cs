using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class NoteGenerate : MonoSingleton<NoteGenerate>
{

    public GameObject notePrefab;

    public Transform[] noteSpawnPos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            print(1);   
            D_SHORTNOTEGENERATE();
        }
    }

    private void D_SHORTNOTEGENERATE()
    {
        GameObject note = Instantiate(notePrefab, noteSpawnPos[5]);
        note.transform.localRotation = Quaternion.identity;
        note.transform.localPosition = Vector2.zero;
        note.AddComponent<NoteShort>().Move();
    }
}
