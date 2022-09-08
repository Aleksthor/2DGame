using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCreationManager : MonoBehaviour
{
    [System.Serializable]
    class MainCharacter
    {
        [SerializeField] Sprite Head_Face_Top;
        [SerializeField] Sprite Head_Face_Bottom;
        [SerializeField] Sprite Head_Ear;

        [SerializeField] Sprite Head_Eye;
        [SerializeField] Sprite Head_Eyebrow;
        [SerializeField] Sprite Head_Mouth;
        [SerializeField] Sprite Head_Nose;

        [SerializeField] Sprite Head_Hair;
        [SerializeField] Sprite Head_FacialHair;
    };

    #region Setup - Sprite List
    [Header("Sprite List")]
    [SerializeField] List<Sprite> face_top = new List<Sprite>();
    [SerializeField] List<Sprite> face_bottom = new List<Sprite>();
    [SerializeField] List<Sprite> face_ear = new List<Sprite>();

    [SerializeField] List<Sprite> face_eye = new List<Sprite>();
    [SerializeField] List<Sprite> face_eyebrow = new List<Sprite>();
    [SerializeField] List<Sprite> face_mouth = new List<Sprite>();
    [SerializeField] List<Sprite> face_nose = new List<Sprite>();

    [SerializeField] List<Sprite> face_hair = new List<Sprite>();
    [SerializeField] List<Sprite> face_facialhair = new List<Sprite>();
    #endregion

    #region GameObjects CharacterView
    [Header("GameObjects CharacterView")]
    [SerializeField] GameObject Head_face_top_GO;
    [SerializeField] GameObject Head_face_bottom_GO;
    [SerializeField] GameObject Head_ear_GO;

    [SerializeField] GameObject Head_hair_GO;
    [SerializeField] GameObject Head_facialhair_GO;

    [SerializeField] GameObject Head_eye_GO;
    [SerializeField] GameObject Head_eyebrow_GO;
    [SerializeField] GameObject Head_mouth_GO;
    [SerializeField] GameObject Head_nose_GO;
    #endregion

    #region Sliders
    [Header("Head Sliders")]
    //Main
    [SerializeField] Slider head_face_color_R; //includes: Head Top, Head Bottom, Ear
    [SerializeField] Slider head_face_color_G;
    [SerializeField] Slider head_face_color_B;

    [SerializeField] Slider head_hair_color_R;
    [SerializeField] Slider head_hair_color_G;
    [SerializeField] Slider head_hair_color_B;

    [SerializeField] Slider head_facialhair_color_R;
    [SerializeField] Slider head_facialhair_color_G;
    [SerializeField] Slider head_facialhair_color_B;

    //Other
    [SerializeField] Slider head_eye_color_R;
    [SerializeField] Slider head_eye_color_G;
    [SerializeField] Slider head_eye_color_B;
    [SerializeField] Slider head_eye_color_A;

    [SerializeField] Slider head_eyebrow_color_R;
    [SerializeField] Slider head_eyebrow_color_G;
    [SerializeField] Slider head_eyebrow_color_B;
    [SerializeField] Slider head_eyebrow_color_A;

    [SerializeField] Slider head_mouth_color_R;
    [SerializeField] Slider head_mouth_color_G;
    [SerializeField] Slider head_mouth_color_B;
    [SerializeField] Slider head_mouth_color_A;

    [SerializeField] Slider head_nose_color_R;
    [SerializeField] Slider head_nose_color_G;
    [SerializeField] Slider head_nose_color_B;
    [SerializeField] Slider head_nose_color_A;
    #endregion

    #region SliderColor
    [Header("Slider Handle Colors")]
    //Main
    [SerializeField] Image head_face_handleColor_R;
    [SerializeField] Image head_face_handleColor_G;
    [SerializeField] Image head_face_handleColor_B;

    [SerializeField] Image head_hair_handleColor_R;
    [SerializeField] Image head_hair_handleColor_G;
    [SerializeField] Image head_hair_handleColor_B;

    [SerializeField] Image head_facialhair_handleColor_R;
    [SerializeField] Image head_facialhair_handleColor_G;
    [SerializeField] Image head_facialhair_handleColor_B;

    //Other
    [SerializeField] Image head_eye_handleColor_R;
    [SerializeField] Image head_eye_handleColor_G;
    [SerializeField] Image head_eye_handleColor_B;
    [SerializeField] Image head_eye_handleColor_A;

    [SerializeField] Image head_eyebrow_handleColor_R;
    [SerializeField] Image head_eyebrow_handleColor_G;
    [SerializeField] Image head_eyebrow_handleColor_B;
    [SerializeField] Image head_eyebrow_handleColor_A;

    [SerializeField] Image head_mouth_handleColor_R;
    [SerializeField] Image head_mouth_handleColor_G;
    [SerializeField] Image head_mouth_handleColor_B;
    [SerializeField] Image head_mouth_handleColor_A;

    [SerializeField] Image head_nose_handleColor_R;
    [SerializeField] Image head_nose_handleColor_G;
    [SerializeField] Image head_nose_handleColor_B;
    [SerializeField] Image head_nose_handleColor_A;
    #endregion

    #region Images
    [Header("Images")]
    [SerializeField] Image Head_face_top;
    [SerializeField] Image Head_face_bottom;
    [SerializeField] Image Head_ear;

    [SerializeField] Image Head_hair;
    [SerializeField] Image Head_facialhair;

    [SerializeField] Image Head_eye;
    [SerializeField] Image Head_eyebrow;
    [SerializeField] Image Head_mouth;
    [SerializeField] Image Head_nose;
    #endregion

    #region Colors
    Color color_Head_Face;

    Color color_Head_Eye;
    Color color_Head_Eyebrow;
    Color color_Head_Mouth;
    Color color_Head_Nose;

    Color color_Head_Hair;
    Color color_Head_Facialhair;
    #endregion


    //----------------------------------------------------------------------


    private void Awake()
    {
        #region Color Setup

        #region Head_Face
        //Slider Color
        head_face_color_R.value = 0.5f;
        head_face_color_G.value = 0.5f;
        head_face_color_B.value = 0.5f;
        #endregion

        #region Head_Hair
        //Slider Color
        head_hair_color_R.value = 0.5f;
        head_hair_color_G.value = 0.5f;
        head_hair_color_B.value = 0.5f;
        #endregion
        #region Head_Facialhair
        //Slider Color
        head_facialhair_color_R.value = 0.5f;
        head_facialhair_color_G.value = 0.5f;
        head_facialhair_color_B.value = 0.5f;
        #endregion

        #region Head_Eye
        head_eye_color_R.value = 0.5f;
        head_eye_color_G.value = 0.5f;
        head_eye_color_B.value = 0.5f;
        head_eye_color_A.value = 1f;
        #endregion
        #region Head_Eyebrow
        head_eyebrow_color_R.value = 0.5f;
        head_eyebrow_color_G.value = 0.5f;
        head_eyebrow_color_B.value = 0.5f;
        head_eyebrow_color_A.value = 1f;
        #endregion
        #region Head_Mouth
        head_mouth_color_R.value = 0.5f;
        head_mouth_color_G.value = 0.5f;
        head_mouth_color_B.value = 0.5f;
        head_mouth_color_A.value = 1f;
        #endregion
        #region Head_Nose
        head_nose_color_R.value = 0.5f;
        head_nose_color_G.value = 0.5f;
        head_nose_color_B.value = 0.5f;
        head_nose_color_A.value = 1f;
        #endregion
        #endregion
    }
    private void Update()
    {
        UpdateColors();
        UpdateMainImage();
    }


    //----------------------------------------------------------------------


    void UpdateMainImage()
    {
        Head_face_top.sprite = face_top[0];
        Head_face_bottom.sprite = face_bottom[0];
        Head_ear.sprite = face_ear[0];

        Head_hair.sprite = face_hair[0];
        Head_facialhair.sprite = face_facialhair[0];

        Head_eye.sprite = face_eye[0];
        Head_eyebrow.sprite = face_eyebrow[0];
        Head_mouth.sprite = face_mouth[0];
        Head_nose.sprite = face_nose[0];


        //---------------

        #region Check Visibility
        if (Head_face_top.sprite == null)
            Head_face_top_GO.SetActive(false);
        else
            Head_face_top_GO.SetActive(true);

        if (Head_face_bottom.sprite == null)
            Head_face_bottom_GO.SetActive(false);
        else
            Head_face_bottom_GO.SetActive(true);

        if (Head_ear.sprite == null)
            Head_ear_GO.SetActive(false);
        else
            Head_ear_GO.SetActive(true);

        if (Head_hair.sprite == null)
            Head_hair_GO.SetActive(false);
        else
            Head_hair_GO.SetActive(true);

        if (Head_facialhair.sprite == null)
            Head_facialhair_GO.SetActive(false);
        else
            Head_facialhair_GO.SetActive(true);

        if (Head_eye.sprite == null)
            Head_eye_GO.SetActive(false);
        else
            Head_eye_GO.SetActive(true);

        if (Head_eyebrow.sprite == null)
            Head_eyebrow_GO.SetActive(false);
        else
            Head_eyebrow_GO.SetActive(true);

        if (Head_mouth.sprite == null)
            Head_mouth_GO.SetActive(false);
        else
            Head_mouth_GO.SetActive(true);

        if (Head_nose.sprite == null)
            Head_nose_GO.SetActive(false);
        else
            Head_nose_GO.SetActive(true);
        #endregion
    }

    void UpdateColors()
    {
        //Set Colors
        //Head Top, Head Bottom, Ear
        color_Head_Face = new Color(head_face_color_R.value, head_face_color_G.value, head_face_color_B.value, 1f);

        //Hair
        color_Head_Hair = new Color(head_hair_color_R.value, head_hair_color_G.value, head_hair_color_B.value, 1f);
        color_Head_Facialhair = new Color(head_facialhair_color_R.value, head_facialhair_color_G.value, head_facialhair_color_B.value, 1f);

        //Other
        color_Head_Eye = new Color(head_eye_color_R.value, head_eye_color_G.value, head_eye_color_B.value, head_eye_color_A.value);
        color_Head_Eyebrow = new Color(head_eyebrow_color_R.value, head_eyebrow_color_G.value, head_eyebrow_color_B.value, head_eyebrow_color_A.value);
        color_Head_Mouth = new Color(head_mouth_color_R.value, head_mouth_color_G.value, head_mouth_color_B.value, head_mouth_color_A.value);
        color_Head_Nose = new Color(head_nose_color_R.value, head_nose_color_G.value, head_nose_color_B.value, head_nose_color_A.value);


        //--------------------


        //Update Image colors
        Head_face_top.color = color_Head_Face;
        Head_face_bottom.color = color_Head_Face;
        Head_ear.color = color_Head_Face;

        Head_eye.color = color_Head_Eye;
        Head_eyebrow.color = color_Head_Eyebrow;

        Head_mouth.color = color_Head_Mouth;
        Head_nose.color = color_Head_Nose;

        Head_hair.color = color_Head_Hair;
        Head_facialhair.color = color_Head_Facialhair;
    }

    #region Color on handle of slider changes with value once clicked and dragged
   
    #region Head Face
    public void Head_Face_handleColor_R()
    {
        head_face_handleColor_R.GetComponent<Image>().color = new Color(head_face_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Face_handleColor_G()
    {
        head_face_handleColor_G.GetComponent<Image>().color = new Color(0f, head_face_color_G.value, 0f, 1f);
    }
    public void Head_Face_handleColor_B()
    {
        head_face_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_face_color_B.value, 1f);
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
    #region Head Facialhair
    public void Head_Facialhair_handleColor_R()
    {
        head_facialhair_handleColor_R.GetComponent<Image>().color = new Color(head_facialhair_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Facialhair_handleColor_G()
    {
        head_facialhair_handleColor_G.GetComponent<Image>().color = new Color(0f, head_facialhair_color_G.value, 0f, 1f);
    }
    public void Head_Facialhair_handleColor_B()
    {
        head_facialhair_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_facialhair_color_B.value, 1f);
    }
    #endregion

    #region Head Eye
    public void Head_Eye_handleColor_R()
    {
        head_eye_handleColor_R.GetComponent<Image>().color = new Color(head_eye_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Eye_handleColor_G()
    {
        head_eye_handleColor_G.GetComponent<Image>().color = new Color(0f, head_eye_color_G.value, 0f, 1f);
    }
    public void Head_Eye_handleColor_B()
    {
        head_eye_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_eye_color_B.value, 1f);
    }
    public void Head_Eye_handleColor_A()
    {
        head_eye_handleColor_A.GetComponent<Image>().color = new Color(0f, 0f, 0f, head_eye_color_A.value);
    }
    #endregion
    #region Head Eyebrow
    public void Head_Eyebrow_handleColor_R()
    {
        head_eyebrow_handleColor_R.GetComponent<Image>().color = new Color(head_eyebrow_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Eyebrow_handleColor_G()
    {
        head_eyebrow_handleColor_G.GetComponent<Image>().color = new Color(0f, head_eyebrow_color_G.value, 0f, 1f);
    }
    public void Head_Eyebrow_handleColor_B()
    {
        head_eyebrow_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_eyebrow_color_B.value, 1f);
    }
    public void Head_Eyebrow_handleColor_A()
    {
        head_eyebrow_handleColor_A.GetComponent<Image>().color = new Color(0f, 0f, 0f, head_eyebrow_color_A.value);
    }
    #endregion
    #region Head Mouth
    public void Head_Mouth_handleColor_R()
    {
        head_mouth_handleColor_R.GetComponent<Image>().color = new Color(head_mouth_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Mouth_handleColor_G()
    {
        head_mouth_handleColor_G.GetComponent<Image>().color = new Color(0f, head_mouth_color_G.value, 0f, 1f);
    }
    public void Head_Mouth_handleColor_B()
    {
        head_mouth_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_mouth_color_B.value, 1f);
    }
    public void Head_Mouth_handleColor_A()
    {
        head_mouth_handleColor_A.GetComponent<Image>().color = new Color(0f, 0f, 0f, head_mouth_color_A.value);
    }
    #endregion
    #region Head Nose
    public void Head_Nose_handleColor_R()
    {
        head_nose_handleColor_R.GetComponent<Image>().color = new Color(head_nose_color_R.value, 0f, 0f, 1f);
    }
    public void Head_Nose_handleColor_G()
    {
        head_nose_handleColor_G.GetComponent<Image>().color = new Color(0f, head_nose_color_G.value, 0f, 1f);
    }
    public void Head_Nose_handleColor_B()
    {
        head_nose_handleColor_B.GetComponent<Image>().color = new Color(0f, 0f, head_nose_color_B.value, 1f);
    }
    public void Head_Nose_handleColor_A()
    {
        head_nose_handleColor_A.GetComponent<Image>().color = new Color(0f, 0f, 0f, head_nose_color_A.value);
    }
    #endregion

    #endregion
}
