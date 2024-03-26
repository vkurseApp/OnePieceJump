using UnityEngine;
using TMPro;

public class Power : MonoBehaviour
{
    public GameObject attackPrefab; // Префаб атаки босса
    public Sprite[] powerSprites;
    public TextMeshProUGUI scoreText;
    public Transform target; // Цель атаки (обычно персонаж игрока)
    public float attackInterval = 2f; // Интервал между атаками
    public float attackSpeed = 5f; // Скорость атаки
    private float attackTimer; // Таймер для отслеживания интервала между атаками
    private Vector3 initialPosition; // Изначальная позиция босса
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        attackTimer = attackInterval;
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        int totalScore;
        int index = 0;
        if (int.TryParse(scoreText.text.Replace("Score: ", ""), out totalScore))
        {
            if (totalScore >= 50000)
                index = 1;

            if (totalScore >= 100000)
                index = 2;

            if (totalScore >= 150000)
                index = 3;
        }

        if (index != 0)
            spriteRenderer.sprite = powerSprites[index];
        attackTimer -= Time.deltaTime;

        if (target)
        {
            if (attackTimer <= 0)
            {
                SpawnAttack();
                attackTimer = attackInterval;
            }
        }
    }

    void SpawnAttack()
    {
        GameObject newAttack = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        Vector3 direction = (new Vector3(target.position.x * 1.5f, target.position.y, 10) - transform.position).normalized;
        newAttack.GetComponent<Rigidbody2D>().velocity = direction * attackSpeed;
    }

    public void ResetBossPosition()
    {
        transform.position = initialPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sea"))
        {
            Destroy(gameObject);
        }
    }
}