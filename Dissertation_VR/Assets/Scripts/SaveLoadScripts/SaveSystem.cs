using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(DnParent mama)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/roll.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        DnData data = new DnData(mama);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DnData LoadData()
    {
        string path = Application.persistentDataPath + "/roll.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DnData data = formatter.Deserialize(stream) as DnData;
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
