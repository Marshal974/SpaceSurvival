using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UtilsCrew
{
    /***
     * 
     * isMinDist : permet de savoir si la destination 1 (dest1) est la plus proche du point d'origine 
     * 
     * */
    public static CrewCharacter newRandomCrew()
    {
        CrewCharacter crew = new CrewCharacter();
        crew.name = generatorName();

        return crew;

    }



    public static string generatorName()
    {
        string result = "";

        List<string> firstNameArray = new List<string>() { "François", "Alex", "Guillaume", "Pierre", "Annie", "Jeanne", "Marion", "Lucie" };
        List<string> lastNameArray = new List<string>() { "Lebreton", "Nax", "Martin", "Dufour", "Miaou", "fourre", "Baduc", "Kikoo" };

        result += firstNameArray[Random.Range(0, firstNameArray.Count)] + " " + lastNameArray[Random.Range(0, firstNameArray.Count)];

        return result;
    }

    public static CrewCharacter randomCompetence(CrewCharacter crew)
    {
        

        return crew;
    }

}

