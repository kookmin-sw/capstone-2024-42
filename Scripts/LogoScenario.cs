using UnityEngine;

public class LogoScenario : MonoBehaviour
{
    [SerializeField]
    private Progress progress;

    [SerializeField]
    private SceneNames nextScene;

    private void Awake()
    {
        SystemSetup();
    }
    private void SystemSetup()
    {
        Application.runInBackground = true;

        int height = Screen.height;
        int width = (int)(Screen.height * 18.5f / 9);
        Screen.SetResolution(width, height, true);

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);
    }
    private void OnAfterProgress()
    {
        Utils.LoadScene(nextScene);
    }
}
