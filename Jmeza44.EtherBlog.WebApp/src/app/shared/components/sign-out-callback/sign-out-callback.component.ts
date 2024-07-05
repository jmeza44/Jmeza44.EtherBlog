import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'ether-sign-out-callback',
  template: ``,
  styles: ``
})
export class SignOutCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  async ngOnInit() {
    await this.authService.handleSignOutCallback();
    this.router.navigate(['/']);
  }
}
