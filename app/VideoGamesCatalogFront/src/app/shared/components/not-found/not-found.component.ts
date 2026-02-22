import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="container text-center py-5">
      <h1 class="display-1 text-muted">404</h1>
      <h2 class="mb-4">Page Not Found</h2>
      <p class="text-muted mb-4">The page you're looking for doesn't exist.</p>
      <a routerLink="/games" class="btn btn-primary">Go to Games</a>
    </div>
  `,
})
export class NotFoundComponent {}
