using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    private GridObjectCollection grid;
    private GameObject scrollCardTemplate;
    [SerializeField]
    private TextMeshProUGUI titleTMP;
    private List<string> titles;
    private List<string> tags;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        grid = transform.Find("ScrollParent").GetComponentInChildren<GridObjectCollection>();
        scrollCardTemplate = GameObject.Find("ScrollCardTemplate");
        titles = new List<string> {"Molecule List", "Isosurface List"};
        tags = new List<string> { "b_mol", "b_iso" };
        
        yield return new WaitUntil(() => UIManager.UIMan.init);

        for (int i = 0; i < UIManager.UIMan.isosurfaceStart; i++)
        {
            string objectName = UIManager.moleculeNames[i];
            Debug.Log("Instantiated " + objectName);
            //GameObject molecule = UIManager.UIMan.LoadAssetFromBundle(objectName);
            // copy a button and set property
            GameObject newCard = Instantiate(scrollCardTemplate);
            newCard.GetComponentInChildren<TextMeshPro>().text = objectName;
            // set buttons parent and set relative position
            newCard.tag = "b_mol";
            newCard.transform.SetParent(grid.transform);
            newCard.transform.localScale = Vector3.one;
        }
        for (int i = UIManager.UIMan.isosurfaceStart; i < UIManager.UIMan.count; i++)
        {
            string objectName = UIManager.moleculeNames[i];
            Debug.Log("Instantiated " + objectName);
            // copy a button and set property
            GameObject newCard = Instantiate(scrollCardTemplate);
            newCard.GetComponentInChildren<TextMeshPro>().text = objectName;
            // set buttons parent and set relative position
            newCard.tag = "b_iso";
            newCard.transform.SetParent(grid.transform);
            newCard.transform.localScale = Vector3.one;
        }
        Destroy(scrollCardTemplate);
        titleTMP.SetText(titles[0]);
        foreach (Transform card in grid.transform)
        {
            card.gameObject.SetActive(card.tag == tags[0]);
        }
        grid.UpdateCollection();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTag()
    {
        int index = titles.IndexOf(titleTMP.text);
        Debug.Log(index + titleTMP.text);
        
        if (index == -1) return;
        int newIndex = (index + 1) % titles.Count;
        Debug.Log(newIndex);
        titleTMP.SetText(titles[newIndex]);
        foreach (Transform card in grid.transform)
        {
            card.gameObject.SetActive(card.tag == tags[newIndex]);
        }
        grid.UpdateCollection();
    }

}
