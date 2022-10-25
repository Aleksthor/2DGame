using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives;
    }
}
