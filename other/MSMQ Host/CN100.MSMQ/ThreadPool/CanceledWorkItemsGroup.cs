namespace CN100.MSMQ.ThreadPool.Internal
{
    internal class CanceledWorkItemsGroup
    {
        public readonly static CanceledWorkItemsGroup NotCanceledWorkItemsGroup = new CanceledWorkItemsGroup();

        public CanceledWorkItemsGroup()
        {
            IsCanceled = false;
        }

        public bool IsCanceled { get; set; }
    }
}