using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageData", menuName = "Custom/ScriptableObject")]
public class StageDatas : ScriptableObject
{
    public int lastPlayStage;
    public int bestStage;
    public bool gameAllClear;
    public bool[] clearStage = new bool[12];

    public bool isLoadData = false;
    public bool isTutorialClear = false;

}
