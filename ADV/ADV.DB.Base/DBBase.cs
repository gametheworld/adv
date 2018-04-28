using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.DB
{


    /// <summary>
    /// 基类 析构
    /// </summary>
    public class DBBase : IDisposable
    {
        // Fields
        private bool _isdisposed;

        /// <summary>
        /// 基类构造函数

        /// </summary>
        public DBBase()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._isdisposed)
            {
                this._isdisposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~DBBase()
        {
            this.Dispose(false);
        }
    }
}
