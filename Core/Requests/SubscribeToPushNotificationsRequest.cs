// ---------------------------------------------------------------------------
// <copyright file="SubscribeToPushNotificationsRequest.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the SubscribeToPushNotificationsRequest class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a "push" Subscribe request.
    /// </summary>
    internal class SubscribeToPushNotificationsRequest : SubscribeRequest<PushSubscription>
    {
        private int frequency = 30;
        private Uri url;
        private string callerData;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeToPushNotificationsRequest"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        internal SubscribeToPushNotificationsRequest(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Validate request.
        /// </summary>
        internal override void Validate()
        {
            base.Validate();
            EwsUtilities.ValidateParam(this.Url, "Url");
            if ((this.Frequency < 1) || (this.Frequency > 1440))
            {
                throw new ArgumentException(string.Format(Strings.InvalidFrequencyValue, this.Frequency));
            }
        }

        /// <summary>
        /// Gets the name of the subscription XML element.
        /// </summary>
        /// <returns>XML element name.</returns>
        internal override string GetSubscriptionXmlElementName()
        {
            return XmlElementNames.PushSubscriptionRequest;
        }

        /// <summary>
        /// Internals the write elements to XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void InternalWriteElementsToXml(EwsServiceXmlWriter writer)
        {
            writer.WriteElementValue(
                XmlNamespace.Types,
                XmlElementNames.StatusFrequency,
                this.Frequency);

            writer.WriteElementValue(
                XmlNamespace.Types,
                XmlElementNames.URL,
                this.Url.ToString());

            if (this.Service.RequestedServerVersion >= ExchangeVersion.Exchange2013
                && !String.IsNullOrEmpty(this.callerData))
            {
                writer.WriteElementValue(
                    XmlNamespace.Types,
                    XmlElementNames.CallerData,
                    this.CallerData);
            }
        }

        /// <summary>
        /// Adds the json properties.
        /// </summary>
        /// <param name="jsonSubscribeRequest">The json subscribe request.</param>
        /// <param name="service">The service.</param>
        internal override void AddJsonProperties(JsonObject jsonSubscribeRequest, ExchangeService service)
        {
            jsonSubscribeRequest.Add(XmlElementNames.StatusFrequency, this.Frequency);
            jsonSubscribeRequest.Add(XmlElementNames.URL, this.Url.ToString());
        }

        /// <summary>
        /// Creates the service response.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="responseIndex">Index of the response.</param>
        /// <returns>Service response.</returns>
        internal override SubscribeResponse<PushSubscription> CreateServiceResponse(ExchangeService service, int responseIndex)
        {
            return new SubscribeResponse<PushSubscription>(new PushSubscription(service));
        }

        /// <summary>
        /// Gets the request version.
        /// </summary>
        /// <returns>Earliest Exchange version in which this request is supported.</returns>
        internal override ExchangeVersion GetMinimumRequiredServerVersion()
        {
            return ExchangeVersion.Exchange2007_SP1;
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public int Frequency
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url
        {
            get { return this.url; }
            set { this.url = value; }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string CallerData
        {
            get { return this.callerData; }
            set { this.callerData = value; }
        }
    }
}