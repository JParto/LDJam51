using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/SentenceDictionary")]
public class SO_Sentences : SO_DescriptionBase
{
    public List<List<List<string>>> Sentences(){
        List<List<List<string>>> s = new List<List<List<string>>>();
        s.Add(Animals());
        s.Add(Color());
        s.Add(LifeStyle());
        s.Add(Music());
        s.Add(SuperPower());
        s.Add(Sound());
        s.Add(Age());

        return s;
    }

    public List<List<string>> Animals(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(dog);
        ss.Add(cat);
        ss.Add(anyAnimal);
        ss.Add(noAnimal);

        return ss;
    }
    [Header("Animals")]
    public List<string> dog;
    public List<string> cat;
    public List<string> anyAnimal;
    public List<string> noAnimal;

    public List<List<string>> Color(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(red);
        ss.Add(blue);
        ss.Add(green);

        return ss;
    }
    [Header("Color")]
    public List<string> red;
    public List<string> blue;
    public List<string> green;

    public List<List<string>> LifeStyle(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(Money);
        ss.Add(Luck);

        return ss;
    }
    [Header("LifeStyel")]
    public List<string> Money;
    public List<string> Luck;

    public List<List<string>> Music(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(rock);
        ss.Add(pop);
        ss.Add(jazz);
        ss.Add(noMusic);

        return ss;
    }
    [Header("Music")]
    public List<string> rock;
    public List<string> pop;
    public List<string> jazz;
    public List<string> noMusic;

    public List<List<string>> SuperPower(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(flying);
        ss.Add(invisibility);
        ss.Add(invincibility);

        return ss;
    }
    [Header("SuperPower")]
    public List<string> flying;
    public List<string> invisibility;
    public List<string> invincibility;

    public List<List<string>> Sound(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(quiet);
        ss.Add(loud);

        return ss;
    }
    [Header("Music")]
    public List<string> quiet;
    public List<string> loud;

    public List<List<string>> Age(){
        List<List<string>> ss = new List<List<string>>();
        ss.Add(younger);
        ss.Add(older);
        ss.Add(sameAge);

        return ss;
    }
    [Header("Music")]
    public List<string> younger;
    public List<string> older;
    public List<string> sameAge;
}
