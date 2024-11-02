using UnityEngine;

public class Health 
{
    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        Value = _maxHealth;
    }

    public int _maxHealth { get; private set; }
    public int Value { get; private set; }

    public virtual void Reduce(int value)
    {
        if (value < 0)
        {
            Debug.LogError(" Внимание! Значение здоровья < 0");
            return;
        }

        Value -= value;

        if (Value < 0)
        {
            Value = 0;
            //Debug.Log(" Игрок умер");
        }
        Debug.Log($" Уровень здоровья игрока = {Value}");
    }

}
