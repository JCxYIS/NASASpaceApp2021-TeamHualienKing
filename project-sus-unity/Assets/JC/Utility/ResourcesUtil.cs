using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JC.Utility
{
    public static class ResourcesUtil
    {
        /// <summary>
        /// 從 Resources Instantiate 一個 GameObject
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static GameObject InstantiateFromResources(string key)
        {
            return MonoBehaviour.Instantiate(Resources.Load<GameObject>(key));
        }
    }
}