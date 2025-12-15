using UnityEngine;

public class Izartxoa : MonoBehaviour
{
    [SerializeField] float izartxoaAbiadura = 20f;
    Rigidbody2D nireRigidbody2D;
    PertsonaiMugimendua pertsonaiMugimendua;
    float xMugimendua;

    void Start()
    {
        nireRigidbody2D = GetComponent<Rigidbody2D>();
        pertsonaiMugimendua = FindFirstObjectByType<PertsonaiMugimendua>();
        xMugimendua = pertsonaiMugimendua.transform.localScale.x * izartxoaAbiadura;
    }

    void Update()
    {
        nireRigidbody2D.linearVelocity = new Vector2(xMugimendua, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Etsaia")
        {
            EtsaiaMugitzen estaia = other.GetComponent<EtsaiaMugitzen>();

            if (estaia != null)
            {
                estaia.TakeDamage();
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 1f);
    }
}
