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

using Amazon.CloudTrail.Model;

namespace Amazon.CloudTrail
{
    /// <summary>
    /// Interface for accessing AmazonCloudTrail.
    /// 
    /// AWS Cloud Trail <para>This is the CloudTrail API Reference. It provides descriptions of actions, data types, common parameters, and common
    /// errors for CloudTrail.</para> <para>CloudTrail is a web service that records AWS API calls for your AWS account and delivers log files to an
    /// Amazon S3 bucket. The recorded information includes the identity of the user, the start time of the AWS API call, the source IP address, the
    /// request parameters, and the response elements returned by the service.</para> <para><b>NOTE:</b> As an alternative to using the API, you can
    /// use one of the AWS SDKs, which consist of libraries and sample code for various programming languages and platforms (Java, Ruby, .NET, iOS,
    /// Android, etc.). The SDKs provide a convenient way to create programmatic access to AWSCloudTrail. For example, the SDKs take care of
    /// cryptographically signing requests, managing errors, and retrying requests automatically. For information about the AWS SDKs, including how
    /// to download and install them, see the Tools for Amazon Web Services page. </para> <para>See the CloudTrail User Guide for information about
    /// the data that is included with each AWS API call listed in the log files.</para>
    /// </summary>
	public partial interface IAmazonCloudTrail : IDisposable
    {
 
        /// <summary>
        /// <para>From the command line, use create-subscription. </para> <para>Creates a trail that specifies the settings for delivery of log data to
        /// an Amazon S3 bucket. The request includes a Trail structure that specifies the following:</para>
        /// <ul>
        /// <li>Trail name.</li>
        /// <li>The name of an existing Amazon S3 bucket to which CloudTrail delivers your log files.</li>
        /// <li>The name of the Amazon S3 key prefix that precedes each log file.</li>
        /// <li>The name of an existing Amazon SNS topic that notifies you that a new file is available in your bucket.</li>
        /// <li>Whether the log file should include AWS API calls from global services. Currently, the only global AWS API calls included in CloudTrail
        /// log files are from IAM and AWS STS.</li>
        /// 
        /// </ul>
        /// </summary>
        /// 
        /// <param name="createTrailRequest">Container for the necessary parameters to execute the CreateTrail service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the CreateTrail service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.S3BucketDoesNotExistException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidS3PrefixException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailAlreadyExistsException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.MaximumNumberOfTrailsExceededException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidS3BucketNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotProvidedException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InsufficientSnsTopicPolicyException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidSnsTopicNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InsufficientS3BucketPolicyException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<CreateTrailResponse> CreateTrailAsync(CreateTrailRequest createTrailRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Deletes a trail.</para>
        /// </summary>
        /// 
        /// <param name="deleteTrailRequest">Container for the necessary parameters to execute the DeleteTrail service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the DeleteTrail service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotFoundException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<DeleteTrailResponse> DeleteTrailAsync(DeleteTrailRequest deleteTrailRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Retrieves the settings for some or all trails associated with an account. Returns a list of Trail structures in JSON format.</para>
        /// </summary>
        /// 
        /// <param name="describeTrailsRequest">Container for the necessary parameters to execute the DescribeTrails service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the DescribeTrails service method, as returned by AmazonCloudTrail.</returns>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<DescribeTrailsResponse> DescribeTrailsAsync(DescribeTrailsRequest describeTrailsRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Returns a JSON-formatted list of information about the specified trail. Fields include information such as delivery errors, Amazon SNS
        /// and Amazon S3 errors, and times that logging started and stopped for each trail.</para>
        /// </summary>
        /// 
        /// <param name="getTrailStatusRequest">Container for the necessary parameters to execute the GetTrailStatus service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the GetTrailStatus service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotFoundException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<GetTrailStatusResponse> GetTrailStatusAsync(GetTrailStatusRequest getTrailStatusRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Starts the recording of AWS API calls and log file delivery for a trail.</para>
        /// </summary>
        /// 
        /// <param name="startLoggingRequest">Container for the necessary parameters to execute the StartLogging service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the StartLogging service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotFoundException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<StartLoggingResponse> StartLoggingAsync(StartLoggingRequest startLoggingRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Suspends the recording of AWS API calls and log file delivery for the specified trail. Under most circumstances, there is no need to
        /// use this action. You can update a trail without stopping it first. This action is the only way to stop recording.</para>
        /// </summary>
        /// 
        /// <param name="stopLoggingRequest">Container for the necessary parameters to execute the StopLogging service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the StopLogging service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotFoundException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<StopLoggingResponse> StopLoggingAsync(StopLoggingRequest stopLoggingRequest, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>From the command line, use update-subscription.</para> <para>Updates the settings that specify delivery of log files. Changes to a
        /// trail do not require stopping the CloudTrail service. You use this action to designate an existing bucket for log delivery. If the existing
        /// bucket has previously been a target for CloudTrail log files, an IAM policy exists for the bucket. Use a Trail data type to pass updated
        /// bucket or topic names.</para>
        /// </summary>
        /// 
        /// <param name="updateTrailRequest">Container for the necessary parameters to execute the UpdateTrail service method on
        /// AmazonCloudTrail.</param>
        /// 
        /// <returns>The response from the UpdateTrail service method, as returned by AmazonCloudTrail.</returns>
        /// 
        /// <exception cref="T:Amazon.CloudTrail.Model.S3BucketDoesNotExistException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidS3PrefixException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidTrailNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidS3BucketNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotProvidedException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InsufficientSnsTopicPolicyException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InvalidSnsTopicNameException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.InsufficientS3BucketPolicyException" />
        /// <exception cref="T:Amazon.CloudTrail.Model.TrailNotFoundException" />
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
		Task<UpdateTrailResponse> UpdateTrailAsync(UpdateTrailRequest updateTrailRequest, CancellationToken cancellationToken = default(CancellationToken));
    }
}
