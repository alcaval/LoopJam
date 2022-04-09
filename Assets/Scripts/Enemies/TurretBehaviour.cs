/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFMEsada
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
        [SerializeField] private float _rotationSpeed;

        [Tooltip("How close the turret must be to its target to shoot.")]
        [SerializeField] private float _rotationThreshold;

        [Header("Shoot options")]

        [Tooltip("Prefab of the bullet the turret shoots.")]
        [SerializeField] private GameObject _bulletPrefab;

        [Tooltip("Time between bullets.")]
        [SerializeField] private float _shootSpeed;

	    #endregion
	 
	    #region LifeCycle

        private void Update() 
        {
            var direction = (_objective.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            // lookRotation = Quaternion.Euler(new Vector3(lookRotation.eulerAngles.x, 90, -90));

            Debug.Log(lookRotation.eulerAngles);

            

            {
                // Rotate towards objective.
                // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
                transform.rotation = lookRotation;
            }
        }
      
        #endregion
    }
}
