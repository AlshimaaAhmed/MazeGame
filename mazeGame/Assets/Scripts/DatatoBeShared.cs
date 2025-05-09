using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatatoBeShared 
{
    public static string Question;
    public static string Answer1;
    public static string Answer2;
    public static string Answer3;
    public static string Answer4;
    public static Sprite Backgroundimg;
    public static Sprite Questionimg;
    public static string CorrectAnswer;

    public static Vector3 ReturnPosition;
    public static string LastScene = "";
    public static string ReturnScene = "";
    public static HashSet<string> EnteredDoors = new HashSet<string>();
    public static bool IsDoorOpen = false;

}

