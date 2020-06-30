using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeBackup.Core.Communication.Bucket;
using LifeBackup.Core.Communication.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LifeBackupApi.Controllers


{

    [Route("api/bucket")]
    [ApiController]

    public class BucketController : ControllerBase
    {
        private readonly IBucketRepository _bucketRepository;

        public BucketController (IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;

        }


        [HttpPost]
        [Route("create/{bucketname}")]
        public async Task<ActionResult<CreateBucketResponse>> CreateS3Bucket([FromRoute] string bucketName)
        {
            var BucketExists = await _bucketRepository.DoesS3BucketExist(bucketName);
            if (BucketExists)
            {
                return BadRequest("S3 bucket already exits");

            }

            var result = await _bucketRepository.CreateBucket(bucketName);

            if (result == null){
                return BadRequest();
            }

            return Ok(result);


    }
    }

  

}
