using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float NoteSpeed;
    public bool hasAlive;
    Note(float bpm, float x_speed)
    {
        NoteSpeed = bpm * x_speed / 60f;
    }

    void Start()
    {
        hasAlive = true;
    }

    void Update()
    {
        transform.position -= new Vector3(NoteSpeed * Time.deltaTime, 0f, 0f);

        if (!hasAlive)
        {
            Destroy(gameObject);
        }
    }
}
