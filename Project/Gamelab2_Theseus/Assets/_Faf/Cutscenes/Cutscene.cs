using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Cutscene : MonoBehaviour
{
    public KeyCode startKey;
    public KeyCode startKeyArmorPiecesCutscenes;

    [Header("Starting Locations")]
    public Transform[] PointA;

    [Header("End Locations")]
    public Transform[] PointB;

    int currentCutscene;
    int currentArmorPieceCutscene;

    [Header("ARMOR PIECES Start Locations")]
    public Transform[] ArmorPointA;

    [Header("ARMOR PIECES End Locations")]
    public Transform[] ArmorPointB;


    [Header("Fill these")]
    public Transform cutsceneCam;

    public CanvasGroup Canvas;
    CanvasGroup CutsceneCanvas;

    Vector3 camPos;

    public float moveSpeed;

    
    void Awake()
    {
        CutsceneCanvas = GetComponent<CanvasGroup>();
    }



	void Update()
    {
        if(Input.GetKeyDown(startKey))
        {
            StartCoroutine(StartCutscene());
        }
        if (Input.GetKeyDown(startKeyArmorPiecesCutscenes))
        {

            StartCoroutine(StartArmorPiecesCutscene());
        }

        if (Input.GetButtonDown("0"))
        {
            moveSpeed = 5f;
        }

    }


    IEnumerator StartArmorPiecesCutscene()
    {
        cutsceneCam.gameObject.SetActive(true);
        camPos = cutsceneCam.transform.position;
        Canvas.alpha = 0;
        CutsceneCanvas.alpha = 1;

        cutsceneCam.position = ArmorPointA[currentArmorPieceCutscene].position;
        while (Vector3.Distance(cutsceneCam.position, ArmorPointB[currentArmorPieceCutscene].position) > 0.1)
        {
            cutsceneCam.position = Vector3.MoveTowards(cutsceneCam.position, ArmorPointB[currentArmorPieceCutscene].position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        if(currentArmorPieceCutscene < 1)
        {
            currentArmorPieceCutscene++;
            StartCoroutine(StartArmorPiecesCutscene());
        }
        Canvas.alpha = 1;
        CutsceneCanvas.alpha = 0;
        cutsceneCam.transform.position = camPos;
        cutsceneCam.gameObject.SetActive(false);
        yield break;
    }

    IEnumerator StartCutscene()
    {
        cutsceneCam.gameObject.SetActive(true);
        camPos = cutsceneCam.transform.position;
        Canvas.alpha = 0;
        CutsceneCanvas.alpha = 1;

            cutsceneCam.position = PointA[currentCutscene].position;
            while (Vector3.Distance(cutsceneCam.position, PointB[currentCutscene].position) > 0.1)
            {
                cutsceneCam.position = Vector3.MoveTowards(cutsceneCam.position, PointB[currentCutscene].position, moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

        if (currentCutscene < PointA.Length -1)
        {
            currentCutscene++;
        }



        
        Canvas.alpha = 1;
        CutsceneCanvas.alpha = 0;
        cutsceneCam.transform.position = camPos;
        cutsceneCam.gameObject.SetActive(false);
        yield break;
    }
}
