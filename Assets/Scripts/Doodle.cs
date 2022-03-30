using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // меня не забудь подключить, мы ж уровень перезагружать будем


public class Doodle : MonoBehaviour
{
    public static Doodle instance;                          // это штучка нужна, чтобы мы могли использовать переменные в этом скрипте в других скриптах

    float horizontal;                                       // переменная для акселерометра
    public Rigidbody2D DoodleRigid;                         // публичный RB для дудлика

    public string deathSoundName = "Die";
    public string damageSoundName = "Hurt";

    private AudioManager audioManager;

    void Start()
    {
        if (instance == null)                               // пишем эти строчки, чтоб можно было корректно использовать переменные в других скриптах
        {
            instance = this;
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC! No audiomanager in scene.");
        }
    }


    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)    // если платформа Андроид
        {
            horizontal = Input.acceleration.x;                  // то подключаем акселерометр по оси х
        }

       // if (Input.acceleration.x < 0)                           // если наклон акселерометра меньше нуля
       // {
       //     gameObject.GetComponent<SpriteRenderer>().flipX = false;    // то объект поворачивается налево
       // }
       //
       // if (Input.acceleration.x > 0)                           // если наклон акселерометра больше нуля
        //{
        //    gameObject.GetComponent<SpriteRenderer>().flipX = true;     // то объект поворачивется направо
        //}

        DoodleRigid.velocity = new Vector2(Input.acceleration.x * 10f, DoodleRigid.velocity.y);     //  добавляем силу к акселерометру, чтоб он не просто разворачивался в разные стороны
    }

    public void OnCollisionEnter2D(Collision2D collision)       // столкновение объекта
    {
        if (collision.collider.name == "DeadZone")              // если дудлик сталкивается с объектом с именем "DeadZone"
        {
            SceneManager.LoadScene(0);                          // то уровень перезагружается
        }
    }
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float startingHealth;
        public float currentHealth { get; private set; }
        private Animator anim;
        private bool dead;

        [Header("iFrames")]
        [SerializeField] private float iFramesDuration;
        [SerializeField] private int numberOfFlashes;
        private SpriteRenderer spriteRend;



        private void Awake()
        {
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

        public bool Dead
        {
            get => dead;
        }
    }

    // Подписывайся на канал ICE CREAM
    // Нашел неточность - напиши мне на почту или в комменты! Пасибос)
}
