namespace Reggie.Upward.Blockchain
{
    /// <summary>
    /// 交易
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 容器
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender { get; set; }
    }
}