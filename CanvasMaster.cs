using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制所有界面的开关和关闭
/// </summary>
public class CanvasMaster : MonoBehaviour
{
    [SerializeField]private CanvasController m_cancon = null;
    [SerializeField] private XmlRead m_xmlRead = null;
    [SerializeField] private TartgetMove m_tartgetMove = null;
    [SerializeField]private float waitTimes = 0.3f;
    [SerializeField] private ButtonExt buttonExt = null;
    //public Animator anim = null;
    public GameObject stand_Panel = null;
    public GameObject explain_Panel = null;
    public GameObject game_Panel = null;
    public GameObject story_panel = null;
    //开始为Game界面按钮
    public GameObject goTitle = null;      //滚动条
    public GameObject Btn_Back = null;
    public GameObject drug_Select = null;  //6个圆选择按钮
    public bool ISmove = false;
    public GameObject Obj_title = null;
    #region 以上为前两个界面按钮
    //事件方法
    /// <summary>
    /// 显示在一开始的图片选择
    /// </summary>
    public void StandPlanEvents()
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(WaitTime());
        else
            return;
    }
    public void ExplainPlanEvents()
    {
        m_cancon.HideCanvaas(explain_Panel);
        game_Panel.SetActive(true);
        StopAllCoroutines();
    }
    /// <summary>
    /// 键盘按键显示对应圆圈的信息
    /// </summary>
    public void ShowGamePanel()
    {
        goTitle.SetActive(false);
        Btn_Back.SetActive(false);
        m_cancon.ShowCanvas(game_Panel);
        ShowPlayer();

    }
    public void Backone()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(WaitTimes());
        }
        else
            return;
        
    }
    /// <summary>
    /// 游戏返回界面的返回键
    /// 全部返回到第二个游戏说明部分
    /// </summary>
    public void BackTwo()
    {
        m_cancon.HideCanvaas(game_Panel);  //关闭游戏界面显示第二部分介绍界面
        m_cancon.ShowCanvas(explain_Panel);
        //下面所有物体重新赋初值
        goTitle.SetActive(false);
        Btn_Back.SetActive(false);
        drug_Select.SetActive(false);
        ISmove = false;
        m_cancon.HideCanvaas(story_panel);
    }
    //等待函数，播放一个过渡动画
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTimes);
        m_cancon.HideCanvaas(stand_Panel);
        m_cancon.ShowCanvas(explain_Panel);
    }
    IEnumerator WaitTimes()
    {
        yield return new WaitForSeconds(waitTimes);
        m_cancon.HideCanvaas(explain_Panel);
        m_cancon.ShowCanvas(stand_Panel);
        //按钮抬起时间
    }
    #endregion

    #region game界面
    /// <summary>
    /// 首界面 类型选择
    /// </summary>
    /// <param name="num">记录按钮选择</param>
    /// int 保存num
    /// 
    public GameObject[] parfabs = null;   //7个预制体
    int baoCun1 = -1;
    float time = 0;
    bool mBeginClick = false;
    Vector3[] bornPos= new Vector3[4] {new Vector3(85, 78, 0), new Vector3(955, 125, 0), new Vector3(600, 455, 0), new Vector3(85, 78, 0), };     //创建3个
    /// <summary>
    /// 多选界面（多个感应元件）界面生成预制体
    /// </summary>
    /// <param name="num"></param>
    public void PrfabBorn(int num)
    {

        Instantiate(parfabs[num], bornPos[0], Quaternion.identity);
        Instantiate(parfabs[num], bornPos[1], Quaternion.identity);
        Instantiate(parfabs[num], bornPos[2], Quaternion.identity);
        Instantiate(parfabs[num], bornPos[3], Quaternion.identity);
    }
    public void Title(int num)
    {
        drug_Select.SetActive(true);
        m_xmlRead.SecondPanel_text(num);
        m_xmlRead.PictureRead(num);
        m_xmlRead.XmlReadFristPanel(num);
        Titlechange();
        //anim.SetTrigger("2");
        StartCoroutine(HideTitle());
        baoCun1 = num;
        ISmove = false;

    }
    /// <summary>
    /// 二号界面的选取
    /// </summary>
    /// <param name="num">按钮数值</param>
    public void DrugSelect(int num)
    {
        m_xmlRead.SelectPicture(num);
        m_xmlRead.ReadSecondPanle(num);
        m_xmlRead.SelectPhotoes(baoCun1, num);
        m_xmlRead.FinalPanel_Story(baoCun1, num);
        m_cancon.ShowCanvas(story_panel);
        
        ISmove = true;
        return;
    }
    #region 末尾界面
    /// <summary>
    /// 最后故事界面 界面回到选择界面
    /// </summary>
    public void Back_final()
    {
        drug_Select.SetActive(false);
        ISmove = false;
        m_cancon.HideCanvaas(story_panel);
        m_xmlRead.SecondPanel_text(baoCun1);
        m_xmlRead.PictureRead(baoCun1);
        m_cancon.HideCanvaas(story_panel);
    }
    public void GameButtonBack()
    {
        Obj_title.SetActive(true);
        goTitle.SetActive(true);
        Btn_Back.SetActive(true);
    }
    /// <summary>
    /// 按键抬起
    /// </summary>
    public void ButtonUp()
    {
        Obj_title.SetActive(true);
        goTitle.SetActive(true);
        Btn_Back.SetActive(true);
        drug_Select.SetActive(false);
        ISmove = false;
        m_cancon.HideCanvaas(story_panel);  
        m_cancon.HideCanvaas(game_Panel);
    }
    public void Titlechange()
    {
        StartCoroutine(HideTitle());
    }
    /// <summary>
    /// 3个标题栏逐渐消失
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public IEnumerator HideTitle()
    {
        yield return new WaitForSeconds(1.0f);
        Obj_title.SetActive(false);
    }
    #endregion
    #endregion

    #region cartoon效果
    int localnum = -100;
    public void ShowPlayer()
    {
        //首先判断游戏界面显示
        if (buttonExt.num!= localnum)
        {
            if (game_Panel.GetComponent<CanvasGroup>().alpha == 1&&(drug_Select.activeSelf!=true))
            {
                //anim.SetTrigger("1");
                Debug.Log("local" + localnum + "num" + buttonExt.num);
                localnum = buttonExt.num;
            }
            else
            {
                //anim.SetTrigger("2");
                localnum = -100;
            }
        }

    }
    //private void Update()
    //{
    //    ShowPlayer();
    //}
    #endregion
}
