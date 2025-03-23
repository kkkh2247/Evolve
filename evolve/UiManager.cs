using Godot;
using System;

public partial class UiManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public void OpenOrCloseUI<T>(T uiElement,bool IsVisible) where T : Control
    {
        if (uiElement == null)
        {
            GD.PrintErr("UI 요소가 존재하지 않습니다.");
            return;
        }

        switch (uiElement)
        {
            case PanelContainer panel:
                if (IsVisible) { 
                panel.Visible = true;
                GD.Print("PanelContainer 숨김 처리");
                }
                else
                {
                    panel.Visible = false;
                    
                }
                break;

            case Button button:
                if (IsVisible) { 
                button.Visible = true;
                GD.Print("Button 숨김 처리");
                }
                else
                {
                    button.Visible = false;
                }
                break;

            case VBoxContainer vbox:
                if (IsVisible) { 
                vbox.Visible = true;
                GD.Print("VBoxContainer 숨김 처리");
                }
                else
                {
                    vbox.Visible = false;    
                }
                break;

            default:
                GD.PrintErr("지원되지 않는 UI 요소입니다.");
                break;
        }
    }
}
