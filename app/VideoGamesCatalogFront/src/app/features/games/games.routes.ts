import { Routes } from '@angular/router';
import { GamesListPageComponent } from './pages/games-list-page/games-list-page.component';
import { GameUpdatePageComponent } from './pages/game-update-page/game-update-page.component';

export const GAMES_ROUTES: Routes = [
  { path: '', component: GamesListPageComponent },
  { path: 'add', component: GameUpdatePageComponent },
  { path: ':id/update', component: GameUpdatePageComponent },
];
