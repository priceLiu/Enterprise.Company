using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN100.EnterprisePlatform.Web.Core
{
    public interface IDespatchItem : IDisposable
    {
        void Execute();

        Action<IDespatchItem> Completed
        {
            get;
            set;
        }
    }
    public abstract class DespatchItem : IDespatchItem
    {
        private bool mIsDisposed = false;

        public abstract void Execute();

        protected virtual void OnDisposed()
        {
            try
            {
                if (Completed != null)
                {
                    Completed(this);
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            lock (this)
            {
                if (!mIsDisposed)
                {
                    mIsDisposed = true;
                    OnDisposed();
                }
            }
        }

        public Action<IDespatchItem> Completed
        {
            get;
            set;
        }
    }
}
