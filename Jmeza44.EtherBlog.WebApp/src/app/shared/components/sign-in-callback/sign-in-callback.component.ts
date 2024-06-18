import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'ether-sign-in-callback',
  template: `
    <p>
      sign-in-callback works!
    </p>
  `,
  styles: ``
})
export class SignInCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  async ngOnInit() {
    await this.authService.handleCallback();
    this.router.navigate(['/']);
  }
}
