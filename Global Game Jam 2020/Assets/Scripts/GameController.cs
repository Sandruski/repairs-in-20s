using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { startscreen, play, spawn, endscreen };
    public GameState gameState;

    void Update()
    {
        switch(gameState)
        {
            case GameState.startscreen:
                break;
            case GameState.play:
                break;
            case GameState.spawn:
                break;
            case GameState.endscreen:
                break;
        }
    }
}
