using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Character/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float moveSpeed;
    public float damage;
    public float health;

    public AnimatorController animController;
    public Sprite sprite;

}
