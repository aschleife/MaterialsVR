﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using UInput = UnityEngine.Input;

namespace Microsoft.MixedReality.Toolkit.Input
{
    /// <summary>
    /// Uses the desktop mouse cursor instead of any mouse representation within the scene.
    /// It's movement is bound to screenspace.
    /// </summary>
    public class ScreenSpaceMousePointer : BaseMousePointer
    {
        private Vector2 lastMousePosition;

        protected override string ControllerName => "ScreenSpace Mouse Pointer";

        /// <inheritdoc />
        public override void OnPreSceneQuery()
        {
            if (UInput.mousePosition.x < 0 ||
                UInput.mousePosition.y < 0 ||
                UInput.mousePosition.x > Screen.width ||
                UInput.mousePosition.y > Screen.height)
            {
                return;
            }

            Vector3 currentMousePosition = UInput.mousePosition;

            if ((lastMousePosition - (Vector2)currentMousePosition).magnitude >= MovementThresholdToUnHide)
            {
                SetVisibility(true);
            }

            lastMousePosition = currentMousePosition;

            Camera mainCamera = CameraCache.Main;
            Ray ray = mainCamera.ScreenPointToRay(currentMousePosition);
            Rays[0].CopyRay(ray, float.MaxValue);

            transform.position = mainCamera.transform.position;
            transform.rotation = Quaternion.LookRotation(ray.direction);
        }

        protected override void SetVisibility(bool visible)
        {
            base.SetVisibility(visible);
            Cursor.visible = visible;
        }
    }
}