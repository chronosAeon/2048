using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ResourceManager {
    public static Dictionary<int, Sprite> dic_Sprite;
    static ResourceManager()//静态类里面只能调用静态的构造方法，方法内加载sprite图集并且把资源加载到建立的字典里面
    {
        dic_Sprite = new Dictionary<int, Sprite>();
        Sprite[] spriteArray = Resources.LoadAll<Sprite>("2048Atlas");
        for(int i=0;i<spriteArray.Length;i++)
        {
            dic_Sprite.Add(int.Parse(spriteArray[i].name), spriteArray[i]);
        }
    }
    public static Sprite GetImage(int number)//通过数字的索引返回精灵
    {
        return dic_Sprite[number];
    }
}

