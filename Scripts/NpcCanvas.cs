using UnityEngine;

public class NpcCanvas : MonoBehaviour
{
    [SerializeField] private GameObject dialogUI; 
    public ReconnaissanceVocal reconnaissanceVocal; 
    public OpenAIController openaiController; 

    // Fonction déclenchée lorsque le joueur entre dans le périmètre 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openaiController.DebutConversation(); 
            reconnaissanceVocal.perimetre = true; // Indique que le joueur est dans le périmètre
            
            // Afficher l'interface utilisateur de dialogue VR si elle n'est pas nulle
            if (dialogUI != null)
            {
                dialogUI.SetActive(true);
            }
        }
    }

    // Fonction déclenchée lorsque le joueur sort du périmètre 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openaiController.TerminerConversation(); 
            reconnaissanceVocal.perimetre = false; // Indique que le joueur n'est plus dans le périmètre

            // Réinitialise la reconnaissance vocale et les messages
            lock (reconnaissanceVocal.threadLocker)
            {
                reconnaissanceVocal.message = "";
                reconnaissanceVocal.lastNonEmptyMessage = "";
            }
            
            // Masquer l'interface utilisateur de dialogue VR si elle n'est pas nulle
            if (dialogUI != null)
            {
                dialogUI.SetActive(false);
            }
        }
    }
}
