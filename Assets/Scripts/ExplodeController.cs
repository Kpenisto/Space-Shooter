/*
Author: Kyle Peniston
Date: 10/29/2024
Description: This script is used to destroy the explosion animations after 1 second to conserve memory.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy explosion animation after 1 second
        Destroy(gameObject, 1f);
    }
}
