/*
* Farseer Physics Engine:
* Copyright (c) 2012 Ian Qvist
*
* Original source Box2D:
* Copyright (c) 2006-2011 Erin Catto http://www.box2d.org
*
* This software is provided 'as-is', without any express or implied
* warranty.  In no event will the authors be held liable for any damages
* arising from the use of this software.
* Permission is granted to anyone to use this software for any purpose,
* including commercial applications, and to alter it and redistribute it
* freely, subject to the following restrictions:
* 1. The origin of this software must not be misrepresented; you must not
* claim that you wrote the original software. If you use this software
* in a product, an acknowledgment in the product documentation would be
* appreciated but is not required.
* 2. Altered source versions must be plainly marked as such, and must not be
* misrepresented as being the original software.
* 3. This notice may not be removed or altered from any source distribution.
*/

namespace Robust.Shared.Physics.Collision
{
    /// <summary>
    /// This is used for determining the state of contact points.
    /// </summary>
    public enum PointState : byte
    {
        /// <summary>
        /// Point does not exist
        /// </summary>
        Null,

        /// <summary>
        /// Point was added in the update
        /// </summary>
        Add,

        /// <summary>
        /// Point persisted across the update
        /// </summary>
        Persist,

        /// <summary>
        /// Point was removed in the update
        /// </summary>
        Remove,
    }
}
