using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ElementType
{
    None,
    Earth,
    Water,
    Forest,
    Swamp
}

[System.Serializable]
public class Card
{
    public int id;
    public string cardName;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;
    public int drawXcards;
    public int addXmaxMana;

    public Sprite thisImage;
    public int returnXcards;
    public int healXpower;

    public bool spell;
    public int damageDealBySpell;

    public string rarity;
    public ElementType elementType;

    public Card(int Id, string CardName, int Cost, int Attack, int Defense, string CardDescription,
                Sprite ThisImage, int DrawXcards, int AddXmaxMana, int ReturnXcards, int HealXpower,
                bool Spell, int DamageDealBySpell, string Rarity, ElementType Type)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        attack = Attack;
        defense = Defense;
        cardDescription = CardDescription;
        thisImage = ThisImage;
        drawXcards = DrawXcards;
        addXmaxMana = AddXmaxMana;
        returnXcards = ReturnXcards;
        healXpower = HealXpower;

        spell = Spell;
        damageDealBySpell = DamageDealBySpell;

        rarity = Rarity;
        elementType = Type;
    }
}
