using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaMovil : MonoBehaviour
{
    public bool Active;
    public GameObject movingPlatform;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject button;
    private buttonManager butt1;
    public bool noRepetir = false;

    public float Speed = .5f;

    private Vector3 MoveTo;

    // Start is called before the first frame update
    void Start()
    {
        butt1 = button.GetComponent<buttonManager>();
        MoveTo = endPoint.position;
        Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (butt1.Activado)
        {
            StartCoroutine(Timer());
        }
        if (Active)
        {
            Movement();
        }
        ChangeDirection();
        if (noRepetir)
        {
            Stop();
        }

    }
    private void Movement()
    {
        movingPlatform.transform.position = Vector3.MoveTowards(movingPlatform.transform.position, MoveTo, Speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        if (movingPlatform.transform.position == endPoint.position)
        {
            MoveTo = startPoint.position;
        }
        else if (movingPlatform.transform.position == startPoint.position)
        {
            MoveTo = endPoint.position;
        }
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
    private void Stop()
    {
        startPoint.transform.position = endPoint.transform.position;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);
        Active = true;

    }
}
