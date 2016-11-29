using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {
	public int startingHealth = 200;
	public int currentHealth;
	public Slider healthSlider;
    // 0 aggr, 1 bal, 2 def
    public int agroState;
	
	PlayerControls playerControls;
	bool isDead = false;

	void Awake() {
		currentHealth = startingHealth;
		healthSlider.maxValue = currentHealth;
		healthSlider.value = currentHealth;
	}

	public void TakeDamage (int damage) {
		currentHealth -= damage;
		healthSlider.value = currentHealth;
		if (currentHealth <= 0 && !isDead) {
            isDead = true;
			Death();
		}
	}

    void determineAggroState() {
		if (currentHealth < (startingHealth / 4)) {
			agroState = 2;
		} else if (currentHealth < (startingHealth / 2)) {
			agroState = 1;
		} else {
			agroState = 0;
		}
	}

    public int GetAggressiveState() {
        return agroState;
    }

    void Death() {
        // TODO: Set text to return to station
        Destroy(this.gameObject);
    }
}
