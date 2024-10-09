using UnityEngine;

public class RVbouton : MonoBehaviour
{
    public OpenAIController openaiController;
    
    // Bouton pour quitter la conversation
    public void QuitterConversation()
    {
        Debug.Log("RVBouton: quitter conversation");
        openaiController.TerminerConversation();
    }
}