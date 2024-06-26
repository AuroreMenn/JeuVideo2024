using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
	//Si lors d'un niveau, il faut réinitialiser la position de certains obstacles : mettre un tableauReinit à votre tableau et mettre la liste des FollowingPlayerMove à réinitialiser
	//Mettre le tableauReinit dans tous les objets avec un impact dans le tableau
	[SerializeField] private TableauReinit tableauReinit = null;
	[SerializeField] TableauManager tableauManager;
	
	//Si un objet rentre en collision avec l'obstacle
    void OnTriggerEnter2D(Collider2D col) {
		//Si l'obstacle entre en collision avec le joueur (objet avec le tag "Player")
        if (col.gameObject.tag == "Player") {

            // Récupère tous les ennemis avec le tag "Ennemis"
            GameObject[] ennemis = GameObject.FindGameObjectsWithTag("Ennemi");
            foreach (GameObject ennemi in ennemis)
            {
                // Assurez-vous que l'ennemi possède le script FollowingPlayerMove avant de le déplacer
                ennemi.GetComponent<Reset>().resetPosition();
                
            }
			
			//S'il faut réinitialiser des obstacles lorsqu'on perd, uniquement s'il y a un tableauReinit
			if(tableauReinit != null){
				tableauReinit.Reinit();
			}
			
			//On change la position du joueur et on le téléporte aux coordonnées sauvegardées dans le TableauManager dans la variable checkpointPosition.
            col.gameObject.transform.position = TableauManager.GetCheckpointPosition();	
			
			if(col.GetComponent<PlayerManagerAnimated>().nourriture.gameObject.activeSelf == false) {
				GameObject nourriture = col.GetComponent<PlayerManagerAnimated>().nourriture.gameObject;
				nourriture.gameObject.SetActive(true);
				nourriture.gameObject.GetComponent<Nourriture>().door.SetActive(true);
				col.gameObject.GetComponent<PlayerManagerAnimated>().DeleteNourriture();
			}
			//On augmente de 1 le compteur de morts
			col.gameObject.GetComponent<PlayerManagerAnimated>().AddDeath(); //On récupère le PlayerManager du joueur pour ajouter la mort
			//On immobilise le joueur pendant 0.5 s
			PlayerManager.SetFreeze(0.5f);
        }
    }
}
