using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShortcutLib.Shortcut;
using static OtherFunc;
using UnityEngine;
using System.Reflection;

namespace PuppyCatSlime
{
    internal class PuppyCat
    {
        public static (SlimeDefinition, GameObject, SlimeAppearance) puppyCatSlime;
        public static GameObject puppyCatPlort;
        public static AssetBundle puppyCatBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Main), "Assets.puppycat_slime"));
        public static Color[] puppyCatPalette =
        {
            Other.LoadHex("#eef0e7"), // White/Gray?
            Other.LoadHex("#f9c5c5"), // Pink
            Other.LoadHex("#a79063"), // Brown
            Other.LoadHex("#5e4e35") // Darker Brown
        };

        public static void CreateSlime()
        {
            puppyCatSlime = Slime.CreateSlime(Identifiable.Id.TABBY_SLIME, Identifiable.Id.TABBY_SLIME, Enums.PUPPYCAT_SLIME, "PuppyCat Slime", CreateSprite(LoadImage("Assets.puppycat_slime_ico")), puppyCatPalette[1], puppyCatPalette[0], puppyCatPalette[1], puppyCatPalette[2], puppyCatPalette[1]);

            SlimeDefinition puppyCDef = puppyCatSlime.Item1;
            GameObject puppyCObj = puppyCatSlime.Item2;
            SlimeAppearance puppyCApp = puppyCatSlime.Item3;

            puppyCDef.Diet.MajorFoodGroups = new SlimeEat.FoodGroup[] { SlimeEat.FoodGroup.MEAT, SlimeEat.FoodGroup.FRUIT, SlimeEat.FoodGroup.VEGGIES };
            puppyCDef.Diet.Produces = new Identifiable.Id[] { Enums.PUPPYCAT_PLORT };

            SlimeAppearance.SlimeBone[] attachedBones = new SlimeAppearance.SlimeBone[]
            {
                SlimeAppearance.SlimeBone.JiggleBack,
                SlimeAppearance.SlimeBone.JiggleBottom,
                SlimeAppearance.SlimeBone.JiggleFront,
                SlimeAppearance.SlimeBone.JiggleLeft,
                SlimeAppearance.SlimeBone.JiggleRight,
                SlimeAppearance.SlimeBone.JiggleTop
            };

            (GameObject, SlimeAppearanceObject, SlimeAppearance.SlimeBone[]) ears = Structure.CreateBasicStructure(puppyCatBundle, "ears", "puppyCat_ears", SlimeAppearance.SlimeBone.JiggleTop, SlimeAppearance.SlimeBone.None, attachedBones, RubberBoneEffect.RubberType.Slime);
            ears.Item2.IgnoreLODIndex = true;
            Structure.SetStructureElement("puppyCatEars", puppyCApp, new SlimeAppearanceObject[] { ears.Item2 }, 1);

            (GameObject, SlimeAppearanceObject, SlimeAppearance.SlimeBone[]) tail = Structure.CreateBasicStructure(puppyCatBundle, "tail", "puppyCat_tail", SlimeAppearance.SlimeBone.JiggleTop, SlimeAppearance.SlimeBone.None, attachedBones, RubberBoneEffect.RubberType.Slime);
            ears.Item2.IgnoreLODIndex = true;
            Structure.SetStructureElement("puppyCatTail", puppyCApp, new SlimeAppearanceObject[] { tail.Item2 }, 2);

            Material mouth = Slime.GetSlimeDef(Identifiable.Id.TABBY_SLIME).AppearancesDefault[0].Face.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.Angry).Mouth;
            Material eyes = Slime.GetSlimeDef(Identifiable.Id.TABBY_SLIME).AppearancesDefault[0].Face.ExpressionFaces.First(x => x.SlimeExpression == SlimeFace.SlimeExpression.Angry).Eyes;
            
            for (int i = 0; i < puppyCApp.Face.ExpressionFaces.Length; i++)
            {
                if (puppyCApp.Face.ExpressionFaces[i].SlimeExpression == SlimeFace.SlimeExpression.Angry) continue;
                puppyCApp.Face.ExpressionFaces[i].Mouth = mouth;
                puppyCApp.Face.ExpressionFaces[i].Eyes = eyes;
            }
            SlimeExpressionFace[] expressionFaces = puppyCApp.Face.ExpressionFaces;
            for (int k = 0; k < expressionFaces.Length; k++)
            {
                SlimeExpressionFace slimeExpressionFace = expressionFaces[k];
                if ((bool)slimeExpressionFace.Eyes)
                {
                    slimeExpressionFace.Eyes.SetColor("_EyeRed", puppyCatPalette[3]);
                    slimeExpressionFace.Eyes.SetColor("_EyeGreen", puppyCatPalette[3]);
                    slimeExpressionFace.Eyes.SetColor("_EyeBlue", puppyCatPalette[3]);
                }
                if ((bool)slimeExpressionFace.Mouth)
                {
                    slimeExpressionFace.Mouth.SetColor("_MouthBot", puppyCatPalette[3]);
                    slimeExpressionFace.Mouth.SetColor("_MouthMid", puppyCatPalette[3]);
                    slimeExpressionFace.Mouth.SetColor("_MouthTop", puppyCatPalette[3]);
                }
            }
            puppyCApp.Face.OnEnable();

            Slime.ColorSlime(Enums.PUPPYCAT_SLIME, Identifiable.Id.TABBY_SLIME, puppyCatPalette[0], puppyCatPalette[0], puppyCatPalette[0], puppyCatPalette[0]);
            Structure.ColorStructure(Enums.PUPPYCAT_SLIME, Identifiable.Id.PINK_SLIME, puppyCatPalette[2], puppyCatPalette[1], puppyCatPalette[2], materialStructureNum: 0, structureNum: 1);
            Structure.ColorStructure(Enums.PUPPYCAT_SLIME, Identifiable.Id.MOSAIC_SLIME, puppyCatPalette[2], puppyCatPalette[0], puppyCatPalette[0], materialStructureNum: 0, structureNum: 2);

            puppyCatPlort = Slime.CreatePlort(Identifiable.Id.PINK_PLORT, Enums.PUPPYCAT_PLORT, "PuppyCat Plort", CreateSprite(LoadImage("Assets.puppycat_plort_ico")), puppyCatPalette[0], 120);
            Slime.ColorPlort(Enums.PUPPYCAT_PLORT, puppyCatPalette[1], puppyCatPalette[2], puppyCatPalette[0]);
        }
    }
}
