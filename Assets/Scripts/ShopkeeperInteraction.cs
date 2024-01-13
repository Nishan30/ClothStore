using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopkeeperInteraction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI shopKeeperText;

    [SerializeField]
    private TextMeshProUGUI buyerText;

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private Button RejectButton;

    [SerializeField]
    private Button BargainButton;

    [SerializeField]
    private PlayerMovement playerMovement;

    private bool hasBargained;
    private void Start()
    {
        buyButton.onClick.AddListener(buyCloth);
        RejectButton.onClick.AddListener(rejectCloth);
        BargainButton.onClick.AddListener(bargainPrice);
    }

    private void OnEnable()
    {
        shopKeeperText.text = "Hi!. That is an amazing choice. It will cost you " + playerMovement.currentPrice + "$";
        shopKeeperText.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        BargainButton.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        shopKeeperText.gameObject.SetActive(false);
        buyerText.gameObject.SetActive(false);
    }
    private void buyCloth()
    {
        playerMovement.initiatePurchase();
    }
    private void rejectCloth()
    {
        gameObject.SetActive(false);
    }
    private void bargainPrice()
    {
        if(!hasBargained)
        {
            buyerText.text = "Buyer: Hi!. I am really curious. Do we have any discounts on this item?";
            buyerText.gameObject.SetActive(true);
            StartCoroutine("firstBargain");
            hasBargained = true;
        }
        else
        {
            buyerText.text = "Buyer: I am really sorry. I cannot buy this without a discount. Even 5% discount works for me.";
            buyerText.gameObject.SetActive(true);
            StartCoroutine("secondBargain");
        }
        
    }
    private IEnumerator firstBargain()
    {
        yield return new WaitForSeconds(3);
        buyerText.gameObject.SetActive(false);
        if(Random.Range(0,10) > 3) //30 percent chance for discount
        {
            shopKeeperText.text = "Seller: I am really sorry. This is a premium item. Its a fixed price.";
            buyButton.gameObject.SetActive(true);
            BargainButton.gameObject.SetActive(true);
        }
        else
        {
            shopKeeperText.text = "Seller: Yes. We do have a 1$ discount on this product.";
            playerMovement.currentPrice -= 1;
            buyButton.gameObject.SetActive(true);
            BargainButton.gameObject.SetActive(true);
        }
        
    }
    private IEnumerator secondBargain()
    {
        yield return new WaitForSeconds(3);
        buyerText.gameObject.SetActive(false);
        shopKeeperText.text = "Seller: We cannot do anything here. You can buy this one or find some other product.";
        buyButton.gameObject.SetActive(true);
        RejectButton.gameObject.SetActive(true);
    }

}
