using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialScore = 500;
    private Player player;
    private GameState state;
    private CreationPanelHider panelHider;
    private ScreenFeedback screenFeedback;
    private int currentScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        panelHider = FindObjectOfType<CreationPanelHider>();
        screenFeedback = FindObjectOfType<ScreenFeedback>();
        updateState(GameState.CONSTRUCTION);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onEnterPressed();
        }   
    }

    private void onEnterPressed()
    {
        if (state == GameState.CONSTRUCTION)
        {
            player.startMoving();
            updateState(GameState.RUNNING);
        }
        else if (state == GameState.RUNNING)
        {
            player.stopMoving();
            player.resetPlayer();
            updateState(GameState.CONSTRUCTION);
        }
    }

    private void updateState(GameState newState)
    {
        switch (newState)
        {
            case GameState.RUNNING:
                screenFeedback.updateDisplay("Running", currentScore);
                panelHider.hideCreationPannel();
                break;
            case GameState.WIN:
                screenFeedback.updateDisplay("Win", currentScore);
                player.stopMoving();
                break;
            case GameState.LOOSE:
                currentScore = 0;
                screenFeedback.updateDisplay("Loose", currentScore);
                player.stopMoving();
                break;
            case GameState.CONSTRUCTION:
                currentScore = initialScore;
                screenFeedback.updateDisplay("Construction", currentScore);
                panelHider.showCreationPannel();
                break;
        }

        state = newState;
    }

    public static void PlayerTriggerFinish(){
        print("Finished");
    }
    public static void PlayerTriggerDeath(){
        print("You are dead");
    }

}

enum GameState
{
    CONSTRUCTION,
    RUNNING,
    WIN,
    LOOSE
}
