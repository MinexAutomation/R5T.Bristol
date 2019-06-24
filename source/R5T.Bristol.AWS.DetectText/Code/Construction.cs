using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.Runtime;

using R5T.NetStandard.IO.Serialization;


namespace R5T.Bristol.AWS.DetectText
{
    public static class Construction
    {
        public static void SubMain()
        {
            //Construction.DetectTextOnS3ObjectRequest();
            Construction.DetectTextGetJson();
        }

        private static void DetectTextGetJson()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            var imageFilePath = configuration["ImageFilePath"];
            var objectKey = Construction.GetKeyNameFromFilePath(imageFilePath);

            using (var rekognitionClient = Construction.GetAmazonRekognitionClient())
            {
                rekognitionClient.AfterResponseEvent += RekognitionClient_AfterResponseEvent;

                var detectTextRequest = new DetectTextRequest
                {
                    Image = new Image
                    {
                        S3Object = new S3Object
                        {
                            Bucket = bucketName,
                            Name = objectKey,
                        },
                    },
                };

                var detectTextResponse = rekognitionClient.DetectTextAsync(detectTextRequest).Result;

                JsonFileSerializer.Serialize(@"C:\Temp\temp.json", detectTextResponse);
            }
        }

        private static void RekognitionClient_AfterResponseEvent(object sender, ResponseEventArgs e)
        {
            Console.WriteLine("Here");
        }

        private static void DetectTextOnS3ObjectRequest()
        {
            var configuration = Construction.GetConfiguration();

            var bucketName = configuration["S3BucketName"];

            var imageFilePath = configuration["ImageFilePath"];
            var objectKey = Construction.GetKeyNameFromFilePath(imageFilePath);

            using (var rekognitionClient = Construction.GetAmazonRekognitionClient())
            {
                var detectTextRequest = new DetectTextRequest
                {
                    Image = new Image
                    {
                        S3Object = new S3Object
                        {
                            Bucket = bucketName,
                            Name = objectKey,
                        },
                    },
                };

                try
                {
                    var detectTextResponse = rekognitionClient.DetectTextAsync(detectTextRequest).Result;

                    Construction.DisplayDetectTextResponse(detectTextResponse);
                }
                catch(AmazonRekognitionException ex)
                {
                    Console.WriteLine($"{nameof(AmazonRekognitionException)}: {ex.Message}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"{nameof(Exception)}: {ex.Message}");
                }
            }
        }

        private static void DisplayDetectTextResponse(DetectTextResponse detectTextResponse)
        {
            var writer = Console.Out;

            writer.WriteLine($"{nameof(detectTextResponse.HttpStatusCode)}: {detectTextResponse.HttpStatusCode}");
            Construction.DisplayResponseMetadata(writer, detectTextResponse.ResponseMetadata);

            foreach (var textDetection in detectTextResponse.TextDetections)
            {
                writer.WriteLine("Detected: " + textDetection.DetectedText);
                writer.WriteLine("Confidence: " + textDetection.Confidence);
                writer.WriteLine("Id : " + textDetection.Id);
                writer.WriteLine("Parent Id: " + textDetection.ParentId);
                writer.WriteLine("Type: " + textDetection.Type);

                var boundingBox = textDetection.Geometry.BoundingBox;
                writer.WriteLine($"{nameof(boundingBox.Height)}: {boundingBox.Height}");
                writer.WriteLine($"{nameof(boundingBox.Left)}: {boundingBox.Left}");
                writer.WriteLine($"{nameof(boundingBox.Top)}: {boundingBox.Top}");
                writer.WriteLine($"{nameof(boundingBox.Width)}: {boundingBox.Width}");
            }
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

        private static AmazonRekognitionClient GetAmazonRekognitionClient()
        {
            var configuration = Construction.GetConfiguration();

            var awsAccessKeyID = configuration["AccessKeyID"];
            var awsSecretAccessKey = configuration["SecretAccessKey"];
            var awsRegionEndpoint = RegionEndpoint.USWest1;

            var rekognitionClient = new AmazonRekognitionClient(awsAccessKeyID, awsSecretAccessKey, awsRegionEndpoint);
            return rekognitionClient;
        }

        private static IConfiguration GetConfiguration()
        {
            var usersDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var configurationFilePath = Path.Combine(usersDirectoryPath, @"Dropbox\Organizations\Rivet\Data\User Secret Files\AWS-David-Rekognition.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationFilePath)
                .Build()
                ;

            return configuration;
        }
    }
}
