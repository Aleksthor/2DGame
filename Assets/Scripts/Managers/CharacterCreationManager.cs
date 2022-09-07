using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreationManager : MonoBehaviour
{
    [System.Serializable]
    class HeadSprite
    {
        [SerializeField] Sprite Head_Face_Top;
        [SerializeField] Sprite Head_Face_Bottom;
        [SerializeField] Sprite Head_Eye;
        [SerializeField] Sprite Head_Eyebrow;
        [SerializeField] Sprite Head_Mouth;
        [SerializeField] Sprite Head_Ear;
        [SerializeField] Sprite Head_Nose;
    };

    [Header("Sprite List")]
    [SerializeField] List<HeadSprite> headSprites = new List<HeadSprite>();

    #region Sliders
    [Header("Head Sliders")]
    [SerializeField] Slider head_main_color_R;
    [SerializeField] Slider head_main_color_G;
    [SerializeField] Slider head_main_color_B;

    [SerializeField] Slider head_front_color_R;
    [SerializeField] Slider head_front_color_G;
    [SerializeField] Slider head_front_color_B;

    [SerializeField] Slider head_hair_color_R;
    [SerializeField] Slider head_hair_color_G;
    [SerializeField] Slider head_hair_color_B;

    [SerializeField] Slider head_hairDecor_color_R;
    [SerializeField] Slider head_hairDecor_color_G;
    [SerializeField] Slider head_hairDecor_color_B;

    [SerializeField] Slider head_top_color_R;
    [SerializeField] Slider head_top_color_G;
    [SerializeField] Slider head_top_color_B;
    #endregion

    #region SliderColor
    [Header("Slider Handle Colors")]
    [SerializeField] Image head_main_handleColor_R;
    [SerializeField] Image head_main_handleColor_G;
    [SerializeField] Image head_main_handleColor_B;

    [SerializeField] Image head_front_handleColor_R;
    [SerializeField] Image head_front_handleColor_G;
    [SerializeField] Image head_front_handleColor_B;

    [SerializeField] Image head_hair_handleColor_R;
    [SerializeField] Image head_hair_handleColor_G;
    [SerializeField] Image head_hair_handleColor_B;

    [SerializeField] Image head_hairDecor_handleColor_R;
    [SerializeField] Image head_hairDecor_handleColor_G;
    [SerializeField] Image head_hairDecor_handleColor_B;

    [SerializeField] Image head_top_handleColor_R;
    [SerializeField] Image head_top_handleColor_G;
    [SerializeField] Image head_top_handleColor_B;
    #endregion

    #region Images
    [Header("Images")]
    [SerializeField] Image Head_Main;
    [SerializeField] Image Head_Front;
    [SerializeField] Image Head_Hair;
    [SerializeField] Image Head_HairDecor;
    [SerializeField] Image Head_Top;
    #endregion

    #region Colors
    Color color_Head_Main;
    Color color_Head_Front;
    Color color_Head_Hair;
    Color color_Head_HairDecor;
    Color color_Head_Top;
    #endregion


    //----------------------------------------------------------------------


    private void Awake()
    {
        #region Color Setup

        #region Head_Main
        //Slider Color
        head_main_color_R.value = 1f;
        head_main_color_G.value = 1f;
        head_main_color_B.value = 1f;
        #endregion
        #region Head_Front
        //Slider Color
        head_front_color_R.value = 1f;
        head_front_color_G.value = 1f;
        head_front_color_B.value = 1f;
        #endregion
        #region Head_Hair
        //Slider Color
        head_hair_color_R.value = 1f;
        head_hair_color_G.value = 1f;
        head_hair_color_B.value = 1f;
        #endregion
        #region Head_HairDecor
        //Slider Color
        head_hairDecor_color_R.value = 1f;
        head_hairDecor_color_G.value = 1f;
        head_hairDecor_color_B.value = 1f;
        #endregion
        #region Head_Top
        //Slider Color
        head_top_color_R.value = 1f;
        head_top_color_G.value = 1f;
        head_top_color_B.value = 1f;
        #endregion

        #endregion

    }

    private void Update()
    {
        UpdateColors();
    }


    //----------------------------------------------------------------------


    void UpdateColors()
    {
        color_Head_Main = new Color(head_main_color_R.value, head_main_color_G.value, head_main_color_B.value, 1f);
        color_Head_Front = new Color(head_front_color_R.value, head_front_color_G.value, head_front_color_B.value, 1f);
        color_Head_Hair = new Color(head_hair_color_R.value, head_hair_color_G.value, head_hair_color_B.value, 1f);
        color_Head_HairDecor = new Color(head_hairDecor_color_R.value, head_hairDecor_color_G.value, head_hairDecor_color_B.value, 1f);
        color_Head_Top = new Color(head_top_color_R.value, head_top_color_G.value, head_top_color_B.value, 1f);

        Head_Main.color = color_Head_Main;
        Head_Front.color = color_Head_Front;
        Head_Hair.color = color_Head_Hair;
        Head_HairDecor.color = color_Head_HairDecor;
        Head_Top.color = color_Head_Top;
    }

    #region Color on handle of slider changes with value once clicked and dragged
   
    #region Head Main
    public void Head_Main_handleColor_R()
    {
        head_main_handleColor_R.GetComponent<Image>().color = new Color(head_main_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Main_handleColor_G()
    {
        head_main_handleColor_G.GetComponent<Image>().color = new Color(0f, head_main_color_G.value, 0f, 1f);
    }
    public void Head_Main_handleColor_B()
    {
        head_main_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_main_color_B.value, 1f);
    }
    #endregion
    #region Head Front
    public void Head_Front_handleColor_R()
    {
        head_front_handleColor_R.GetComponent<Image>().color = new Color(head_front_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Front_handleColor_G()
    {
        head_front_handleColor_G.GetComponent<Image>().color = new Color(0f, head_front_color_G.value, 0f, 1f);
    }
    public void Head_Front_handleColor_B()
    {
        head_front_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_front_color_B.value, 1f);
    }
    #endregion
    #region Head Hair
    public void Head_Hair_handleColor_R()
    {
        head_hair_handleColor_R.GetComponent<Image>().color = new Color(head_hair_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Hair_handleColor_G()
    {
        head_hair_handleColor_G.GetComponent<Image>().color = new Color(0f, head_hair_color_G.value, 0f, 1f);
    }
    public void Head_Hair_handleColor_B()
    {
        head_hair_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_hair_color_B.value, 1f);
    }
    #endregion
    #region Head HairDecor
    public void Head_HairDecor_handleColor_R()
    {
        head_hairDecor_handleColor_R.GetComponent<Image>().color = new Color(head_hairDecor_color_R.value, 0f, 0f, 1f);
    }
    public void Head_HairDecor_handleColor_G()
    {
        head_hairDecor_handleColor_G.GetComponent<Image>().color = new Color(0f, head_hairDecor_color_G.value, 0f, 1f);
    }
    public void Head_HairDecor_handleColor_B()
    {
        head_hairDecor_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_hairDecor_color_B.value, 1f);
    }
    #endregion
    #region Head Top
    public void Head_Top_handleColor_R()
    {
        head_top_handleColor_R.GetComponent<Image>().color = new Color(head_top_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Top_handleColor_G()
    {
        head_top_handleColor_G.GetComponent<Image>().color = new Color(0f, head_top_color_G.value, 0f, 1f);
    }
    public void Head_Top_handleColor_B()
    {
        head_top_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_top_color_B.value, 1f);
    }
    #endregion

    #endregion
}
