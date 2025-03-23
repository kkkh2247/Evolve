using Godot;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class Player : Node2D
{
    private static Player instance;
    private PlayerData playerData;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                //GD.PrintErr("Player 싱글톤이 초기화되지 않았습니다.");
                InitializePlayer();
               // instance = this;
            }
            return instance;
        }
    }

    public string Name { get; set; }
    public int Level { get; set; }

    // Player 싱글톤 초기화 함수
    public static void InitializePlayer()
    {
      

        if (instance != null)
        {
            GD.PrintErr("Player 인스턴스가 이미 초기화되었습니다.");
            return;  // 이미 초기화된 경우 다시 초기화하지 않음
        }

        instance = new Player();  
        GD.Print("Player 싱글톤 초기화 완료");



        // Player 초기화에 필요한 추가 작업을 여기서 수행
        // 예: 데이터 로드, 상태 초기화 등
    }
    public void SetData(string jsonData)
    {
        if (!string.IsNullOrEmpty(jsonData))
        {
            playerData = JsonSerializer.Deserialize<PlayerData>(jsonData);
            GD.Print($"플레이어 데이터 적용 완료: {playerData.name}, Level: {playerData.level}");
        }
        else
        {
            GD.PrintErr("잘못된 플레이어 데이터입니다.");
        }
    }

    private void DefalutPlayerInfo(string name)
    {
        //PlayerData data = new PlayerData();

        //List<string> skill = null;

        //data = new PlayerData();

        //data.name = name;
        //data.hp = -1;
        //data.mp = -1;
        //data.experience = -1;
        //data.level = -1;
        //data.Skill = skill;

        //FileManager.Instance.ConvertJson<PlayerData>(data);
        //FileManager.Instance.SaveJson<PlayerData>(playerinfo_path, data);
        // 새로 만들기
        //ConvertToJson(playerinfo);
      //  FileManager.Instance.SaveFile(data);

        //Isplayerinfo = true;
    }

  
}
