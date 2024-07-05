import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client-ts';
import { oidcConfig } from '../authentication/auth-config';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userManager: UserManager;
  private currentUser: User | null = null;

  constructor() {
    this.userManager = new UserManager(oidcConfig);

    this.userManager.getUser().then(user => {
      this.currentUser = user;
    });

    this.userManager.events.addUserLoaded(user => {
      this.currentUser = user;
    });

    this.userManager.events.addUserUnloaded(() => {
      this.currentUser = null;
    });
  }

  login() {
    this.userManager.signinRedirect();
  }

  async handleSignInCallback() {
    const user = await this.userManager.signinRedirectCallback();
    this.currentUser = user;
    console.log(this.currentUser);
  }

  async handleSignOutCallback() {
    await this.userManager.signoutCallback();
    this.currentUser = null;
  }

  logout() {
    this.userManager.signoutRedirect();
  }

  get isLoggedIn(): boolean {
    return this.currentUser !== null;
  }

  get user(): User | null {
    return this.currentUser;
  }

  get isViewer(): boolean {
    return this.user?.profile['role'] === 'Viewer';
  }

  get isEditor(): boolean {
    return this.user?.profile['role'] === 'Editor';
  }

  get isAdmin(): boolean {
    return this.user?.profile['role'] === 'Admin';
  }
}
