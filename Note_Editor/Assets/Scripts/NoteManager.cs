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
    public List<NoteData> notes = new List<NoteData>();
    public TMP_InputField timeInputField; // TMP 입력 필드에 대한 참조

  
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

        float lastNoteTime = 0f;
        notes.Clear(); 
        foreach (NoteData noteData in loadedData.Notes)
        {
            notes.Add(noteData);
            lastNoteTime = Mathf.Max(lastNoteTime, noteData.time);
        }
        audioManager.SetMusicTime(lastNoteTime);
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
        notes.RemoveAll(note => note.time >= newTime && note.time <= currentTime);
    }

    // Add a note with direction
    public void AddNote(float time, string type)
    {
        NoteData newNote = new NoteData
        {
            time = time,
            type = type
        };
        notes.Add(newNote); // 수정된 노트 정보를 리스트에 추가
    }


    [System.Serializable]
    public class NoteDataWrapper
    {
        public List<NoteData> Notes;
    }
}
