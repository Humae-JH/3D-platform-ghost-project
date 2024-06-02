using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilitySpace
{
    public class Utils : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject FindChildObject(GameObject Object , string name)
        {
            GameObject result = null;
            for (int i = 0; i < Object.transform.childCount; i++)
            {
                if (Object.transform.GetChild(i).gameObject.name == name)
                {
                    result = Object.transform.GetChild(i).gameObject;
                }

                //Do something with child
            }

            return result;
        }
    }

}
