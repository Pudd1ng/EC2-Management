using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2_Management_Tool.Models
{
    public class CloudWatchModel
    {
        public AmazonEC2Client client = new AmazonEC2Client();

        public void EnableMonitoring(string instanceId)
        {
            var enableMonitoringRequest = new MonitorInstancesRequest();
            enableMonitoringRequest.InstanceIds = new List<string>() { instanceId };
            var enableMonitoringResponse = client.MonitorInstances(enableMonitoringRequest);
        }

    }
}