using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyDebug
{


public class EnemyBase : MonoBehaviour {

    public Transform Target; 
    public Transform ShootPoint; 
    public float LerpSpeed = 1f; 
    public float ShootingTime = 3f; 
    public float MaxAngleToShoot = 10f; 
    public float LaserSpeed = 1f; 
    public float MaxLaserDistance = 50f; 
    public float MinWidthMultiplier = 0f; 
    public float MaxWidthMultiplier = 3f; 
    public float WidthMultiplierLerpSpeed = 1f; 

    public enum EnemyStates {move, laser, balls};
    public EnemyStates State; 

    LineRenderer line_renderer; 

    Vector3 dir_to_target; 
    float shoot_time; 
    Quaternion rot_ini; 
    [SerializeField] float angle; 

	// Use this for initialization
	void Start () {


        line_renderer = GetComponent<LineRenderer>(); 
        shoot_time = 0f; 

        line_renderer.enabled = false; 
        rot_ini = transform.rotation; 
		
	}
	
	// Update is called once per frame
	void Update () {

        if(State == EnemyStates.move)
        {
            angle = Rotate(); 
            if(Mathf.Abs(angle) < MaxAngleToShoot)
            {
                State = EnemyStates.laser;
                shoot_time  = ShootingTime;
                line_renderer.enabled = true; 
                ResetLineRenderer();   
            }
        }
        else if(State == EnemyStates.laser)
        {
            Shoot(); 
            shoot_time -= Time.deltaTime; 
            if(shoot_time < 0f)
            {
                State = EnemyStates.move; 
                line_renderer.enabled = false; 
            }

        }
		
	}

    void Shoot()
    {
        Vector3 p1 = transform.position; 
        Vector3 p2 = line_renderer.GetPosition(1); 

        line_renderer.SetPosition(0, p1); 
        p2 = (p1 - p2).magnitude < MaxLaserDistance ? p2 + dir_to_target*LaserSpeed*Time.deltaTime : p2; 
        line_renderer.SetPosition(1, p2); 
        line_renderer.widthMultiplier = Mathf.MoveTowards(line_renderer.widthMultiplier, MinWidthMultiplier, WidthMultiplierLerpSpeed*Time.deltaTime);
    }

    void ResetLineRenderer()
    {
        Vector3[] ini = new Vector3 []{transform.position, transform.position};  
        line_renderer.SetPositions(ini); 
        line_renderer.widthMultiplier = MaxWidthMultiplier; 
    }
    float Rotate()
    {
        dir_to_target = Target.position - ShootPoint.position; 
        Vector3 plane_dir = Vector3.ProjectOnPlane(dir_to_target, Vector3.up);

        float s_angle = Vector3.SignedAngle(plane_dir, ShootPoint.forward, Vector3.up); 
        Quaternion rot =  Quaternion.AngleAxis(s_angle, Vector3.up)*transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime*LerpSpeed); 
        // transform.rotation = Quaternion.Slerp(transform.rotation, rot_ini*Quaternion.LookRotation(plane_dir, Vector3.up), LerpSpeed*Time.deltaTime);  
    
        return s_angle; 
    }

    float GetAngle()
    {
        return Mathf.Abs(Vector3.Angle(transform.forward, dir_to_target));
    }
}


}
