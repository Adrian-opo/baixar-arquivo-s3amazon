using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;


namespace S3.Demo.API.Controllers;

[Route("api/files")]
[ApiController]
public class Dowload : ControllerBase
{
    private readonly IAmazonS3 s3Dwl;
    public Dowload(IAmazonS3 s3Donwload)
    {
        s3Dwl = s3Donwload;
    }

    [HttpGet("Donwload")]
    public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string archiveName)
    {
        //verificação
        var verificacaoBck = await s3Dwl.DoesS3BucketExistAsync(bucketName);
        if (!verificacaoBck) 
        return NotFound($"Bucket {bucketName} não encontrado.");

        
        var s3archive = await s3Dwl.GetObjectAsync(bucketName, archiveName);
        return File(s3archive.ResponseStream, s3archive.Headers.ContentType);
    }
}


