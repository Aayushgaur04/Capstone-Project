using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public int maxHealth = 1000;
    [SerializeField]private int currentHealth;

    public Animator animator;
    public Transform player;

    [Header("Laser Attack")]
    public GameObject laserWarningPrefab;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public float laserWarningDuration = 1.5f;

    [Header("Tackle Attack")]
    public float tackleSpeed = 15f;
    public int tackleDamage = 40;
    private bool isDashing = false;
    private Vector2 tackleDirection;
    private enum State { Intro, Idle, Laser, InvulnerableSwords, Tackle, Dead }
    private State state = State.Intro;

    private bool isAttacking = false;

    [Header("Sword Rain Attack")]
    public GameObject swordPrefab;
    public Transform swordLeftBound;
    public Transform swordRightBound;
    public float swordSpawnInterval = 0.3f;
    public float swordRainDuration = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        BossUIManager.Instance?.SetBossName("The Fallen Warden"); // or dynamic
        BossUIManager.Instance?.ShowUI(true);
        BossUIManager.Instance?.SetHealth(currentHealth, maxHealth);

        PlayIntro();
    }

    void Update()
    {
        if (state == State.Idle)
        {
            FacePlayer();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1000); // For testing death
        }
    }

    void PlayIntro()
    {
        animator.Play("BossIntro");
    }

    public void OnIntroComplete()
    {
        state = State.Idle;
        animator.Play("Idle");
        animator.SetTrigger("IntroComplete");
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (currentHealth > 0)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                float rand = UnityEngine.Random.value;

                if (rand <= 0.4f)
                {
                    StartCoroutine(DoLaserAttack());
                }
                else if (rand <= 0.8f)
                {
                    StartCoroutine(DoTackleAttack());
                }
                else
                {
                    StartCoroutine(DoSwordAttack());
                }

                while (isAttacking)
                    yield return null;

                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator DoLaserAttack()
    {
        state = State.Laser;
        animator.SetTrigger("Laser");

        // Show warning
        ShowLaserWarning();

        yield return new WaitForSeconds(laserWarningDuration);

        // Fire the laser
        GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        state = State.Idle;
    }

    void ShowLaserWarning()
    {
        GameObject warning = Instantiate(laserWarningPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        Destroy(warning, laserWarningDuration);
    }

    IEnumerator DoTackleAttack()
    {
        state = State.Tackle;
        animator.SetTrigger("Tackle");

        yield return new WaitForSeconds(0.3f); // wind_up

        isDashing = true;
        tackleDirection = (transform.eulerAngles.y == 0) ? Vector2.right : Vector2.left;

        while (isDashing)
        {
            transform.Translate(tackleDirection * tackleSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        // dash ended = tell Animator to go back to Idle
        animator.SetTrigger("TackleComplete");

        isAttacking = false;
        state = State.Idle;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing)
        {
            if (collision.CompareTag("Player"))
            {
                Health health = collision.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(tackleDamage);
                }

                StopTackle();
            }
            else if (collision.CompareTag("Wall"))
            {
                StopTackle();
            }
        }

        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(collision.gameObject);
            }
        }
    }

    void StopTackle()
    {
        isDashing = false;
    }

    IEnumerator DoSwordAttack()
    {
        state = State.InvulnerableSwords;

        // Move to center between swordLeftBound and swordRightBound
        Vector3 targetPos = new Vector3(
            (swordLeftBound.position.x + swordRightBound.position.x) / 2f,
            transform.position.y,
            transform.position.z
        );

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        }

        // Start invincibility animation
        animator.SetBool("Invincible", true);

        // Start spawning swords
        float elapsed = 0f;
        while (elapsed < swordRainDuration)
        {
            float randomX = Random.Range(swordLeftBound.position.x, swordRightBound.position.x);
            Vector3 spawnPos = new Vector3(randomX, swordLeftBound.position.y + 10f, 0f); // +10 so it falls from above
            Instantiate(swordPrefab, spawnPos, Quaternion.identity);

            elapsed += swordSpawnInterval;
            yield return new WaitForSeconds(swordSpawnInterval);
        }

        animator.SetBool("Invincible", false);
        isAttacking = false;
        state = State.Idle;
    }

    void FacePlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        float yRot = toPlayer.x >= 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }

    public void TakeDamage(int dmg)
    {
        if (state == State.InvulnerableSwords) return;
        currentHealth -= dmg;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        BossUIManager.Instance?.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0) StartDeath();
    }

    void StartDeath()
    {
        state = State.Dead;
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        Vector3 targetPos = transform.position + new Vector3(0, 1.41f, 0);

        // Smoothly move up
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        }

        animator.Play("Death");
        BossUIManager.Instance?.ShowUI(false);
    }

    public void OnDeathComplete()
    {
        Destroy(gameObject);
        // Optionally trigger win state or cutscene
        FindObjectOfType<GameCompletionManager>()?.ShowCompletionScreen();
    }
}
