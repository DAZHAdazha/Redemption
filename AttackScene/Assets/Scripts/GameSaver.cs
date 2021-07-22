using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver
{
    public static float healthMax = 20;
    public static float manaMax = 15;
    public static int coinNum = 10;
    public static bool attackLock = true;
    public static bool duckLock = true;
    public static bool shadowLock = true;
    public static bool bonusLock = true;
    public static bool defenseLock = true;

    public static int difficulty;
    public static Dictionary<int, Dictionary<string, int>> difficultySetting = new Dictionary<int, Dictionary<string, int>>{
            {0, new Dictionary<string, int>{
                        { "batAttack", 1},
                        { "skeletonAttck", 1},
                        { "fireWarmAttack", 1},
                        { "puzzleAttack", 1},
                        { "nightmare1Attack", 1},
                        { "nightmare2Attack", 1},
                        { "nightmare2Sweep", 1},
                        { "smallStoneAttack", 1},
                        { "middleStoneAttack", 1},
                        { "bigStoneAttack", 1}
                    } },
            {1, new Dictionary<string, int>{
                        { "batAttack", 2},
                        { "skeletonAttck", 2},
                        { "fireWarmAttack", 2},
                        { "puzzleAttack", 2},
                        { "nightmare1Attack", 2},
                        { "nightmare2Attack", 2},
                        { "nightmare2Sweep", 2},
                        { "smallStoneAttack", 2},
                        { "middleStoneAttack", 2},
                        { "bigStoneAttack", 2}
                    } },
            {2, new Dictionary<string, int>{
                        { "batAttack", 3},
                        { "skeletonAttck", 3},
                        { "fireWarmAttack", 3},
                        { "puzzleAttack", 3},
                        { "nightmare1Attack", 3},
                        { "nightmare2Attack", 3},
                        { "nightmare2Sweep", 3},
                        { "smallStoneAttack", 3},
                        { "middleStoneAttack", 3},
                        { "bigStoneAttack", 3}
                    } }
    };

}
