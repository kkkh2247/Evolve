using Godot;
using System;
using System.Collections.Generic;

public partial class Quest : Node
{
    private static Quest instance;
    public static Quest Instance
    {
        get
        {
            return instance;
        }
    }

    private Dictionary<int, QuestData> questDictionary = new Dictionary<int, QuestData>();
    private Dictionary<int, QuestDescriptions> questDescriptDictionary = new Dictionary<int, QuestDescriptions>();

    public override void _Ready()
    {
        if (instance == null)
        {
            instance = this; // 🚀 AutoLoad에서 등록된 인스턴스를 사용
            GD.Print("퀘스트 싱글톤이 AutoLoad에서 정상적으로 설정됨.");
        }
        else
        {
            GD.PrintErr("이미 존재하는 퀘스트 싱글톤이 있음!");
        }
    }

    public void SetQuestData(List<QuestData> questData)
    {
        questDictionary.Clear();
        foreach (QuestData data in questData)
        {
            questDictionary[data.Quest_ID] = data;
            GD.Print($"퀘스트 ID: {data.Quest_ID}, 시작 NPC: {data.Start_NPC}");
        }
    }

    public void SetQuestDescriptionData(List<QuestDescriptions> questDescriptions)
    {
        questDescriptDictionary.Clear();
        foreach (QuestDescriptions data in questDescriptions)
        {
            questDescriptDictionary[data.Description_ID] = data;
            GD.Print($"설명 ID: {data.Description_ID}, 퀘스트 이름: {data.Questname}");
        }
    }
}
