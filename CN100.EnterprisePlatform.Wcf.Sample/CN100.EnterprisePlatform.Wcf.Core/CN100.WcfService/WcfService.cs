using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace CN100.WcfService
{
    public partial class WcfService : ServiceBase
    {
        LoadWcfService service;
        public WcfService()
        {
            InitializeComponent();
            service = new LoadWcfService();
        }

        protected override void OnStart(string[] args)
        {
            service.Start();
            LoadWcfService.Log.InfoFormat("{0}{1}Start......",WcfServiceSection.Instance.ServiceName,DateTime.Now);
        }

        protected override void OnStop()
        {
            service.Stop();
            LoadWcfService.Log.InfoFormat("{0}{1}Stop......", WcfServiceSection.Instance.ServiceName, DateTime.Now);
        }
    }
}
