using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JankenManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI enemyText;

    private bool gameStarted = false;

    int playerHP = 30;
    int enemyHP = 30;

    int[] damageValues = { 5, 9, 9 }; 

    public Slider playerHPBar;
    public Slider enemyHPBar;

    public AttackAnimation playerAttack;
    public AttackAnimationEnemy enemyAttack;

    public TextMeshProUGUI roundText;

    public Image playerHandImage;
    public Image enemyHandImage;

    public Sprite[] handSprites;

    public static bool playerWin;

    public Image enemyImage;

    public Sprite[] enemySprites;

    int[] enemyHPList = { 30, 50, 80 };

    public Vector2[] enemyImageSizes;

    int round = 1;

    void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        int id = GameData.selectedEnemy;

        enemyHP = enemyHPList[id];
        playerHP = 30;

        enemyImage.sprite = enemySprites[id];
        enemyImage.rectTransform.sizeDelta = enemyImageSizes[id];

        enemyHPBar.maxValue = enemyHP;
        enemyHPBar.value = enemyHP;

        playerHPBar.maxValue = playerHP;
        playerHPBar.value = playerHP;

        round = 1;
        roundText.text = "ROUND " + round;

        gameStarted = true;
        resultText.text = "Select your hands";
        enemyText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void PlayerChoice(int playerHand)
    {
        StartCoroutine(PlayerChoiceRoutine(playerHand));

        // if (!gameStarted) return;

        // int enemyHand = Random.Range(0, 3);

        // string[] hands = { "GLICO", "CHOCOLATE", "PINEAPPLE" };

        // enemyText.text = "ENEMY: " + hands[enemyHand];

        // int result = (playerHand - enemyHand + 3) % 3;

        // if (result == 0)
        // {
        //     resultText.text = "AGAIN";
        // }
        // else if (result == 2)   // ← プレイヤー勝ち
        // {
        //     enemyHP -= damageValues[playerHand];
        //     enemyHPBar.value = enemyHP;

        //     resultText.text = "YOU WIN";
        //     round++;
        // }
        // else                    // ← プレイヤー負け
        // {
        //     playerHP -= damageValues[enemyHand];
        //     playerHPBar.value = playerHP;

        //     resultText.text = "YOU LOSE";
        //     round++;
        // }

        // roundText.text = "ROUND " + round;

        // if (enemyHP <= 0)
        // {
        //     resultText.text = "YOU WIN THE GAME!";
        //     enemyText.text = "PRESS START BUTTON";
        //     gameStarted = false;
        // }
        // else if (playerHP <= 0)
        // {
        //     resultText.text = "YOU LOSE THE GAME!";
        //     enemyText.text = "PRESS START BUTTON";
        //     gameStarted = false;
        // }
    }

    IEnumerator PlayerChoiceRoutine(int playerHand)
    {
        if (!gameStarted) yield break;

        int enemyHand = Random.Range(0, 3);

        playerHandImage.sprite = handSprites[playerHand];
        enemyHandImage.sprite = handSprites[enemyHand];

        playerHandImage.enabled = true;
        enemyHandImage.enabled = true;

        string[] hands = { "GLICO", "CHOCOLATE", "PINEAPPLE" };

        enemyText.text = "ENEMY: " + hands[enemyHand];

        int result = (playerHand - enemyHand + 3) % 3;

        if (result == 0)
        {
            resultText.text = "AGAIN";

            StartCoroutine(playerAttack.Shake());
            StartCoroutine(enemyAttack.Shake());

            yield return new WaitForSeconds(0.4f);
        }
        else if (result == 2)   // プレイヤー勝ち
        {
            resultText.text = "YOU WIN";

            yield return StartCoroutine(playerAttack.Attack());

            enemyHP -= damageValues[playerHand];
            StartCoroutine(AnimateHP(enemyHPBar, enemyHP));

            round++;
        }
        else                    // プレイヤー負け
        {
            resultText.text = "YOU LOSE";

            yield return StartCoroutine(enemyAttack.Attack());

            playerHP -= damageValues[enemyHand];
            StartCoroutine(AnimateHP(playerHPBar, playerHP));

            round++;
        }

        roundText.text = "ROUND " + round;

        if (enemyHP <= 0)
        {
            // resultText.text = "YOU WIN THE GAME!";
            // enemyText.text = "PRESS START BUTTON";
            // gameStarted = false;
            playerWin = true;
            SceneManager.LoadScene("ResultScene");
        }
        else if (playerHP <= 0)
        {
            // resultText.text = "YOU LOSE THE GAME!";
            // enemyText.text = "PRESS START BUTTON";
            // gameStarted = false;
            playerWin = false;
            SceneManager.LoadScene("ResultScene");
        }
    }

    IEnumerator AnimateHP(Slider hpBar, int targetHP)
    {
        while (hpBar.value > targetHP)
        {
            hpBar.value -= 1;
            yield return new WaitForSeconds(0.03f);
        }

        hpBar.value = targetHP;
    }
}