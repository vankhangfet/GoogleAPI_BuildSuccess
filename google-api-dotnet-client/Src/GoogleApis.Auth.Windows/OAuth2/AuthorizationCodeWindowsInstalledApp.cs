﻿/*
Copyright 2013 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2.Flows;

namespace Google.Apis.Auth.OAuth2
{
    /// <summary>
    /// OAuth 2.0 authorization code flow for a Windows 8.1 installed application that persists end-user credentials.
    /// <remarks>
    /// This installed app class uses an internal <see cref="AuthorizationCodeInstalledApp"/> with a
    /// <see cref="Google.Apis.Auth.OAuth2.AuthorizationCodeBroker"/> for retrieving the authorization code. 
    /// </remarks>
    /// </summary>
    public sealed class AuthorizationCodeWindowsInstalledApp : IAuthorizationCodeInstalledApp
    {
        private readonly IAuthorizationCodeInstalledApp innerInstallApp;

        /// <summary>
        /// Constructs a new authorization code for Windows Store application targeting an installed application flow.
        /// </summary>
        /// <param name="authorizationCodeFlowInitializer">An authorization code flow initializer.</param>
        public AuthorizationCodeWindowsInstalledApp(AuthorizationCodeFlow.Initializer authorizationCodeFlowInitializer)
        {
            innerInstallApp = new AuthorizationCodeInstalledApp(
                new AuthorizationCodeFlow(authorizationCodeFlowInitializer), new AuthorizationCodeBroker());
        }

        /// <summary>
        /// Constructs a new authorization code for Windows Store application targeting an installed application flow.
        /// </summary>
        /// <param name="flow">An authorization code flow.</param>
        public AuthorizationCodeWindowsInstalledApp(IAuthorizationCodeFlow flow)
        {
            innerInstallApp = new AuthorizationCodeInstalledApp(flow, new AuthorizationCodeBroker());
        }

        #region IAuthorizationCodeInstalledApp Members

        public IAuthorizationCodeFlow Flow
        {
            get { return innerInstallApp.Flow; }
        }

        public ICodeReceiver CodeReceiver
        {
            get { return innerInstallApp.CodeReceiver; }
        }

        public async Task<UserCredential> AuthorizeAsync(string userId, CancellationToken taskCancellationToken)
        {
            return await innerInstallApp.AuthorizeAsync(userId, taskCancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
