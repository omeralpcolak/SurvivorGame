
public class HealthSystem
{
    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }
}
