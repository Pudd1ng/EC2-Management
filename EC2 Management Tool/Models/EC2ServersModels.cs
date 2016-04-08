using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2_Management_Tool.Models
{
    public class EC2ServersModels
    {
        public AmazonEC2Client client = new AmazonEC2Client();

        public void newInstance()
        {
            
        }

        public void deleteInstance()
        {

        }

        public void startInstance()
        {

        }

        public void stopInstance()
        {

        }

        public object getInstances()
        {
            var allEC2Requests = new DescribeInstancesRequest();
            var allEC2Response = client.DescribeInstances(allEC2Requests);
            return allEC2Response;
        }

    }
}