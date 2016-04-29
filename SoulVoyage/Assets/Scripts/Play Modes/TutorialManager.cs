using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    public void GoToMainMenu() {
        SceneManager.LoadScene(Scenes.MainMenuScene);
    }

    public void GoToControlsTutorial() {
        SceneManager.LoadScene(Scenes.ControlsTutorialScene);
    }

    public void GoToBlockingObstaclesTutorial() {
        SceneManager.LoadScene(Scenes.BlockingObstacleTutorialScene);
    }

    public void GoToForceObstacleTutorial() {
        SceneManager.LoadScene(Scenes.ForceObstacleTutorialScene);
    }

    public void GoToForceSwapObstacleTutorial() {
        SceneManager.LoadScene(Scenes.ForceSwapObstacleTutorialScene);
    }

    public void GoToDamagingObstaclesTutorial() {
        SceneManager.LoadScene(Scenes.DamagingObstacleTutorialScene);
    }

    public void GoToGameModesTutorial() {
        SceneManager.LoadScene(Scenes.GameModesTutorial);
    }
}
