using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class NoteJudgment : MonoBehaviour
{
    public Transform judgmentLine_Y;
    public Transform judgmentLine_G;
    public Transform judgmentLine_R;
    public GameObject effectPrefab_Y;
    public GameObject effectPrefab_G;
    public GameObject effectPrefab_R;
    public Button yellowButton;
    public Button greenButton;
    public Button redButton;

    public float perfectThreshold = 1.0f;
    public float greatThreshold = 2.0f;
    public float goodThreshold = 3.0f;
    public CharacterSpriteManager spriteManager;

    private List<GameObject> notesInPlay = new List<GameObject>();

    private void Awake()
    {
        spriteManager = FindObjectOfType<CharacterSpriteManager>();
        yellowButton.onClick.AddListener(() => TriggerJudgment(judgmentLine_Y));
        greenButton.onClick.AddListener(() => TriggerJudgment(judgmentLine_G));
        redButton.onClick.AddListener(() => TriggerJudgment(judgmentLine_R));
    }

    void Update()
    {
        UpdateNotesInPlay();
        CheckForMissedNotes();
    }

    void UpdateNotesInPlay()
    {
        notesInPlay.Clear();
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_Y"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_G"));
        notesInPlay.AddRange(GameObject.FindGameObjectsWithTag("Note_R"));
    }

    void CheckForMissedNotes()
    {
        foreach (var note in notesInPlay.ToList())
        {
            Transform judgmentLine = GetJudgmentLine(note.tag);
            if (note.transform.position.y < judgmentLine.position.y)
            {
                Debug.Log("Miss detected for: " + note.tag);
                PerformMissAction();
                Destroy(note);
                notesInPlay.Remove(note);
            }
        }
    }

    private Transform GetJudgmentLine(string noteType)
    {
        switch (noteType)
        {
            case "Note_Y": return judgmentLine_Y;
            case "Note_G": return judgmentLine_G;
            case "Note_R": return judgmentLine_R;
            default: return null;
        }
    }   

    private void TriggerJudgment(Transform judgmentLine)
    {
        var collider = judgmentLine.GetComponent<Collider2D>();
        var result = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        collider.Overlap(filter, result);

        foreach (var hit in result)
        {
            if (hit.CompareTag("Note_Y") || hit.CompareTag("Note_G") || hit.CompareTag("Note_R"))
            {
                JudgeNoteBasedOnDistance(hit.gameObject, judgmentLine);
            }
        }
    }

    private void JudgeNoteBasedOnDistance(GameObject note, Transform judgmentLine)
    {
        float distance = Vector2.Distance(note.transform.position, judgmentLine.position);
        string judgment = DetermineJudgment(distance);
        Debug.Log(note.tag + " " + judgment);

        ApplyEffectsAndCleanup(note, judgmentLine, judgment);
    }

    private string DetermineJudgment(float distance)
    {
        if (distance <= perfectThreshold)
        {
            PerformAction(100, 1, 1);
            CharacterSpriteManager.Instance.ChangeSprite("Perfect");
            return "Perfect";
        }
        else if (distance <= greatThreshold)
        {
            PerformAction(80, 1, 1);
            CharacterSpriteManager.Instance.ChangeSprite("Great");
            return "Great";
        }
        else if (distance <= goodThreshold)
        {
            PerformAction(50, 1, 1);
            CharacterSpriteManager.Instance.ChangeSprite("Good");
            return "Good";
        }
        else
        {
            PerformMissAction();
            CharacterSpriteManager.Instance.ChangeSprite("Miss");
            return "Miss";
        }
    }

    private void ApplyEffectsAndCleanup(GameObject note, Transform judgmentLine, string judgment)
    {
        GameObject effectPrefab = GetEffectPrefab(note.tag);
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, judgmentLine.position, Quaternion.Euler(0, 0, 315));
            Destroy(effect, 0.7f);
            Debug.Log("Effect created for " + note.tag);
        }
        Destroy(note);
    }

    private GameObject GetEffectPrefab(string noteType)
    {
        switch (noteType)
        {
            case "Note_Y": return effectPrefab_Y;
            case "Note_G": return effectPrefab_G;
            case "Note_R": return effectPrefab_R;
            default: return null;
        }
    }

    private void PerformAction(int score, int combo, int points)
    {
        if (ScoreManager.Instance != null) ScoreManager.Instance.AddScore(score);
        if (ComboManager.Instance != null) ComboManager.Instance.AddCombo(combo);
        if (LifeManager.Instance != null) LifeManager.Instance.Plus(points);
    }

    private void PerformMissAction()
    {
        Debug.Log("Miss Action Triggered");
        if (spriteManager != null)
        {
            spriteManager.ChangeSprite("Miss");
        }
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(0);
        if (ComboManager.Instance != null)
            ComboManager.Instance.ResetCombo_play();
        if (LifeManager.Instance != null)
            LifeManager.Instance.Minus(0);
    }
}
