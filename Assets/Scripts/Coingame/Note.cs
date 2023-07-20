using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Note
{
    private bool hit = false;
    private bool pass = false;
    private float startX;
    private float startY;
    private float speed;
    private float spriteScale = 0.5f;
    private Sprite arrow;
    private GameObject newNote;
    private KeyCode key;
    private float goodRange = 1.0f;
    private float yLim;
    private float topOfScreen;
    public Note(Enums.Direction direction, float speed, float yLim, float topOfScreen)
    {
        this.speed = speed;
        startY = -5.0f;
        this.yLim = yLim;
        this.topOfScreen = topOfScreen;

        if (direction == Enums.Direction.Left)
        {
            startX = -4.5f;
            arrow = Resources.Load<Sprite>("Arrows/arrow_left2");
            key = KeyCode.A;
        }
        if (direction == Enums.Direction.Up)
        {
            startX = -1.5f;
            arrow = Resources.Load<Sprite>("Arrows/arrow_up2");
            key = KeyCode.W;
        }
        if (direction == Enums.Direction.Down)
        {
            startX = 1.5f;
            arrow = Resources.Load<Sprite>("Arrows/arrow_down2");
            key = KeyCode.S;
        }
        if (direction == Enums.Direction.Right)
        {
            startX = 4.5f;
            arrow = Resources.Load<Sprite>("Arrows/arrow_right2");
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

    public void Move()
    {
        newNote.transform.position = newNote.transform.position + speed * Vector3.up;
    }

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
