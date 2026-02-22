import { Component, input, output } from '@angular/core';
import { SlicePipe } from '@angular/common';
import { GameModel } from '../../data-access/game.model';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';

@Component({
  selector: 'app-games-table',
  standalone: true,
  imports: [SlicePipe, LoadingSpinnerComponent],
  templateUrl: './games-table.component.html',
})
export class GamesTableComponent {
  games = input<GameModel[]>([]);
  loading = input(false);

  update = output<GameModel>();
  remove = output<GameModel>();
}
