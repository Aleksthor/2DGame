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

    #region Displays
    [Header("Displays")]
    [SerializeField] GameObject displayPrefab;

    [SerializeField] GameObject Display_HeadTop;
    [SerializeField] GameObject Display_HeadBottom;
    [SerializeField] GameObject Display_Ear;

    [SerializeField] GameObject Display_Hair;
    [SerializeField] GameObject Display_Facialhair;

    [SerializeField] GameObject Display_Eye;
    [SerializeField] GameObject Display_Eyebrow;
    [SerializeField] GameObject Display_Mouth;
    [SerializeField] GameObject Display_Nose;
    #endregion

    #region GameObject List
    [Header("GameObject List")]
    [SerializeField] List<GameObject> GO_Head_Top_List = new List<GameObject>();
    #endregion

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

    #region Bools
    bool headTop;
    bool headBottom;
    bool ear;
    bool hair;
    bool facialhair;
    bool eye;
    bool eyebrow;
    bool mouth;
    bool nose;
    #endregion


    //--------------------


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
    private void Start()
    {
        headTop = false;
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        SetDisplayItems();
    }
    private void Update()
    {
        UpdateColors();
        UpdateDisplayColors();

        UpdateMainImage();

        UpdateDisplayPanelVisibility();
        DisplayButton_Clicked();
    }


    //--------------------


    void SetDisplayItems()
    {
        //Face Top
        for (int i = 0; i < face_top.Count; i++)
        {
            GO_Head_Top_List.Add(displayPrefab);
        }

        for (int i = 0; i < GO_Head_Top_List.Count; i++)
        {
            GO_Head_Top_List[i].GetComponent<Image>().sprite = face_top[i];

            (Instantiate(GO_Head_Top_List[i], new Vector3(0, 0, 0), Quaternion.identity)).transform.parent = Display_HeadTop.transform;
        }

        //Face Bottom
        for (int i = 0; i < face_bottom.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_bottom[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_HeadBottom.transform;
        }

        //Ear
        for (int i = 0; i < face_ear.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_ear[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Ear.transform;
        }

        //Hair
        for (int i = 0; i < face_hair.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_hair[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Hair.transform;
        }

        //Facialhair
        for (int i = 0; i < face_facialhair.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_facialhair[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Facialhair.transform;
        }

        //Eye
        for (int i = 0; i < face_eye.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_eye[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Eye.transform;
        }

        //Eyebrow
        for (int i = 0; i < face_eyebrow.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_eyebrow[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Eyebrow.transform;
        }

        //Mouth
        for (int i = 0; i < face_mouth.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_mouth[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Mouth.transform;
        }

        //Nose
        for (int i = 0; i < face_nose.Count; i++)
        {
            displayPrefab.GetComponent<Image>().sprite = face_nose[i];

            (Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject).transform.parent = Display_Nose.transform;
        }
    }

    public void DisplayButton_Clicked()
    {
        for (int i = 0; i < face_top.Count; i++)
        {
            GO_Head_Top_List[i].GetComponent<Button>().onClick.AddListener(ChangeCharacterSprite);
        }
    }
    void ChangeCharacterSprite()
    {
        print("A Button is clicked");
    }

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

    void UpdateDisplayColors()
    {
        //Update Display Sprite colors
        for (int i = 0; i < face_top.Count; i++)
        {
            GO_Head_Top_List[i].GetComponent<Image>().color = color_Head_Face;


            if (GO_Head_Top_List[i].GetComponent<Image>().color == color_Head_Face)
            {
                //print("It works: " + GO_Head_Top_List[i].GetComponent<Image>().color);
            }

        }
    }

    void UpdateDisplayPanelVisibility()
    {
        if (headTop)
            Display_HeadTop.SetActive(true);
        else
            Display_HeadTop.SetActive(false);

        if (headBottom)
            Display_HeadBottom.SetActive(true);
        else
            Display_HeadBottom.SetActive(false);

        if (ear)
            Display_Ear.SetActive(true);
        else
            Display_Ear.SetActive(false);

        if (hair)
            Display_Hair.SetActive(true);
        else
            Display_Hair.SetActive(false);

        if (facialhair)
            Display_Facialhair.SetActive(true);
        else
            Display_Facialhair.SetActive(false);

        if (eye)
            Display_Eye.SetActive(true);
        else
            Display_Eye.SetActive(false);

        if (eyebrow)
            Display_Eyebrow.SetActive(true);
        else
            Display_Eyebrow.SetActive(false);

        if (mouth)
            Display_Mouth.SetActive(true);
        else
            Display_Mouth.SetActive(false);

        if (nose)
            Display_Nose.SetActive(true);
        else
            Display_Nose.SetActive(false);
    }


    //--------------------


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


    //--------------------


    //Buttons
    public void HeadTopButton()
    {
        headTop = true;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("HeadTopButton - Clicked");
    }
    public void HeadButtomButton()
    {
        headTop = false;
        headBottom = true;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("HeadButtomButton - Clicked");
    }
    public void EarButton()
    {
        headTop = false;
        headBottom = false;
        ear = true;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("EarButton - Clicked");
    }
    public void HairButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = true;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("HairButton - Clicked");
    }
    public void FacialhairButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = true;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("FacialhairButton - Clicked");
    }
    public void EyeButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = true;
        eyebrow = false;
        mouth = false;
        nose = false;

        print("EyeButton - Clicked");
    }
    public void EyebrowButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = true;
        mouth = false;
        nose = false;

        print("EyebrowButton - Clicked");
    }
    public void MouthButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = true;
        nose = false;

        print("MouthButton - Clicked");
    }
    public void NoseButton()
    {
        headTop = false;
        headBottom = false;
        ear = false;
        hair = false;
        facialhair = false;
        eye = false;
        eyebrow = false;
        mouth = false;
        nose = true;

        print("NoseButton - Clicked");
    }
}