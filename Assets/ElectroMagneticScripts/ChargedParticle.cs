using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedParticle : MonoBehaviour
{
    public float charge = 1.0f;
    protected static List<MovableChargedParticle> movableChargedParticles;
    protected const float Kelec = 100000;
    // Start is called before the first frame update
    void Start()
    {
        ColorParticle();
        if(movableChargedParticles == null)
            movableChargedParticles = new List<MovableChargedParticle>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach(MovableChargedParticle particle in movableChargedParticles)
        {
            Vector3 totalAppliedForce = Vector3.zero;
            totalAppliedForce += CalculateElectricForce(particle);
            particle.rigidbody.AddForce(totalAppliedForce);
        }
    }

    protected void ColorParticle()
    {
        Color color;
        
        if(charge > 0)
        {
            color = Color.red;
        }
        else if(charge == 0)
        {
            color = Color.grey;
        }
        else
        {
            color = Color.blue;
        }

        GetComponent<Renderer>().material.color = color;
    }

    protected Vector3 CalculateElectricForce(MovableChargedParticle particle)
    {
        float distance = Vector3.Distance(particle.transform.position, this.transform.position);

        float q1 = this.charge;
        float q2 = particle.charge;
        /**
         * Felec = ( ( Kelec * q1 * q2 ) / ( r^2 ) ) * ( r^ )
         */
        float force = Kelec * q1 * q2 / Mathf.Pow(distance, 2.0f);

        Vector3 direction = particle.transform.position - this.transform.position;
        direction.Normalize();

        Vector3 electricForce = force * direction;

        return electricForce;
    }


}
