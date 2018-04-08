using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static void SavePlayer(player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data;
        }

        return null;
    }

    public static string GetLevel()
    {
        return LoadPlayer().level;
    }
}

[Serializable]
public class PlayerData
{
    public float[] xyz;

    public string level;

    public PlayerData(player player)
    {
        this.xyz = new float[3];
        this.xyz[0] = player.respawnPoint.x;
        this.xyz[1] = player.respawnPoint.y;
        this.xyz[2] = player.respawnPoint.z;

        this.level = SceneManager.GetActiveScene().name;
    }
}
