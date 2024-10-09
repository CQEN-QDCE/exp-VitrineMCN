using System;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;

public class SyntheseVocal : MonoBehaviour
{
   
    private const string VoiceName = "fr-CA-JeanNeural"; // Voix utilisé par le synthétiseur

    [SerializeField]
    private AudioSource audioSourceIA; // La source audio pour la lecture vocale
    private SpeechConfig configIA;     // Configuration pour la synthèse vocale
    private SpeechSynthesizer synthesizer; // Synthétiseur pour convertir le texte en parole

    // Initialisation des paramètres à l'aide de l'API
    void Start()
    {
        // Clé et région
        configIA = SpeechConfig.FromSubscription("", "");
        
        configIA.SpeechSynthesisVoiceName = VoiceName;
        
        synthesizer = new SpeechSynthesizer(configIA);
    }
    
    // Utilise le synthétiseur pour convertir le tableau de bytes en audio
    public async Task UseSynthesizer(string message)
    {
        byte[] audioData = await SynthesizeSpeech(message, synthesizer);

        if (audioData != null)
        {
            AudioClip clip = CreateAudioClipFromAudioData(audioData);
            PlayAudioClip(clip);
        }
    }
    
    // Convertit le texte en données audio et les renvoie sous forme de tableau de bytes
    private async Task<byte[]> SynthesizeSpeech(string responseText, SpeechSynthesizer synthesizer)
    {
        using (var result = await synthesizer.SpeakTextAsync(responseText))
        {
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                return result.AudioData;
            }
            if (result.Reason == ResultReason.Canceled)
            {
                HandleSpeechSynthesisCancellation(result);
                return null;
            }
        }
        return null;
    }

    // Gère les erreurs de synthèse vocale
    private void HandleSpeechSynthesisCancellation(SpeechSynthesisResult result)
    {
        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
        Debug.Log($"ANNULÉ: Raison={cancellation.Reason}");

        if (cancellation.Reason == CancellationReason.Error)
        {
            Debug.Log($"ANNULÉ: CodeErreur={cancellation.ErrorCode}");
            Debug.Log($"ANNULÉ: DétailsErreur=[{cancellation.ErrorDetails}]");
            Debug.Log($"ANNULÉ: Avez-vous mis à jour les informations d'abonnement?");
        }
    }

    // Convertit les données audio en un AudioClip pour Unity
    private AudioClip CreateAudioClipFromAudioData(byte[] audioData)
    {
        short[] audioAsInt16 = new short[audioData.Length / 2];
        Buffer.BlockCopy(audioData, 0, audioAsInt16, 0, audioData.Length);

        float[] audioAsFloat = Array.ConvertAll(audioAsInt16, i => i / (float)short.MaxValue);

        AudioClip clip = AudioClip.Create("reponse", audioAsFloat.Length, 1, 16000, false);
        clip.SetData(audioAsFloat, 0);

        return clip;
    }

    // Joue l'AudioClip
    private void PlayAudioClip(AudioClip clip)
    {
        audioSourceIA.clip = clip;
        audioSourceIA.Play();
    }

    // Arrête la lecture audio et le synthétiseur
    public void Stop()
    {
        audioSourceIA.Stop();

        if (audioSourceIA.isPlaying)
        {
            Debug.Log("L'audio est en cours de lecture");
        }
        
        if (!audioSourceIA.isPlaying)
        {
            Debug.Log("L'audio n'est pas en cours de lecture");
        }
        
        synthesizer.StopSpeakingAsync();
    }

    // Désactive et libère les ressources du synthétiseur
    public void Disable()
    {
        synthesizer.Dispose();
    }
}
