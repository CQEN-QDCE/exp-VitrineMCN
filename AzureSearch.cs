using System;
using System.Linq;
using System.Text;
using Azure;
using UnityEngine;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

public class AzureSearch : MonoBehaviour
{
   
    private const string ContentFieldName = "content"; // Nom du champ à chercher dans le résultat de la recherche
    private SearchClient srchclient; // Le client utilisé pour interagir avec le service de recherche Azure
    private string source; // Variable pour stocker le résultat de la recherche

    // Initialisation des paramètres à l'aide de l'API
    void Start()
    {
        // URI
        Uri serviceEndpoint = new Uri($"https://{""}.search.windows.net/");
        
        // Clé
        AzureKeyCredential credential = new AzureKeyCredential("");

        // Initialise le client de recherche Azure avec les détails de l'endpoint et la clé d'authentification.
        srchclient = new SearchClient(serviceEndpoint, "", credential);
    }

    // Exécute une recherche avec Azure Search et la question fourni.
    public void RunQueries(string question)
    {
        string searchTerm = question;

        SearchOptions options = new SearchOptions() {
            Filter = "",
            OrderBy = { "" }
        };

        options.Select.Add(ContentFieldName);

        // Exécute la recherche et stocke la réponse.
        SearchResults<SearchDocument> response = srchclient.Search<SearchDocument>(searchTerm, options);

        source = Chercher(response);
    }

    // Traite la réponse de recherche et renvoie les trois premiers résultats.
    private string Chercher(SearchResults<SearchDocument> response)
    {
        // Prend les trois premiers résultats de la réponse.
        var firstThreeResults = response.GetResults().Take(3);

        StringBuilder sourceBuilder = new StringBuilder();

        foreach (var result in firstThreeResults)
        {
            // Si le document contient le champ "content", ajoute-le au StringBuilder.
            if (result.Document.TryGetValue(ContentFieldName, out var content))
            {
                sourceBuilder.Append(content);
            }
            else
            {
                Debug.Log("Document does not contain 'content' field");
            }
        }

        if (sourceBuilder.Length == 0)
        {
            Debug.Log("No results found.");
        }

        return sourceBuilder.ToString();
    }

    public string GetSource()
    {
        return source;
    }
}
