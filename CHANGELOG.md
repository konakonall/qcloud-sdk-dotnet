# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Generated by [`auto-changelog`](https://github.com/CookPete/auto-changelog).

## [5.4.15](https://github.com/tencentyun/qcloud-sdk-dotnet/compare/5.4.14...5.4.15) - 2020-11-09

1. 增加智能分层、云上数据处理、图片审核接口
2. 老旧的异步调用接口增加 Obsolete 标识

### Commits

- remove packages [`9c17ef2`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/9c17ef2033ceb5366d9bf0818f8b44978ff12774)
- 增加智能分层、云上数据处理、图片审核接口 [`73069b5`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/73069b59654925b15b5d15763710022be048d0f8)
- add coverlet [`5001cd6`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/5001cd6f5e127aeaba43e9c434b76ab74317f9d2)

## [5.4.14](https://github.com/tencentyun/qcloud-sdk-dotnet/compare/5.4.9...5.4.14) - 2020-10-23

1. 支持 async/await 的编程范式。
2. 去掉对设置自定义请求超时时间的范围限制。

### Commits

- add select object [`0e0c46d`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/0e0c46debe2bf769c1f34cd5a46697dff0cdb887)
- 优化注释 [`b5e9ba7`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/b5e9ba7cebb084b51b8d1c744f9ee8ec5456a2ad)
- 更新 5.4.12 [`f87b130`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/f87b130af283ff524e5763f8851c1cec21df7343)
- add traffic limit and custom host interface. [`250601d`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/250601d7cd08ab2df4c991f2271e866171496159)
- get and put bucket tagging. [`0061dee`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/0061dee9ac4887a1813759c9398dc115fa1c3275)
- add domain [`8aee238`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/8aee2388cf70561e934f6d8824a545ebb6f127b8)
- add async/await paradigm [`32b8b9e`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/32b8b9e77859af2aa782679f78b3d318b55d9ace)
- 优化签名算法 [`a8f8d80`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/a8f8d803a18a622edfcca6cd605955fed344d743)
- fix interface issue [`7aa09c6`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/7aa09c6b4855970d59697f81f006e35f38bd0eae)
- release 5.4.11 [`27a1a62`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/27a1a62fa4968339d54c2eaa1090769bf946bd8c)
- add delete bucket tagging [`cf8664e`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/cf8664e9efbfb0db24a0ce6c4111f7bd7d059c3a)
- add endpoint suffix. [`097ec59`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/097ec59a401e18da3cf5c54ae60d03dafd3a206e)
- fix DefaultCredentialProvider key expire error. [`d3c1950`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/d3c1950ca01bb89e4054a3a527fc359bebfcdc91)
- release 5.4.13 [`3767008`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/3767008307920fce81223ee48fee621b056b46d6)
- fix put bucket logging request null issue [`8a4c1bb`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/8a4c1bbe87add94f18cf41eb9a57b28a5dde5d0e)
- fix inventory [`c667198`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/c6671988e799ee14fd9f1a237ad4b135cf54f2a1)
- fix ut. [`2a8f350`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/2a8f3507c36b6c8278b8d98beee2ec070023e67c)
- fix website issues [`4602235`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/46022356004318ffafdd3af11ebf511bf45922f7)
- remote newton dep. [`cee051b`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/cee051b54d0fc8540f41747ebaeef0706c40df64)
- update to 5.4.10 [`c75a77f`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/c75a77fc4d5dd594374c83ca87d9b45f19d9cf7e)

## [5.4.9](https://github.com/tencentyun/qcloud-sdk-dotnet/compare/5.4.8...5.4.9) - 2020-02-11

支持 logging, inventory, website, domain, bucket tagging 接口

### Commits

- add simple demo [`1944e78`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/1944e783b159f5e436ebef1ed386822d79b3738d)
- update demo [`d0d11d6`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/d0d11d6bcc14f80fec13b9f2c97b15828aa042f7)

## [5.4.8](https://github.com/tencentyun/qcloud-sdk-dotnet/compare/5.4.7...5.4.8) - 2019-11-15

1. 增加 endpoint 设置接口
2. 修复上传高级接口报错的问题

### Commits

