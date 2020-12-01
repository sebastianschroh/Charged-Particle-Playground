using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedParticle : MonoBehaviour
{
    public float charge = 1.0f;
    protected static List<ChargedParticle> chargedParticles;
    protected const float Kelec = 1000;
    // Start is called before the first frame update
    void Start()
    {
        ColorParticle();
        if(chargedParticles == null)
            chargedParticles = new List<ChargedParticle>();

        chargedParticles.Add(this);
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

    protected Vector3 CalculateElectricForce(ChargedParticle particle)
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

    public virtual Vector3 GetForce(ChargedParticle particle)
    {
        return CalculateElectricForce(particle);
    }


}
