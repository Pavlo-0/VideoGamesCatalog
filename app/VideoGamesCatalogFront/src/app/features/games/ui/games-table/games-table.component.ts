import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SlicePipe } from '@angular/common';
import { GameModel } from '../../data-access/game.model';

@Component({
  selector: 'app-games-table',
  standalone: true,
  imports: [SlicePipe],
  template: `
    {{ games.length }}
    @if (loading) {
      <div class="text-center py-4">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    } @else if (games.length === 0) {
      <div class="alert alert-info" role="alert">No games found.</div>
    } @else {
      <div class="table-responsive">
        <table class="table table-striped table-hover">
          <thead class="table-dark">
            <tr>
              <th scope="col">Title</th>
              <th scope="col">Description</th>
              <th scope="col" style="width: 150px;">Actions</th>
            </tr>
          </thead>
          <tbody>
            @for (game of games; track game.id) {
              <tr>
                <td>{{ game.title }}</td>
                <td>
                  {{ game.description | slice: 0 : 100
                  }}{{ game.description.length > 100 ? '...' : '' }}
                </td>
                <td>
                  <button
                    class="btn btn-sm btn-outline-primary me-1"
                    title="Edit"
                    (click)="update.emit(game)"
                  >
                    <i class="bi bi-pencil"></i> Edit
                  </button>
                  <button
                    class="btn btn-sm btn-outline-danger"
                    title="Delete"
                    (click)="delete.emit(game)"
                  >
                    <i class="bi bi-trash"></i> Delete
                  </button>
                </td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    }
  `,
})
export class GamesTableComponent {
  @Input() games: GameModel[] = [];
  @Input() loading = false;

  @Output() update = new EventEmitter<GameModel>();
  @Output() delete = new EventEmitter<GameModel>();
}
