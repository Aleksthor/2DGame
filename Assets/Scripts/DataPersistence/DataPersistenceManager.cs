using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;


    private void Awake()
    {
        if(instance != null)
        {

        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void NewGame()
    {
        // call the constructor of game data to start a new game
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any Saved data
        this.gameData = fileDataHandler.Load();

        // if there was no data to load initialize a new game
        if (this.gameData == null)
        {
            Debug.Log("No Save Data was found. Starting a New Game");
            NewGame();
        }

        //push the loaded date to all other scripts that need it
        foreach(IDataPersistence dataObject in dataPersistenceObjects)
        {
            dataObject.LoadData(gameData);
        }
        
    }
    public void SaveGame()
    {
        Debug.Log("Saved Game");
        //pass the data to other scripts so they ca update it
        foreach (IDataPersistence dataObject in dataPersistenceObjects)
        {
            dataObject.SaveData(ref gameData);
        }

        //save data to a file using datahandler
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>(); 
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
