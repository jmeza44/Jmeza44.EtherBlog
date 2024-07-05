import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../services/auth-service.service';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  if (authService.isLoggedIn && authService.user) {
    // Clone the request and set the Authorization header
    const authReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${authService.user.access_token}`),
    });
    // Proceed with the authenticated request
    return next(authReq);
  } else {
    // Proceed with the original request if not authenticated
    return next(req);
  }
};
