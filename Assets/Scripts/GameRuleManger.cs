using UnityEngine;
using Photon.Pun;

public class GameRuleManager : MonoBehaviourPun
{
    // This method will update rule values on all clients
    [PunRPC]
    void UpdateRule(string ruleKey, string ruleValue)
    {
        // Apply the rule change locally on this client
        ApplyRule(ruleKey, ruleValue);
    }

    // A method to actually update the rule in your game
    void ApplyRule(string ruleKey, string ruleValue)
    {
        // Here, you can update your UI or store the rule in a variable
        // For example, if ruleKey is "Difficulty", update the difficulty setting
        Debug.Log($"Rule {ruleKey} set to {ruleValue}");

        // You would add code here to update the UI if necessary
    }

    // Method to be called when a player selects a rule option
    public void OnRuleSelected(string ruleKey, string ruleValue)
    {
        // Call the UpdateRule method on all clients using RPC
        photonView.RPC("UpdateRule", RpcTarget.All, ruleKey, ruleValue);
    }
}
