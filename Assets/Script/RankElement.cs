using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankElement : MonoBehaviour {
    //string  textScore, textRank,textName;错误写法，因为你拿到的不是引用，而是值，你把拿到的值改了也不能改变本体
    Text textName,textRank,textScore;
	void  Awake() {
        textName = this.transform.Find("name").GetComponent<Text>();//这个地方不能使用findchild但是不知道为什么，下次问一下老师
        textRank = this.transform.Find("ranking").GetComponent<Text>();
        textScore = this.transform.Find("score").GetComponent<Text>();
	}
	public void init(string Name,string rank,string score)
    {
        this.textName.text = Name;
        this.textScore.text = score;
        this.textRank.text = rank;
    }
}
