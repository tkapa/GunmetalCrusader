using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerManager : MonoBehaviour {

    public List<GameObject> StickerSlots;

    public List<GameObject> StickerObjects;

    public List<Transform> StickerWorldLocations;

	// Use this for initialization
	void Start ()
    {
        PlaceStickers();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaceStickers()
    {
        List<GameObject> eachsticker = StickerObjects;
        foreach (Transform Locale in StickerWorldLocations)
        {
            int LocationNmbr = Random.Range(0, StickerWorldLocations.Count);
            eachsticker[Random.Range(0, eachsticker.Count)].transform.position = StickerWorldLocations[LocationNmbr].position;
            StickerWorldLocations.Remove(StickerWorldLocations[LocationNmbr]);
        }
    }

    public void PickedUpSticker()
    {
        GameObject SelectedSticker;
        GameObject Slot;

        int StickerListSelection = Random.Range(0, StickerObjects.Count);

        SelectedSticker = StickerObjects[StickerListSelection];

        int SlotToPutStickerIn = Random.Range(0, StickerObjects.Count);

        Slot = StickerSlots[SlotToPutStickerIn];


        Slot.GetComponent<SpriteRenderer>().sprite = SelectedSticker.GetComponent<SpriteRenderer>().sprite;

        StickerSlots.Remove(Slot);

        StickerObjects.Remove(SelectedSticker);

    }
}
