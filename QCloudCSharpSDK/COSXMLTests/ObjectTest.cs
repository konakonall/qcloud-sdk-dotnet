using COSXML.Common;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Object;
using COSXML.Model.Tag;
using COSXML.Model.Bucket;
using COSXML.Utils;
using COSXML.Transfer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COSXMLTests
{
    [TestFixture]
    public class ObjectTest
    {

        internal COSXML.CosXml cosXml;

        internal TransferManager transferManager;

        internal string bucket;

        internal string bigFileSrcPath;

        internal string smallFileSrcPath;

        internal string commonKey;

        internal string copykey;
        internal string copyKeySmall;

        internal string multiKey;

        internal string localDir;

        internal string localFileName;

        [OneTimeSetUp]
        public void Init()
        {
            cosXml = QCloudServer.Instance().cosXml;
            bucket = QCloudServer.Instance().bucketForObjectTest;
            transferManager = new TransferManager(cosXml, new TransferConfig());
            smallFileSrcPath = QCloudServer.CreateFile("small_" + TimeUtils.GetCurrentTime(TimeUnit.Seconds) + ".txt", 1024 * 1024 * 1);
            bigFileSrcPath = QCloudServer.CreateFile("big_" + TimeUtils.GetCurrentTime(TimeUnit.Seconds) + ".txt", 1024 * 1024 * 10);
            FileInfo fileInfo = new FileInfo(smallFileSrcPath);

            DirectoryInfo directoryInfo = fileInfo.Directory;

            localDir = directoryInfo.FullName;
            localFileName = "local.txt";

            commonKey = "simpleObject" + TimeUtils.GetCurrentTime(TimeUnit.Seconds);
            multiKey = "bigObject" + TimeUtils.GetCurrentTime(TimeUnit.Seconds);
            copykey = "copy_objecttest.txt";
            copyKeySmall = "copy_target";

            PutObject();
        }

        [OneTimeTearDown]
        public void Clear()
        {
            MultiDeleteObject();

            QCloudServer.DeleteAllFile(localDir, "*.txt");
        }

        [Test()]
        public void PutObjectWithAES256()
        {

            try
            {
                string key = "objectWithAES256";

                PutObjectRequest request = new PutObjectRequest(bucket, key, smallFileSrcPath);


                request.SetCosServerSideEncryption();

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);


                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void PutObjectWithCustomerKey()
        {

            try
            {
                string key = "objectWithSSEC";

                PutObjectRequest request = new PutObjectRequest(bucket, key, smallFileSrcPath);

                //32字符
                //32字符
                string customerKey = "25rN73uQtl1bUGnvHe0URgFWBNu4vBba";

                request.SetCosServerSideEncryptionWithCustomerKey(customerKey);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);


                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        public void PutObjectWithKMS()
        {

            try
            {
                string key = "objectWithKMS";

                PutObjectRequest request = new PutObjectRequest(bucket, key, smallFileSrcPath);


                request.SetCosServerSideEncryptionWithKMS(null, null);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);


                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                //Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                //Assert.True(true);
            }
        }

        [Test()]
        public void PutObject()
        {

            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, commonKey, smallFileSrcPath);
                QCloudServer.SetRequestACLData(request);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);


                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void PutObjectBytes()
        {

            try
            {
                byte[] data = File.ReadAllBytes(smallFileSrcPath);

                PutObjectRequest request = new PutObjectRequest(bucket, commonKey, data);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);


                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void HeadObject()
        {

            try
            {
                HeadObjectRequest request = new HeadObjectRequest(bucket, commonKey);


                //执行请求
                HeadObjectResult result = cosXml.HeadObject(request);


                Assert.True(result.httpCode == 200);
                Assert.Null(result.cosStorageClass);
                Assert.NotZero(result.size);
                Assert.NotNull(result.eTag);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestObjectACL()
        {

            try
            {
                PutObjectACLRequest request = new PutObjectACLRequest(bucket, commonKey);

                QCloudServer.SetRequestACLData(request);

                //执行请求
                PutObjectACLResult result = cosXml.PutObjectACL(request);

                Assert.True(result.httpCode == 200);

                GetObjectACLRequest getRequest = new GetObjectACLRequest(bucket, commonKey);

                //执行请求
                GetObjectACLResult getResult = cosXml.GetObjectACL(getRequest);

                AccessControlPolicy acl = getResult.accessControlPolicy;

                // Console.WriteLine(result.GetResultInfo());
                Assert.IsNotEmpty((result.GetResultInfo()));

                Assert.AreEqual(result.httpCode, 200);
                Assert.NotNull(acl.owner);
                Assert.NotNull(acl.owner.id);
                Assert.NotNull(acl.owner.displayName);
                Assert.NotNull(acl.accessControlList);
                Assert.NotNull(acl.accessControlList.grants);
                Assert.NotZero(acl.accessControlList.grants.Count);
                Assert.NotNull(acl.accessControlList.grants[0].permission);
                Assert.NotNull(acl.accessControlList.grants[0].grantee);
                Assert.NotNull(acl.accessControlList.grants[0].grantee.id);
                Assert.NotNull(acl.accessControlList.grants[0].grantee.displayName);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void OptionObject()
        {

            try
            {
                string origin = "http://cloud.tencent.com";
                string accessMthod = "PUT";

                OptionObjectRequest request = new OptionObjectRequest(bucket, commonKey, origin, accessMthod);
                var header = new List<string>();
                header.Add("Content-Type");
                header.Add("Content-Disposition");
                request.SetAccessControlHeaders(header);

                //执行请求
                OptionObjectResult result = cosXml.OptionObject(request);
                // Console.WriteLine(result.GetResultInfo());
                Assert.IsNotEmpty((result.GetResultInfo()));

                Assert.AreEqual(result.httpCode, 200);
                Assert.NotNull(result.accessControlAllowExposeHeaders);
                Assert.NotNull(result.accessControlAllowHeaders);
                Assert.NotNull(result.accessControlAllowMethods);
                Assert.NotNull(result.accessControlAllowOrigin);
                Assert.NotZero(result.accessControlMaxAge);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void CopyObject()
        {

            try
            {
                CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                    bucket, QCloudServer.Instance().region, copykey);

                CopyObjectRequest copyObjectRequest = new CopyObjectRequest(bucket, multiKey);

                //设置拷贝源
                copyObjectRequest.SetCopySource(copySource);
                copyObjectRequest.SetCosStorageClass(CosStorageClass.Standard);
                QCloudServer.SetRequestACLData(copyObjectRequest);

                //执行请求
                CopyObjectResult result = cosXml.CopyObject(copyObjectRequest);
                var copyObject = result.copyObject;

                // Console.WriteLine(result.GetResultInfo());
                Assert.IsNotEmpty((result.GetResultInfo()));
                Assert.AreEqual(result.httpCode, 200);
                Assert.NotNull(copyObject.crc64);
                Assert.NotNull(copyObject.eTag);
                Assert.NotNull(copyObject.lastModified);
                Assert.Null(copyObject.versionId);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void MultiUpload()
        {
            string key = multiKey;

            try
            {
                InitMultipartUploadRequest initMultipartUploadRequest = new InitMultipartUploadRequest(bucket, multiKey);
                QCloudServer.SetRequestACLData(initMultipartUploadRequest);

                //执行请求
                InitMultipartUploadResult initMultipartUploadResult = cosXml.InitMultipartUpload(initMultipartUploadRequest);
                Assert.AreEqual(initMultipartUploadResult.httpCode, 200);
                Assert.IsNotEmpty((initMultipartUploadResult.GetResultInfo()));

                string uploadId = initMultipartUploadResult.initMultipartUpload.uploadId;
                Assert.NotNull(uploadId);

                var sliceSize = 1024 * 1024;
                var bigFile = new FileInfo(bigFileSrcPath);
                var sliceCount = bigFile.Length / sliceSize;

                for (int partNumber = 1; partNumber <= sliceCount; partNumber++) 
                {
                    UploadPartRequest uploadPartRequest = new UploadPartRequest(bucket, key, partNumber, uploadId, bigFileSrcPath, sliceSize * (partNumber - 1), sliceSize);
                    //设置进度回调
                    uploadPartRequest.SetCosProgressCallback(
                        delegate (long completed, long total)
                        {
                            // Console.WriteLine(String.Format("{0} progress = {1} / {2} : {3:##.##}%",
                            //     DateTime.Now.ToString(), completed, total, completed * 100.0 / total));
                        }
                    );
                    //执行请求
                    UploadPartResult uploadPartResult = cosXml.UploadPart(uploadPartRequest);
                    Assert.AreEqual(uploadPartResult.httpCode, 200);
                    Assert.IsNotEmpty((uploadPartResult.GetResultInfo()));

                    Assert.NotNull(uploadPartResult.eTag);
                }
                

                ListPartsRequest listPartsRequest = new ListPartsRequest(bucket, key, uploadId);
                listPartsRequest.SetMaxParts(1000);
                listPartsRequest.SetEncodingType("url");
                //执行请求
                ListPartsResult listPartsResult = cosXml.ListParts(listPartsRequest);
                var parts = listPartsResult.listParts;

                Assert.IsNotEmpty((listPartsResult.GetResultInfo()));
                Assert.AreEqual(listPartsResult.httpCode, 200);
                Assert.NotNull(parts.bucket);
                Assert.NotNull(parts.key);
                Assert.NotNull(parts.uploadId);
                Assert.NotNull(parts.owner.id);
                Assert.NotNull(parts.owner.disPlayName);
                Assert.NotNull(parts.initiator.id);
                Assert.NotNull(parts.initiator.disPlayName);
                Assert.NotNull(parts.storageClass);
                Assert.NotNull(parts.maxParts);
                Assert.False(parts.isTruncated);
                Assert.AreEqual(parts.parts.Count, sliceCount);

                CompleteMultipartUploadRequest completeMultiUploadRequest = new CompleteMultipartUploadRequest(bucket, key, uploadId);

                foreach (var part in parts.parts) 
                {
                    Assert.NotNull(part.lastModified);
                    Assert.AreEqual(Int32.Parse(part.size), sliceSize);
                    Assert.NotZero(Int32.Parse(part.partNumber));
                    Assert.NotNull(part.eTag);
                    //设置已上传的parts
                    completeMultiUploadRequest.SetPartNumberAndETag(Int32.Parse(part.partNumber), part.eTag);
                }

                //执行请求
                CompleteMultipartUploadResult completeMultiUploadResult = cosXml.CompleteMultiUpload(completeMultiUploadRequest);
                var completeResult = completeMultiUploadResult.completeResult;

                Assert.IsNotEmpty((completeMultiUploadResult.GetResultInfo()));
                Assert.AreEqual(completeMultiUploadResult.httpCode, 200);
                Assert.NotNull(completeResult.location);
                Assert.NotNull(completeResult.bucket);
                Assert.NotNull(completeResult.key);
                Assert.NotNull(completeResult.eTag);

            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void AbortMultiUpload()
        {
            string key = multiKey;

            try
            {
                InitMultipartUploadRequest initMultipartUploadRequest = new InitMultipartUploadRequest(bucket, key);

                //执行请求
                InitMultipartUploadResult initMultipartUploadResult = cosXml.InitMultipartUpload(initMultipartUploadRequest);

                string uploadId = initMultipartUploadResult.initMultipartUpload.uploadId;
                Assert.AreEqual(initMultipartUploadResult.httpCode, 200);
                Assert.IsNotEmpty((initMultipartUploadResult.GetResultInfo()));
                Assert.NotNull(uploadId);

                AbortMultipartUploadRequest request = new AbortMultipartUploadRequest(bucket, key, uploadId);

                //执行请求
                AbortMultipartUploadResult result = cosXml.AbortMultiUpload(request);
                Assert.True(result.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void PartCopyObject()
        {
            string key = commonKey;
            CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                bucket, QCloudServer.Instance().region, copykey);

            try
            {
                InitMultipartUploadRequest initMultipartUploadRequest = new InitMultipartUploadRequest(bucket, key);
                //执行请求
                InitMultipartUploadResult initMultipartUploadResult = cosXml.InitMultipartUpload(initMultipartUploadRequest);
                Assert.AreEqual(initMultipartUploadResult.httpCode, 200);
                Assert.IsNotEmpty((initMultipartUploadResult.GetResultInfo()));

                string uploadId = initMultipartUploadResult.initMultipartUpload.uploadId;
                Assert.NotNull(uploadId);

                var sliceSize = 1024 * 1024;
                var etags = new List<string>();

                for (int partNumber = 1; partNumber <= 2; partNumber++) 
                {
                    UploadPartCopyRequest uploadPartCopyRequest = new UploadPartCopyRequest(bucket, key, partNumber, uploadId);

                    //设置拷贝源
                    uploadPartCopyRequest.SetCopySource(copySource);
                    //设置拷贝范围
                    uploadPartCopyRequest.SetCopyRange((partNumber - 1) * sliceSize, partNumber * sliceSize);
                    //执行请求
                    UploadPartCopyResult uploadPartCopyResult = cosXml.PartCopy(uploadPartCopyRequest);

                    Assert.IsNotEmpty((uploadPartCopyResult.GetResultInfo()));
                    Assert.AreEqual(uploadPartCopyResult.httpCode, 200);

                    Assert.NotNull(uploadPartCopyResult.copyPart.eTag);
                    etags.Add(uploadPartCopyResult.copyPart.eTag);
                }
                

                CompleteMultipartUploadRequest completeMultiUploadRequest = new CompleteMultipartUploadRequest(bucket, key, uploadId);

                //设置已上传的parts
                for (int i = 0; i < etags.Count; i++) 
                {
                    completeMultiUploadRequest.SetPartNumberAndETag(i + 1, etags[i]);
                }
                //执行请求
                CompleteMultipartUploadResult completeMultiUploadResult = cosXml.CompleteMultiUpload(completeMultiUploadRequest);
                var completeResult = completeMultiUploadResult.completeResult;

                Assert.IsNotEmpty((completeMultiUploadResult.GetResultInfo()));
                Assert.AreEqual(completeMultiUploadResult.httpCode, 200);
                Assert.NotNull(completeResult.location);
                Assert.NotNull(completeResult.bucket);
                Assert.NotNull(completeResult.key);
                Assert.NotNull(completeResult.eTag);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void RestoreObject()
        {
            string objectKey = "archive_object";

            try
            {
                byte[] data = File.ReadAllBytes(smallFileSrcPath);
                PutObjectRequest putRequest = new PutObjectRequest(bucket, objectKey, data);
                putRequest.SetCosStorageClass("ARCHIVE");
                cosXml.PutObject(putRequest);

                RestoreObjectRequest request = new RestoreObjectRequest(bucket, objectKey);
                //恢复时间
                request.SetExpireDays(3);
                request.SetTier(COSXML.Model.Tag.RestoreConfigure.Tier.Standard);

                //执行请求
                RestoreObjectResult result = cosXml.RestoreObject(request);

                Assert.True(result.IsSuccessful());

                DeleteObjectRequest deleteRequest = new DeleteObjectRequest(bucket, objectKey);
                DeleteObjectResult deleteObjectResult = cosXml.DeleteObject(deleteRequest);

                Assert.True(deleteObjectResult.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void PostObject()
        {

            try
            {
                PostObjectRequest request = new PostObjectRequest(bucket, commonKey, smallFileSrcPath);

                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    // Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
                });

                //设置policy
                request.SetPolicy(null);
                request.SetCacheControl("no-cache");
                request.SetContentType("text/plain");
                request.SetContentDisposition("inline");
                request.SetContentEncoding("gzip");
                request.SetExpires("Wed, 21 Oct 2021 07:28:00 GMT");
                request.SetCustomerHeader("x-cos-key", "value");

                //执行请求
                PostObjectResult result = cosXml.PostObject(request);

                Assert.True(result.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }



        [Test()]
        public async Task PostObjectBytes()
        {

            try
            {
                byte[] data = File.ReadAllBytes(smallFileSrcPath);
                PostObjectRequest request = new PostObjectRequest(bucket, commonKey, data);

                //设置policy
                request.SetCosACL(CosACL.Private);
                request.SetContentType("image/jpeg");
                request.SetCosStorageClass("Standard");
                var policy = new PostObjectRequest.Policy();
                policy.SetExpiration(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 60000);
                policy.AddConditions("acl", "private", false);
                policy.AddConditions("$Content-Type", "image/", true);
                policy.AddContentConditions(0, data.Length + 1);
                request.SetPolicy(policy);

                //执行请求
                PostObjectResult result = await cosXml.ExecuteAsync<PostObjectResult>(request);

                Assert.True(result.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        public void DeleteObject()
        {

            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, commonKey);


                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);


                Assert.True(result.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        public void MultiDeleteObject()
        {

            try
            {
                DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucket);

                //设置返回结果形式
                request.SetDeleteQuiet(false);

                //设置删除的keys
                List<string> keys = new List<string>();

                keys.Add(commonKey);
                keys.Add(multiKey);
                request.SetObjectKeys(keys);

                //执行请求
                DeleteMultiObjectResult result = cosXml.DeleteMultiObjects(request);
                var deleteResult = result.deleteResult;

                Assert.True(result.IsSuccessful());
                Assert.IsNotEmpty((result.GetResultInfo()));
                Assert.AreEqual(deleteResult.deletedList.Count, 2);
                Assert.AreEqual(deleteResult.errorList.Count, 0);
                foreach (var deleted in deleteResult.deletedList)
                {
                    Assert.NotNull(deleted.key);
                }

            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestCreateDirectory()
        {

            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket,
                    "dir/", new byte[0]);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);

                Assert.AreEqual(result.httpCode, 200);
            }
            catch (CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestGetObject()
        {

            try
            {
                GetObjectRequest request = new GetObjectRequest(bucket, commonKey, localDir, localFileName);


                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    // Console.WriteLine(String.Format("progress = {0} / {1} : {2:##.##}%", completed, total, completed * 100.0 / total));
                });

                //执行请求
                GetObjectResult result = cosXml.GetObject(request);

                Assert.AreEqual(result.httpCode, 200);
                Assert.NotNull(result.eTag);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        [Test()]
        public void TestGetObjectByte()
        {

            try
            {
                HeadObjectRequest request = new HeadObjectRequest(bucket, commonKey);

                //执行请求
                HeadObjectResult result = cosXml.HeadObject(request);

                long contentLength = Int64.Parse(result.responseHeaders["Content-Length"][0]);

                GetObjectBytesRequest getObjectBytesRequest = new GetObjectBytesRequest(bucket, commonKey);

                GetObjectBytesResult getObjectBytesResult = cosXml.GetObject(getObjectBytesRequest);

                byte[] content = getObjectBytesResult.content;


                Assert.True(getObjectBytesResult.IsSuccessful());
                Assert.AreEqual(content.Length, contentLength);

            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestGetObjectByteRange()
        {

            try
            {
                GetObjectBytesRequest getObjectBytesRequest = new GetObjectBytesRequest(bucket, commonKey);
                getObjectBytesRequest.SetRange(0, 199);

                GetObjectBytesResult getObjectBytesResult = cosXml.GetObject(getObjectBytesRequest);

                byte[] content = getObjectBytesResult.content;


                Assert.True(getObjectBytesResult.IsSuccessful());
                Assert.AreEqual(content.Length, 200);

            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestSelectObjectToFile()
        {

            try
            {
                string key = "select_target.json";


                SelectObjectRequest request = new SelectObjectRequest(bucket, key);

                ObjectSelectionFormat.JSONFormat jSONFormat = new ObjectSelectionFormat.JSONFormat();

                jSONFormat.Type = "DOCUMENT";
                jSONFormat.RecordDelimiter = "\n";

                string outputFile = "select_local_file.json";


                request.SetExpression("Select * from COSObject")
                        .SetInputFormat(new ObjectSelectionFormat(null, jSONFormat))
                        .SetOutputFormat(new ObjectSelectionFormat(null, jSONFormat))
                        .SetCosProgressCallback(delegate (long progress, long total)
                        {
                            // Console.WriteLine("OnProgress : " + progress + "," + total);
                        })
                        .OutputToFile(outputFile)
                        ;

                SelectObjectResult selectObjectResult = cosXml.SelectObject(request);

                Assert.AreEqual(selectObjectResult.httpCode, 200);
                Assert.Null(selectObjectResult.searchContent);
                Assert.NotZero(selectObjectResult.stat.BytesProcessed);
                Assert.NotZero(selectObjectResult.stat.BytesReturned);
                Assert.NotZero(selectObjectResult.stat.BytesScanned);
                Assert.AreEqual(selectObjectResult.stat.BytesReturned, new FileInfo(outputFile).Length);
                Assert.NotNull(selectObjectResult.stat.ToString());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void TestSelectObjectInMemory()
        {

            try
            {
                string key = "select_target.json";


                SelectObjectRequest request = new SelectObjectRequest(bucket, key);

                ObjectSelectionFormat.JSONFormat jSONFormat = new ObjectSelectionFormat.JSONFormat();

                jSONFormat.Type = "DOCUMENT";
                jSONFormat.RecordDelimiter = "\n";

                request.SetExpression("Select * from COSObject")
                        .SetInputFormat(new ObjectSelectionFormat(null, jSONFormat))
                        .SetOutputFormat(new ObjectSelectionFormat(null, jSONFormat))
                        .SetCosProgressCallback(delegate (long progress, long total)
                        {
                            // Console.WriteLine("OnProgress : " + progress + "," + total);
                        })
                        ;

                SelectObjectResult selectObjectResult = cosXml.SelectObject(request);

                Assert.AreEqual(selectObjectResult.httpCode, 200);
                Assert.NotZero(selectObjectResult.stat.BytesProcessed);
                Assert.NotZero(selectObjectResult.stat.BytesReturned);
                Assert.NotZero(selectObjectResult.stat.BytesScanned);
                Assert.AreEqual(selectObjectResult.stat.BytesReturned, Encoding.UTF8.GetByteCount(
                    selectObjectResult.searchContent));
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public async Task TestUploadTaskWithBigFile()
        {
            string key = multiKey;


            PutObjectRequest request = new PutObjectRequest(bucket, key, bigFileSrcPath);

            request.SetRequestHeader("Content-Type", "image/png");

            COSXMLUploadTask uploadTask = new COSXMLUploadTask(request);

            uploadTask.SetSrcPath(bigFileSrcPath);

            uploadTask.progressCallback = delegate (long completed, long total)
            {
                // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            COSXMLUploadTask.UploadTaskResult result = await transferManager.UploadAsync(uploadTask);

            Assert.AreEqual(result.httpCode, 200);
            Assert.NotNull(result.eTag);
        }

        [Test()]
        public async Task TestUploadTaskWithSmallFile()
        {
            string key = commonKey;

            COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucket, key);

            uploadTask.SetSrcPath(smallFileSrcPath);

            COSXMLUploadTask.UploadTaskResult result = await transferManager.UploadAsync(uploadTask);

            Assert.AreEqual(result.httpCode, 200);
            Assert.NotNull(result.eTag);
        }


        [Test()]
        public void TestUploadTaskWithInteractive()
        {
            string key = multiKey;

            PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, key, bigFileSrcPath);

            putObjectRequest.LimitTraffic(4096000);

            COSXMLUploadTask uploadTask = new COSXMLUploadTask(putObjectRequest);

            uploadTask.SetSrcPath(bigFileSrcPath);

            uploadTask.progressCallback = delegate (long completed, long total)
            {
                // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            transferManager.UploadAsync(uploadTask);

            Thread.Sleep(2000);
            uploadTask.Pause();
            Thread.Sleep(200);
            uploadTask.Resume();
            Thread.Sleep(2000);
            uploadTask.Pause();
            Thread.Sleep(1000);

            if (uploadTask.state() != TaskState.Completed)
            {

                QCloudServer.TestWithServerFailTolerance(() =>
                {
                    // new task
                    COSXMLUploadTask uploadTask2 = new COSXMLUploadTask(bucket, key);

                    uploadTask2.SetSrcPath(bigFileSrcPath);
                    uploadTask2.SetUploadId(uploadTask.GetUploadId());

                    uploadTask2.progressCallback = delegate (long completed, long total)
                    {
                        // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                    };
                    var asyncTask = transferManager.UploadAsync(uploadTask2);
                    asyncTask.Wait(10000);
                    COSXMLUploadTask.UploadTaskResult result = asyncTask.Result;

                    Assert.True(result.httpCode == 200);
                    Assert.NotNull(result.eTag);
                    Assert.NotNull(result.GetResultInfo());
                }
                );
            } else {
                Console.WriteLine("Uplaod is Complete");
                Assert.Pass();
            }
        }

        [Test()]
        public void TestUploadTaskCancelled()
        {
            string key = multiKey;

            COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucket, key);

            uploadTask.SetSrcPath(bigFileSrcPath);
            uploadTask.MaxConcurrent = 1;
            uploadTask.StorageClass = "Standard_IA";

            var asyncTask = transferManager.UploadAsync(uploadTask);

            Thread.Sleep(2000);
            uploadTask.Cancel();
            Thread.Sleep(500);
            Assert.Pass();
        }

        [Test()]
        public async Task TestDownloadTask()
        {
            GetObjectRequest request = new GetObjectRequest(bucket,
                commonKey, localDir, localFileName);

            //执行请求
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(request);

            COSXMLDownloadTask.DownloadTaskResult result = await transferManager.DownloadAsync(downloadTask);

            Assert.True(result.httpCode == 200);
            Assert.NotNull(result.eTag);
        }


        [Test()]
        public async Task TestDownloadTaskRanged()
        {
            HeadObjectRequest headRequest = new HeadObjectRequest(bucket, commonKey);

            //执行请求
            HeadObjectResult headResult = cosXml.HeadObject(headRequest);

            long contentLength = Int64.Parse(headResult.responseHeaders["Content-Length"][0]);

            GetObjectRequest request = new GetObjectRequest(bucket,
                commonKey, localDir, localFileName);
            request.SetRange(100);
            request.LimitTraffic(8 * 1000 * 1024);

            //执行请求
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(request);
            downloadTask.SetRange(200, -1);

            COSXMLDownloadTask.DownloadTaskResult result = await transferManager.DownloadAsync(downloadTask);

            Assert.True(result.IsSuccessful());
            Assert.AreEqual(Int64.Parse(result.responseHeaders["Content-Length"][0]), contentLength - 200);
        }

        [Test()]
        public void TestDownloadTaskInteractive()
        {
            PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, multiKey, bigFileSrcPath);
            cosXml.PutObject(putObjectRequest);

            //执行请求
            GetObjectRequest getRequest = new GetObjectRequest(bucket, multiKey, localDir, localFileName);
            getRequest.LimitTraffic(8192000);

            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(getRequest);

            downloadTask.progressCallback = delegate (long completed, long total)
            {
                // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            var asyncTask = transferManager.DownloadAsync(downloadTask);

            Thread.Sleep(2000);
            downloadTask.Pause();

            Thread.Sleep(200);
            downloadTask.Resume();
            if (downloadTask.state() != TaskState.Completed)
            {
                asyncTask = downloadTask.AsyncTask<COSXMLDownloadTask.DownloadTaskResult>();
                asyncTask.Wait(10000);
                COSXMLDownloadTask.DownloadTaskResult result = asyncTask.Result;

                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.GetResultInfo());
            } else {
                Console.WriteLine("Download is Complete");
                Assert.Pass();
            }
        }

        [Test()]
        public void TestDownloadTaskCancelled()
        {
            GetObjectRequest request = new GetObjectRequest(bucket,
                commonKey, localDir, localFileName);

            //执行请求
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(request);

            var asyncTask = transferManager.DownloadAsync(downloadTask);

            Thread.Sleep(2000);
            downloadTask.Cancel();
            Thread.Sleep(200);
            Assert.Pass();
        }

        [Test()]
        public async Task TestCopyTaskWithBigFile()
        {
            CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                    bucket, QCloudServer.Instance().region, copykey);


            COSXMLCopyTask copyTask = new COSXMLCopyTask(bucket, multiKey, copySource);

            copyTask.progressCallback = delegate (long completed, long total)
            {
                // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            COSXMLCopyTask.CopyTaskResult result = await transferManager.CopyAsync(copyTask);

            Assert.True(result.httpCode == 200);
            Assert.NotNull(result.eTag);
            Assert.NotNull(result.GetResultInfo());
        }

        [Test()]
        public async Task TestCopyTaskWithSmallFile()
        {
            CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                    bucket, QCloudServer.Instance().region, copyKeySmall);

            COSXMLCopyTask copyTask = new COSXMLCopyTask(bucket, multiKey, copySource);

            COSXMLCopyTask.CopyTaskResult result = await transferManager.CopyAsync(copyTask);

            Assert.True(result.httpCode == 200);
            Assert.NotNull(result.eTag);
        }

        [Test()]
        public void TestCopyTaskWithInteractive()
        {
            CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                    bucket, QCloudServer.Instance().region, copykey);


            COSXMLCopyTask copyTask = new COSXMLCopyTask(bucket, multiKey, copySource);

            copyTask.CompleteOnAllPartsCopyed = false;

            var asyncTask = transferManager.CopyAsync(copyTask);

            Thread.Sleep(2000);
            copyTask.Pause();

            Thread.Sleep(200);
            copyTask.CompleteOnAllPartsCopyed = true;
            copyTask.Resume();
            if (copyTask.state() != TaskState.Completed)
            {
                asyncTask = copyTask.AsyncTask<COSXMLCopyTask.CopyTaskResult>();
                asyncTask.Wait(10000);
                COSXMLCopyTask.CopyTaskResult result = asyncTask.Result;

                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            else 
            {
                Console.WriteLine("Copy is Completed");
                Assert.Pass();
            }
        }

        [Test()]
        public void TestCopyTaskCancelled()
        {
            CopySourceStruct copySource = new CopySourceStruct(QCloudServer.Instance().appid,
                    bucket, QCloudServer.Instance().region, copykey);

            COSXMLCopyTask copyTask = new COSXMLCopyTask(bucket, multiKey, copySource);

            var asyncTask = transferManager.CopyAsync(copyTask);

            Thread.Sleep(2000);
            copyTask.Cancel();
            Thread.Sleep(500);
            Assert.Pass();
        }

        [Test()]
        public void TestPutObjectUploadTrafficLimit()
        {

            try
            {
                long now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                PutObjectRequest request = new PutObjectRequest(bucket,
                    commonKey, smallFileSrcPath);


                request.LimitTraffic(8 * 1000 * 1024);
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                long costTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - now;

                Assert.True(result.httpCode == 200);
                Assert.NotNull(result.eTag);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        [Test()]
        public void TestPostObjectTrafficLimit()
        {

            try
            {
                long now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                PostObjectRequest request = new PostObjectRequest(bucket,
                    commonKey, smallFileSrcPath);


                request.LimitTraffic(8 * 1000 * 1024);
                //执行请求
                PostObjectResult result = cosXml.PostObject(request);
                long costTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - now;

                Assert.True(result.IsSuccessful());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        [Test()]
        public void GenerateSignUrl()
        {
            string key = commonKey;
            PreSignatureStruct signatureStruct = new PreSignatureStruct();

            signatureStruct.bucket = bucket;
            signatureStruct.appid = QCloudServer.Instance().appid;
            signatureStruct.region = QCloudServer.Instance().region;
            signatureStruct.key = key;
            signatureStruct.httpMethod = "GET";
            signatureStruct.isHttps = true;
            signatureStruct.signDurationSecond = 600;
            signatureStruct.queryParameters = new Dictionary<string, string>();
            signatureStruct.queryParameters.Add("a", "1");
            signatureStruct.headers = new Dictionary<string, string>();
            string url = cosXml.GenerateSignURL(signatureStruct);

            Assert.NotNull(url);
        }

        [Test()]
        public async Task AsyncPutObject()
        {
            PutObjectRequest request = new PutObjectRequest(bucket, commonKey, smallFileSrcPath);
            request.SetRequestHeader("Content-Type", "text/plain");

            PutObjectResult result = await cosXml.ExecuteAsync<PutObjectResult>(request);

            Assert.True(result.httpCode == 200);
            Assert.NotNull(result.eTag);
        }

        [Test()]
        public void TestListUploads()
        {
            try
            {
                ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucket);
                request.SetMaxUploads("100");
                request.SetEncodingType("url");

                //执行请求
                ListMultiUploadsResult result = cosXml.ListMultiUploads(request);
                var uploads = result.listMultipartUploads;

                Assert.IsNotEmpty(result.GetResultInfo());
                Assert.NotNull(uploads.bucket);
                Assert.True(result.httpCode == 200);

                if (uploads.uploads != null && uploads.uploads.Count > 0) 
                {
                    foreach (var upload in uploads.uploads) 
                    {
                        Assert.NotNull(upload.key);
                        Assert.NotNull(upload.uploadID);
                        Assert.NotNull(upload.storageClass);
                        Assert.NotNull(upload.initiator.id);
                        Assert.NotNull(upload.initiator.displayName);
                        Assert.NotNull(upload.initiated);
                        Assert.NotNull(upload.owner.id);
                        Assert.NotNull(upload.owner.displayName);
                    }
                }
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }


        }
    }
}