- remote newton dep. [`c911ab0`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/c911ab09559c2c63820048c3c732e2815af3d2f9)
- add endpoint suffix. [`6c33b8f`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/6c33b8f8185fbeb293e6e25e2383f6996a729021)
- remove some API for compatibility. [`65d49c2`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/65d49c2bcd033c715b37480d46a76af567315778)
- fix DefaultCredentialProvider key expire error. [`1f91b69`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/1f91b694ffcc09af6d6d8e3e04bfb0c7cb71e2b9)
- fix ut issue. [`314d26b`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/314d26bde1911d036ad5eb5797e355af516025ec)

## [5.4.7](https://github.com/tencentyun/qcloud-sdk-dotnet/compare/5.4.5...5.4.7) - 2019-10-29

修复 DefaultQCloudCredentialProvider 密钥过期的问题

### Commits

- update sdk proj. [`319b5aa`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/319b5aa1e0177cc4a70e1ce85168407adf3e39d2)
- Revert "get and put bucket tagging." [`13ca0c8`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/13ca0c88e0e795f403e360f14219572fbf290759)
- get and put bucket tagging. [`547bab0`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/547bab08a53cd736f026615ff0cea839354b9188)
- fix get object bytes issue [`7ea8704`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/7ea8704a2e49f81560810f26f9047e05fef13bf0)
- update ut. [`9b9ad6e`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/9b9ad6e9d20bf20c42e6adb2f84bf05f8e9e4601)
- update travis.xml [`f591a44`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/f591a4438a9bf12f43d593a1b259a4683103b7e5)
- remove get version logic. [`3be66a0`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/3be66a0e8badad358695f912e33568c2a0e80787)
- add bucket for env [`ea69a42`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/ea69a42fccb6c2eeb4fcb6e95a18e2bd9ec061fd)
- update README. [`a07b09d`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/a07b09ddd54fef72204e5b3eadce2fd351e391ae)
- update travis [`929c849`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/929c849a96957a35ad5c065de520b816da5f1191)
- update travis [`b3f25a4`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/b3f25a4dad3eeb0cebd85b85c2715bacec48abd2)
- update travis. [`3caf919`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/3caf919c3122d96e70ed10ebcc298437c656af06)

## 5.4.5 - 2019-09-17

Update to .Net standard library.

### Commits

- add ut [`7bcf835`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/7bcf835d2fdc270c0958279d6bfe1e7995444af2)
- add dll 文件 [`3e7813d`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/3e7813d2bbbb45448d6ef27c7a4724c04c822b39)
- remove temp file. [`828d41b`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/828d41b9bee81cac0d330b47095d95eb9df6d5c4)
- update 5.4.1 [`d794224`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/d79422416dbb5ba45c7554ee9f351400a9cb3686)
- update travis.yml [`f89169c`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/f89169c46ab85a58e7443db2d1115edddbe5433a)
- add C# sdk [`df1aaca`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/df1aaca28fe42c83d8c6b4fb4771678606f4b249)
- update libs [`37e4e3f`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/37e4e3feb02d212411a67443166600764afa693b)
- update libs [`8257e3b`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/8257e3b4f8b2567dea2958bba782fa9678d86503)
- add 接口文档 [`c9461c0`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/c9461c0467353d3831e09b594fadf14d84effc6c)
- update ut [`f3ba05e`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/f3ba05e6c32d2dce570857d1b73fb7f5466eb328)
- update libs [`ac37834`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/ac3783426bb13f5f9460c2a81e044ffa6f44f4e4)
- add 注释 [`d485bd4`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/d485bd4b72f61ddd57041b8aba107935ed9c48da)
- update C# sdk [`32a8db9`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/32a8db92f6d549cfc067b1b5a0af83a2cedfc36f)
- update 5.4.3 [`3e4b492`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/3e4b4928c9594fd1c14c801a1865bded615d4272)
- update 5.4.3 [`b2539ed`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/b2539ed2d02cda1aee0110e933029c16f40b4e80)
- update cosxml [`0893263`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/089326322e0a0ba504b278237a57963825350a19)
- update sdk [`1082b9a`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/1082b9a2f840f806e7c2c41b55906db2a6fb30bc)
- update for functiosn called stack [`2355e28`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/2355e28619c56706907acc28fc02d1c8c334c7aa)
- rename muliti-&gt; multiupart [`94644c5`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/94644c5e5774402341465c43b1d9e2ed63caca23)
- add download bytes [`5ef818b`](https://github.com/tencentyun/qcloud-sdk-dotnet/commit/5ef818bd3a5478186fbd08b7c1bf05279db6ee07)
