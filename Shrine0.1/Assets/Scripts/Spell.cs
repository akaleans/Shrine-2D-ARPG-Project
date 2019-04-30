using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Spell
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject spellPrefab;

    public string MyName { get => name; set => name = value; }
    public int MyDamage { get => damage; set => damage = value; }
    public Sprite MyIcon { get => icon; set => icon = value; }
    public float MySpeed { get => speed; set => speed = value; }
    public GameObject MySpellPrefab { get => spellPrefab; set => spellPrefab = value; }
}