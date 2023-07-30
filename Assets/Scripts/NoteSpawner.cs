using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NoteSpawner : MonoBehaviour
{
    //list to hold all currently spawned notes
    private List<Note> notes;

    //y position of the receiver, used by notes for scoring
    private float yLim = 3.0f;

    //y position of the top, used by notes for despawning missed notes
    private float topOfScreen = 5.0f;

    private float speed = 0.1f;

    //prevScore is used to update current score
    private int score = 0;
    private int prevScore = 50;
    //output the score on the screen 
    [SerializeField] protected TextMeshProUGUI scoreText;

    //used for timing
    private float currentTime = 0.0f;
    private float bps = 2f;
    
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

    //checks user input and whether notes have passed screen, removes them from list
    //updates score if different from prevScore
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
            }
        }

        if (score != prevScore)
        {
            scoreText.text = score.ToString();
        }
    }

    //moves all notes upwards
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
