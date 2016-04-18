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

        public void CreateInstance(string amiId, string keyPairName,string instanceName, string instanceType, int minCount, int maxCount, string group)
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
            //setup the request. As the InstanceType class cannot be passed from a view you need to create a seperate request for future types.
            if (instanceType == "T2Micro")
            {
                var launchRequest = new RunInstancesRequest()
                {
                    ImageId = amiId,
                    InstanceType = InstanceType.T2Micro,
                    MinCount = minCount,
                    MaxCount = maxCount,
                    KeyName = keyPairName,
                    SecurityGroupIds = groupsList
                };
                //launch the instances
                var launchresponse = client.RunInstances(launchRequest);
                //set the instance in a variable
                var myInstance = launchresponse.Reservation.Instances[0].InstanceId;
                //set up the naming request
                var tagRequest = new CreateTagsRequest();
                tagRequest.Resources.Add(myInstance);
                tagRequest.Tags.Add(new Tag {Key = "Name", Value = instanceName });
                client.CreateTags(tagRequest);
            }
        }

        public void DeleteInstance(string instanceId)
        {
            var deleteRequest = new TerminateInstancesRequest();
            deleteRequest.InstanceIds = new List<string>() { instanceId };
            var deleteResponse = client.TerminateInstances(deleteRequest);
        }

        public void StartInstance(string instanceId)
        {
            var startRequest = new StartInstancesRequest();
            startRequest.InstanceIds = new List<string> { instanceId };
            var startResponse = client.StartInstances(startRequest);
        }

        public void StopInstance(string instanceId)
        {
            var stopRequest = new StopInstancesRequest();
            stopRequest.InstanceIds = new List<string> { instanceId };
            var stopReponse = client.StopInstances(stopRequest);
        }

        public object getInstances()
        {
            var allEC2Requests = new DescribeInstancesRequest();
            var allEC2Response = client.DescribeInstances(allEC2Requests);
            return allEC2Response;
        }

    }
}