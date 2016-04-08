using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.EC2;
using Amazon.EC2.Model;

namespace EC2_Management_Tool.Models
{
    public class S3Model
    {
        public AmazonEC2Client client = new AmazonEC2Client();

        public object getVolumes()
        {
            var allS3Reqeust = new DescribeVolumesRequest();
            var allS3Response = client.DescribeVolumes(allS3Reqeust);
            return allS3Response;
        }
    }
}