using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    public class Despatch : IDisposable
    {
        private Queue<IDespatchItem> mItems = new Queue<IDespatchItem>();

        private bool mIsDisposed = false;

        public Despatch()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(OnRun);
        }

        public void Add(IDespatchItem item)
        {
            lock (this)
            {
                mItems.Enqueue(item);
            }
        }

        private IDespatchItem GetItem()
        {
            lock (this)
            {
                if (mItems.Count > 0)
                    return mItems.Dequeue();
                return null;
            }
        }

        private void OnRun(object state)
        {
            while (!mIsDisposed)
            {
                IDespatchItem item = GetItem();
                if (item != null)
                {
                    try
                    {
                        using (item)
                        {
                            item.Execute();
                        }
                    }
                    catch
                    {
                    }
                }
                else
                    System.Threading.Thread.Sleep(50);
            }
        }

        public void Dispose()
        {
            lock (this)
            {
                if (!mIsDisposed)
                {
                    mIsDisposed = true;
                    while (mItems.Count > 0)
                    {
                        mItems.Dequeue().Dispose();
                    }
                }
            }
        }
    }
}
