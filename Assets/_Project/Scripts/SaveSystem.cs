using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath =>
        Path.Combine(Application.persistentDataPath, "players.json");

    public static void SavePlayers(PlayerListData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static PlayerListData LoadPlayers()
    {
        if (!File.Exists(savePath))
        {
            return new PlayerListData();
        }

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<PlayerListData>(json);
    }

    public static bool HasSave()
    {
        return File.Exists(savePath);
    }
}