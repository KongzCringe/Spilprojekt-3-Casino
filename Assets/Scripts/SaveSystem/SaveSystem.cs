using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerProgress(GameObject[] objects)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/player.progress";
        var stream = new FileStream(path, FileMode.Create);

        var data = new PlayerProgress(objects);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static PlayerProgress LoadPlayerProgress()
    {
        var path = Application.persistentDataPath + "/player.progress";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var data = formatter.Deserialize(stream) as PlayerProgress;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
