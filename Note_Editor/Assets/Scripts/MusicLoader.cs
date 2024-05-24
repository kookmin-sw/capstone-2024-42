using UnityEngine;
using System.Collections;
using UnityEngine.Networking; // UnityWebRequestMultimedia를 사용하기 위해 필요
using SFB; // StandaloneFileBrowser 사용

[RequireComponent(typeof(AudioSource))]
public class MusicLoader : MonoBehaviour
{
    public delegate void MusicLoadedHandler(AudioClip loadedClip);
    public event MusicLoadedHandler OnMusicLoaded; // 음악 로드 완료 이벤트
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource 컴포넌트를 가져오거나 없으면 추가
        audioSource = GetComponent<AudioSource>();
    }

    // "Add_Music" 버튼 클릭 시 호출될 메서드
    public void OnAddMusicClicked()
    {
        // 파일 선택 다이얼로그를 열고 오디오 파일 선택
        var extensions = new[] { new ExtensionFilter("Audio Files", "mp3", "wav") };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open Audio File", "", extensions, false);
        if (paths.Length > 0)
        {
            StartCoroutine(LoadAndPlayMusic(paths[0]));
        }
    }

    // 선택된 음악 파일을 로드하고 재생
    IEnumerator LoadAndPlayMusic(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play(); // 자동으로 재생 시작

                // 음악 로드 완료 이벤트 호출
                OnMusicLoaded?.Invoke(clip);
            }
        }
    }
}
