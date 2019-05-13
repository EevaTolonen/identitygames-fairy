using System.Collections;
using UnityEngine;

public class pendulum : MonoBehaviour
{
    #region Public Variables
    public Rigidbody2D body2d;
    public float vasenTyontoPituus;
    public float oikeaTyontoPituus;
    public float velocityThreshold;
    #endregion

    #region Private Variables
    #endregion

    #region Main Methods
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Tyonna();
    }
    #endregion

    #region Utility Methods
    public void Tyonna()
    {
        if (transform.rotation.z > 0 
            && transform.rotation.z < oikeaTyontoPituus
            && (body2d.angularVelocity > 0)
            && body2d.angularVelocity < velocityThreshold)
        {
            body2d.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
        && transform.rotation.z > vasenTyontoPituus
        && (body2d.angularVelocity < 0)
        && body2d.angularVelocity > velocityThreshold * -1)
        {
            body2d.angularVelocity = velocityThreshold * -1;
        }
     }
    #endregion
}