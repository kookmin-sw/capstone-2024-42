using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    public GameObject NotePrefab;
    private List<GameObject> Notes = new List<GameObject>();
    public enum Shooting_direction { Left = -1, Right = 1 };
    public Shooting_direction Shoot_Direct;
    public float Create_Time;

    // Start is called before the first frame update
    void Start()
    {
        Create_Time = 2.0f;

        StartCoroutine("Generate", Create_Time);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator Generate()
    {
        GameObject gameObject = Instantiate(NotePrefab);
        Notes.Add(gameObject);

        yield return new WaitForSeconds(Create_Time);

        StartCoroutine("Generate", Create_Time);
    }
}
