using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSightMngr : MonoBehaviour {

	[SerializeField]
	private GameObject[] Points = new GameObject[4];
    [SerializeField]
    private GameObject JumpPoint;

    [SerializeField]
    private SpriteRenderer emitter;

    public float angle = 0.0f;
	public Vector3 target;
    public Color beamCol = Color.red;

    private float displayAngle = 0.0f;
    private Vector3 displayTarget;

    public bool inJumpMode = false;
	
	// Update is called once per frame
	void Update () {
        displayTarget = Vector3.Lerp(displayTarget, target, 1.0f);
        displayAngle = Mathf.Lerp(displayAngle, angle, 0.1f);

        emitter.color = beamCol;

        this.transform.rotation = Quaternion.LookRotation(Vector3.Normalize(displayTarget - this.transform.position));

		int pos = 0;
        if (!inJumpMode)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 && j != 0)
                    {
                        Vector3 dir = transform.forward;

                        dir = Quaternion.AngleAxis(displayAngle * i, Vector3.up) * dir;
                        dir = Quaternion.AngleAxis(displayAngle * j, Vector3.right) * dir;

                        dir = Vector3.Normalize(dir);

                        Vector3[] hitPoint = new Vector3[2];

                        hitPoint[0] = this.transform.position + (dir * 1000);

                        hitPoint[1] = this.transform.position;

                        Points[pos].GetComponent<LineRenderer>().SetPositions(hitPoint);
                        Points[pos].transform.position = hitPoint[0];


                        Points[pos].SetActive(false);

                        pos++;
                    }
                }
            }

            JumpPoint.SetActive(true);
        }
        else
        {
            Vector3 dir = transform.forward;

            Vector3[] hitPoint = new Vector3[2];

            hitPoint[0] = this.transform.position + (dir * 1000);

            hitPoint[1] = this.transform.position;

            JumpPoint.GetComponent<LineRenderer>().SetPositions(hitPoint);
            JumpPoint.transform.position = hitPoint[0];

            for (int i = -1; i < Points.Length; i++)
            {
                Points[i].SetActive(false);
            }
            JumpPoint.SetActive(true);
        }

    }
}
