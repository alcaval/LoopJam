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
	  
	    #endregion
	  
	    #region LifeCycle
	  
        // Start, OnAwake, Update, etc
        private void Start() 
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * _speed;
        }

        #endregion
    }
}
