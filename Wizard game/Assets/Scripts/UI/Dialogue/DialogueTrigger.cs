using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Overlap Circle Settings")]
    [SerializeField] private float overlapSize;
    [SerializeField] private LayerMask overlapLayerMask;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    //for talk with NPC
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapSize, overlapLayerMask);
        playerInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                playerInRange = true;
                Debug.Log("called.");
                break;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, overlapSize);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //for talk with object
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //for talk with object
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
