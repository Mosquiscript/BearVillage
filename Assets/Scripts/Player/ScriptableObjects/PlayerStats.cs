using UnityEngine;
using UnityEngine.UI;

public enum Gender
{
    Hombre,
    Mujer
}
public enum Profession
{
    Herrero,
    Granjero,
    Minero, 
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject 
{
    public Sprite Avatar;
    public string Name;
    public Profession Profession;
    public Gender Gander;
    [Range(0, 100)] public int Age;
    [Range(0, 100)] public int Life;
    [Range(0, 100)] public int Energy;
    [Range(0, 100)] public int Mood;
    [Range(0, 100)] public int LevelProfession;
}
