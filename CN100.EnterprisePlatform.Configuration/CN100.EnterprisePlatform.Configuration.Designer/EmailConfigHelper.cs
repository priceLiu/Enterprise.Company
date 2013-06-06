// -----------------------------------------------------------------------
// <copyright file="EmailConfigHelper.cs" company="CN100.COM">
// Email配置节帮助类
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Configuration
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Email配置节帮助类
    /// </summary>
    [Guid("E2FADB77-1E2E-41D4-9505-D753CAEDB669")]
    public class EmailConfigHelper
    {
        readonly int residue = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigHelper"/> class.
        /// </summary>
        public EmailConfigHelper()
        {
            residue = Environment.TickCount % EmailSection.Instance.Hosts.Count;
        }

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName
        {
            get
            {
                return EmailSection.Instance.Hosts.GetItemAt(residue).Name;
            }
        }
        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address
        {
            get
            {
                return EmailSection.Instance.Hosts.GetItemAt(residue).Address;
            }
        }
        /// <summary>
        /// Gets the login account.
        /// </summary>
        public string LoginAccount
        {
            get
            {
                return EmailSection.Instance.Hosts.GetItemAt(residue).Account;
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return EmailSection.Instance.Hosts.GetItemAt(residue).Password;
            }
        }
    }
}
