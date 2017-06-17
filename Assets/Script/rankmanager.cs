using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rankmanager : MonoBehaviour {
    List<rankmessage> list;
    Transform contentTf;
    void Start () {
        list=new List<rankmessage>();
        contentTf = this.transform.FindChild("content");
        loaddata();
        displayrank();
	}
    void loaddata()
    {
        list.Add(new rankmessage(500,"roy"));
        list.Add(new rankmessage(5200, "fata"));
        list.Add(new rankmessage(5300, "given"));
        list.Add(new rankmessage(5200, "sophia"));
        list.Add(new rankmessage(5300, "lala"));
    }
    public GameObject rankprefab;
    void displayrank()
    {
        //作排序
        for(int i=0;i<list.Count;i++)
        {
            GameObject instance = GameObject.Instantiate(rankprefab);
            instance.GetComponent<RankElement>().init(list[i].Name,(i+1).ToString(),list[i].score.ToString());
            instance.transform.parent = contentTf;
            instance.transform.localScale = Vector3.one;
            instance.transform.localPosition = Vector3.zero;
        }
    }
}
