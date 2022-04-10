/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopJam
{
    /// <summary>  
	/// AI for the turret enemy, which tracks the player, rotates towards it and shoots at it.
	/// </summary>
    public class TurretBehaviour : MonoBehaviour
    {
        #region Fields

        [Header("Tracking options")]

        [Tooltip("Objective to destroy.")]
        [SerializeField] private Transform _objective;

        [Tooltip("Speed the turret rotates towards its objective.")]
        [Min(0.01f)]
        [SerializeField] private float _rotationSpeed;

        [Tooltip("How close the turret must be to its target to shoot.")]
        [SerializeField] private float _rotationThreshold;

        [Header("Shoot options")]

        [Tooltip("Prefab of the bullet the turret shoots.")]
        [SerializeField] private GameObject _bulletPrefab;

        [Tooltip("Time between bullets.")]
        [SerializeField] private float _shootSpeed;
        private float _timeSinceShot;

        #endregion

        #region LifeCycle

        private void Awake() 
        {
            _objective = GameObject.FindGameObjectWithTag("Car").transform;    
        }

        private void Update()
        {
            if(!rotateTowardsObjective())
            {
                // Shoot
                if(_timeSinceShot >= _shootSpeed)
                {
                    Instantiate(_bulletPrefab, transform.position + transform.right, transform.rotation);
                    _timeSinceShot = 0;
                }
                else
                {
                    _timeSinceShot += Time.deltaTime;
                }
            }
            else
            {
                _timeSinceShot = _shootSpeed/2;
            }
        }

        #endregion


        #region Private Methods

        private bool rotateTowardsObjective()
        {
            Vector3 targ = _objective.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

            if(Quaternion.Angle(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle))) > _rotationThreshold)
            {
                angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, _rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                return true;
            }

            return false;
        }

        #endregion
    }
}
