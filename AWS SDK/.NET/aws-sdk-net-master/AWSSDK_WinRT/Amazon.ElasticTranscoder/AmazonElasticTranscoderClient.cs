/*
 * Copyright 2010-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Threading;
using System.Threading.Tasks;

using Amazon.ElasticTranscoder.Model;
using Amazon.ElasticTranscoder.Model.Internal.MarshallTransformations;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Auth;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.ElasticTranscoder
{
    /// <summary>
    /// Implementation for accessing AmazonElasticTranscoder.
    /// 
    /// AWS Elastic Transcoder Service <para>The AWS Elastic Transcoder Service.</para>
    /// </summary>
	public partial class AmazonElasticTranscoderClient : AmazonWebServiceClient, Amazon.ElasticTranscoder.IAmazonElasticTranscoder
    {

        AWS4Signer signer = new AWS4Signer();
        #region Constructors

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        public AmazonElasticTranscoderClient(AWSCredentials credentials)
            : this(credentials, new AmazonElasticTranscoderConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Credentials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        public AmazonElasticTranscoderClient(AWSCredentials credentials, RegionEndpoint region)
            : this(credentials, new AmazonElasticTranscoderConfig(){RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Credentials and an
        /// AmazonElasticTranscoderClient Configuration object.
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="clientConfig">The AmazonElasticTranscoderClient Configuration Object</param>
        public AmazonElasticTranscoderClient(AWSCredentials credentials, AmazonElasticTranscoderConfig clientConfig)
            : base(credentials, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonElasticTranscoderConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, new AmazonElasticTranscoderConfig() {RegionEndpoint=region})
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonElasticTranscoderClient Configuration object. If the config object's
        /// UseSecureStringForAwsSecretKey is false, the AWS Secret Key
        /// is stored as a clear-text string. Please use this option only
        /// if the application environment doesn't allow the use of SecureStrings.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="clientConfig">The AmazonElasticTranscoderClient Configuration Object</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey, AmazonElasticTranscoderConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonElasticTranscoderConfig())
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID and AWS Secret Key
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region)
            : this(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, new AmazonElasticTranscoderConfig(){RegionEndpoint = region})
        {
        }

        /// <summary>
        /// Constructs AmazonElasticTranscoderClient with AWS Access Key ID, AWS Secret Key and an
        /// AmazonElasticTranscoderClient Configuration object. If the config object's
        /// UseSecureStringForAwsSecretKey is false, the AWS Secret Key
        /// is stored as a clear-text string. Please use this option only
        /// if the application environment doesn't allow the use of SecureStrings.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="clientConfig">The AmazonElasticTranscoderClient Configuration Object</param>
        public AmazonElasticTranscoderClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, AmazonElasticTranscoderConfig clientConfig)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig, AuthenticationTypes.User | AuthenticationTypes.Session)
        {
        }

        #endregion

 
		internal CancelJobResponse CancelJob(CancelJobRequest request)
        {
            var task = CancelJobAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The CancelJob operation cancels an unfinished job.</para> <para><b>NOTE:</b>You can only cancel a job that has a status of Submitted.
        /// To prevent a pipeline from starting to process a job while you're getting the job identifier, use UpdatePipelineStatus to temporarily pause
        /// the pipeline.</para>
        /// </summary>
        /// 
        /// <param name="cancelJobRequest">Container for the necessary parameters to execute the CancelJob service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the CancelJob service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceInUseException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<CancelJobResponse> CancelJobAsync(CancelJobRequest cancelJobRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new CancelJobRequestMarshaller();
            var unmarshaller = CancelJobResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, CancelJobRequest, CancelJobResponse>(cancelJobRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal CreateJobResponse CreateJob(CreateJobRequest request)
        {
            var task = CreateJobAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para> When you create a job, Elastic Transcoder returns JSON data that includes the values that you specified plus information about the
        /// job that is created. </para> <para>If you have specified more than one output for your jobs (for example, one output for the Kindle Fire and
        /// another output for the Apple iPhone 4s), you currently must use the Elastic Transcoder API to list the jobs (as opposed to the AWS
        /// Console).</para>
        /// </summary>
        /// 
        /// <param name="createJobRequest">Container for the necessary parameters to execute the CreateJob service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the CreateJob service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.LimitExceededException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<CreateJobResponse> CreateJobAsync(CreateJobRequest createJobRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new CreateJobRequestMarshaller();
            var unmarshaller = CreateJobResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, CreateJobRequest, CreateJobResponse>(createJobRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal CreatePipelineResponse CreatePipeline(CreatePipelineRequest request)
        {
            var task = CreatePipelineAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The CreatePipeline operation creates a pipeline with settings that you specify.</para>
        /// </summary>
        /// 
        /// <param name="createPipelineRequest">Container for the necessary parameters to execute the CreatePipeline service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the CreatePipeline service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.LimitExceededException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<CreatePipelineResponse> CreatePipelineAsync(CreatePipelineRequest createPipelineRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new CreatePipelineRequestMarshaller();
            var unmarshaller = CreatePipelineResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, CreatePipelineRequest, CreatePipelineResponse>(createPipelineRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal CreatePresetResponse CreatePreset(CreatePresetRequest request)
        {
            var task = CreatePresetAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The CreatePreset operation creates a preset with settings that you specify.</para> <para><b>IMPORTANT:</b>Elastic Transcoder checks
        /// the CreatePreset settings to ensure that they meet Elastic Transcoder requirements and to determine whether they comply with H.264
        /// standards. If your settings are not valid for Elastic Transcoder, Elastic Transcoder returns an HTTP 400 response (ValidationException) and
        /// does not create the preset. If the settings are valid for Elastic Transcoder but aren't strictly compliant with the H.264 standard, Elastic
        /// Transcoder creates the preset and returns a warning message in the response. This helps you determine whether your settings comply with the
        /// H.264 standard while giving you greater flexibility with respect to the video that Elastic Transcoder produces.</para> <para>Elastic
        /// Transcoder uses the H.264 video-compression format. For more information, see the International Telecommunication Union publication
        /// <i>Recommendation ITU-T H.264: Advanced video coding for generic audiovisual services</i> .</para>
        /// </summary>
        /// 
        /// <param name="createPresetRequest">Container for the necessary parameters to execute the CreatePreset service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the CreatePreset service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.LimitExceededException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<CreatePresetResponse> CreatePresetAsync(CreatePresetRequest createPresetRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new CreatePresetRequestMarshaller();
            var unmarshaller = CreatePresetResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, CreatePresetRequest, CreatePresetResponse>(createPresetRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal DeletePipelineResponse DeletePipeline(DeletePipelineRequest request)
        {
            var task = DeletePipelineAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The DeletePipeline operation removes a pipeline.</para> <para> You can only delete a pipeline that has never been used or that is not
        /// currently in use (doesn't contain any active jobs). If the pipeline is currently in use, <c>DeletePipeline</c> returns an error. </para>
        /// </summary>
        /// 
        /// <param name="deletePipelineRequest">Container for the necessary parameters to execute the DeletePipeline service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the DeletePipeline service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceInUseException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<DeletePipelineResponse> DeletePipelineAsync(DeletePipelineRequest deletePipelineRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new DeletePipelineRequestMarshaller();
            var unmarshaller = DeletePipelineResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, DeletePipelineRequest, DeletePipelineResponse>(deletePipelineRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal DeletePresetResponse DeletePreset(DeletePresetRequest request)
        {
            var task = DeletePresetAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The DeletePreset operation removes a preset that you've added in an AWS region.</para> <para><b>NOTE:</b> You can't delete the default
        /// presets that are included with Elastic Transcoder. </para>
        /// </summary>
        /// 
        /// <param name="deletePresetRequest">Container for the necessary parameters to execute the DeletePreset service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the DeletePreset service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<DeletePresetResponse> DeletePresetAsync(DeletePresetRequest deletePresetRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new DeletePresetRequestMarshaller();
            var unmarshaller = DeletePresetResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, DeletePresetRequest, DeletePresetResponse>(deletePresetRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ListJobsByPipelineResponse ListJobsByPipeline(ListJobsByPipelineRequest request)
        {
            var task = ListJobsByPipelineAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ListJobsByPipeline operation gets a list of the jobs currently in a pipeline.</para> <para>Elastic Transcoder returns all of the
        /// jobs currently in the specified pipeline. The response body contains one element for each job that satisfies the search criteria.</para>
        /// </summary>
        /// 
        /// <param name="listJobsByPipelineRequest">Container for the necessary parameters to execute the ListJobsByPipeline service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ListJobsByPipeline service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ListJobsByPipelineResponse> ListJobsByPipelineAsync(ListJobsByPipelineRequest listJobsByPipelineRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ListJobsByPipelineRequestMarshaller();
            var unmarshaller = ListJobsByPipelineResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ListJobsByPipelineRequest, ListJobsByPipelineResponse>(listJobsByPipelineRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ListJobsByStatusResponse ListJobsByStatus(ListJobsByStatusRequest request)
        {
            var task = ListJobsByStatusAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ListJobsByStatus operation gets a list of jobs that have a specified status. The response body contains one element for each job
        /// that satisfies the search criteria.</para>
        /// </summary>
        /// 
        /// <param name="listJobsByStatusRequest">Container for the necessary parameters to execute the ListJobsByStatus service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ListJobsByStatus service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ListJobsByStatusResponse> ListJobsByStatusAsync(ListJobsByStatusRequest listJobsByStatusRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ListJobsByStatusRequestMarshaller();
            var unmarshaller = ListJobsByStatusResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ListJobsByStatusRequest, ListJobsByStatusResponse>(listJobsByStatusRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ListPipelinesResponse ListPipelines(ListPipelinesRequest request)
        {
            var task = ListPipelinesAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ListPipelines operation gets a list of the pipelines associated with the current AWS account.</para>
        /// </summary>
        /// 
        /// <param name="listPipelinesRequest">Container for the necessary parameters to execute the ListPipelines service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ListPipelines service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ListPipelinesResponse> ListPipelinesAsync(ListPipelinesRequest listPipelinesRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ListPipelinesRequestMarshaller();
            var unmarshaller = ListPipelinesResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ListPipelinesRequest, ListPipelinesResponse>(listPipelinesRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ListPresetsResponse ListPresets(ListPresetsRequest request)
        {
            var task = ListPresetsAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ListPresets operation gets a list of the default presets included with Elastic Transcoder and the presets that you've added in an
        /// AWS region.</para>
        /// </summary>
        /// 
        /// <param name="listPresetsRequest">Container for the necessary parameters to execute the ListPresets service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ListPresets service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ListPresetsResponse> ListPresetsAsync(ListPresetsRequest listPresetsRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ListPresetsRequestMarshaller();
            var unmarshaller = ListPresetsResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ListPresetsRequest, ListPresetsResponse>(listPresetsRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ReadJobResponse ReadJob(ReadJobRequest request)
        {
            var task = ReadJobAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ReadJob operation returns detailed information about a job.</para>
        /// </summary>
        /// 
        /// <param name="readJobRequest">Container for the necessary parameters to execute the ReadJob service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ReadJob service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ReadJobResponse> ReadJobAsync(ReadJobRequest readJobRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ReadJobRequestMarshaller();
            var unmarshaller = ReadJobResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ReadJobRequest, ReadJobResponse>(readJobRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ReadPipelineResponse ReadPipeline(ReadPipelineRequest request)
        {
            var task = ReadPipelineAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ReadPipeline operation gets detailed information about a pipeline.</para>
        /// </summary>
        /// 
        /// <param name="readPipelineRequest">Container for the necessary parameters to execute the ReadPipeline service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ReadPipeline service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ReadPipelineResponse> ReadPipelineAsync(ReadPipelineRequest readPipelineRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ReadPipelineRequestMarshaller();
            var unmarshaller = ReadPipelineResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ReadPipelineRequest, ReadPipelineResponse>(readPipelineRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal ReadPresetResponse ReadPreset(ReadPresetRequest request)
        {
            var task = ReadPresetAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The ReadPreset operation gets detailed information about a preset.</para>
        /// </summary>
        /// 
        /// <param name="readPresetRequest">Container for the necessary parameters to execute the ReadPreset service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the ReadPreset service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<ReadPresetResponse> ReadPresetAsync(ReadPresetRequest readPresetRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new ReadPresetRequestMarshaller();
            var unmarshaller = ReadPresetResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, ReadPresetRequest, ReadPresetResponse>(readPresetRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal TestRoleResponse TestRole(TestRoleRequest request)
        {
            var task = TestRoleAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The TestRole operation tests the IAM role used to create the pipeline.</para> <para>The <c>TestRole</c> action lets you determine
        /// whether the IAM role you are using has sufficient permissions to let Elastic Transcoder perform tasks associated with the transcoding
        /// process. The action attempts to assume the specified IAM role, checks read access to the input and output buckets, and tries to send a test
        /// notification to Amazon SNS topics that you specify.</para>
        /// </summary>
        /// 
        /// <param name="testRoleRequest">Container for the necessary parameters to execute the TestRole service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the TestRole service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<TestRoleResponse> TestRoleAsync(TestRoleRequest testRoleRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new TestRoleRequestMarshaller();
            var unmarshaller = TestRoleResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, TestRoleRequest, TestRoleResponse>(testRoleRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal UpdatePipelineResponse UpdatePipeline(UpdatePipelineRequest request)
        {
            var task = UpdatePipelineAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para> Use the <c>UpdatePipeline</c> operation to update settings for a pipeline. <para><b>IMPORTANT:</b>When you change pipeline settings,
        /// your changes take effect immediately. Jobs that you have already submitted and that Elastic Transcoder has not started to process are
        /// affected in addition to jobs that you submit after you change settings. </para> </para>
        /// </summary>
        /// 
        /// <param name="updatePipelineRequest">Container for the necessary parameters to execute the UpdatePipeline service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the UpdatePipeline service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceInUseException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<UpdatePipelineResponse> UpdatePipelineAsync(UpdatePipelineRequest updatePipelineRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new UpdatePipelineRequestMarshaller();
            var unmarshaller = UpdatePipelineResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, UpdatePipelineRequest, UpdatePipelineResponse>(updatePipelineRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal UpdatePipelineNotificationsResponse UpdatePipelineNotifications(UpdatePipelineNotificationsRequest request)
        {
            var task = UpdatePipelineNotificationsAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>With the UpdatePipelineNotifications operation, you can update Amazon Simple Notification Service (Amazon SNS) notifications for a
        /// pipeline.</para> <para>When you update notifications for a pipeline, Elastic Transcoder returns the values that you specified in the
        /// request.</para>
        /// </summary>
        /// 
        /// <param name="updatePipelineNotificationsRequest">Container for the necessary parameters to execute the UpdatePipelineNotifications service
        /// method on AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the UpdatePipelineNotifications service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceInUseException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<UpdatePipelineNotificationsResponse> UpdatePipelineNotificationsAsync(UpdatePipelineNotificationsRequest updatePipelineNotificationsRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new UpdatePipelineNotificationsRequestMarshaller();
            var unmarshaller = UpdatePipelineNotificationsResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, UpdatePipelineNotificationsRequest, UpdatePipelineNotificationsResponse>(updatePipelineNotificationsRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
 
		internal UpdatePipelineStatusResponse UpdatePipelineStatus(UpdatePipelineStatusRequest request)
        {
            var task = UpdatePipelineStatusAsync(request);
            try
            {
                return task.Result;
            }
            catch(AggregateException e)
            {
                throw e.InnerException;
            }
        }

        /// <summary>
        /// <para>The UpdatePipelineStatus operation pauses or reactivates a pipeline, so that the pipeline stops or restarts the processing of
        /// jobs.</para> <para>Changing the pipeline status is useful if you want to cancel one or more jobs. You can't cancel jobs after Elastic
        /// Transcoder has started processing them; if you pause the pipeline to which you submitted the jobs, you have more time to get the job IDs for
        /// the jobs that you want to cancel, and to send a CancelJob request. </para>
        /// </summary>
        /// 
        /// <param name="updatePipelineStatusRequest">Container for the necessary parameters to execute the UpdatePipelineStatus service method on
        /// AmazonElasticTranscoder.</param>
        /// 
        /// <returns>The response from the UpdatePipelineStatus service method, as returned by AmazonElasticTranscoder.</returns>
        /// 
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceNotFoundException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.AccessDeniedException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ResourceInUseException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.InternalServiceException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.ValidationException" />
        /// <exception cref="T:Amazon.ElasticTranscoder.Model.IncompatibleVersionException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		public async Task<UpdatePipelineStatusResponse> UpdatePipelineStatusAsync(UpdatePipelineStatusRequest updatePipelineStatusRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            var marshaller = new UpdatePipelineStatusRequestMarshaller();
            var unmarshaller = UpdatePipelineStatusResponseUnmarshaller.GetInstance();
            var response = await Invoke<IRequest, UpdatePipelineStatusRequest, UpdatePipelineStatusResponse>(updatePipelineStatusRequest, marshaller, unmarshaller, signer, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
    }
}
