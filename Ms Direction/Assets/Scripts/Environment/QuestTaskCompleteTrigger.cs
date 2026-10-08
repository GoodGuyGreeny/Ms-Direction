using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuestInteractor))]
public class QuestTaskCompleteTrigger : MonoBehaviour
{
    QuestInteractor questInteractor;
    private void Start()
    {
        questInteractor = this.GetComponent<QuestInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        questInteractor.AttemptToCompleteTask();
    }
}
