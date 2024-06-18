import { Component } from '@angular/core';
import { AuthService } from './shared/services/auth-service.service';

@Component({
  selector: 'ether-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'EtherBlogWebApp';

  constructor(public authService: AuthService) {}

  signIn() {
    this.authService.login();
  }
}
