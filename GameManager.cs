using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialScore = 500;
    private Player player;
    private Boat boat;
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
        boat = FindObjectOfType<Boat>();
        updateState(GameState.CONSTRUCTION);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onEnterPressed();
        }
        screenFeedback.updateScore(currentScore);
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
            player.gameObject.SetActive(true);
            player.stopMoving();
            player.resetPlayer();
            boat.resetBoat();
            updateState(GameState.CONSTRUCTION);
        }
    }

    private void updateState(GameState newState)
    {
        switch (newState)
        {
            case GameState.RUNNING:
                screenFeedback.updateDisplay("Running");
                panelHider.hideCreationPannel();
                break;
            case GameState.WIN:
                screenFeedback.updateDisplay("Win");
                player.stopMoving();
                break;
            case GameState.LOOSE:
                currentScore = 0;
                screenFeedback.updateDisplay("Loose");
                player.stopMoving();
                break;
            case GameState.CONSTRUCTION:
                currentScore = initialScore;
                screenFeedback.updateDisplay("Construction");
                panelHider.showCreationPannel();
                break;
        }

        state = newState;
    }

    public void PlayerTriggerFinish(){
        print("Finished");
        updateState(GameState.WIN);
    }
    public void PlayerTriggerDeath(){
        print("You are dead");
        updateState(GameState.LOOSE);
    }

    public void PlayerTriggerDrown(){
        print("You drowned");
        updateState(GameState.LOOSE);
    }

    public void addScore(int value){
        currentScore += value;
    }

}

enum GameState
{
    CONSTRUCTION,
    RUNNING,
    WIN,
    LOOSE
}
