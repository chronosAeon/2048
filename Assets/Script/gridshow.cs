using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//半成品并没有动画和排行榜功能有待完善
public class gridshow : MonoBehaviour, IPointerDownHandler, IDragHandler { 
    GameCore core;
	void Start ()
    {
        core = new GameCore();
        spriteArray = new Transform[4, 4];
        SetGridGroup();//初始化布局大小和单元格大小
        init();
        core.ComputeEmpty();

        CreateNewNumberSprite();

        CreateNewNumberSprite();


    }

    private void CreateNewNumberSprite()
    {
        int number;
        Location loc;
        core.CreateNewNumber(out number, out loc);
        spriteArray[loc.RIndex, loc.CIndex].GetComponent<Image>().sprite = ResourceManager.GetImage(number);
    }

    public float interval =15;
	void SetGridGroup()//获取布局组件并初始化整个单元格大小和间隙的大小
    {
        GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
        int intinterval = (int)interval;
        grid.padding = new RectOffset(intinterval, intinterval, intinterval, intinterval);
        float cellsize = (this.GetComponent<RectTransform>().sizeDelta.x - 5 * interval)/4;//计算单元格大小
        grid.cellSize = new Vector2(cellsize,cellsize);
        grid.spacing = new Vector2(interval, interval);

    }
    Transform[,] spriteArray;
    void init()//初始化背景
    {
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                spriteArray[r,c] = CreateSprite(new Location(r, c));
    }

    Transform CreateSprite(Location  loc)
    {
        GameObject spriteObject = new GameObject(loc.RIndex.ToString()+loc.CIndex.ToString());
        spriteObject.AddComponent<Image>().sprite=ResourceManager.GetImage(0);
        //spriteObject.AddComponent<demotry>().dotry();自动添加脚本并且调用方法的方法
        //设置父物体
        spriteObject.transform.SetParent(this.transform);
        spriteObject.transform.localScale = Vector3.one;
        spriteObject.transform.localPosition = Vector3.zero;
        return spriteObject.transform;
    }
    Vector2 beginPointer;
    bool isDown = false;
    //当点击时执行
    public void OnPointerDown(PointerEventData eventData)
    {
        beginPointer = eventData.position;
        isDown = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDown == false) return;
        Vector2 touchOffset = eventData.position - beginPointer;
        float x = Mathf.Abs(touchOffset.x);
        float y = Mathf.Abs(touchOffset.y);
        direction dir = direction.None;
        if (x>y&&x>50)
            {
            //水平移动
            dir = touchOffset.x > 0 ? direction.right : direction.left;
        }
        if(x<y&&y>50)
        {
            dir = touchOffset.y > 0 ? direction.up : direction.down;
        }
        if(dir !=direction.None)
        {
            core.move(dir);
            isDown = false;
        }
    }
    void Update()
    {
        if (core.IsChange)
       {

            ChangeMap();
           core.ComputeEmpty();
            CreateNewNumberSprite();

            core.IsChange = false;
        }
    }
    void ChangeMap()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                spriteArray[r, c].GetComponent<Image>().sprite = ResourceManager.GetImage(core.map[r,c]);
            }
        }
    }
}
