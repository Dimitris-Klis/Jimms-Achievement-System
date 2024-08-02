using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem instance; private void Awake() { instance = this; }


    public AchievementObject AchievementPrefab;
    public Transform AchievementParent;
    [Space]
    public Achievement[] achievements;
    public List<AchievementObject> achievementObjects = new();


    // Start is called before the first frame update
    void Start()
    {
        ToastManager.instance.skipToasts = true;
        achievements = Resources.LoadAll<Achievement>("Achievements");
        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementObject obj = Instantiate(AchievementPrefab, AchievementParent);
            obj.SetAchievement(achievements[i]);
            
            //We get the completion of the achievement from playerPrefs. If you know json saving, you should probably write your own saving system for this.
            obj.SetCompletion(PlayerPrefs.GetInt($"CompletedAchievementNum{i}", 0) == 1);
            achievementObjects.Add(obj);
        }
        ToastManager.instance.skipToasts = false;
    }
    //If you're planning on changing the achievement order,
    //use CompleteAchievement(string achievementName) instead.
    public void CompleteAchievement(int index)
    {
        achievementObjects[index].SetCompletion(true);
    }
    //If you're planning on changing the achievement names,
    //use CompleteAchievement(int index) instead.
    public void CompleteAchievement(string achievementName)
    {
        achievementObjects.Find(o => o.assignedAchievement.Name == achievementName).SetCompletion(true);
    }

    
    [ContextMenu("Reset Achievements")]
    public void ResetAllAchievements()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            achievementObjects[i].SetCompletion(false);
        }
    }

    public int GetAchievementIndex(AchievementObject obj)
    {
        return achievementObjects.FindIndex(o => o.assignedAchievement.Name == obj.assignedAchievement.Name);
    }
}