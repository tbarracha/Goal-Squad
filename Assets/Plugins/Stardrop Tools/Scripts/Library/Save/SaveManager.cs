
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    /// <summary>
    /// Save item on persistant data path + defined file path (with extension)
    /// Ex: "/player/data.fun"
    /// </summary>
    public static void BinarySave<T>(T data, string filePathWithExtenstion)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + filePathWithExtenstion;
        FileStream stream = new FileStream(filePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    /// <summary>
    /// Attempts to retreave data on persistant data path + defined file path (with extension)
    /// Ex: "/player/data.fun"
    /// </summary>
    public static T BinaryLoad<T>(string filePathWithExtenstion)
    {
        string filePath = Application.persistentDataPath + filePathWithExtenstion;

        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            T data = (T)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }

        else
        {
            Debug.Log("No file found at: " + filePath);
            return default(T);
        }
    }


    /// <summary>
    /// TO DO!
    /// Save item on persistant data path, with defined file path and extension
    /// Ex: "/player/data.fun"
    /// </summary>
    public static void JSONSave<T>(T data, string filePathWithExtenstion)
    {
        string jsonData = JsonUtility.ToJson(data);
    }
}
