using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialScore = 1000;
    private MenuManager menuManager;
    private Player player;
    private Boat boat;
    private GameObject playerDir;
    private EchoEffect trail;
    private GameState state;
    private ScreenFeedback screenFeedback;
    public int currentScore = 0;
    public bool firstTry = true;
    private DiePannelScript diePannel;
    private WinPannelScript winPannel;
    private LevelLoader levelLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        print("Init the game manager");
        player = FindObjectOfType<Player>();
        playerDir = FindObjectOfType<DirectionPointer>().gameObject;
        screenFeedback = FindObjectOfType<ScreenFeedback>();
        menuManager = FindObjectOfType<MenuManager>();
        boat = FindObjectOfType<Boat>();
        trail = FindObjectOfType<EchoEffect>();
        diePannel = FindObjectOfType<DiePannelScript>();
        diePannel.hideDisplayPannel();
        winPannel = FindObjectOfType<WinPannelScript>();
        winPannel.hideDisplayPannel();
        levelLoader = FindObjectOfType<LevelLoader>();
        updateState(GameState.CONSTRUCTION);
    }

    // Update is called once per frame
    void Update()
    {
        screenFeedback.updateScore(currentScore);
    }

    public void onEnterPressed()
    {
        var listOfEnemies = FindObjectsOfType<Patrol>();
        if (state == GameState.CONSTRUCTION)
        {
            player.startMoving(playerDir.transform.right);
            playerDir.SetActive(false);
            trail.startTrail();
            
            foreach(Patrol enemy in listOfEnemies)
            {
                enemy.startPatrol();
            }
            updateState(GameState.RUNNING);
        }
        else if (state == GameState.RUNNING)
        {
            retry();
        }
    }

    private void retry()
    {
        var listOfEnemies = FindObjectsOfType<Patrol>();
        
        firstTry = false;
        player.gameObject.SetActive(true);
        playerDir.SetActive(true);

        player.reset();
        trail.clearTrail();
        if (boat != null)
        {
            boat.reset();
        }
        foreach (Patrol enemy in listOfEnemies)
        {
            enemy.stopMoving();
            enemy.reset();
        }
        updateState(GameState.CONSTRUCTION);
    }

    private void updateState(GameState newState)
    {
        switch (newState)
        {
            case GameState.RUNNING:
                screenFeedback.updateDisplay("Running");
                break;
            case GameState.WIN:
                screenFeedback.updateDisplay("Win");
                player.stopMoving();
                trail.stopTrail();
                break;
            case GameState.LOOSE:
                screenFeedback.updateDisplay("Loose");
                player.stopMoving();
                trail.stopTrail();
                break;
            case GameState.CONSTRUCTION:
                if (firstTry) currentScore = initialScore;
                screenFeedback.updateDisplay("Construction");
                break;
        }

        state = newState;
    }

    public void onRetryPressed()
    {
        // Do something
        retry();
    }

    public GameState getState()
    {
        return state;
    }

    public void PlayerTriggerFinish(){
        print("Finished");
        updateState(GameState.WIN);
        winPannel.displayWinPannel(currentScore);
    }

    public void finishGame()
    {
        levelLoader.SaveAndBackToLevelSelection(currentScore);
    }
    public void PlayerTriggerDeath(){
        print("You are dead");
        updateState(GameState.LOOSE);
        diePannel.displayDiePannel("stuck");
    }

    public void PlayerTriggerDrown(){
        print("drowned");
        updateState(GameState.LOOSE);
        diePannel.displayDiePannel("Drown");
    }

    public void addScore(int value){
        currentScore += value;
    }
    public void removeScore(int value){
        currentScore -= value;
    }

    public bool canStart()
    {
        return menuManager.isReadyToStart();
    }

    public bool isAllowedToSelectItem()
    {
        return state == GameState.CONSTRUCTION;
    }

}

public enum GameState
{
    CONSTRUCTION,
    RUNNING,
    WIN,
    LOOSE
}
