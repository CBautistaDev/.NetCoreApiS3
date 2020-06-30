using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using LifeBackup.Core.Communication.Bucket;
using LifeBackup.Core.Communication.Interfaces;

namespace LifeBackup.Infrastructure.Repositories
{
    public class BucketRepository : IBucketRepository
    {

        private readonly IAmazonS3 _s3Client;

        public BucketRepository(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<bool> DoesS3BucketExist(string bucktName)
        {
            return await _s3Client.DoesS3BucketExistAsync(bucktName);
        }

        public async Task<CreateBucketResponse> CreateBucket(string bucketname)
        {
            var putBucketRequest = new PutBucketRequest

            {
                BucketName = bucketname,
                UseClientRegion = true
            };
            var response = await _s3Client.PutBucketAsync(putBucketRequest);

            return new CreateBucketResponse
            {
                BucketName = bucketname,
                RequestID = response.ResponseMetadata.RequestId


            };

        }
      
    }
}
