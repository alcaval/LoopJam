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
	/// Spawns turrets in a radius around the player.
	/// </summary>
    public class TurretSpawner : MonoBehaviour
    {
        #region Fields
      
        // [Tooltip("Public variables set in the Inspector, should have a Tooltip")]
        /// <summary>  
	    /// They should also have a summary
	    /// </summary>
        // public static string Ejemplo;
	  
	    // private float _ejemplo;

        [SerializeField] private Transform _playerTransform;

        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDistance;

        [SerializeField] private float _timeToSpawn;

	    #endregion
	 
	    #region LifeCycle
	  
        private void OnEnable() 
        {
            StartCoroutine(crSpawnTurret());    
        }

        private void OnDisable() 
        {
            StopAllCoroutines();
        }
      
        #endregion

        #region Private Methods

        private IEnumerator crSpawnTurret()
        {
            while(true)
            {
                yield return new WaitForSeconds(_timeToSpawn);
                InstantiateTurret();
            }
        }

        private void InstantiateTurret()
        {
            Vector2 pos = _playerTransform.position;
            Vector2 point;
            do
            {   
                point = pos + Random.insideUnitCircle * _maxDistance;
            }while(Vector2.Distance(point, pos) < _minDistance);

            GameObject turret = TurretPool.SharedInstance.GetPooledObject();
            if (turret != null)
            {
                turret.transform.position = point;
                turret.transform.rotation = Quaternion.Euler(Vector3.right * -90f);
                turret.SetActive(true);
            }
        }
	   
        #endregion
    }
}
