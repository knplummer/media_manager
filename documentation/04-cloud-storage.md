# 04. Cloud Storage Strategy

## Overview
Media Manager requires high-reliability cloud storage for digital preservation. The system supports connecting multiple S3-compatible cloud providers simultaneously (AWS S3, Backblaze B2, Google Cloud Storage) and intelligently routing file uploads based on available storage quotas.

## Implementation Guidelines

### 1. Unified S3 Compatibility
* Do **not** use provider-specific SDKs (e.g., Azure Blob SDK, Google Cloud Storage SDK) unless absolutely necessary.
* Use the official `AWSSDK.S3` library. AWS S3, B2, and GCS all expose standard S3-compatible endpoints that can be configured by overriding the `ServiceURL`.
* Abstract all cloud interaction behind a single `ICloudStorageService` interface.

### 2. Multi-Provider Configuration
* Cloud providers are configured via an array in `appsettings.json`. The database does **not** store cloud API keys.
* Example Configuration:
  ```json
  "CloudProviders": [
    {
      "ProviderId": "b2-main",
      "ServiceUrl": "https://s3.us-west-004.backblazeb2.com",
      "AccessKey": "...",
      "SecretKey": "..."
    }
  ]
  ```
* When an upload is routed to a provider, the `ProviderId` is saved in the `Media` table under the `ExternalStorageKey` column.

### 3. API-Proxied Streaming (No Buffering)
Because the Web API dynamically routes uploads based on quotas, it must intercept the upload stream.
* **CRITICAL RULE:** Do not buffer incoming file uploads into server memory (`IFormFile` loaded entirely) or the local API server disk.
* **Stream directly:** Read the HTTP Request Body stream and pipe it directly into the `AWSSDK.S3` client's upload stream. This ensures the backend uses very little memory even when handling 50GB files.

## Routing Logic Requirements
When a file upload request arrives, the Web API must:
1. Identify the incoming file size.
2. Read the list of available providers from configuration.
3. Select the optimal provider based on available quotas.
4. Instantiate the S3 client for that specific provider and execute the streamed upload.
