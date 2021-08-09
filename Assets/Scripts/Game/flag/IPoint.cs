namespace Game.flag
{
    public interface IPoint
    {
        /// <summary>
        /// 是否被占领
        /// </summary>
        /// <returns></returns>
        bool IsOccupied();

        /// <summary>
        /// 占领方的标记
        /// </summary>
        /// <returns></returns>
        string GetOccupiedSign();

        /// <summary>
        /// 更换标记
        /// </summary>
        /// <param name="sign"></param>
        void ChangeOccupiedSign(string sign);
    }
}