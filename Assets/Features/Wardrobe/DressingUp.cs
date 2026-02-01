using System.Collections;
using System.Linq;
using UnityEngine;

public class DressingUp : MonoBehaviour
{
    [SerializeField] bool isSuitedUp = false;

    [SerializeField] MeshRenderer headMeshRenderer;
    [SerializeField] MeshRenderer bodyMeshRenderer;
    [SerializeField] MeshRenderer legsMeshRenderer;

    [SerializeField] MeshRenderer shortHairMeshRenderer;
    [SerializeField] MeshRenderer baldHairMeshRenderer;
    [SerializeField] MeshRenderer fancyHairMeshRenderer;
    [SerializeField] MeshRenderer bunHairMeshRenderer;


	public void Start()
	{
        if(isSuitedUp)
            DressBlackSuit();
	}

    public void DressBlackSuit()
    {
		ClothingItem suit = FindAnyObjectByType<GameStarter>().ClothingItems.First(item => item.itemName.ToLower() == "black suit");
        ClothingItem pants = FindAnyObjectByType<GameStarter>().ClothingItems.First(item => item.itemName.ToLower() == "black pants");

		ChangeClothing(suit);
        ChangeClothing(pants);
	}

	public void ChangeClothing(ClothingItem clothes)
    {
        if(clothes.bodyPart.ToLower() == "head")
        {
            shortHairMeshRenderer.gameObject.SetActive(false);
            baldHairMeshRenderer.gameObject.SetActive(false);
            fancyHairMeshRenderer.gameObject.SetActive(false);
            bunHairMeshRenderer.gameObject.SetActive(false);

			if(clothes.hairModel.ToLower() == "short")
            {
                shortHairMeshRenderer.gameObject.SetActive(true);
				shortHairMeshRenderer.material.mainTexture = clothes.texture;
            }
            else if(clothes.hairModel.ToLower() == "bald")
            {
                baldHairMeshRenderer.gameObject.SetActive(true);
				baldHairMeshRenderer.material.mainTexture = clothes.texture;
            }
            else if(clothes.hairModel.ToLower() == "fancy")
            {
                fancyHairMeshRenderer.gameObject.SetActive(true);
                fancyHairMeshRenderer.material.mainTexture = clothes.texture;
            }
            else if(clothes.hairModel.ToLower() == "bun")
            {
                bunHairMeshRenderer.gameObject.SetActive(true);
                bunHairMeshRenderer.material.mainTexture = clothes.texture;
			}
		}
        else if(clothes.bodyPart.ToLower() == "body")
        {
			bodyMeshRenderer.material.mainTexture = clothes.texture;
        }
        else if(clothes.bodyPart.ToLower() == "legs")
        {
			legsMeshRenderer.material.mainTexture = clothes.texture;
		}
	}
}
