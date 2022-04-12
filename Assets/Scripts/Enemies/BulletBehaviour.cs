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
	/// AI for bullets, which travels along their forward on creation
	/// </summary>
    public class BulletBehaviour : MonoBehaviour
    {
        #region Fields

        [Tooltip("Bullet speed when created.")]
	    [SerializeField] private float _speed;
	    [SerializeField] private float _lifeTime;

	  
	    #endregion
	  
	    #region LifeCycle
	  
        // Start, OnAwake, Update, etc
        private void OnEnable()   
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
            StartCoroutine(crDestroy());
        }

        private void OnDisable() 
        {
            StopAllCoroutines();
        }

        #endregion

        private IEnumerator crDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other) 
        {
            if(other.gameObject.tag == "Car")
            {
                other.gameObject.GetComponent<CarController>().takeDamage(20f);
            }    
            
            gameObject.SetActive(false);
        }
    }
}
