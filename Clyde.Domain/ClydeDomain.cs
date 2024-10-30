using System;

namespace Clyde.Domain
{
    /// <summary>
    /// Utility class for domain driven operations
    /// </summary>
    public static class ClydeDomain
    {
        #region Constants

        /// <summary> Name of the application </summary>
        public const string APPLICATION_NAME = "CLYDE";

        /// <summary> Timeformat for <see cref="TimeSpan.ToString(string)"/></summary>
        public const string TIME_FORMAT = @"hh\:mm\:ss";

        #endregion

        #region Methods

        /// <summary>
        /// Formats the <see cref="IStateHolder.Duration"/> into a string the repesents the time
        /// </summary>
        /// <param name="stateHolder"></param>
        /// <returns>A formated <see cref="IStateHolder.Duration"/></returns>
        public static string GetFormattedDuration(this IStateHolder stateHolder)
            => stateHolder.Duration.ToString(TIME_FORMAT);

        /// <summary>
        /// Checks if the <see cref="IStateHolder"/> can successfully perform <see cref="IStateHolder.Start"/>
        /// </summary>
        /// <param name="stateHolder">The <see cref="IStateHolder"/></param>
        /// <returns><see langword="true"/> if <paramref name="stateHolder"/> can perform <see cref="IStateHolder.Start"/> else <see langword="false"/></returns>
        public static bool CanStart(this IStateHolder stateHolder)
            => stateHolder.State == ExecutionState.Idle;

        /// <summary>
        /// Checks if the <see cref="IStateHolder"/> can successfully perform <see cref="IStateHolder.Stop"/>
        /// </summary>
        /// <param name="stateHolder">The <see cref="IStateHolder"/></param>
        /// <returns><see langword="true"/> if <paramref name="stateHolder"/> can perform <see cref="IStateHolder.Stop"/> else <see langword="false"/></returns>
        public static bool CanStop(this IStateHolder stateHolder)
            => stateHolder.State == ExecutionState.Running;

        #endregion Methods
    }
}
