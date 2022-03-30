using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public string deathSoundName = "Die";
    public string damageSoundName = "Hurt";

    private AudioManager audioManager;

    public static bool gameOver;
    public GameObject gameOverPanel;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;


    public bool Dead
    {
        get => dead;
    }
    private void Awake()
    {
        if (PlayerPrefs.GetInt("HealthyStartX2", 0) > 0)
        {
            PlayerPrefs.SetInt("HealthyStartX2", PlayerPrefs.GetInt("HealthyStartX2", 0) - 1);
            startingHealth *= 2;
        }
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());

            audioManager.PlaySound(damageSoundName);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<Doodle>().enabled = false;
                dead = true;

                audioManager.PlaySound(deathSoundName);

                gameOver = true;
                gameOverPanel.SetActive(true);
                //currentHealth = 100;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null) ;
    }
}