// =====================================================================
//  File:		CrmServiceUtility.cs
//  Summary:	TODO
// =====================================================================
//
//  This file is part of the Microsoft CRM 4.0 SDK Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================

using System;
using System.Collections.Generic;
using System.Text;
using activiser.WebService.OutputGateway;
using CrmSdk = activiser.WebService.OutputGateway.CrmSdk;
using activiser.WebService.OutputGateway.CrmSdk;
using MetadataServiceSdk = activiser.WebService.OutputGateway.MetadataServiceSdk;
using activiser.WebService.OutputGateway.MetadataServiceSdk;

namespace Microsoft.Crm.Sdk.Utility
{
	public class CrmServiceUtility
	{
		public static CrmService GetCrmService()
		{
			return GetCrmService(null, null);
		}

		public static CrmService GetCrmService(string organizationName)
		{
			return GetCrmService(null, organizationName);
		}

		/// <summary>
		/// Set up the CRM Service.
		/// </summary>
		/// <param name="organizationName">My Organization</param>
		/// <returns>CrmService configured with AD Authentication</returns>
		public static CrmService GetCrmService(string crmServerUrl, string organizationName)
		{
			// Get the CRM Users appointments
			// Setup the Authentication Token
			CrmSdk.CrmAuthenticationToken token = new CrmSdk.CrmAuthenticationToken();
			token.OrganizationName = organizationName;
		
			CrmService service = new CrmService();

			if (crmServerUrl != null &&
				crmServerUrl.Length > 0)
			{
				UriBuilder builder = new UriBuilder(crmServerUrl);				
				builder.Path = "//MSCRMServices//2007//CrmService.asmx";
				service.Url = builder.Uri.ToString();
			}

			service.Credentials = System.Net.CredentialCache.DefaultCredentials;
			service.CrmAuthenticationTokenValue = token;

			return service;
		}

		/// <summary>
		/// Set up the CRM Metadata Service.
		/// </summary>
		/// <param name="organizationName">My Organization</param>
		/// <returns>MetadataService configured with AD Authentication</returns>
		public static MetadataService GetMetadataService(string crmServerUrl, string organizationName)
		{
			// Get the CRM Users appointments
			// Setup the Authentication Token
			MetadataServiceSdk.CrmAuthenticationToken token = new MetadataServiceSdk.CrmAuthenticationToken();
			token.OrganizationName = organizationName;

			MetadataService service = new MetadataService();

			if (crmServerUrl != null &&
				crmServerUrl.Length > 0)
			{
				UriBuilder builder = new UriBuilder(crmServerUrl);				
				builder.Path = "//MSCRMServices//2007//MetadataService.asmx";
				service.Url = builder.Uri.ToString();
			}
			
			service.Credentials = System.Net.CredentialCache.DefaultCredentials;
			service.CrmAuthenticationTokenValue = token;

			return service;
		}
		
		/// <summary>
		/// Create a Crm label
		/// </summary>
		/// <param name="label">string label value for LocLabel</param>
		/// <param name="langCode">Language Code for CrmLabel</param>
		/// <returns></returns>
        public static MetadataServiceSdk.CrmLabel CreateSingleLabel(string label, int langCode)
		{
			MetadataServiceSdk.CrmNumber crmNumber = new MetadataServiceSdk.CrmNumber();
			crmNumber.Value = langCode;

			MetadataServiceSdk.LocLabel locLabel = new MetadataServiceSdk.LocLabel();
			locLabel.LanguageCode = crmNumber;
			locLabel.Label = label;

			MetadataServiceSdk.CrmLabel crmLabel = new MetadataServiceSdk.CrmLabel();
			crmLabel.LocLabels = new MetadataServiceSdk.LocLabel[] { locLabel };

			return crmLabel;
		}
	}


}
