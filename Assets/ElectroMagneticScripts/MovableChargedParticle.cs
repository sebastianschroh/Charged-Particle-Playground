using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableChargedParticle : ChargedParticle
{
    public Rigidbody rigidbody;
    private const float Kmag = 10;
    // Start is called before the first frame update
    void Start()
    {
        ColorParticle();
        if(movableChargedParticles == null)
            movableChargedParticles = new List<MovableChargedParticle>();

        movableChargedParticles.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (MovableChargedParticle particle in movableChargedParticles)
        {
            if (particle == this)
                continue;

            Vector3 totalAppliedForce = Vector3.zero;
            totalAppliedForce += CalculateElectricForce(particle);
            totalAppliedForce += CalculateMagneticForce(particle);
            particle.rigidbody.AddForce(totalAppliedForce);
        }
    }

    private Vector3 CalculateMagneticForce(MovableChargedParticle particle)
    {
        float distance = Vector3.Distance(particle.transform.position, this.transform.position);

        Vector3 v1 = this.rigidbody.velocity;
        Vector3 v2 = particle.rigidbody.velocity;

        float q1 = this.charge;
        float q2 = particle.charge;
        /**
         *  Fmag = ( Kmag * q1 * q2 ) / ( r^2 ) ) * ( v1-> x ( v2-> x r^ ) 
         * 
         */
        float force = (Kmag * q1 * q2) / (Mathf.Pow(distance, 2.0f));

        Vector3 direction = particle.transform.position - this.transform.position;
        direction.Normalize();

        Vector3 crossproduct1 = Vector3.Cross(v2, direction);
        Vector3 crossproduct2 = Vector3.Cross(v1, crossproduct1);

        Vector3 magneticForce = force * crossproduct2;
        return magneticForce;
    }


}
