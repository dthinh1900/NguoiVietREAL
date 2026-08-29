using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text progressText;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button deleteButton;

    private PlayerData playerData;
    private Action<PlayerData> onSelect;
    private Action<PlayerData> onDelete;

    public void Setup(PlayerData playerData, Action<PlayerData> onSelect, Action<PlayerData> onDelete)
    {
        this.playerData = playerData;
        this.onSelect = onSelect;
        this.onDelete = onDelete;

        playerNameText.text = playerData.playerName;

        progressText.text =
            $"Chương {playerData.currentChapter} - Màn {playerData.currentStage}";

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelectButtonClicked);

        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    private void OnSelectButtonClicked()
    {
        onSelect?.Invoke(playerData);
    }

    private void OnDeleteButtonClicked()
    {
        onDelete?.Invoke(playerData);
    }
}