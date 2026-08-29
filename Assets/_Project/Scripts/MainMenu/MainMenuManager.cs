using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    [SerializeField] private GameObject playerItemPrefab;
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject deleteConfirmPanel;
    [SerializeField] private GameObject playerListPanel;
    [SerializeField] private GameObject continueButton;

    // Dữ liệu chứa danh sách tất cả người chơi
    private PlayerListData playerListData;

    // Người chơi đang được chọn để xóa
    private PlayerData playerToDelete;

    private void Start()
    {
        LoadPlayerList();
    }

    // Kiểm tra số lượng người chơi và quyết định
    public void ContinueGame()
    {
        LoadPlayerList();

        if (playerListData.players.Count == 0)
        {
            return;
        }

        if (playerListData.players.Count == 1)
        {
            currentPlayer = playerListData.players[0];

            GameManager.Instance.SetCurrentPlayer(currentPlayer);

            Debug.Log("Tiếp tục với người chơi: " + currentPlayer.playerName);
            Debug.Log("GameManager đang giữ: " + GameManager.Instance.CurrentPlayer.playerName);

            return;
        }

        playerListPanel.SetActive(true);
        CreatePlayerItems();
    }

    // HIỆN / ẨN NÚT "CHƠI TIẾP"
    private void UpdateContinueButton()
    {
        bool hasPlayers =
            playerListData != null &&
            playerListData.players != null &&
            playerListData.players.Count > 0;

        continueButton.SetActive(hasPlayers);

        if (!hasPlayers)
        {
            playerListPanel.SetActive(false);
        }
    }
    
    // LOAD DANH SÁCH NGƯỜI CHƠI
    private void LoadPlayerList()
    {
        playerListData = SaveSystem.LoadPlayers();
        UpdateContinueButton();

    }

    // TẠO iteam NGƯỜI CHƠI hiển thị trong bảng
    private void CreatePlayerItems()
    {
        foreach (PlayerData player in playerListData.players)
        {
            GameObject itemObject =
            Instantiate(playerItemPrefab, playerListContent);

            PlayerItemUI itemUI =
                itemObject.GetComponent<PlayerItemUI>();

            itemUI.Setup(player, OnPlayerSelected, OnPlayerDelete);
        }
    }

    // PLAYER ĐANG ĐƯỢC CHỌN để tiếp tục chơi
    private PlayerData currentPlayer;

    private void OnPlayerSelected(PlayerData player)
    {
        currentPlayer = player;

        GameManager.Instance.SetCurrentPlayer(currentPlayer);

        Debug.Log("Đã chọn người chơi: " + currentPlayer.playerName);
        Debug.Log("GameManager đang giữ: " + GameManager.Instance.CurrentPlayer.playerName);
    }

    // CHỌN PLAYER ĐỂ XÓA 
    private void OnPlayerDelete(PlayerData player)
    {
        playerToDelete = player;

        deleteConfirmPanel.SetActive(true);

        Debug.Log("Đang xác nhận xóa: " + player.playerName);
    }

    //Xác nhận xóa 
    public void ConfirmDeletePlayer()
    {
        if (playerToDelete == null)
            return;

        string deletedPlayerName = playerToDelete.playerName;

        playerListData.players.Remove(playerToDelete);

        SaveSystem.SavePlayers(playerListData);

        playerToDelete = null;

        deleteConfirmPanel.SetActive(false);

        ClearPlayerItems();
        if (playerListData.players.Count > 0)
        {
            CreatePlayerItems();
        }

        UpdateContinueButton();

        Debug.Log("Đã xóa người chơi: " + deletedPlayerName);
    }

    public void CancelDeletePlayer()
    {
        playerToDelete = null;

        deleteConfirmPanel.SetActive(false);
    }
    
    // CHƠI MỚI
    public void NewGame()
    {
        SceneManager.LoadScene("PlayerCreate");
    }
    
    //Mở danh sách 
    //public void OpenPlayerList()
    //{
    //    playerListPanel.SetActive(true);
    //    LoadPlayerList();
    //    CreatePlayerItems();
    //}

    //Đóng danh sách 
    public void ClosePlayerList()
    {
        playerListPanel.SetActive(false);
        ClearPlayerItems();
    }

    //xóa player
    private void ClearPlayerItems()
    {
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
    }
}