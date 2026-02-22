import { Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

export const routes: Routes = [
  { path: '', redirectTo: 'games', pathMatch: 'full' },
  {
    path: 'games',
    loadChildren: () =>
      import('./features/games/games.routes').then((m) => m.GAMES_ROUTES),
  },
  { path: '**', component: NotFoundComponent },
];
