using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ResourceAmount {

    public ResourceTypeSO resourceTypeSO;
    public int amount;


    public static string GetTooltipString(List<ResourceAmount> resourceAmountList) {
        string tooltipString = "";

        foreach (ResourceAmount resourceAmount in resourceAmountList) {
            tooltipString += "<color=#" + GetStringFromColor(resourceAmount.resourceTypeSO.color) + ">" + 
                resourceAmount.resourceTypeSO.shortString + resourceAmount.amount + "</color> ";
        }

        return tooltipString;
    }

    // Get Hex Color FF00FF
	    public static string GetStringFromColor(Color color) {
		    string red = Dec01_to_Hex(color.r);
		    string green = Dec01_to_Hex(color.g);
		    string blue = Dec01_to_Hex(color.b);
		    return red+green+blue;
	    }

        public static string Dec_to_Hex(int value) {
		    return value.ToString("X2");
	    }


		// Returns a hex string based on a number between 0->1
	    public static string Dec01_to_Hex(float value) {
		    return Dec_to_Hex((int)Mathf.Round(value*255f));
	    }

}
