using System;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Configuration;

using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;


namespace R5T.Bristol.AWS.S3
{
    public static class Construction
    {
        public static void SubMain()
        {
            //Construction.CreateBucketSetup();
            //Construction.CreateBucket();
            //Construction.TestIfBucketExists(Construction.GenerateRandomBucketName());
            //Construction.TestIfBucketExists();
            //Construction.DeleteBucket();
            //Construction.UploadFileToS3();
            //Construction.TestIfFileExists();
            //Construction.TestIfS3ObjectExists("DOES_NOT_EXIST");
            Construction.DeleteObjectFromS3();
        }

        private static void DeleteObjectFromS3()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            var imageFilePath = configuration["ImageFilePath"];
            var objectKey = Construction.GetKeyNameFromFilePath(imageFilePath);

            using (var s3Client = Construction.GetAmazonS3Client())
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectKey,
                };

                var deleteObjectResponse = s3Client.DeleteObjectAsync(deleteObjectRequest).Result;

                Construction.DisplayDeleteObjectResponse(deleteObjectResponse);
            }
        }

        private static void DisplayDeleteObjectResponse(DeleteObjectResponse deleteObjectResponse)
        {
            var writer = Console.Out;

            writer.WriteLine($"{nameof(deleteObjectResponse.HttpStatusCode)}: {deleteObjectResponse.HttpStatusCode}");
            Construction.DisplayResponseMetadata(writer, deleteObjectResponse.ResponseMetadata);
        }

        private static void TestIfFileExists()
        {
            var configuration = Construction.GetConfiguration();

            var imageFilePath = configuration["ImageFilePath"];
            var objectKey = Construction.GetKeyNameFromFilePath(imageFilePath);

            Construction.TestIfS3ObjectExists(objectKey);
        }

        private static void TestIfS3ObjectExists(string objectKey)
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            using (var s3Client = Construction.GetAmazonS3Client())
            {
                var objectExists = Construction.S3ObjectExists(s3Client, bucketName, objectKey);
                if (objectExists)
                {
                    Console.WriteLine($"Object '{objectKey}' exists!");
                }
                else
                {
                    Console.WriteLine($"Object '{objectKey}' does NOT exist...");
                }
            }
        }

        /// <summary>
        /// Uses low-level ListObjects API.
        /// </summary>
        private static bool S3ObjectExists(IAmazonS3 s3Client, string bucketName, string objectKey)
        {
            var listObjectsRequest = new ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = objectKey,
                MaxKeys = 1
            };

            var listObjectsResponse = s3Client.ListObjectsAsync(listObjectsRequest).Result;

            var fileExists = listObjectsResponse.S3Objects.Any();
            return fileExists;
        }
        
        private static void UploadFileToS3()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            var imageFilePath = configuration["ImageFilePath"];

            using (var s3Client = Construction.GetAmazonS3Client())
            using (var fileTransferUtility = new TransferUtility(s3Client))
            {
                fileTransferUtility.UploadAsync(imageFilePath, bucketName).Wait();
            }
        }

        private static string GetKeyNameFromFilePath(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            var keyName = Construction.GetKeyNameFromFileName(fileName);
            return keyName;
        }

        /// <summary>
        /// Default key-name is the same as the file-name.
        /// </summary>
        private static string GetKeyNameFromFileName(string fileName)
        {
            return fileName;
        }

        private static void DeleteBucket()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            using (var s3Client = Construction.GetAmazonS3Client())
            {
                var deleteBucketRequest = new DeleteBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true,
                };

                var deleteBucketResponse = s3Client.DeleteBucketAsync(deleteBucketRequest).Result;

                Construction.DisplayDeleteBucketResponse(deleteBucketResponse);
            }
        }

        private static void DisplayDeleteBucketResponse(DeleteBucketResponse deleteBucketResponse)
        {
            var writer = Console.Out;

            writer.WriteLine($"{nameof(deleteBucketResponse.HttpStatusCode)}: {deleteBucketResponse.HttpStatusCode}");
            Construction.DisplayResponseMetadata(writer, deleteBucketResponse.ResponseMetadata);
        }

        private static void TestIfBucketExists()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            Construction.TestIfBucketExists(bucketName);
        }

        private static void TestIfBucketExists(string bucketName)
        {
            using (var s3Client = Construction.GetAmazonS3Client())
            {
                var bucketExists = AmazonS3Util.DoesS3BucketExistAsync(s3Client, bucketName).Result;
                if(bucketExists)
                {
                    Console.WriteLine($"Bucket '{bucketName}' exists!");
                }
                else
                {
                    Console.WriteLine($"Bucket '{bucketName}' does NOT exist...");
                }
            }
        }

        private static void CreateBucket()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            using (var s3Client = Construction.GetAmazonS3Client())
            {
                // Create bucket.
                try
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true,
                    };

                    var putBucketResponse = s3Client.PutBucketAsync(putBucketRequest).Result;

                    Construction.DisplayPutBucketResponse(putBucketResponse);
                }
                catch(AmazonS3Exception ex)
                {
                    Console.WriteLine($"{nameof(AmazonS3Exception)}:\n{ex.Message}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{nameof(Exception)}:\n{ex.Message}");
                }

                // Get bucket location.
                var getBucketLocationRequest = new GetBucketLocationRequest
                {
                    BucketName = bucketName,
                };

                var getBucketLocationResponse = s3Client.GetBucketLocationAsync(getBucketLocationRequest).Result;

                Construction.DisplayGetBucketLocationResponse(getBucketLocationResponse);
            }
        }

        private static void DisplayGetBucketLocationResponse(GetBucketLocationResponse getBucketLocationResponse)
        {
            var writer = Console.Out;

            writer.WriteLine($"{nameof(getBucketLocationResponse.HttpStatusCode)}: {getBucketLocationResponse.HttpStatusCode}");
            Construction.DisplayResponseMetadata(writer, getBucketLocationResponse.ResponseMetadata);

            writer.WriteLine($"{nameof(getBucketLocationResponse.Location)}: {getBucketLocationResponse.Location}");
        }

        private static void DisplayPutBucketResponse(PutBucketResponse putBucketResponse)
        {
            var writer = Console.Out;

            writer.WriteLine($"{nameof(putBucketResponse.HttpStatusCode)}: {putBucketResponse.HttpStatusCode}");
            Construction.DisplayResponseMetadata(writer, putBucketResponse.ResponseMetadata);
        }

        private static void DisplayResponseMetadata(TextWriter writer, ResponseMetadata responseMetadata)
        {
            writer.WriteLine("Response Metadata:");
            writer.WriteLine($"{nameof(responseMetadata.RequestId)}: {responseMetadata.RequestId}");
            foreach (var pair in responseMetadata.Metadata)
            {
                writer.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        private static AmazonS3Client GetAmazonS3Client()
        {
            var configuration = Construction.GetConfiguration();

            var awsAccessKeyID = configuration["AccessKeyID"];
            var awsSecretAccessKey = configuration["SecretAccessKey"];
            var awsRegionEndpoint = RegionEndpoint.USWest1;

            var s3Client = new AmazonS3Client(awsAccessKeyID, awsSecretAccessKey, awsRegionEndpoint);
            return s3Client;
        }

        private static void CreateBucketSetup()
        {
            var configuration = Construction.GetConfiguration();

            var awsAccessKeyID = configuration["AccessKeyID"];
            var awsSecretAccessKey = configuration["SecretAccessKey"];

            var bucketName = Construction.GenerateRandomBucketName();
        }

        private static IConfiguration GetConfiguration()
        {
            var dropboxRivetDataDirectoryPath = Construction.GetDropboxRivetDataDirectoryPath();
            var configurationFilePath = Path.Combine(dropboxRivetDataDirectoryPath, @"User Secret Files\AWS-David-Rekognition.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationFilePath)
                .Build()
                ;

            return configuration;
        }

        private static string GetDropboxRivetDataDirectoryPath()
        {
            var dropboxDirectoryPath = Construction.GetDropboxDirectoryPath();

            var dropboxRivetDataDirectoryPath = Path.Combine(dropboxDirectoryPath, @"Organizations\Rivet\Data");
            return dropboxRivetDataDirectoryPath;
        }

        private static string GetDropboxDirectoryPath()
        {
            var usersDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var dropboxDirectoryPath = Path.Combine(usersDirectoryPath, @"Dropbox");
            return dropboxDirectoryPath;
        }

        private static string GenerateRandomBucketName()
        {
            var guidString = Guid.NewGuid().ToString().ToLowerInvariant();

            var bucketName = $"{"Bristol".ToLowerInvariant()}.{guidString}";
            return bucketName;
        }
    }
}
