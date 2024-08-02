using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{
    private void Start()
    {
        
    }
    public static ToastManager instance;
    void Awake() 
    {
        if (instance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }


    public TMP_Text ToastText;
    public Image ToastImage;
    [Space]
    public Animator ToastAnimator;

    //skipToasts was added to prevent toasts when loading the already completed achievements.
    [HideInInspector] public bool skipToasts;
    public void Toast(Achievement achievement)
    {
        if (skipToasts) return;
        StartCoroutine(HandleToast(achievement));
    }
    //Prevents new toast from happening until previous toast is done.
    IEnumerator HandleToast(Achievement achievement)
    {
        while (CurrentToast != null)
        {
            yield return null;
        }
        CurrentToast = StartCoroutine(NewToast(achievement));
    }
    IEnumerator NewToast(Achievement achievement)
    {
        ToastText.text = $"Achievement Unlocked!\n\n{achievement.Name}";
        ToastImage.sprite = achievement.AchievementIcon;
        ToastAnimator.SetTrigger("ToastEnter");
        yield return new WaitForSecondsRealtime(4);
        ToastAnimator.SetTrigger("ToastExit");
        yield return new WaitForSecondsRealtime(.5f);
        //PlayAchievementSound
        CurrentToast = null;
    }
    Coroutine CurrentToast;
}
