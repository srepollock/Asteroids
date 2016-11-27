using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public MenuManager menuManager;
	
	PlayerControls playerControls;
	bool isDead = false;

	void Awake() {
		playerControls = GetComponent<PlayerControls>();
		currentHealth = startingHealth;
		healthSlider.maxValue = currentHealth;
		healthSlider.value = currentHealth;
	}

	public void TakeDamage (int damage) {
		currentHealth -= damage;
		healthSlider.value = currentHealth;
		PlayerPrefs.SetInt("playerhealth", currentHealth);
		if (currentHealth <= 0 && !isDead) {
			Death();
		}
	}

	void Death() {
		isDead = true;
		menuManager.goToScene("end_screen");
	}
}
