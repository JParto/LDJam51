using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Preferences 
{
    public static readonly int preferenceAmount = 12;

    public enum Animal
    {
        Dogs,
        Cats,
        Any,
        None
    }
    public Animal prefAnimal;

    public enum Color
    {
        Red,
        Blue,
        Green
    }

    public Color prefColor;

    public enum LifeStyle
    {
        Money,
        Luck
    }
    public LifeStyle prefLifeStyle;

    public enum Music
    {
        Rock,
        Pop,
        Jazz,
        None
    }
    public Music prefMusic;

    public enum SuperPower
    {
        Flying,
        Invisibility,
        Invincibility
    }
    public SuperPower prefSuperPower;

    public enum Sound
    {
        Quiet,
        Loud
    }
    public Sound prefSound;

    public enum Age
    {
        Younger,
        Older,
        SameAge
    }
    public Age prefAge;

    public bool smoking;
    public bool drinking;
    public bool kids;
    public bool marriage;
    public bool humour;

    public static Preferences RandomizePreference(){
        Preferences prefs = new Preferences();

        prefs.prefAnimal     = (Animal)Random.Range(0, 4);
        prefs.prefColor      = (Color)Random.Range(0, 3);
        prefs.prefLifeStyle  = (LifeStyle)Random.Range(0, 2);
        prefs.prefMusic      = (Music)Random.Range(0, 4);
        prefs.prefSuperPower = (SuperPower)Random.Range(0, 3);
        prefs.prefSound      = (Sound)Random.Range(0, 2);
        prefs.prefAge        = (Age)Random.Range(0, 3);

        prefs.smoking   = Random.value > 0.5f;
        prefs.drinking  = Random.value > 0.5f;
        prefs.kids      = Random.value > 0.5f;
        prefs.marriage  = Random.value > 0.5f;
        prefs.humour    = Random.value > 0.5f;

        return prefs;
    }

    public static Preferences RandomizePreferencesAtIndex(int index, Preferences p){
        switch (index)
        {
            case 0:
                p.prefAnimal = (Animal)Random.Range(0, 4);
                break;
            case 1:
                p.prefColor = (Color)Random.Range(0, 3);
                break;
            case 2:
                p.prefLifeStyle = (LifeStyle)Random.Range(0, 2);
                break;
            case 3:
                p.prefMusic = (Music)Random.Range(0, 4);
                break;
            case 4:
                p.prefSuperPower = (SuperPower)Random.Range(0, 3);
                break;
            case 5:
                p.prefSound = (Sound)Random.Range(0, 2);
                break;
            case 6:
                p.prefAge = (Age)Random.Range(0, 3);
                break;

            case 7:
                p.smoking = Random.value > 0.5f;
                break;
            case 8:
                p.drinking = Random.value > 0.5f;
                break;
            case 9:
                p.kids = Random.value > 0.5f;
                break;
            case 10:
                p.marriage = Random.value > 0.5f;
                break;
            case 11:
                p.humour = Random.value > 0.5f;
                break;
        }
        
        return p;
    }

    public static bool SamePrefs(Preferences p1, Preferences p2){
        bool be1 = p1.prefAnimal == p2.prefAnimal;
        bool be2 = p1.prefColor == p2.prefColor;
        bool be3 = p1.prefLifeStyle == p2.prefLifeStyle;
        bool be4 = p1.prefMusic == p2.prefMusic;
        bool be5 = p1.prefSuperPower == p2.prefSuperPower;
        bool be6 = p1.prefSound == p2.prefSound;
        bool be7 = p1.prefAge == p2.prefAge;

        bool be = be1 && be2 && be3 && be4 && be5 && be6 && be7;


        bool bb1 = p1.smoking == p2.smoking;
        bool bb2 = p1.drinking == p2.drinking;
        bool bb3 = p1.kids == p2.kids;
        bool bb4 = p1.marriage == p2.marriage;
        bool bb5 = p1.humour == p2.humour;

        bool bb = bb1 && bb2 && bb3 && bb4 && bb5;

        return be && bb;
    }

    public static Preferences CopyPreferences(Preferences from, Preferences to){
        to.prefAnimal = from.prefAnimal;
        to.prefColor = from.prefColor;
        to.prefLifeStyle = from.prefLifeStyle;
        to.prefMusic = from.prefMusic;
        to.prefSuperPower = from.prefSuperPower;
        to.prefSound = from.prefSound;
        to.prefAge = from.prefAge;

        to.smoking = from.smoking;
        to.drinking = from.drinking;
        to.kids = from.kids;
        to.marriage = from.marriage;
        to.humour = from.marriage;

        return to;
    }

    public static int Compatibility(Preferences p1, Preferences p2){
        // lower compatibility is better 
        // adds one to the total if the preferences are not the same

        int be1 = p1.prefAnimal == p2.prefAnimal ? 0 : 1;
        int be2 = p1.prefColor == p2.prefColor ? 0 : 1;
        int be3 = p1.prefLifeStyle == p2.prefLifeStyle ? 0 : 1;
        int be4 = p1.prefMusic == p2.prefMusic ? 0 : 1;
        int be5 = p1.prefSuperPower == p2.prefSuperPower ? 0 : 1;
        int be6 = p1.prefSound == p2.prefSound ? 0 : 1;
        int be7 = p1.prefAge == p2.prefAge ? 0 : 1;

        int be = be1 + be2 + be3 + be4 + be5 + be6 + be7;


        int bb1 = p1.smoking == p2.smoking ? 0 : 1;
        int bb2 = p1.drinking == p2.drinking ? 0 : 1;
        int bb3 = p1.kids == p2.kids ? 0 : 1;
        int bb4 = p1.marriage == p2.marriage ? 0 : 1;
        int bb5 = p1.humour == p2.humour ? 0 : 1;

        int bb = bb1 + bb2 + bb3 + bb4 + bb5;

        return be + bb;
    }

    public static bool EvaluatePrefIndex(int index, Preferences p1, Preferences p2){
        switch (index)
        {
            case 0:
                return p1.prefAnimal == p2.prefAnimal;
            case 1:
                return p1.prefColor == p2.prefColor;
            case 2:
                return p1.prefLifeStyle == p2.prefLifeStyle;
            case 3:
                return p1.prefMusic == p2.prefMusic;
            case 4:
                return p1.prefSuperPower == p2.prefSuperPower;
            case 5:
                return p1.prefSound == p2.prefSound;
            case 6:
                return p1.prefAge == p2.prefAge;

            case 7:
                return p1.smoking == p2.smoking;
            case 8:
                return p1.drinking == p2.drinking;
            case 9:
                return p1.kids == p2.kids;
            case 10:
                return p1.marriage == p2.marriage;
            case 11:
                return p1.humour == p2.humour;
        }

        return false;
    }

    public struct PreferenceItems
    {
        public bool animal;
        public bool color;
        public bool lifeStyle;
        public bool music;
        public bool superPower;
        public bool sound;
        public bool age;

        public bool smoking;
        public bool drinking;
        public bool kids;
        public bool marriage;
        public bool humour;
    }
}
