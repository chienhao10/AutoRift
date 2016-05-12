using System.Collections.Generic;
using AutoRift.Data;

namespace AutoRift.Logic
{
    public interface ILogic
    {
        /// <summary>
        ///     Called when LogicManager has loaded everything
        /// </summary>
        void Init();

        /// <summary>
        ///     Called when the logic starts (is enabled)
        ///     Use this to hook any events.
        /// </summary>
        void Start();

        /// <summary>
        ///     Called when the logic ends (is disabled)
        ///     Use this to remove any hooked events.
        /// </summary>
        void End();

        /// <summary>
        ///     Called before every `Update`, should return where you wish the player to move to
        /// </summary>
        /// <returns>
        ///     Where the player should Orbwalk to, and what Orbwalker.ActiveMode flags should be active.
        ///     Return null if you wish to not change the path.
        /// </returns>
        MovementData GetMovementData();


        /// <summary>
        ///     Called after every `GetMovementData`, this should include your common logic for what do to (cast spells, set
        ///     target, attack, etc; and NOT where to move to!)
        /// </summary>
        void Update();
        /// <summary>
        /// Called to check if the current logic has finished its purpose, if it has (true) and there is a better logic to select, it will end the current logic.
        /// </summary>
        bool Finished();

        Status StatusMessages();
    }
}