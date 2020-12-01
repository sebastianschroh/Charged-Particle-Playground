using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableChargedParticle : ChargedParticle
{
    public Rigidbody rigidbody;
    private const float Kmag = 10;
    private LineRenderer line;
    public Material lineMaterial;
    private static bool play = false;

    // Start is called before the first frame update
    void Start()
    {
        ColorParticle();
        if(chargedParticles == null)
            chargedParticles = new List<ChargedParticle>();

        chargedParticles.Add(this);
        this.line = gameObject.AddComponent<LineRenderer>();
        line.startColor = Color.blue;
        line.endColor = Color.blue;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.positionCount = 2;
        line.material = Resources.Load("Yellow") as Material;
    }

    private void FixedUpdate()
    {
        play = GameObject.Find("SimulationController").GetComponent<SimulationController>().play;
        if (play)
        {
            Vector3 totalAppliedForce = Vector3.zero;
            foreach (ChargedParticle particle in chargedParticles)
            {
                if (particle == this)
                    continue;

                totalAppliedForce += particle.GetForce(this);
            }
            this.rigidbody.AddForce(totalAppliedForce);
            if (totalAppliedForce.magnitude > 20)
            {
                totalAppliedForce = Vector3.ClampMagnitude(totalAppliedForce, 20);
            }

            Vector3[] positions = { this.transform.position, 2 * totalAppliedForce + this.transform.position };
            line.SetPositions(positions);
        }
        else
        {
            this.rigidbody.velocity = Vector3.zero;
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

    public Vector3 GetForce(MovableChargedParticle particle)
    {
        return CalculateElectricForce(particle) + CalculateMagneticForce(particle);
    }

}
