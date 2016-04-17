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

        public void CreateInstance(string amiId, string keyPairName, string instanceType, int minCount, int maxCount, string group)
        {
            //set the blank id
            string groupId = "";
            //pull the groups
            var dsgRequest = new DescribeSecurityGroupsRequest();
            var dsgResponse = client.DescribeSecurityGroups(dsgRequest);
            List<SecurityGroup> mySGs = dsgResponse.SecurityGroups;
            //look for the requested group anme
            foreach(SecurityGroup item in mySGs)
            {
                //attach the id if the group name is the same
                if (item.GroupName == group)
                {
                    groupId = item.GroupId;
                }
            }
            //cast it to a list
            List<string> groupsList = new List<string>()
            {
                groupId
            };
            //setup the request
            var launchRequest = new RunInstancesRequest()
            {
                ImageId = amiId,
                InstanceType = instanceType,
                MinCount = minCount,
                MaxCount = maxCount,
                KeyName = keyPairName,
                SecurityGroupIds = groupsList
            };
            //launch the instances
            var launchresponse = client.RunInstances(launchRequest);
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