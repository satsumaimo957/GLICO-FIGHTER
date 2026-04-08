using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public void SelectEnemy(int enemyID)
    {
        GameData.selectedEnemy = enemyID;
        SceneManager.LoadScene("BattleScene");
    }
}