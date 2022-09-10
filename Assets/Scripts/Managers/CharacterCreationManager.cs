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
    [SerializeField] List<GameObject> GO_Head_Bottom_List = new List<GameObject>();
    [SerializeField] List<GameObject> GO_Ear_List = new List<GameObject>();

    [SerializeField] List<GameObject> GO_Hair_List = new List<GameObject>();
    [SerializeField] List<GameObject> GO_Facialhair_List = new List<GameObject>();

    [SerializeField] List<GameObject> GO_Eye_List = new List<GameObject>();
    [SerializeField] List<GameObject> GO_Eyebrow_List = new List<GameObject>();
    [SerializeField] List<GameObject> GO_Mouth_List = new List<GameObject>();
    [SerializeField] List<GameObject> GO_Nose_List = new List<GameObject>();
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
    public GameObject Head_face_top_GO;
    public GameObject Head_face_bottom_GO;
    public GameObject Head_ear_GO;

    public GameObject Head_hair_GO;
    public GameObject Head_facialhair_GO;

    public GameObject Head_eye_GO;
    public GameObject Head_eyebrow_GO;
    public GameObject Head_mouth_GO;
    public GameObject Head_nose_GO;
    #endregion

    #region Sliders Colors
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

    #region Sliders - Position
    [Header("Position Sliders")]
    [SerializeField] Slider slider_Hair_x;
    [SerializeField] Slider slider_Hair_y;

    [SerializeField] Slider slider_Facialhair_x;
    [SerializeField] Slider slider_Facialhair_y;

    [SerializeField] Slider slider_Eye_x;
    [SerializeField] Slider slider_Eye_y;

    [SerializeField] Slider slider_Eyebrow_x;
    [SerializeField] Slider slider_Eyebrow_y;

    [SerializeField] Slider slider_Mouth_x;
    [SerializeField] Slider slider_Mouth_y;

    [SerializeField] Slider slider_Nose_x;
    [SerializeField] Slider slider_Nose_y;
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
    [HideInInspector] public bool headTop;
    [HideInInspector] public bool headBottom;
    [HideInInspector] public bool ear;
    [HideInInspector] public bool hair;
    [HideInInspector] public bool facialhair;
    [HideInInspector] public bool eye;
    [HideInInspector] public bool eyebrow;
    [HideInInspector] public bool mouth;
    [HideInInspector] public bool nose;
    #endregion

    #region SaveCharacterVariables
    //Sprites
    public Sprite saved_Head_Top;
    public Sprite saved_Head_Bottom;
    public Sprite saved_Ear;
    public Sprite saved_Hair;
    public Sprite saved_Facialhair;
    public Sprite saved_Eye;
    public Sprite saved_Eyebrow;
    public Sprite saved_Mouth;
    public Sprite saved_Nose;

    //Position
    public Vector2 saved_Local_Hair;
    public Vector2 saved_Local_Facialhair;
    public Vector2 saved_Local_Eye;
    public Vector2 saved_Local_Eyebrow;
    public Vector2 saved_Local_Mouth;
    public Vector2 saved_Local_Nose;
    #endregion

    #region Side Scroller
    [Header("Side Scroller")]
    [SerializeField] GameObject SideScrollContent;
    [SerializeField] Scrollbar scrollbar_Vertical;
    int SideScrollSize_Height = 430;
    int SideScrollSize_Width = 0;
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

        #region Position Startup
        slider_Hair_x.value = 0.5f;
        slider_Hair_y.value = 0.5f;

        slider_Facialhair_x.value = 0.5f;
        slider_Facialhair_y.value = 0.5f;

        slider_Eye_x.value = 0.5f;
        slider_Eye_y.value = 0.5f;

        slider_Eyebrow_x.value = 0.5f;
        slider_Eyebrow_y.value = 0.5f;

        slider_Mouth_x.value = 0.5f;
        slider_Mouth_y.value = 0.5f;

        slider_Nose_x.value = 0.5f;
        slider_Nose_y.value = 0.5f;
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
        SetStartImages();
    }
    private void Update()
    {
        UpdateColors();
        UpdateDisplayColors();

        UpdateMainImage();
        UpdateImagePosition();

        UpdateDisplayPanelVisibility();
    }


    //--------------------


    void SetDisplayItems()
    {
        //Face Top
        for (int i = 0; i < face_top.Count; i++)
        {
            GO_Head_Top_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Head_Top_List[i].transform.parent = Display_HeadTop.transform;

            GO_Head_Top_List[i].GetComponent<Image>().sprite = face_top[i];
        }

        //Face Bottom
        for (int i = 0; i < face_bottom.Count; i++)
        {
            GO_Head_Bottom_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Head_Bottom_List[i].transform.parent = Display_HeadBottom.transform;

            GO_Head_Bottom_List[i].GetComponent<Image>().sprite = face_bottom[i];
        }

        //Ear
        for (int i = 0; i < face_ear.Count; i++)
        {
            GO_Ear_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Ear_List[i].transform.parent = Display_Ear.transform;

            GO_Ear_List[i].GetComponent<Image>().sprite = face_ear[i];
        }

        //Hair
        for (int i = 0; i < face_hair.Count; i++)
        {
            GO_Hair_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Hair_List[i].transform.parent = Display_Hair.transform;

            GO_Hair_List[i].GetComponent<Image>().sprite = face_hair[i];
        }

        //Facialhair
        for (int i = 0; i < face_facialhair.Count; i++)
        {
            GO_Facialhair_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Facialhair_List[i].transform.parent = Display_Facialhair.transform;

            GO_Facialhair_List[i].GetComponent<Image>().sprite = face_facialhair[i];
        }

        //Eye
        for (int i = 0; i < face_eye.Count; i++)
        {
            GO_Eye_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Eye_List[i].transform.parent = Display_Eye.transform;

            GO_Eye_List[i].GetComponent<Image>().sprite = face_eye[i];
        }

        //Eyebrow
        for (int i = 0; i < face_eyebrow.Count; i++)
        {
            GO_Eyebrow_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Eyebrow_List[i].transform.parent = Display_Eyebrow.transform;

            GO_Eyebrow_List[i].GetComponent<Image>().sprite = face_eyebrow[i];
        }

        //Mouth
        for (int i = 0; i < face_mouth.Count; i++)
        {
            GO_Mouth_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Mouth_List[i].transform.parent = Display_Mouth.transform;

            GO_Mouth_List[i].GetComponent<Image>().sprite = face_mouth[i];
        }

        //Nose
        for (int i = 0; i < face_nose.Count; i++)
        {
            GO_Nose_List.Add(Instantiate(displayPrefab, new Vector3(0, 0, 0), Quaternion.identity));
            GO_Nose_List[i].transform.parent = Display_Nose.transform;

            GO_Nose_List[i].GetComponent<Image>().sprite = face_nose[i];
        }
    }
    void SetStartImages()
    {
        Head_face_top_GO.GetComponent<Image>().sprite = face_top[0];
        Head_face_bottom_GO.GetComponent<Image>().sprite = face_bottom[0];
        Head_ear_GO.GetComponent<Image>().sprite = face_ear[0];

        Head_hair_GO.GetComponent<Image>().sprite = face_hair[0];
        Head_facialhair_GO.GetComponent<Image>().sprite = face_facialhair[0];

        Head_eye_GO.GetComponent<Image>().sprite = face_eye[0];
        Head_eyebrow_GO.GetComponent<Image>().sprite = face_eyebrow[0];
        Head_mouth_GO.GetComponent<Image>().sprite = face_mouth[0];
        Head_nose_GO.GetComponent<Image>().sprite = face_nose[0];
    }


    //--------------------


    void UpdateMainImage()
    {
        #region Check Visibility
        if (Head_face_top_GO.GetComponent<Image>().sprite == null)
            Head_face_top_GO.SetActive(false);
        else
            Head_face_top_GO.SetActive(true);

        if (Head_face_bottom_GO.GetComponent<Image>().sprite == null)
            Head_face_bottom_GO.SetActive(false);
        else
            Head_face_bottom_GO.SetActive(true);

        if (Head_ear_GO.GetComponent<Image>().sprite == null)
            Head_ear_GO.SetActive(false);
        else
            Head_ear_GO.SetActive(true);

        if (Head_hair_GO.GetComponent<Image>().sprite == null)
            Head_hair_GO.SetActive(false);
        else
            Head_hair_GO.SetActive(true);

        if (Head_facialhair_GO.GetComponent<Image>().sprite == null)
            Head_facialhair_GO.SetActive(false);
        else
            Head_facialhair_GO.SetActive(true);

        if (Head_eye_GO.GetComponent<Image>().sprite == null)
            Head_eye_GO.SetActive(false);
        else
            Head_eye_GO.SetActive(true);

        if (Head_eyebrow_GO.GetComponent<Image>().sprite == null)
            Head_eyebrow_GO.SetActive(false);
        else
            Head_eyebrow_GO.SetActive(true);

        if (Head_mouth_GO.GetComponent<Image>().sprite == null)
            Head_mouth_GO.SetActive(false);
        else
            Head_mouth_GO.SetActive(true);

        if (Head_nose_GO.GetComponent<Image>().sprite == null)
            Head_nose_GO.SetActive(false);
        else
            Head_nose_GO.SetActive(true);
        #endregion
    }

    void UpdateImagePosition()
    {
        int positionSpeed = 10;

        Head_hair_GO.transform.localPosition = new Vector2(slider_Hair_x.value * positionSpeed - (0.5f * positionSpeed), slider_Hair_y.value * positionSpeed - (0.5f * positionSpeed));
        Head_facialhair_GO.transform.localPosition = new Vector2(slider_Facialhair_x.value * positionSpeed - (0.5f * positionSpeed), slider_Facialhair_y.value * positionSpeed - (0.5f * positionSpeed));
        Head_eye_GO.transform.localPosition = new Vector2(slider_Eye_x.value * positionSpeed - (0.5f * positionSpeed), slider_Eye_y.value * positionSpeed - (0.5f * positionSpeed));
        Head_eyebrow_GO.transform.localPosition = new Vector2(slider_Eyebrow_x.value * positionSpeed - (0.5f * positionSpeed), slider_Eyebrow_y.value * positionSpeed - (0.5f * positionSpeed));
        Head_mouth_GO.transform.localPosition = new Vector2(slider_Mouth_x.value * positionSpeed - (0.5f * positionSpeed), slider_Mouth_y.value * positionSpeed - (0.5f * positionSpeed));
        Head_nose_GO.transform.localPosition = new Vector2(slider_Nose_x.value * positionSpeed - (0.5f * positionSpeed), slider_Nose_y.value * positionSpeed - (0.5f * positionSpeed));
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
        Head_face_top_GO.GetComponent<Image>().color = color_Head_Face;
        Head_face_bottom_GO.GetComponent<Image>().color = color_Head_Face;
        Head_ear_GO.GetComponent<Image>().color = color_Head_Face;

        Head_eye_GO.GetComponent<Image>().color = color_Head_Eye;
        Head_eyebrow_GO.GetComponent<Image>().color = color_Head_Eyebrow;

        Head_mouth_GO.GetComponent<Image>().color = color_Head_Mouth;
        Head_nose_GO.GetComponent<Image>().color = color_Head_Nose;

        Head_hair_GO.GetComponent<Image>().color = color_Head_Hair;
        Head_facialhair_GO.GetComponent<Image>().color = color_Head_Facialhair;
    }

    void UpdateDisplayColors()
    {
        //Update Display Sprite colors
        for (int i = 0; i < face_top.Count; i++)
        {
            GO_Head_Top_List[i].GetComponent<Image>().color = color_Head_Face;
        }

        for (int i = 0; i < face_bottom.Count; i++)
        {
            GO_Head_Bottom_List[i].GetComponent<Image>().color = color_Head_Face;
        }

        for (int i = 0; i < face_ear.Count; i++)
        {
            GO_Ear_List[i].GetComponent<Image>().color = color_Head_Face;
        }

        for (int i = 0; i < face_hair.Count; i++)
        {
            GO_Hair_List[i].GetComponent<Image>().color = color_Head_Hair;
        }

        for (int i = 0; i < face_facialhair.Count; i++)
        {
            GO_Facialhair_List[i].GetComponent<Image>().color = color_Head_Facialhair;
        }

        for (int i = 0; i < face_eye.Count; i++)
        {
            GO_Eye_List[i].GetComponent<Image>().color = color_Head_Eye;
        }

        for (int i = 0; i < face_eyebrow.Count; i++)
        {
            GO_Eyebrow_List[i].GetComponent<Image>().color = color_Head_Eyebrow;
        }

        for (int i = 0; i < face_mouth.Count; i++)
        {
            GO_Mouth_List[i].GetComponent<Image>().color = color_Head_Mouth;
        }

        for (int i = 0; i < face_nose.Count; i++)
        {
            GO_Nose_List[i].GetComponent<Image>().color = color_Head_Nose;
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


    public void ChangeImage(Sprite sprite)
    {
        if (headTop)
        {
            Head_face_top_GO.GetComponent<Image>().sprite = sprite;
        }

        if (headBottom)
        {
            Head_face_bottom_GO.GetComponent<Image>().sprite = sprite;
        }

        if (ear)
        {
            Head_ear_GO.GetComponent<Image>().sprite = sprite;
        }

        if (hair)
        {
            Head_hair_GO.GetComponent<Image>().sprite = sprite;
        }

        if (facialhair)
        {
            Head_facialhair_GO.GetComponent<Image>().sprite = sprite;
        }

        if (eye)
        {
            Head_eye_GO.GetComponent<Image>().sprite = sprite;
        }

        if (eyebrow)
        {
            Head_eyebrow_GO.GetComponent<Image>().sprite = sprite;
        }

        if (mouth)
        {
            Head_mouth_GO.GetComponent<Image>().sprite = sprite;
        }

        if (nose)
        {
            Head_nose_GO.GetComponent<Image>().sprite = sprite;
        }
    }

    //Slider Handle Color Change
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


    //--------------------


    //Buttons
    public void ConfirmButton()
    {
        //Sprites
        if (Head_face_top_GO.GetComponent<Image>().sprite != null)
            saved_Head_Top = Head_face_top_GO.GetComponent<Image>().sprite;

        if (Head_face_bottom_GO.GetComponent<Image>().sprite != null)
            saved_Head_Bottom = Head_face_bottom_GO.GetComponent<Image>().sprite;

        if (Head_ear_GO.GetComponent<Image>().sprite != null)
            saved_Ear = Head_ear_GO.GetComponent<Image>().sprite;

        if (Head_hair_GO.GetComponent<Image>().sprite != null)
            saved_Hair = Head_hair_GO.GetComponent<Image>().sprite;

        if (Head_facialhair_GO.GetComponent<Image>().sprite != null)
            saved_Facialhair = Head_facialhair_GO.GetComponent<Image>().sprite;

        if (Head_eye_GO.GetComponent<Image>().sprite != null)
            saved_Eye = Head_eye_GO.GetComponent<Image>().sprite;

        if (Head_eyebrow_GO.GetComponent<Image>().sprite != null)
            saved_Eyebrow = Head_eyebrow_GO.GetComponent<Image>().sprite;

        if (Head_mouth_GO.GetComponent<Image>().sprite != null)
            saved_Mouth = Head_mouth_GO.GetComponent<Image>().sprite;

        if (Head_nose_GO.GetComponent<Image>().sprite != null)
            saved_Nose = Head_nose_GO.GetComponent<Image>().sprite;

        //Position
        saved_Local_Hair = Head_hair_GO.transform.position;
        saved_Local_Facialhair = Head_facialhair_GO.transform.position;
        saved_Local_Eye = Head_eye_GO.transform.position;
        saved_Local_Eyebrow = Head_eyebrow_GO.transform.position;
        saved_Local_Mouth = Head_mouth_GO.transform.position;
        saved_Local_Nose = Head_nose_GO.transform.position;

        //print("saved_Head_Top: " + saved_Head_Top.name);
        //print("saved_Head_Bottom: " + saved_Head_Bottom.name);
        //print("saved_Ear: " + saved_Ear.name);
        //print("saved_Hair: " + saved_Hair.name);
        //print("saved_Facialhair: " + saved_Facialhair.name);
        //print("saved_Eye: " + saved_Eye.name);
        //print("saved_Eyebrow: " + saved_Eyebrow.name);
        //print("saved_Mouth: " + saved_Mouth.name);
        //print("saved_Nose: " + saved_Nose.name);

        //print("saved_Local_Hair.x: " + saved_Local_Hair.x + " | saved_Local_Hair.y" + saved_Local_Hair.y);
        //print("saved_Local_Facialhair.x: " + saved_Local_Facialhair.x + " | saved_Local_Facialhair.y" + saved_Local_Facialhair.y);
        //print("saved_Local_Eye.x: " + saved_Local_Eye.x + " | saved_Local_Eye.y" + saved_Local_Eye.y);
        //print("saved_Local_Eyebrow.x: " + saved_Local_Eyebrow.x + " | saved_Local_Eyebrow.y" + saved_Local_Eyebrow.y);
        //print("saved_Local_Mouth.x: " + saved_Local_Mouth.x + " | saved_Local_Mouth.y" + saved_Local_Mouth.y);
        //print("saved_Local_Nose.x: " + saved_Local_Nose.x + " | saved_Local_Nose.y" + saved_Local_Nose.y);
    }

    void AdjustDisplaySize(List<GameObject> list)
    {
        SideScrollSize_Height = list.Count;

        if (SideScrollSize_Height % 4 == 0)
        {
            SideScrollSize_Height /= 4;
            SideScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(SideScrollSize_Width, (SideScrollSize_Height) * 110);
        }
        else
        {
            SideScrollSize_Height /= 4;
            SideScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(SideScrollSize_Width, (SideScrollSize_Height + 1) * 110);
        }

        scrollbar_Vertical.value = 1;
    }

    #region Bodyparts
    public void HeadTopButton()
    {
        AdjustDisplaySize(GO_Head_Top_List);

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
        AdjustDisplaySize(GO_Head_Bottom_List);

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
        AdjustDisplaySize(GO_Ear_List);

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
        AdjustDisplaySize(GO_Hair_List);

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
        AdjustDisplaySize(GO_Facialhair_List);
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
        AdjustDisplaySize(GO_Eye_List);

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
        AdjustDisplaySize(GO_Eyebrow_List);

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
        AdjustDisplaySize(GO_Mouth_List);

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
        AdjustDisplaySize(GO_Nose_List);

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
    #endregion
}