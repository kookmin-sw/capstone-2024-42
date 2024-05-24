using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SFB;

public class NoteManager : MonoBehaviour
{
    public AudioManager audioManager;
    public RectTransform panelR, panelG, panelY;
    public List<NoteData> notes = new List<NoteData>();
    public TMP_InputField timeInputField;
    private float musicLength;
    void Update()
    {
        musicLength = audioManager.GetClipLength();
    }



    public void AddNote(float time, string type, GameObject notePrefab)
    {
        RectTransform parentPanel = GetPanelByType(type);
        if (parentPanel != null && notePrefab != null)
        {
            GameObject noteGO = Instantiate(notePrefab, parentPanel);
            float xPos = ConvertTimeToPositionX(time, parentPanel);
            float yPos = CalculateNoteYPosition(parentPanel);
            noteGO.transform.localPosition = new Vector3(xPos, yPos, 0);
            noteGO.SetActive(true);
            NoteBehavior noteBehavior = noteGO.GetComponent<NoteBehavior>();
            if (noteBehavior != null)
            {
                noteBehavior.Initialize(this, noteGO, time, type);
            }
            NoteData newNote = new NoteData { time = time, type = type, gameObject = noteGO };
            notes.Add(newNote);
        }
    }
    public float ConvertTimeToPositionX(float time, RectTransform panel)
    {
        float panelWidth = panel.rect.width;
        return (time / musicLength) * panelWidth; // 음악 시작이 패널의 왼쪽 끝에 위치하도록 조정
    }

    public float ConvertPositionXToTime(float positionX, RectTransform panel)
    {
        float panelWidth = panel.rect.width;
        // x좌표를 패널의 가로 길이에 비례하여 음악 시간으로 변환
        return (positionX / panelWidth) * musicLength;
    }
    private float CalculateNoteYPosition(RectTransform panel) 
    {
        // 패널의 높이를 기반으로 노트의 Y 위치를 계산하여 중앙에 배치
        return -panel.anchoredPosition.y  ;
    }

    

    RectTransform GetPanelByType(string type)
    {
        return type switch
        {
            "Note_R" => panelR,
            "Note_G" => panelG,
            "Note_Y" => panelY,
            _ => null,
        };
    }

    public void UpdateNoteTime(GameObject note, float oldTime, float newTime)
    {
        NoteBehavior noteBehavior = note.GetComponent<NoteBehavior>();
        if (noteBehavior != null)
        {
            NoteData noteData = notes.Find(n => n.gameObject == note && Mathf.Approximately(n.time, oldTime));
            if (noteData != null)
            {
                noteData.time = newTime; // 노트 시간 업데이트
                noteBehavior.noteTime = newTime; // 노트 컴포넌트 내 시간도 업데이트
                note.transform.localPosition = new Vector3(ConvertTimeToPositionX(newTime, GetPanelByType(noteBehavior.noteType)), note.transform.localPosition.y, 0);
            }
        }   
    }

    public void DeleteNote(NoteBehavior noteBehavior)
    {
        NoteData noteData = notes.Find(n => n.gameObject == noteBehavior.gameObject);
        if (noteData != null)
        {
            notes.Remove(noteData); // 노트 데이터 삭제
            Destroy(noteBehavior.gameObject); // 게임 오브젝트 삭제
        }
    }
    
    public void SaveNotes()
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Save Notes", "", "notes", "json");
        if (!string.IsNullOrEmpty(path))
        {
            string json = JsonUtility.ToJson(new NoteDataWrapper { Notes = notes }, true);
            File.WriteAllText(path, json);
        }
    }

    public void LoadNotes()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Load Notes", "", "json", false);
        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(LoadNotesFromFile(paths[0]));
        }
        else
        {
            Debug.Log("No file selected or invalid file path.");
        }
    }

    IEnumerator LoadNotesFromFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("File path is empty.");
            yield break; 
        }

        string jsonData = File.ReadAllText(filePath);
        NoteDataWrapper loadedData = JsonUtility.FromJson<NoteDataWrapper>(jsonData);
        foreach (NoteData noteData in loadedData.Notes)
        {
            AddNote(noteData.time, noteData.type, noteData.gameObject);
        }
        yield return null;
    }

    public void DeleteNotesButtonPressed()
    {
        if (float.TryParse(timeInputField.text, out float seconds))
        {
            RewindAndDeleteNotes(seconds);
        }
        else
        {
            Debug.LogError("Invalid input. Please enter a valid number.");
        }
    }

    
    public void RewindAndDeleteNotes(float seconds)
    {
        float currentTime = audioManager.GetMusicTime();
        float newTime = Mathf.Max(0, currentTime - seconds);
        audioManager.SetMusicTime(newTime);
        List<NoteData> notesToRemove = notes.FindAll(note => note.time >= newTime && note.time <= currentTime);
        foreach (NoteData note in notesToRemove)
        {
            if (note.gameObject != null) // 해당 노트의 게임 오브젝트가 존재하는지 확인
            {
                Destroy(note.gameObject); // 게임 오브젝트 삭제
            }
        }
        notes.RemoveAll(note => note.time >= newTime && note.time <= currentTime);
    }

    [System.Serializable]
    public class NoteDataWrapper
    {
        public List<NoteData> Notes = new List<NoteData>();
    }
}