using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state { Menu, Game, Settings };

public class StateManager : MonoBehaviour {

    public state currentState;

	void Start () 
    {
        currentState = state.Menu;
	}
	
	void Update () 
    {
        StateSwitcher();
	}

    void StateSwitcher()
    {
        switch (currentState)
        {
            case state.Menu:
                
                break;
            case state.Game:
                
                break;
            case state.Settings:
                
                break;
            default:
                break;
        }

    }

    public void ToMenu()
    {
        currentState = state.Menu;
    }

    public void ToGame()
    {
        currentState = state.Game;
    }

    public void ToSettings()
    {
        currentState = state.Settings;
    }
}
