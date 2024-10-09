using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using TMPro;
using UnityEngine;

public class ReconnaissanceVocal : MonoBehaviour
{
   
    public OpenAIController openaiController; 
    public TMP_Text textField; // Zone de texte pour afficher le message reconnu
    
    private const string Language = "fr-CA"; // Langue de reconnaissance vocale

    public string message; // Message reconnu
    private SpeechConfig config; // Configuration pour le service de reconnaissance vocale
    private AudioConfig audioInput; // Configuration audio pour le service de reconnaissance
    private SpeechRecognizer recognizer; // Reconnaissance vocale
    private PushAudioInputStream pushStream; // Flux audio

    [SerializeField] private AudioSource audioSource; // Source audio pour le microphone

    public object threadLocker = new object(); // Verrou pour sécuriser les opérations multithreads
    public bool recognitionStarted = false; // Si la reconnaissance a commencé
    private int lastSample = 0; // Dernier échantillon audio
    public string lastNonEmptyMessage; // Dernier message non vide reconnu
    public bool perimetre = false; // Indique si le joueur est dans le périmètre du NPC

    // Initialisation des paramètres à l'aide de l'API
    void Start()
    {
        // Clé et région
        config = SpeechConfig.FromSubscription("", "");
        config.SpeechRecognitionLanguage = Language;
        
        pushStream = AudioInputStream.CreatePushStream();
        audioInput = AudioConfig.FromStreamInput(pushStream);

        recognizer = new SpeechRecognizer(config, audioInput);
        recognizer.Recognized += RecognizedHandler;
        recognizer.Recognizing += RecognizingHandler;
        
        // Afficher les microphones disponibles
        foreach (var device in Microphone.devices)
        {
            Debug.Log("DeviceName: " + device);
        }

        StartRecognition();
    }

    // Gestionnaire d'événements lorsqu'un texte est reconnu avec certitude
    private void RecognizedHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            // Si OpenAI répond ou que le joueur est en dehors du périmètre, attendre
            if (openaiController.gettingResponse || !perimetre)
            {
                Debug.Log("Attend avant de parler");
                return;
            }

            // Assigner le texte reconnu au message
            message = e.Result.Text;

            // Si le message n'est pas vide, l'envoyer à OpenAI pour une réponse
            if (!string.IsNullOrEmpty(message))
            {
                lastNonEmptyMessage = message;
                Debug.Log("RecognizedHandler: " + message);
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    openaiController.GetResponse(lastNonEmptyMessage);
                });
            }
        }
    }
    
    // Gestionnaire d'événements lorsqu'un texte est partiellement reconnu
    private void RecognizingHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            if (openaiController.gettingResponse || !perimetre)
            {
                return;
            }

            // Assigner le texte partiellement reconnu au message
            message = e.Result.Text;

            if (!string.IsNullOrEmpty(message))
            {
                lastNonEmptyMessage = message;
                Debug.Log("RecognizingHandler: " + message);
            }
        }
    }

    // Démarrer la reconnaissance vocale
    public async Task StartRecognition()
    {
        if (!Microphone.IsRecording(Microphone.devices[0]))
        {
            Debug.Log("Microphone.Start: " + Microphone.devices[0]);
            audioSource.clip = Microphone.Start(Microphone.devices[0], true, 200, 16000);
            Debug.Log("audioSource.clip channels: " + audioSource.clip.channels);
            Debug.Log("audioSource.clip frequency: " + audioSource.clip.frequency);
        }

        await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(true);

        lock (threadLocker)
        {
            recognitionStarted = true;
            Debug.Log("RecognitionStarted: " + recognitionStarted.ToString());
        }
    }

    void FixedUpdate()
    {
        lock (threadLocker)
        {
            if (!openaiController.gettingResponse)
            {
                textField.text = lastNonEmptyMessage;
            }
        }
        
        // Gestion du flux audio pour la reconnaissance vocale
        if (Microphone.IsRecording(Microphone.devices[0]) && recognitionStarted)
        {
            int pos = Microphone.GetPosition(Microphone.devices[0]);
            int diff = pos - lastSample;

            if (diff > 0)
            {
                if (!audioSource.isPlaying && audioSource.clip.loadState == AudioDataLoadState.Loaded)
                {
                    float[] samples = new float[diff * audioSource.clip.channels];
                    audioSource.clip.GetData(samples, lastSample);
                    byte[] ba = ConvertAudioClipDataToInt16ByteArray(samples);
                    if (ba.Length != 0)
                    {
                        pushStream.Write(ba);
                    }
                }
            }
            lastSample = pos;
        }
    }

    // Convertir les données audio pour la reconnaissance vocale
    private byte[] ConvertAudioClipDataToInt16ByteArray(float[] data)
    {
        MemoryStream dataStream = new MemoryStream();
        int x = sizeof(Int16);
        Int16 maxValue = Int16.MaxValue;
        int i = 0;
        while (i < data.Length)
        {
            dataStream.Write(BitConverter.GetBytes(Convert.ToInt16(data[i] * maxValue)), 0, x);
            ++i;
        }

        byte[] bytes = dataStream.ToArray();
        dataStream.Dispose();
        return bytes;
    }

    // Désactiver la reconnaissance vocale et ses gestionnaires
    public void Disable()
    {
        recognizer.Recognized -= RecognizedHandler;
        recognizer.Recognizing -= RecognizingHandler;
        pushStream.Close(); // Fermer le flux audio
        recognizer.Dispose(); // Libérer les ressources du reconnaissant
        StopMicrophone(); 
    }

    // Arrêter le microphone
    private void StopMicrophone()
    {
        if (Microphone.IsRecording(Microphone.devices[0]))
        {
            Microphone.End(null); 
        }
    }
}
