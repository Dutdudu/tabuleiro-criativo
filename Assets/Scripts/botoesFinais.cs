using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NewBehaviourScript : MonoBehaviour
{
    public void changeScene(string Scene) {

        PhotonNetwork.LoadLevel(Scene);

    }
}
