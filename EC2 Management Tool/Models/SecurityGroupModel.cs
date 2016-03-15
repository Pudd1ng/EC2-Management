using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2_Management_Tool.Models
{
    public class SecurityGroupModel
    {
        public AmazonEC2Client client = new AmazonEC2Client();

        public void CreateGroup(string groupName, string groupDescription, string ipRange, string protocol, int fromPort, int toPort)
        {
            //setup a new request
            var newSGRequest = new CreateSecurityGroupRequest()
            {
                GroupName = groupName,
                Description = groupDescription
            };
            //create the group
            var csgResponse = client.CreateSecurityGroup(newSGRequest);
            //load up newly creating group into var
            List<string> Groups = new List<string>() { csgResponse.GroupId };
            var newSgRequest = new DescribeSecurityGroupsRequest() { GroupIds = Groups };
            var newSgResponse = client.DescribeSecurityGroups(newSgRequest);
            var mySG = newSgResponse.SecurityGroups[0];
            //create the rules
            List<string> ranges = new List<string>() { ipRange };
            var ipPermission = new IpPermission()
            {
                IpProtocol = protocol,
                FromPort = fromPort,
                ToPort = toPort,
                IpRanges = ranges
            };
            //attach the rules to the group
            var ingressRequest = new AuthorizeSecurityGroupIngressRequest();
            ingressRequest.GroupId = mySG.GroupId;
            ingressRequest.IpPermissions.Add(ipPermission);
        }
    }
}