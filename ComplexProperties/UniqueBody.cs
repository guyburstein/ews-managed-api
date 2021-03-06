// ---------------------------------------------------------------------------
// <copyright file="UniqueBody.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

//-----------------------------------------------------------------------
// <summary>Defines the UniqueBody class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Exchange.WebServices.Data
{
    /// <summary>
    /// Represents the body part of an item that is unique to the conversation the item is part of. 
    /// </summary>
    public sealed class UniqueBody : ComplexProperty
    {
        private BodyType bodyType;
        private string text;
        private bool isTruncated;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueBody"/> class.
        /// </summary>
        internal UniqueBody()
        {
        }

        /// <summary>
        /// Defines an implicit conversion of UniqueBody into a string.
        /// </summary>
        /// <param name="messageBody">The UniqueBody to convert to a string.</param>
        /// <returns>A string containing the text of the UniqueBody.</returns>
        public static implicit operator string(UniqueBody messageBody)
        {
            EwsUtilities.ValidateParam(messageBody, "messageBody");
            return messageBody.Text;
        }

        /// <summary>
        /// Reads attributes from XML.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
        {
            this.bodyType = reader.ReadAttributeValue<BodyType>(XmlAttributeNames.BodyType);

            string attributeValue = reader.ReadAttributeValue(XmlAttributeNames.IsTruncated);
            if (!string.IsNullOrEmpty(attributeValue))
            {
                this.isTruncated = bool.Parse(attributeValue);
            }
        }

        /// <summary>
        /// Reads text value from XML.
        /// </summary>
        /// <param name="reader">The reader.</param>
        internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
        {
            this.text = reader.ReadValue();
        }

        /// <summary>
        /// Loads from json.
        /// </summary>
        /// <param name="jsonProperty">The json property.</param>
        /// <param name="service">The service.</param>
        internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
        {
            foreach (string key in jsonProperty.Keys)
            {
                switch (key)
                {
                    case XmlAttributeNames.BodyType:
                        this.bodyType = jsonProperty.ReadEnumValue<BodyType>(key);
                        break;
                    case XmlAttributeNames.IsTruncated:
                        this.isTruncated = jsonProperty.ReadAsBool(key);
                        break;
                    case JsonObject.JsonValueString:
                        this.text = jsonProperty.ReadAsString(key);
                        break;
                    default:
                        break;
                }                
            }
        }

        /// <summary>
        /// Writes attributes to XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
        {
            writer.WriteAttributeValue(XmlAttributeNames.BodyType, this.BodyType);
        }

        /// <summary>
        /// Writes elements to XML.
        /// </summary>
        /// <param name="writer">The writer.</param>
        internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                writer.WriteValue(this.Text, XmlElementNames.UniqueBody);
            }
        }

        /// <summary>
        /// Serializes the property to a Json value.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns>
        /// A Json value (either a JsonObject, an array of Json values, or a Json primitive)
        /// </returns>
        internal override object InternalToJson(ExchangeService service)
        {
            JsonObject jsonProperty = new JsonObject();

            jsonProperty.Add(XmlAttributeNames.BodyType, this.BodyType);
            jsonProperty.Add(XmlAttributeNames.IsTruncated, this.IsTruncated);

            if (!string.IsNullOrEmpty(this.Text))
            {
                jsonProperty.Add(JsonObject.JsonValueString, this.Text);
            }

            return jsonProperty;
        }

        /// <summary>
        /// Gets the type of the unique body's text.
        /// </summary>
        public BodyType BodyType
        {
            get
            {
                return this.bodyType; 
            }
        }

        /// <summary>
        /// Gets the text of the unique body.
        /// </summary>
        public string Text
        {
            get 
            {
                return this.text;
            }
        }

        /// <summary>
        /// Gets whether the unique body is truncated.
        /// </summary>
        public bool IsTruncated
        {
            get
            {
                return this.isTruncated;
            }
        }

        #region Object method overrides
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return (this.Text == null) ? string.Empty : this.Text;
        }
        #endregion
    }
}
