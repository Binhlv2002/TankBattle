using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    private bool isDead;
    [SerializeField] TankSideType tankSide;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor;
    [SerializeField] private Color lowHealthColor;
    [SerializeField] private GameObject explosionPrefab;

    private ParticleSystem explosionParticles;

    private void Awake()
    {
        GameObject explosionObject = Instantiate(explosionPrefab, transform);
        explosionParticles = explosionObject.GetComponent<ParticleSystem>();
        explosionObject.transform.localPosition = Vector3.zero;
        explosionObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isDead = false;

        if (tankSide == TankSideType.Player)
        {
            fullHealthColor = Color.green;
        }
        else if (tankSide == TankSideType.Enemy)
        {
            fullHealthColor = Color.gray;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
        fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthSlider.value);
    }

    private void OnDeath()
    {
        isDead = true;

        


        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();

        StartCoroutine(DisableTankAfterExplosion());
    }

    private IEnumerator DisableTankAfterExplosion()
    {
        yield return new WaitForSeconds(2f);
        if (tankSide == TankSideType.Enemy)
        {
            GameManager.instance.UpdateGameState(GameState.Victory);
        }
        else if (tankSide == TankSideType.Player)
        {
            GameManager.instance.UpdateGameState(GameState.Defeat);
        }

        gameObject.SetActive(false);
    }
}
