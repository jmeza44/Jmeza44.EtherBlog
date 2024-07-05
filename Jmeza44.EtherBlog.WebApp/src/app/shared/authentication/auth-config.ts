import { UserManagerSettings } from 'oidc-client-ts';

export const oidcConfig: UserManagerSettings = {
  authority: 'https://localhost:4686',
  client_id: 'EtherBlogWebApp',
  redirect_uri: 'http://localhost:4200/sign-in-callback',
  response_type: 'code',
  scope: 'openid profile email role ether-blog-api.read ether-blog-api.write',
  post_logout_redirect_uri: 'http://localhost:4200/sign-out-callback',
  silent_redirect_uri: 'http://localhost:4200/silent-renew.html',
  automaticSilentRenew: true,
  loadUserInfo: true,
};
