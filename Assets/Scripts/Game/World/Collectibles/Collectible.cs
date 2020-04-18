﻿using UnityEngine;

namespace Gang1057.Ludiwuri.Game.World.Collectibles
{

    /// <summary>
    /// Base class for all collectibles
    /// </summary>
    public abstract class Collectible : MonoBehaviour, IInteractable
    {

        #region Methods

        /// <inheritdoc/>
        public void Interact()
        {
            // Collect this collectible

            Collect();
        }

        /// <summary>
        /// Called when the pickup is collected
        /// </summary>
        protected abstract void OnCollected();

        /// <summary>
        /// Disposes of the collectible
        /// </summary>
        protected virtual void Dispose()
        {
            Destroy(gameObject);
        }


        /// <summary>
        /// Collects this collectible
        /// </summary>
        private void Collect()
        {
            // Handle collection logic

            OnCollected();

            // Dispose of the collectible

            Dispose();
        }

        #endregion

    }

}