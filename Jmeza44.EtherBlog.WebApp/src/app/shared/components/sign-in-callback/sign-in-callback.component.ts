import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'ether-sign-in-callback',
  template: ``,
  styles: ``
})
export class SignInCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  async ngOnInit() {
    await this.authService.handleSignInCallback();
    this.router.navigate(['/']);
  }
}
