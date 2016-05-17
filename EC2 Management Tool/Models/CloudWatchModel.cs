using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace EC2_Management_Tool.Models
{
    public class CloudWatchModel
    {
        public AmazonEC2Client client = new AmazonEC2Client();
        public AmazonCloudWatchClient client2 = new AmazonCloudWatchClient();

        public void EnableMonitoring(string instanceId)
        {
            var enableMonitoringRequest = new MonitorInstancesRequest();
            enableMonitoringRequest.InstanceIds = new List<string>() { instanceId };
            var enableMonitoringResponse = client.MonitorInstances(enableMonitoringRequest);
        }

        public object GetMetric(string metricName, string instanceId)
        {
            var dimension = new Dimension
            {
                Name = "InstanceId",
                Value = instanceId
            };

            var getMetricRequest = new GetMetricStatisticsRequest
            {
                Dimensions = new List<Dimension>() { dimension },
                EndTime = DateTime.Today,
                MetricName = metricName,
                Namespace = "AWS/EC2",
                Period = (int)TimeSpan.FromDays(1).TotalSeconds,
                StartTime = DateTime.Today.Subtract(TimeSpan.FromDays(7)),
                Statistics = new List<string> { "Average" },
                Unit = StandardUnit.Percent
            };

            var getMetricResponse = client2.GetMetricStatistics(getMetricRequest);
            return getMetricResponse; 
        }

    }
}