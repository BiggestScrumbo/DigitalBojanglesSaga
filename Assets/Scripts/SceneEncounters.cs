using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEncounters : MonoBehaviour
{
    public EncounterInfo encounterInfo;
    public bool EnableEncounter = true;
    public int FirstThreshold = 10; 
    public int SecondThreshold = 20; 

    private bool isFirstRoll = false;
    public PlayerMovement controller;
    public Camera currentCamera;

    public void CheckEncounterThresholds()
    {
        if (EnableEncounter == true)
        {

            if (!isFirstRoll)
            {
                int firstRngRoll = Random.Range(1, FirstThreshold + 1);
                Debug.Log($"First Threshold RNG Roll: {firstRngRoll}");

                if (firstRngRoll == 1)
                {
                    isFirstRoll = true;
                }
            }
            else
            {
                int secondRngRoll = Random.Range(1, SecondThreshold + 1);
                Debug.Log($"Second Threshold RNG Roll: {secondRngRoll}");

                if (secondRngRoll == 1)
                {
                    Debug.Log("Encounter Triggered!");
                    InvokeRandomEncounter(0, 2);
                    isFirstRoll = false;
                }
            }
        }
        else
        {
            Debug.Log("Encounters are currently disabled");
        }
    }


    public void InvokeSetEncounter(int index)
    {
        if (encounterInfo != null && index >= 0 && index < encounterInfo.encounters.Count)
        {
            List<GameObject> selectedEncounter = encounterInfo.encounters[index].enemyPrefabs;
            PrintEncounter(selectedEncounter);
            LoadEnemyData(selectedEncounter);
            EncounterTransition(index);
            Debug.Log("fire!");
        }
        else
        {
            Debug.LogError("Invalid index or EncounterInfo is not assigned.");
        }
    }

    public void InvokeRandomEncounter(int minIndex, int maxIndex) //MIN INDEX MUST BE GREATER THAN INDEXES THAT CONTAIN SET ENCOUNTER INFORMATION!!!!!!!!
    {
        if (encounterInfo != null && minIndex >= 0 && maxIndex < encounterInfo.encounters.Count && minIndex <= maxIndex)
        {
            int randomIndex = Random.Range(minIndex, maxIndex + 1);
            List<GameObject> selectedEncounter = encounterInfo.encounters[randomIndex].enemyPrefabs;
            PrintEncounter(selectedEncounter);
            LoadEnemyData(selectedEncounter);
            EncounterTransition(randomIndex);
        }
        else
        {
            Debug.LogError("Invalid range or EncounterInfo is not assigned.");
        }
    }


    // Debug
    private void PrintEncounter(List<GameObject> encounterPrefabs)
    {
        if (encounterInfo != null)
        {
            foreach (GameObject enemyPrefab in encounterPrefabs)
            {
                Debug.Log("Enemy Prefab: " + enemyPrefab.name);
            }
        }
        else
        {
            Debug.Log("No object attached!");
        }
    }

    public Canvas battleCanvas;

    public void LoadEnemyData(List<GameObject> selectedEncounterPrefabs)
    {
        if (selectedEncounterPrefabs.Count < 3)
        {
            Debug.LogError("too many prefabs in list");
            return;
        }

        for (int i = 0; i < selectedEncounterPrefabs.Count; i++)
        {
            GameObject enemyObject = Instantiate(selectedEncounterPrefabs[i], Vector3.zero, Quaternion.identity);
            enemyObject.transform.SetParent(battleCanvas.transform, false);
            EnemyUnit enemyUnit = enemyObject.GetComponent<EnemyUnit>();
            RectTransform rectTransform = enemyObject.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                //moving x pos of enemies based on their index
                float xOffset = 0.0f;

                if (i == 0)
                {
                    //First prefab (element 0), set X pos to -500
                    xOffset = -500.0f;
                }
                else if (i == selectedEncounterPrefabs.Count - 1)
                {
                    //Last prefab (element 2), set X pos to 500
                    xOffset = 500.0f;
                }

                //
                rectTransform.anchoredPosition = new Vector2(xOffset, rectTransform.anchoredPosition.y);

                if (enemyUnit != null)
                {
                    rectTransform.localScale = new Vector3(enemyUnit.scaleX, enemyUnit.scaleY, 1.0f);
                }
                else
                {
                    Debug.LogWarning("enemyunit script not found on prefab");
                }
            }

            //add other thing
        }
    }







    private Camera newCamera;

    public void EncounterTransition(int encounterIndex)
    {
        if (encounterInfo != null && encounterInfo.encounters.Count > 0 && encounterIndex >= 0 && encounterIndex < encounterInfo.encounters.Count)
        {
            EnableEncounter = false;
            controller.MovementEnabled = false;

            string newCameraName = encounterInfo.encounters[encounterIndex].sceneCameraName; //read encounter info, find name of camera

            GameObject battleObject = GameObject.Find("Battle"); //find battle object // maybe make this one a variable bro

            if (battleObject != null && !string.IsNullOrEmpty(newCameraName))
            {
                newCamera = battleObject.transform.Find(newCameraName)?.GetComponent<Camera>(); //find camera in children of battle object

                if (newCamera != null)
                {
                    //component
                    newCamera.enabled = true;
                }
            }
        }
    }




    public void OverworldTransition()
    {
        EnableEncounter = true;

        //accessing playermovement script
        if (controller != null)
        {
            controller.MovementEnabled = true;
        }

        if (newCamera != null)
        {
            newCamera.enabled = false; //disable new camera COMPONENT!!!
        }

        DestroyEnemyUnitObjects();
    }

    private void DestroyEnemyUnitObjects()
    {
        if (battleCanvas != null)
        {
            foreach (Transform child in battleCanvas.transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("assign canvas dumbass");
        }
    }



}
