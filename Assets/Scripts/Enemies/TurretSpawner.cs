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
        [SerializeField] private CarController _playerController;

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
                Debug.Log("Magnitude: " + _playerController.Velocity.magnitude);
                if(_playerController.Velocity.magnitude > 15f) 
                {
                    _timeToSpawn = 0.25f;
                    _maxDistance = 20f;
                    _minDistance = 10f;
                }
                else if(_playerController.Velocity.magnitude > 5f) 
                {
                    _timeToSpawn = 0.5f;
                    _maxDistance = 15f;
                    _minDistance = 7f;
                }
                else
                {
                    _timeToSpawn = 2f;
                    _maxDistance = 10f;
                    _minDistance = 5f;
                }

                yield return new WaitForSeconds(_timeToSpawn);
                InstantiateTurret();
            }
        }

        private void InstantiateTurret()
        {
            Vector2 pos = _playerTransform.position + _playerTransform.gameObject.GetComponent<CarController>().Velocity;
            Vector2 point;
            bool near;
            do
            {   
                near = false;
                point = pos + Random.insideUnitCircle * _maxDistance;
                var active = TurretPool.SharedInstance.GetActiveObjects();
                for(int i=0; i < active.Count; ++i)
                {
                    if(Vector2.Distance(point, active[i].transform.position) < 2.5f)
                    {
                        near = true;
                        break;
                    }
                }

            }while(Vector2.Distance(point, pos) < _minDistance || near);

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
