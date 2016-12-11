using UnityEngine;

public class ScreenClicker : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            this.Clicked();
        }
    }

    private void Clicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            ClickMove clickable = hit.collider.gameObject.GetComponent<ClickMove>();
            if (clickable != null)
            {
                clickable.OnClick(hit);
            }
        }
    }
}
