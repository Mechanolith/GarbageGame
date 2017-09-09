using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public enum TrashType
    {
        None,
        RedTrash,
        OrangeTrash
    }

    public enum RecycleType
    {
        None,
        BlueRecycle,
        GreenRecycle
    }

    public bool isRecyclable;
    public TrashType trashType = TrashType.None;
    public RecycleType recType = RecycleType.None;
}
