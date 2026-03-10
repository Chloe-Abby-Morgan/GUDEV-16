using UnityEngine;

public class LastSelected : MonoBehaviour
{
   public MusicManager.NoteDirection lSel;
   public static LastSelected instance;

    private void Awake()
    {
        instance = this;
    }
}
