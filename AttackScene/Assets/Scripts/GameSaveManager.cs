using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class GameSaveManager : MonoBehaviour
{
    private float health;
    private int coinNum;
    private Dictionary<string, string> data = new Dictionary<string, string>();
    private HealthSystem myHealthSystem;


    private void Start()
    {

        myHealthSystem = GameObject.Find("TinyHealthSystem").GetComponent<HealthSystem>();
        data.Add("health", "");
        data.Add("coinNum", "");

    }

    public void saveGame(){

        health = myHealthSystem.hitPoint;
        coinNum = CoinUI.coinNum;

        Debug.Log(Application.persistentDataPath);

        if(!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
        {

            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }


        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/data.txt");

        data["health"] = health.ToString();
        data["coinNum"] = coinNum.ToString();

        StreamWriter sw = new StreamWriter(file);
        //��ʼд��

           sw.Write("health" + "," + data["health"]); 
           sw.Write("\n"); 
           sw.Write("coinNum" + "," + data["coinNum"]); 
            //��ջ�����
            sw.Flush();
            //�ر���
            sw.Close();

            file.Close();
    }

    public void loadGame(){

        if(File.Exists(Application.persistentDataPath + "/game_SaveData/data.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/data.txt",FileMode.Open);


            StreamReader sr = new StreamReader(file,Encoding.Default);
            String line;
            var dic = new Dictionary<string, string>();
            while ((line = sr.ReadLine()) != null)
            {
                var li = line.ToString().Split(',');//��һ����,�ֿ��ɼ�ֵ��
                dic.Add(li[0], li[1]);
            }

            Debug.Log(dic["health"]);
            Debug.Log(dic["coinNum"]);

            file.Close();
        }


    }


}
