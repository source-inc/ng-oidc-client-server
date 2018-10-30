# ng-oidc-client-server
Example implementation of IdentityServer to test ng-oidc-client

>This implementation is solely for the purpose of testing and should not be used in production!

# Live Demo
https://ng-oidc-client-server.azurewebsites.net/account/login
> Routing is disabled
> Account registration is disabled
> Server resets daily
> if server is not up, the first call might take a few seconds longer


# Test accounts IdentityServer4 Quickstart 
See [IdentityServer4 TestUsers.cs](https://github.com/IdentityServer/IdentityServer4.Demo/blob/master/src/IdentityServer4Demo/Quickstart/TestUsers.cs)
Username: `bob`
Password: `bob`

Username: `alice`
Password: `alice`

# Example configuration for ng-oidc-client in a local environment
```javascript
NgOidcClientModule.forRoot({
  oidc_config: {
    authority: 'https://localhost:5001',
    client_id: 'ng-oidc-client-identity',
    redirect_uri: 'http://localhost:4200/callback.html',
    response_type: 'id_token token',
    scope: 'openid profile offline_access api1',
    post_logout_redirect_uri: 'http://localhost:4200/signout-callback.html',
    silent_redirect_uri: 'http://localhost:4200/renew-callback.html',
    accessTokenExpiringNotificationTime: 10,
    automaticSilentRenew: true,
    userStore: getWebStorageStateStore
  }
})
```
