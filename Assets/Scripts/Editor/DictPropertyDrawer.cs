using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyMappingDictionary))]
public class EnemyMappingSerializableDictionaryPropertyDrawer :
    SerializableDictionaryPropertyDrawer {}

