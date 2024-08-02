using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: It's recommended to create your achievements in Resources/Achievements
//in order for the achievement system to function properly

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/Create Achievement")]
public class Achievement : ScriptableObject
{
    public string Name;
    public string Description;
    [Space]
    public Sprite AchievementIcon;
}
