using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCreateManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CreatePlayer();
        }
    }
    public void CreatePlayer()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Tên người chơi không được để trống!");
            return;
        }

        PlayerListData data = SaveSystem.LoadPlayers();

        foreach (PlayerData player in data.players)
        {
            if (player.playerName.ToLower() == playerName.ToLower())
            {
                Debug.Log("Tên người chơi đã tồn tại!");
                return;
            }
        }

        PlayerData newPlayer = new PlayerData();
        newPlayer.playerName = playerName;

        data.players.Add(newPlayer);

        SaveSystem.SavePlayers(data);

        Debug.Log("Đã tạo người chơi: " + playerName);

        // Sau này sẽ chuyển sang màn chơi.
        // Hiện tại tạm quay về MainMenu để kiểm tra hệ thống.
        SceneManager.LoadScene("MainMenu");
    }

    public void CreatePlayerByEnter(string text)
    {
        CreatePlayer();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}