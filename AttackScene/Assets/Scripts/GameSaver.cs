using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver
{
    public static float healthMax = 15;
    public static float manaMax = 10;
    public static int coinNum = 0;
    public static bool attackLock = true;
    public static bool duckLock = true;
    public static bool shadowLock = true;
    public static bool bonusLock = true;
    public static bool defenseLock = true;
    public static bool smallHealth = false;
    public static bool bigHealth = false;
    public static bool smallMana = false;
    public static bool bigMana = false;
    public static int unLockLevel = 0;



    public static int difficulty = 1;
    public static Dictionary<int, Dictionary<string, int>> difficultySetting = new Dictionary<int, Dictionary<string, int>>{
            {0, new Dictionary<string, int>{
                        { "batAttack", 1},
                        { "batHealth", 3},
                        { "skeletonAttck", 1},
                        { "skeletonHealth", 15},
                        { "fireWarmAttack", 1},
                        { "fireWarmHealth", 15},
                        { "puzzleAttack", 1},
                        { "puzzleHealth", 15},
                        { "nightmare1Attack", 1},
                        { "nightmare1Health", 20},
                        { "nightmare2Attack", 1},
                        { "nightmare2Health", 15},
                        { "nightmare2Sweep", 1},
                        { "smallStoneAttack", 1},
                        { "smallStoneHealth", 10},
                        { "middleStoneHealth", 15},
                        { "bigStoneHealth", 20},
                        { "middleStoneAttack", 1},
                        { "bigStoneAttack", 1}
                    } },
            {1, new Dictionary<string, int>{
                        { "batAttack", 2},
                        { "batHealth", 5},
                        { "skeletonAttck", 2},
                        { "skeletonHealth", 20},
                        { "fireWarmAttack", 2},
                        { "fireWarmHealth", 20},
                        { "puzzleAttack", 2},
                        { "puzzleHealth", 20},
                        { "nightmare1Attack", 2},
                        { "nightmare1Health", 25},
                        { "nightmare2Attack", 2},
                        { "nightmare2Health", 20},
                        { "nightmare2Sweep", 2},
                        { "smallStoneAttack", 2},
                        { "smallStoneHealth", 15},
                        { "middleStoneHealth", 20},
                        { "bigStoneHealth", 25},
                        { "middleStoneAttack", 2},
                        { "bigStoneAttack", 2}
                    } }
    };

}
