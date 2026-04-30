using DialogueEditor;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    private void OnMouseOver()
    { 
            if (Input.GetMouseButtonDown(0))
            {
                ConversationManager.Instance.StartConversation(myConversation);
                Debug.Log("Pressing 0");
            }
        
      
    }
}
