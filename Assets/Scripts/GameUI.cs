using System;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private DiceDroper diceDroper;

    private int firstDiceValue = 1;
    private int secondDiceValue = 1;

    public void GetFirstValue(string firstDice)
    {
        firstDiceValue = Int32.Parse(firstDice);

        if (firstDiceValue > 6 || firstDiceValue < 1)
            firstDiceValue = 1;
    }

    public void GetSecondValue(string secondDice)
    {
        secondDiceValue = Int32.Parse(secondDice);

        if (secondDiceValue > 6 || secondDiceValue < 1)
            secondDiceValue = 1;
    }

    public void DropDices() => diceDroper.DropDice(firstDiceValue,secondDiceValue);
    
}
