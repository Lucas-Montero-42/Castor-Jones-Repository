using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    private GameObject BotonCerrado;
    private GameObject BotonAbierto;
    public bool Activado = false;
    // Start is called before the first frame update
    void Start()
    {
        BotonCerrado = GetChildWithName(gameObject,"Cerrado");
        BotonAbierto = GetChildWithName(gameObject, "Abierto");
    }

    // Update is called once per frame
    void Update()
    {
        if (Activado)
        {
            StartCoroutine(Timer());
        }
    }
    private void ChambioDeBoton()
    {
        BotonCerrado.SetActive(false);
        BotonAbierto.SetActive(true);
    }
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);
        ChambioDeBoton();

    }
}
