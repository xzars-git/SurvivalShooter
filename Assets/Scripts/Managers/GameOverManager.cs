using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : UnityEngine.MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text warningText;
    public float restartDelay = 5f;
    bool isDead;
    bool isEnemy;


    Animator anim;                          
    float restartTimer;                    


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            if (!isDead)
            {
                anim.SetTrigger("GameOver");
                isDead = true;
            }
           

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("{0} m !!!", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }
}