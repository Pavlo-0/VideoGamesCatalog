import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SlicePipe } from '@angular/common';
import { GameModel } from '../../data-access/game.model';

@Component({
  selector: 'app-games-table',
  standalone: true,
  imports: [SlicePipe],
  templateUrl: './games-table.component.html',
})
export class GamesTableComponent {
  @Input() games: GameModel[] = [];
  @Input() loading = false;

  @Output() update = new EventEmitter<GameModel>();
  @Output() delete = new EventEmitter<GameModel>();
}
