using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //Public stuff for main menu - only define these there
    public Text highScoreText;
    public RectTransform happyMagnets;
    public RawImage menuImage;

    private Texture unmutedMenuPic, mutedMenuPic;
    private Vector2 happyMagnetsInitialPosition;

    //Public stuff for game over menu - only define these there
    public Text scoreText;
    public Text lapsText;


    private AudioManager audioManager;

    
    void Start()
    {
        if (highScoreText)
        {
            // in main menu
            highScoreText.text = GameObject.Find("HighscoreTracker").GetComponent<HighscoreTracker>().GetHighscore().ToString("0 000 000 000 000");
            unmutedMenuPic = Resources.Load<Texture>("2D_graphics/menu_unmuted");
            mutedMenuPic = Resources.Load<Texture>("2D_graphics/menu_muted");
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            ShowMutedMenu();
        }
        else if (scoreText)
        {
            // in "game over" screen
            HighscoreTracker highscoreTracker = GameObject.Find("HighscoreTracker").GetComponent<HighscoreTracker>();
            scoreText.text = highscoreTracker.GetLastScore().ToString("0 000 000 000 000");
            lapsText.text = highscoreTracker.GetLapsLastRound().ToString();
        }

        if (happyMagnets)
        {
            happyMagnetsInitialPosition = happyMagnets.anchoredPosition;
        }
    }

    void Update()
    {
        if (happyMagnets)
        {
            happyMagnets.Rotate(new Vector3(0, 0, 8 * Time.deltaTime), Space.Self);
        }
    }

    /**
     * move happyMagnets in a random space somewhere between [-5, -5] and [5, 5] from its initial position
     */
    public void MoveHappyMagnetsSlightly()
    {
        PlayButtonSound();
        happyMagnets.anchoredPosition = happyMagnetsInitialPosition - (Vector2.one * 5) + (new Vector2(Random.value, Random.value) * 10);
    }

    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        PlayButtonSound();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        PlayButtonSound();
        print("You quit the game!");
        Application.Quit();
    }

    public void ShowMutedMenu()
    {
        if (audioManager.GetMuted())
            menuImage.texture = mutedMenuPic;
        else
            menuImage.texture = unmutedMenuPic;
    }

    public void ToggleMute()
    {
        audioManager.ToggleMute();
    }

    private void PlayButtonSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
