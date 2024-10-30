using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

interface LifeCount
{
    //4 lives - 4 imgaes (0,1,2,3)
    //3 lives - 3 images (0,1,2,[3])
    //2 lives - 2 images (0,1,[2],[3])
    //1 life - 1 image (0,[1],[2],[3])
    //0 lives - 0 images ([0,1,2,3]) LOSE

    public void LoseLife();
}
