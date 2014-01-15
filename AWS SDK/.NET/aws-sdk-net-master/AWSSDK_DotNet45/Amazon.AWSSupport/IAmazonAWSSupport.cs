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

using Amazon.AWSSupport.Model;

namespace Amazon.AWSSupport
{
    /// <summary>
    /// Interface for accessing AmazonAWSSupport.
    /// 
    /// AWS Support <para>The AWS Support API reference is intended for programmers who need detailed information about the AWS Support actions and
    /// data types. This service enables you to manage your AWS Support cases programmatically. It uses HTTP methods that return results in JSON
    /// format.</para> <para>The AWS Support service also exposes a set of <a href="https://aws.amazon.com/support/trustedadvisor">Trusted
    /// Advisor</a> features. You can retrieve a list of checks and their descriptions, get check results, specify checks to refresh, and get the
    /// refresh status of checks. </para> <para>The following list describes the AWS Support case management actions: </para>
    /// <ul>
    /// <li> <b>Service names, issue categories, and available severity levels. </b> The actions DescribeServices and DescribeSeverityLevels enable
    /// you to obtain AWS service names, service codes, service categories, and problem severity levels. You use these values when you call the
    /// CreateCase action. </li>
    /// <li> <b>Case creation, case details, and case resolution.</b> The actions CreateCase, DescribeCases, and ResolveCase enable you to create
    /// AWS Support cases, retrieve them, and resolve them.</li>
    /// <li> <b>Case communication.</b> The actions DescribeCommunications and AddCommunicationToCase enable you to retrieve and add communication
    /// to AWS Support cases. </li>
    /// 
    /// </ul>
    /// <para>The following list describes the actions available from the AWS Support service for Trusted Advisor:</para>
    /// <ul>
    /// <li> DescribeTrustedAdvisorChecks returns the list of checks that run against your AWS resources.</li>
    /// <li>Using the CheckId for a specific check returned by DescribeTrustedAdvisorChecks, you can call DescribeTrustedAdvisorCheckResult to
    /// obtain the results for the check you specified.</li>
    /// <li> DescribeTrustedAdvisorCheckSummaries returns summarized results for one or more Trusted Advisor checks.</li>
    /// <li> RefreshTrustedAdvisorCheck requests that Trusted Advisor rerun a specified check. </li>
    /// <li> DescribeTrustedAdvisorCheckRefreshStatuses reports the refresh status of one or more checks. </li>
    /// 
    /// </ul>
    /// <para>For authentication of requests, AWS Support uses <a href="http://docs.aws.amazon.com/general/latest/gr/signature-version-4.html">Signature Version 4 Signing Process</a> .</para> <para>See the AWS Support <a href="http://docs.aws.amazon.com/awssupport/latest/user/Welcome.html">User Guide</a> for information about how to use this service to
    /// create and manage your support cases, and how to call Trusted Advisor for results of checks on your resources. </para>
    /// </summary>
	public partial interface IAmazonAWSSupport : IDisposable
    {
 

