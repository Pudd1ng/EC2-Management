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

        public object getFreeVolumes()
        {
            List<string> freeIds = new List<string>();
            var allS3Reqeust = new DescribeVolumesRequest();
            var allS3Response = client.DescribeVolumes(allS3Reqeust);
            foreach(var volume in allS3Response.Volumes)
            {
                if (volume.Attachments.Count == 0 )
                {
                    freeIds.Add(volume.VolumeId);
                }
            }
            return freeIds;
        }

        public void DeleteVolume(string volumeId)
        {
            {
                var s3DeleteRequest = new DeleteVolumeRequest();
                s3DeleteRequest.VolumeId = volumeId;
                var s3DeleteResponse = client.DeleteVolume(s3DeleteRequest);
            }
        }

        public void CreateVolume(int size, string type, string zone)
        {
            var s3CreateRequest = new CreateVolumeRequest()
            {
               AvailabilityZone = zone,
               Size = size,
               VolumeType = type,
            };
            var s3CreateResponse = client.CreateVolume(s3CreateRequest);
        }

        public object AttachVolume(string volumeId, string instanceId, string device)
        {
            try {
                var attachRequest = new AttachVolumeRequest()
                {
                    InstanceId = instanceId,
                    VolumeId = volumeId,
                    Device = device
                };
                var attachResponse = client.AttachVolume(attachRequest);
                return 0;
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public void DetachVolume(string volumeId, string instanceId, string device)
        {
            var DetachRequest = new DetachVolumeRequest()
            {
                InstanceId = instanceId,
                VolumeId = volumeId,
                Device = device
            };

            var DeleteResponse = client.DetachVolume(DetachRequest);
        }
    }
}