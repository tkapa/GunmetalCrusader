using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerManager : MonoBehaviour {
    public static StickerManager Instance;
    public List<GameObject> StickerSlots;

    public List<GameObject> StickerObjects;

    public List<Transform> StickerWorldLocations;

    private float EnemiesKilled;
    public float EnemyKillsRequired;

	// Use this for initialization
	void Start ()
    {
        //PlaceStickers();

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.F))
        {
            PlaceSticker();
        }
	}
    void PlaceSticker()
    {

        //    List<GameObject> eachsticker = StickerObjects;
        //    foreach (Transform Locale in StickerWorldLocations)
        //    {
        //    Debug.Log("started");
        //        //int LocationNmbr = Random.Range(0, StickerWorldLocations.Count);

        //        if (Locale.gameObject.name != "UsedLocation")
        //        //{
        //            eachsticker[Random.Range(0, eachsticker.Count)].transform.position = Locale.position;
        //         //   StickerWorldLocations[LocationNmbr].gameObject.name = "UsedLocation";
        //      //  }
        //    Debug.Log("ended");
        //}

        int StickerSelected = Random.Range(0, StickerObjects.Count);

        GameObject StickerToPlace = StickerObjects[StickerSelected];

        Instantiate(StickerToPlace, StickerWorldLocations[Random.Range(0, StickerWorldLocations.Count)].transform.position, Quaternion.identity);



    }

    public void EnemyDiedStickerRelevance()
    {
        EnemiesKilled++;

        if (EnemiesKilled >= EnemyKillsRequired)
        {
            PlaceSticker();
            EnemiesKilled = 0;
        }


    }
   



    //public void PickedUpSticker()
    //{
    //    GameObject SelectedSticker;
    //    GameObject Slot;

    //    int StickerListSelection = Random.Range(0, StickerObjects.Count);

    //    SelectedSticker = StickerObjects[StickerListSelection];

    //    int SlotToPutStickerIn = Random.Range(0, StickerObjects.Count);

    //    Slot = StickerSlots[SlotToPutStickerIn];


    //    Slot.GetComponent<SpriteRenderer>().sprite = SelectedSticker.GetComponent<SpriteRenderer>().sprite;

    //    StickerSlots.Remove(Slot);

    //    StickerObjects.Remove(SelectedSticker);

    //}
}
