using UnityEngine;
using System.Collections;

public class rankmessage : MonoBehaviour {

    public int score
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public rankmessage(int score,string name)
    {
        this.score = score;
        this.Name = name;
    }
}
