using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Azure;
using Azure.AI.OpenAI;
using UnityEngine.Events;

public class OpenAIController : MonoBehaviour
{
    public TMP_Text textField; // Champ de texte où afficher la réponse
    public UnityEvent OnReplyReceived; // Événement déclenché lorsque la réponse est reçue

    private string responseMessage; // Message de réponse de l'assistant
    private OpenAIClient client; // Client pour interagir avec OpenAI
    
    public ReconnaissanceVocal reconnaissanceVocal;
    public SyntheseVocal syntheseVocal;
    public AzureSearch azureSearch;

    public bool gettingResponse = false; // Indicateur pour savoir si une réponse est en cours d'obtention

    // Initialisation des paramètres à l'aide de l'API
    void Start()
    {
        // Uri et clé
        client = new(new Uri(""), new AzureKeyCredential(""));
    }

    // Dire le message de bienvenue
    public async void DebutConversation()
    {
        textField.text  = "Bonjour, comment puis-je vous aider?";
        await syntheseVocal.UseSynthesizer("Bonjour, comment puis-je vous aider?");
    }

    // Terminer la conversation
    public void TerminerConversation()
    {
        textField.text = "";
        reconnaissanceVocal.lastNonEmptyMessage = "";
        syntheseVocal.Stop();
    }

    // Fonction pour obtenir une réponse de l'assistant
    public async Task GetResponse(string message)
    {
        try
        {
            gettingResponse = true;

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            azureSearch.RunQueries(message);// Exécute une recherche avec le message

            // Configuration des options pour la conversation
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "Tu est un assistant pour le ministère de la Cybersérurité et du Numérique(MCN) au Québec." +
                                                     "Répondez UNIQUEMENT à l'aide des faits énumérés dans la liste des sources ci-dessous. " +
                                                     "S'il n'y a pas assez d'informations, dites que vous ne savez pas. Ne générez pas de réponses " +
                                                     "qui n'utilisent pas les sources ci-dessous. Résume toujours les sources, ne répond avec la source mot par mot" +
                                                     "Répond avec une maximum de 100 mots. Sources:" +
                                                     azureSearch.GetSource()),
                    new ChatMessage(ChatRole.User, message)
                },
                MaxTokens = 200,
                Temperature = (float)0.25
            };
            
            // Demande une complétion de chat à OpenAI
            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(
                deploymentOrModelName: "gpt35turbo",
                chatCompletionsOptions);

            // Extraction et affichage du message
            responseMessage = response.Value.Choices[0].Message.Content;
            Debug.Log(responseMessage);
            textField.text = responseMessage;

            // Déclenche l'événement lorsqu'une réponse est reçue
            OnReplyReceived.Invoke();
            
            // Conversion du message en parole
            await syntheseVocal.UseSynthesizer(responseMessage); 

            gettingResponse = false;
        }
        catch (RequestFailedException e)
        {
            Debug.LogError($"Request failed: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Unexpected error: {e.Message}");
        }
    }
    
    // Désactiver les services
    void Disable()
    {
        reconnaissanceVocal.Disable();
        syntheseVocal.Disable();
    }
}