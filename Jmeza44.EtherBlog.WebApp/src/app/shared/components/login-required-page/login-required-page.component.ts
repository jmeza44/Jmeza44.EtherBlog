import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'ether-login-required-page',
  templateUrl: './login-required-page.component.html',
  styleUrl: './login-required-page.component.scss'
})
export class LoginRequiredPageComponent {
  constructor(public authService: AuthService) {}
}
