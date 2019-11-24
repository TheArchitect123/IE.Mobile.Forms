using System;
using System.Collections.Generic;
using System.Text;

namespace IE.Mobile.Forms.Services
{
    public interface IApplicationService : IDisposable
    {
        /// <summary>
        /// Initializes the service synchronously on application startup.
        /// </summary>
        void Initialize();
    }
}
