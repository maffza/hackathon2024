using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public RectTransform rectTransform;

    public Animator panelAnimator;
    private static readonly int SlideInTrigger = Animator.StringToHash("isPanelVisible");
    private static readonly int SlideInOptions = Animator.StringToHash("ShowOptions");

    void Start()
    {
        panelAnimator.SetTrigger(SlideInTrigger);
    }

    void Update()
    {

    }

    public void TogglePanel()
    {
        bool state = panelAnimator.GetBool("ShowOptions");
        panelAnimator.SetBool(SlideInOptions, !state);
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
