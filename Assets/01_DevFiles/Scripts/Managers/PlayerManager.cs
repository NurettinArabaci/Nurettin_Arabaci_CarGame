using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoSingleton<PlayerManager>
{
    public List<Player> selectedPlayers = new List<Player>();

    private int activeIndex = 0;

    private void Start()
    {
        for (int i = 1; i < selectedPlayers.Count; i++)
        {
            selectedPlayers[i].gameObject.SetActive(false);
        }
    }


    public void StartRecording()
    {
        GameStateEvent.Fire_OnChangeGameState(GameState.Play);

        ActiveObject().ResetState();
        ActiveObject().Recording();

        if (activeIndex < 1) return;

        for (int i = 0; i < activeIndex; i++)
        {
            selectedPlayers[i].Playback();
        }
    }

    public void StartPlayback()
    {
        ResetPlayer();

        foreach (var item in selectedPlayers)
        {
            item.Playback();

        }

    }

    public void ResetPlayer()
    {
        foreach (var item in selectedPlayers)
        {
            item.ResetState();
            item.activate = ActiveState.Passive;
            UIManager.Instance.OnLoseGame();

        }

    }

    public void NextPlayer()
    {

        if (activeIndex >= selectedPlayers.Count - 1) return;
        activeIndex++;
        ResetPlayer();
        ActiveObject().gameObject.SetActive(true);
        UIManager.Instance.OnResetGame();
    }

    Player ActiveObject()
    {
        selectedPlayers[activeIndex].activate = ActiveState.Active;
        return selectedPlayers[activeIndex];
    }

    public Player LastPlayer()
    {
        return selectedPlayers[selectedPlayers.Count - 1];
    }
}
