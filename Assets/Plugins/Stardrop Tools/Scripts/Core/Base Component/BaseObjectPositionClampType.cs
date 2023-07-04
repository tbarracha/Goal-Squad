
namespace StardropTools
{
    /// <summary>
    /// Clamp base objects axis values from Positive and Negative (-value, to value), Positive Only (0, to Value) or Negative Only (-value, to 0)
    /// </summary>
    public enum BaseObjectPositionClampType
    {
        /// <summary>
        /// Clamp values from (-value, to value)
        /// </summary>
        Both,

        /// <summary>
        /// Clamp values from (0, to value)
        /// </summary>
        Positive,

        /// <summary>
        /// Clamp values from (-value, to 0)
        /// </summary>
        Negative,
    }
}