        /// <summary>
        /// <para>Adds additional customer communication to an AWS Support case. You use the <c>CaseId</c> value to identify the case to add
        /// communication to. You can list a set of email addresses to copy on the communication using the <c>CcEmailAddresses</c> value. The
        /// <c>CommunicationBody</c> value contains the text of the communication.</para> <para>The response indicates the success or failure of the
        /// request.</para> <para>This operation implements a subset of the behavior on the AWS Support <a href="https://aws.amazon.com/support">Your
        /// Support Cases</a> web form.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the AddCommunicationToCase service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the AddCommunicationToCase service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		AddCommunicationToCaseResponse AddCommunicationToCase(AddCommunicationToCaseRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the AddCommunicationToCase operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.AddCommunicationToCase"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the AddCommunicationToCase operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<AddCommunicationToCaseResponse> AddCommunicationToCaseAsync(AddCommunicationToCaseRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Creates a new case in the AWS Support Center. This operation is modeled on the behavior of the AWS Support Center <a href="https://aws.amazon.com/support/createCase">Open a new case</a> page. Its parameters require you to specify the following information:
        /// </para> <ol> <li> <b>ServiceCode.</b> The code for an AWS service. You obtain the <c>ServiceCode</c> by calling DescribeServices. </li>
        /// <li> <b>CategoryCode.</b> The category for the service defined for the <c>ServiceCode</c> value. You also obtain the category code for a
        /// service by calling DescribeServices. Each AWS service defines its own set of category codes. </li>
        /// <li> <b>SeverityCode.</b> A value that indicates the urgency of the case, which in turn determines the response time according to your
        /// service level agreement with AWS Support. You obtain the SeverityCode by calling DescribeSeverityLevels.</li>
        /// <li> <b>Subject.</b> The <b>Subject</b> field on the AWS Support Center <a href="https://aws.amazon.com/support/createCase">Open a new
        /// case</a> page.</li>
        /// <li> <b>CommunicationBody.</b> The <b>Description</b> field on the AWS Support Center <a href="https://aws.amazon.com/support/createCase">Open a new case</a> page.</li>
        /// <li> <b>Language.</b> The human language in which AWS Support handles the case. English and Japanese are currently supported.</li>
        /// <li> <b>CcEmailAddresses.</b> The AWS Support Center <b>CC</b> field on the <a href="https://aws.amazon.com/support/createCase">Open a
        /// new case</a> page. You can list email addresses to be copied on any correspondence about the case. The account that opens the case is
        /// already identified by passing the AWS Credentials in the HTTP POST method or in a method or function call from one of the programming
        /// languages supported by an <a href="http://aws.amazon.com/tools/">AWS SDK</a> . </li>
        /// <li> <b>IssueType.</b> The type of issue for the case. You can specify either "customer-service" or "technical." If you do not indicate a
        /// value, the default is "technical." </li>
        /// </ol> <para><b>NOTE:</b> The AWS Support API does not currently support the ability to add attachments to cases. You can, however, call
        /// AddCommunicationToCase to add information to an open case. </para> <para>A successful CreateCase request returns an AWS Support case number.
        /// Case numbers are used by the DescribeCases action to retrieve existing AWS Support cases. </para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateCase service method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the CreateCase service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseCreationLimitExceededException" />
		CreateCaseResponse CreateCase(CreateCaseRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the CreateCase operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.CreateCase"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the CreateCase operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<CreateCaseResponse> CreateCaseAsync(CreateCaseRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Returns a list of cases that you specify by passing one or more case IDs. In addition, you can filter the cases by date by setting
        /// values for the <c>AfterTime</c> and <c>BeforeTime</c> request parameters. </para> <para>The response returns the following in JSON
        /// format:</para> <ol> <li>One or more CaseDetails data types. </li>
        /// <li>One or more <c>NextToken</c> values, which specify where to paginate the returned records represented by the <c>CaseDetails</c>
        /// objects.</li>
        /// </ol>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeCases service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeCases service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		DescribeCasesResponse DescribeCases(DescribeCasesRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeCases operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeCases"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeCases operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeCasesResponse> DescribeCasesAsync(DescribeCasesRequest request, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Returns a list of cases that you specify by passing one or more case IDs. In addition, you can filter the cases by date by setting
        /// values for the <c>AfterTime</c> and <c>BeforeTime</c> request parameters. </para> <para>The response returns the following in JSON
        /// format:</para> <ol> <li>One or more CaseDetails data types. </li>
        /// <li>One or more <c>NextToken</c> values, which specify where to paginate the returned records represented by the <c>CaseDetails</c>
        /// objects.</li>
        /// </ol>
        /// </summary>
        /// 
        /// 
        /// <returns>The response from the DescribeCases service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		DescribeCasesResponse DescribeCases();
 

        /// <summary>
        /// <para>Returns communications regarding the support case. You can use the <c>AfterTime</c> and <c>BeforeTime</c> parameters to filter by
        /// date. The <c>CaseId</c> parameter enables you to identify a specific case by its <c>CaseId</c> value.</para> <para>The <c>MaxResults</c> and
        /// <c>NextToken</c> parameters enable you to control the pagination of the result set. Set <c>MaxResults</c> to the number of cases you want
        /// displayed on each page, and use <c>NextToken</c> to specify the resumption of pagination.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeCommunications service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeCommunications service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		DescribeCommunicationsResponse DescribeCommunications(DescribeCommunicationsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeCommunications operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeCommunications"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeCommunications operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeCommunicationsResponse> DescribeCommunicationsAsync(DescribeCommunicationsRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Returns the current list of AWS services and a list of service categories that applies to each one. You then use service names and
        /// categories in your CreateCase requests. Each AWS service has its own set of categories.</para> <para>The service codes and category codes
        /// correspond to the values that are displayed in the <b>Service</b> and <b>Category</b> drop-down lists on the AWS Support Center <a href="https://aws.amazon.com/support/createCase">Open a new case</a> page. The values in those fields, however, do not necessarily match
        /// the service codes and categories returned by the <c>DescribeServices</c> request. Always use the service codes and categories obtained
        /// programmatically. This practice ensures that you always have the most recent set of service and category codes.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeServices service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeServices service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeServicesResponse DescribeServices(DescribeServicesRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeServices operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeServices"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeServices operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeServicesResponse> DescribeServicesAsync(DescribeServicesRequest request, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Returns the current list of AWS services and a list of service categories that applies to each one. You then use service names and
        /// categories in your CreateCase requests. Each AWS service has its own set of categories.</para> <para>The service codes and category codes
        /// correspond to the values that are displayed in the <b>Service</b> and <b>Category</b> drop-down lists on the AWS Support Center <a href="https://aws.amazon.com/support/createCase">Open a new case</a> page. The values in those fields, however, do not necessarily match
        /// the service codes and categories returned by the <c>DescribeServices</c> request. Always use the service codes and categories obtained
        /// programmatically. This practice ensures that you always have the most recent set of service and category codes.</para>
        /// </summary>
        /// 
        /// 
        /// <returns>The response from the DescribeServices service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeServicesResponse DescribeServices();
 

        /// <summary>
        /// <para>Returns the list of severity levels that you can assign to an AWS Support case. The severity level for a case is also a field in the
        /// CaseDetails data type included in any CreateCase request. </para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeSeverityLevels service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeSeverityLevels service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeSeverityLevelsResponse DescribeSeverityLevels(DescribeSeverityLevelsRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeSeverityLevels operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeSeverityLevels"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeSeverityLevels operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeSeverityLevelsResponse> DescribeSeverityLevelsAsync(DescribeSeverityLevelsRequest request, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Returns the list of severity levels that you can assign to an AWS Support case. The severity level for a case is also a field in the
        /// CaseDetails data type included in any CreateCase request. </para>
        /// </summary>
        /// 
        /// 
        /// <returns>The response from the DescribeSeverityLevels service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeSeverityLevelsResponse DescribeSeverityLevels();
 

        /// <summary>
        /// <para>Returns the refresh status of the Trusted Advisor checks that have the specified check IDs. Check IDs can be obtained by calling
        /// DescribeTrustedAdvisorChecks.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the
        /// DescribeTrustedAdvisorCheckRefreshStatuses service method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeTrustedAdvisorCheckRefreshStatuses service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeTrustedAdvisorCheckRefreshStatusesResponse DescribeTrustedAdvisorCheckRefreshStatuses(DescribeTrustedAdvisorCheckRefreshStatusesRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeTrustedAdvisorCheckRefreshStatuses operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeTrustedAdvisorCheckRefreshStatuses"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTrustedAdvisorCheckRefreshStatuses operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeTrustedAdvisorCheckRefreshStatusesResponse> DescribeTrustedAdvisorCheckRefreshStatusesAsync(DescribeTrustedAdvisorCheckRefreshStatusesRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Returns the results of the Trusted Advisor check that has the specified check ID. Check IDs can be obtained by calling
        /// DescribeTrustedAdvisorChecks.</para> <para>The response contains a TrustedAdvisorCheckResult object, which contains these three
        /// objects:</para>
        /// <ul>
        /// <li> TrustedAdvisorCategorySpecificSummary </li>
        /// <li> TrustedAdvisorResourceDetail </li>
        /// <li> TrustedAdvisorResourcesSummary </li>
        /// 
        /// </ul>
        /// <para>In addition, the response contains these fields:</para>
        /// <ul>
        /// <li> <b>Status.</b> The alert status of the check: "ok" (green), "warning" (yellow), "error" (red), or "not_available".</li>
        /// <li> <b>Timestamp.</b> The time of the last refresh of the check.</li>
        /// <li> <b>CheckId.</b> The unique identifier for the check.</li>
        /// 
        /// </ul>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the
        /// DescribeTrustedAdvisorCheckResult service method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeTrustedAdvisorCheckResult service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeTrustedAdvisorCheckResultResponse DescribeTrustedAdvisorCheckResult(DescribeTrustedAdvisorCheckResultRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeTrustedAdvisorCheckResult operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeTrustedAdvisorCheckResult"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTrustedAdvisorCheckResult operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeTrustedAdvisorCheckResultResponse> DescribeTrustedAdvisorCheckResultAsync(DescribeTrustedAdvisorCheckResultRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Returns information about all available Trusted Advisor checks, including name, ID, category, description, and metadata. You must
        /// specify a language code; English ("en") and Japanese ("ja") are currently supported. The response contains a TrustedAdvisorCheckDescription
        /// for each check.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTrustedAdvisorChecks service
        /// method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeTrustedAdvisorChecks service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeTrustedAdvisorChecksResponse DescribeTrustedAdvisorChecks(DescribeTrustedAdvisorChecksRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeTrustedAdvisorChecks operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeTrustedAdvisorChecks"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTrustedAdvisorChecks operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeTrustedAdvisorChecksResponse> DescribeTrustedAdvisorChecksAsync(DescribeTrustedAdvisorChecksRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Returns the summaries of the results of the Trusted Advisor checks that have the specified check IDs. Check IDs can be obtained by
        /// calling DescribeTrustedAdvisorChecks.</para> <para>The response contains an array of TrustedAdvisorCheckSummary objects.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the
        /// DescribeTrustedAdvisorCheckSummaries service method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the DescribeTrustedAdvisorCheckSummaries service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		DescribeTrustedAdvisorCheckSummariesResponse DescribeTrustedAdvisorCheckSummaries(DescribeTrustedAdvisorCheckSummariesRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the DescribeTrustedAdvisorCheckSummaries operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.DescribeTrustedAdvisorCheckSummaries"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the DescribeTrustedAdvisorCheckSummaries operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<DescribeTrustedAdvisorCheckSummariesResponse> DescribeTrustedAdvisorCheckSummariesAsync(DescribeTrustedAdvisorCheckSummariesRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Requests a refresh of the Trusted Advisor check that has the specified check ID. Check IDs can be obtained by calling
        /// DescribeTrustedAdvisorChecks.</para> <para>The response contains a RefreshTrustedAdvisorCheckResult object, which contains these
        /// fields:</para>
        /// <ul>
        /// <li> <b>Status.</b> The refresh status of the check: "none", "enqueued", "processing", "success", or "abandoned".</li>
        /// <li> <b>MillisUntilNextRefreshable.</b> The amount of time, in milliseconds, until the check is eligible for refresh.</li>
        /// <li> <b>CheckId.</b> The unique identifier for the check.</li>
        /// 
        /// </ul>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the RefreshTrustedAdvisorCheck service
        /// method on AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the RefreshTrustedAdvisorCheck service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
		RefreshTrustedAdvisorCheckResponse RefreshTrustedAdvisorCheck(RefreshTrustedAdvisorCheckRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the RefreshTrustedAdvisorCheck operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.RefreshTrustedAdvisorCheck"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the RefreshTrustedAdvisorCheck operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<RefreshTrustedAdvisorCheckResponse> RefreshTrustedAdvisorCheckAsync(RefreshTrustedAdvisorCheckRequest request, CancellationToken cancellationToken = default(CancellationToken));
 

        /// <summary>
        /// <para>Takes a <c>CaseId</c> and returns the initial state of the case along with the state of the case after the call to ResolveCase
        /// completed.</para>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ResolveCase service method on
        /// AmazonAWSSupport.</param>
        /// 
        /// <returns>The response from the ResolveCase service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		ResolveCaseResponse ResolveCase(ResolveCaseRequest request);

        /// <summary>
        /// Initiates the asynchronous execution of the ResolveCase operation.
        /// <seealso cref="Amazon.AWSSupport.IAmazonAWSSupport.ResolveCase"/>
        /// </summary>
        /// 
        /// <param name="request">Container for the necessary parameters to execute the ResolveCase operation.</param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
		Task<ResolveCaseResponse> ResolveCaseAsync(ResolveCaseRequest request, CancellationToken cancellationToken = default(CancellationToken));
 
        /// <summary>
        /// <para>Takes a <c>CaseId</c> and returns the initial state of the case along with the state of the case after the call to ResolveCase
        /// completed.</para>
        /// </summary>
        /// 
        /// 
        /// <returns>The response from the ResolveCase service method, as returned by AmazonAWSSupport.</returns>
        /// 
        /// <exception cref="T:Amazon.AWSSupport.Model.InternalServerErrorException" />
        /// <exception cref="T:Amazon.AWSSupport.Model.CaseIdNotFoundException" />
		ResolveCaseResponse ResolveCase();
    }
}
