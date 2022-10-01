using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
public class Outfitter : MonoBehaviour
{
    private SpriteResolver[] resolvers;
    private OutfitType type;


    private void Start()
    {
        resolvers = GetComponentsInChildren<SpriteResolver>();
        ChangeOutfit();

    }

    private void ChangeOutfit()
    {
        foreach (var resolver in resolvers)
        {
            int outfitInt = (int)Random.Range(0, (int)OutfitType.NumberOfTypes);
            OutfitType value = (OutfitType)outfitInt;

            resolver.SetCategoryAndLabel(resolver.GetCategory(), value.ToString());
        }
    }

    private enum OutfitType
    {
        Default = 0,
        Monopoly = 1,
        Tweed = 2,
        GreyTweed = 3,
        WhiteDress = 4,
        DarkBlueDress = 5,
        LuckyCharms = 6,
        NumberOfTypes
    }
}
