using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    //Trash
    None = 0,
    AppleCore = 1,
    Biscuits = 2,
    BrokenGlass = 3,
    CeramicPlate = 4,
    ChipPack = 5,
    GlassCup = 6,
    FishBones = 7,
    JuiceBox = 8,
    MilkBottle = 9,
    Napkin = 10,
    OldCelery = 11,
    OldBread = 12,
    PlasticBag = 13,
    PringlesCan = 14,
    Styrofoam = 15,
    //Recyclables
    AluminiumCan = 16,
    Cardboard = 17,
    GlassBottle = 18,
    GlassJar = 19,
    Newspaper = 20,
    PaperScraps = 21,
    PetFoodCan = 22,
    SprayCan = 23
}

public class Trash : MonoBehaviour {   

    public bool isRecyclable;
    public TrashType trashType = TrashType.None;

    float airTimer;
    bool throwing;
    Vector2 throwVec;

    Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (throwing)
        {
            rBody.velocity = throwVec * Time.deltaTime;
            airTimer -= Time.deltaTime;
            if(airTimer <= 0f)
            {
                throwing = false;
                gameObject.layer = 9;
                GameManager.inst.aGod.PlaySFX(SFXType.Drop);
                Instantiate(GameManager.inst.trashLandParticle, transform);
            }
        }
    }

    public void Throw(Vector2 direction, float speed, float duration)
    {
        throwVec = direction * speed;
        airTimer = duration;
        throwing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (throwing)
        {
            if (collision.gameObject.layer == 11)
            {
                airTimer = 0.04f;
            }
        }
    }
}
