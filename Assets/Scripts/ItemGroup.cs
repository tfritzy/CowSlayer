using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGroup
{
    public List<Item> Items;
    public int MaxSize;
    public Vector2 dimensions;
    public string UIPrefabName;

    private GameObject emptyItemSlot;

    public ItemGroup(int maxSize, Vector2 dimensions, string UIPrefabName)
    {
        this.MaxSize = maxSize;
        this.dimensions = dimensions;
        this.UIPrefabName = UIPrefabName;
        emptyItemSlot = Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/EmptyItemSlot");
        this.Items = new List<Item>();
    }

    public void OpenMenu(float menuHeight)
    {
        Vector3 uiPosition = new Vector2(
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.width * .5f,
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.height * menuHeight
        );
        GameObject chestUI = GameObject.Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/{UIPrefabName}"), 
            uiPosition, new Quaternion(), Constants.GameObjects.InteractableUI.transform);
        GameObject backdrop = chestUI.transform.Find("Backdrop").gameObject;
        int width = (int)backdrop.GetComponent<RectTransform>().rect.width;
        int height = (int)backdrop.GetComponent<RectTransform>().rect.height;
        int xDistBetweenIcons = (int)(width / dimensions.x);
        int yDistBetweenIcons = (int)(height / dimensions.y);
        int itemCount = 0;
        for (int j = height / 2 - yDistBetweenIcons / 2; j > -height / 2; j -= yDistBetweenIcons)
        {
            for (int i = -width / 2 + xDistBetweenIcons / 2; i < width / 2; i += xDistBetweenIcons)
            {
                GameObject inst = GameObject.Instantiate(emptyItemSlot, backdrop.transform.position + new Vector3(i, j),
                    new Quaternion(), backdrop.transform);
                if (itemCount < Items.Count)
                {
                    inst.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/{Items[itemCount].IconName}");
                }
                itemCount += 1;
            }
        }
    }
}
