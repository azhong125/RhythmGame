using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NoteSpawner : MonoBehaviour
{
    private List<Note> notes;
    private float yLim = 3.0f;
    private float topOfScreen = 5.0f;
    private float speed = 0.1f;
    private int score = 0;
    private int prevScore = 50;
    [SerializeField] protected TextMeshProUGUI scoreText;
    private float currentTime = 0.0f;
    private float bps = 2f;
    private int missCounter = 0;
    
    void Start()
    {
        notes = new List<Note>();
        
        scoreText.text = score.ToString();
       
    }

    void AddNote(Enums.Direction direction)
    {
        Note newNote = new Note(direction, speed, yLim, topOfScreen);
        notes.Add(newNote);
    }

    void RemoveNote(int index)
    {
        notes.RemoveAt(index);
    }

    void Update()
    {
        prevScore = score;

        for (int i = 0; i < notes.Count; i++)
        {
            Note note = notes[i] as Note;
            note.Update();
            if (note.isHit())
            {
                RemoveNote(i);
                score++;
            }
            if (note.hasPassed())
            {
                RemoveNote(i);
                missCounter++;
            }
        }

        if (score != prevScore)
        {
            scoreText.text = score.ToString();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            Note note = notes[i] as Note;
            note.Move();
        }

        currentTime += Time.fixedDeltaTime;
        if (Math.Round(currentTime * bps, 2) == Math.Round(currentTime * bps))
        {
            CreateRandomNote();
        }
    }

    void CreateRandomNote()
    {
        System.Random rnd = new System.Random();
        int num  = rnd.Next(1, 5);

        if (num == 1) AddNote(Enums.Direction.Left);
        if (num == 2) AddNote(Enums.Direction.Right);
        if (num == 3) AddNote(Enums.Direction.Up);
        if (num == 4) AddNote(Enums.Direction.Down);

    }

}
