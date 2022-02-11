using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessWall : MonoBehaviour
{
    public LevelController levelController;

    public void FadeToHallucination()
    {
        levelController.FadeInToLevel(2);
    }

}
