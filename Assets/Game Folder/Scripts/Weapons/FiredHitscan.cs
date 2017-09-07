using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FiredHitscan : FiredObject {

    // How long should our damage ray be?
    [Tooltip("How long should our damage ray be?")]
    [SerializeField]
    private float scanRange = 512.0f;

    private LineRenderer linerend;

    /*
     * Called on instance create. Override to check ray damage;
     */
    protected override void Start()
    {
        base.Start();

        linerend = GetComponent<LineRenderer>();
        doRaycastHit();
    }

    /*
     * Casts a ray from the spawn location and checks if we hit any enemies.
     */
    protected virtual void doRaycastHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, this.transform.forward, out hit, scanRange))
        {
            switch(hit.collider.tag)
            {
                case "Enemy":
                    {
                        // TODO: Add headshot support
                        hitEnemy(hit.collider.gameObject, 1.0f);
                        break;
                    }
            }
            SpawnHitscanEffects(hit.point);
            splashEnemy(hit.collider.gameObject, this.transform.position);
        }
        else
        {
            splashEnemy(null, this.transform.position);
            SpawnHitscanEffects(this.transform.position + this.transform.forward * scanRange);
        }
    }

    /*
     * Called on instance create. Override to check ray damage;
     */
    private void SpawnHitscanEffects(Vector3 hitLocation)
    {
        Instantiate(impactEffect, hitLocation, Quaternion.identity);

        Vector3[] points = new Vector3[2];

        points[0] = this.transform.position;
        points[1] = hitLocation;

        linerend.SetPositions(points);
    }
}
