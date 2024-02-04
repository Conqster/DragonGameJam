using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldPileBehaviour : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer goldPileSkinMesh;



    private void Awake()
    {
        //DragonGameManager managerInstance = DragonGameManager.instance;
        //managerInstance.OnGoldTrigger += UpdateGoldPileVisuals;
    }

    private void Start()
    {
        goldPileSkinMesh = GetComponent<SkinnedMeshRenderer>();
        DragonGameManager managerInstance = DragonGameManager.instance;
        managerInstance.OnGoldTrigger += UpdateGoldPileVisuals;
    }



    private void UpdateGoldPileVisuals(object sender, EventArgs e)
    {
        DragonGameManager managerInstance = DragonGameManager.instance;

        if(goldPileSkinMesh != null)
        {
            float ratio = ((float)managerInstance.CurrentGoldLeft / (float)managerInstance.MaxGold) * 100.0f; ;
            print("My ratio: " + managerInstance.CurrentGoldLeft + " / "+ managerInstance.MaxGold + ":" +  ratio);
            goldPileSkinMesh.SetBlendShapeWeight(0, (100.0f - ratio));
        }
    }


    private void OnDestroy()
    {
        DragonGameManager managerInstance = DragonGameManager.instance;
        managerInstance.OnGoldTrigger -= UpdateGoldPileVisuals;
    }



}
