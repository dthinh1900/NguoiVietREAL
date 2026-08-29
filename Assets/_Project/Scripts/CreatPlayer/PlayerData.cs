using System;

[Serializable]
public class PlayerData
{
    public string playerName;

    // Tiến trình
    public int currentChapter = 1;
    public int currentStage = 1;

    // Người Việt
    public int vietnamesePercent = 100;

    // Độ chính xác của trợ giúp Gọi Người Việt
    public int callVietnameseAccuracy = 100;

    // Trạng thái trợ giúp của MÀN ĐANG CHƠI
    public bool fiftyFiftyUsed = false;
    public bool callVietnameseUsed = false;
    public bool audienceUsed = false;
}