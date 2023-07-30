using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Note
{
    //for instiating the arrow, sprite will be assigned based on direction
    private Sprite arrow;
    private float spriteScale = 10f;
    private GameObject newNote;

    //if the player gets the correct direction within the bounds
    private bool hit = false;

    //if the player doesn't hit the note and it passes the top of the screen
    private bool pass = false;
    private float topOfScreen;

    //starting coordinates, based on canvas
    private float startX;
    private float startY;

    //how fast the arrow goes, decided by spawner
    private float speed;
    
    //user input
    private KeyCode key;

    //ranges for scoring, based on how far arrow is from yLim, which is the arrow target y value
    private float goodRange = 1.0f;
    private float yLim;

    //spawns note at the bottom of the screen and positions x based on direction
    public Note(Enums.Direction direction, float speed, float yLim, float topOfScreen)
    {
        this.speed = speed;
        startY = -5.0f;
        this.yLim = yLim;
        this.topOfScreen = topOfScreen;

        if (direction == Enums.Direction.Left)
        {
            startX = -4.5f;
            arrow = Resources.Load<Sprite>("OtherArrows/leftArrow1");
            key = KeyCode.A;
        }
        if (direction == Enums.Direction.Up)
        {
            startX = -1.5f;
            arrow = Resources.Load<Sprite>("OtherArrows/upArrow1");
            key = KeyCode.W;
        }
        if (direction == Enums.Direction.Down)
        {
            startX = 1.5f;
            arrow = Resources.Load<Sprite>("OtherArrows/downArrow1");
            key = KeyCode.S;
        }
        if (direction == Enums.Direction.Right)
        {
            startX = 4.5f;
            arrow = Resources.Load<Sprite>("OtherArrows/rightArrow1");
            key = KeyCode.D;
        }

        newNote = new GameObject();
        newNote.transform.position = new Vector3(startX, startY, 0);
        newNote.transform.localScale = spriteScale * Vector3.one;
        SpriteRenderer spriteRenderer = newNote.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = arrow;
    }
    public bool isHit()
    {
        return hit;
    }

    public void createNote()
    {
        Object.Instantiate(newNote);
    }

    public bool hasPassed()
    {
        return pass;
    }

    //moves note upwards every update, called by spawner FixedUpdate for consistent timing
    public void Move()
    {
        newNote.transform.position = newNote.transform.position + speed * Vector3.up;
    }

    //checks user input and whether the arrow has passed
    public void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (newNote.transform.position.y < yLim + goodRange && newNote.transform.position.y > yLim - goodRange)
            {
                hit = true;
                Object.Destroy(newNote);
            }
        }
        if (newNote.transform.position.y > topOfScreen)
        {
            pass = true;
            Object.Destroy(newNote);
        }

    }
}
