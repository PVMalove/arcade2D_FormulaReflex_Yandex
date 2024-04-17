﻿using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.StaticData.Infrastructure
{
    [CreateAssetMenu(fileName = nameof(ConfigLoader), menuName = "Configs/Infrastructure/ConfigLoaderStorage")]
    internal class ConfigLoader: ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> loadedScriptable;
        
        public List<ScriptableObject> LoadedScriptable => loadedScriptable;
    }
}