using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//The object that will display your achievement
public class AchievementObject : MonoBehaviour
{
    public TMP_Text Text;
    [Space]
    public Image IconImage;
    [Tooltip("If the achievement isn't done, the overlay will be displayed.")]
    public Image Overlay;
    [Space(30)]

    [HideInInspector] public Achievement assignedAchievement;

    public bool Completed; // Without this we can't know if the achievement was completed or not.


    //Assigns the achievement into the AchievementObject. Used by AchievementSystem.
    public void SetAchievement(Achievement achievement)
    {
        if (achievement != null)
        {
            assignedAchievement = achievement;
            //This just formats the name and description into the Text variable.
            //String readability is not the best here :|
            Text.text = string.Concat
            (
                $"<font=\"Roboto-Bold SDF\">{achievement.Name}</font>", //Title
                $"<line-height=40%>\r\n<size=50%><font=\"Roboto-Regular SDF\"><line-height=100%>{achievement.Description}</font></size>" //Description
            );

            IconImage.sprite = achievement.AchievementIcon;
        }
    }
    public void SetCompletion(bool completed)
    {
        Completed = completed;

        //If we completed the achievement, hide the overlay.
        Overlay.gameObject.SetActive(!completed);

        PlayerPrefs.SetInt($"CompletedAchievementNum{AchievementSystem.instance.GetAchievementIndex(this)}", completed? 1 : 0);
    }
}
