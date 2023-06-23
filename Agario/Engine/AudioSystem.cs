using System.Collections.Generic;
using SFML.Audio;
using Agario.Engine.ExtensionMethods.PathExtentionMethods;
using System.IO;

public class AudioSystem
{
    public static AudioSystem instance;

    private Dictionary<string, Sound> audioSources = new Dictionary<string, Sound>();
    public AudioSystem()
    {
        CreateAudioClipLibrary();
        instance = this;
    }
    private void CreateAudioClipLibrary()
    {
        string currentDirectory = PathExtentionMethods.GetCurrentDirectory();

        string folderPath = currentDirectory + "/Game/" + "Sounds";

        if (!Directory.Exists(folderPath))
        {
            return;
        }

        string[] mp3Files = Directory.GetFiles(folderPath, "*.wav");

        foreach (string filePath in mp3Files)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Sound sound = new Sound(new SoundBuffer(filePath));

            audioSources.Add(fileName, sound);
        }
    }
    #region ControlMethods
    public static void PlaySoundOnce(string name)
    {
        Sound source = instance.audioSources[name];

        source.Loop = false;

        source.Play();
    }
    public static void PlaySoundLooped(string name)
    {
        Sound source = instance.audioSources[name];

        source.Loop = true;

        source.Play();
    }
    public static void StopSound(string name)
    {
        Sound source = instance.audioSources[name];

        source.Stop();
    }
    public static void PlaySetSoundAt(string name)
    {
        Sound source = instance.audioSources[name];

        source.Play();
    }
    public static void SetVolume(string name, int _volume)
    {
        instance.audioSources[name].Volume = _volume;
    }
    public static void SetPitch(string name, int _pitch)
    {
        instance.audioSources[name].Volume = _pitch;
    }
    #endregion
}
