using UnityEngine;

public class GameAssets : MonoBehaviour
{


    private static GameAssets _i;
    // Use this for initialization
    public static GameAssets i {
        get
        {
            if (_i == null) {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
                        
            return _i;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public SoundAudioClip[] soundAudioClipsArray;

}
