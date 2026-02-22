import { Component, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  startWith,
  switchMap,
  catchError,
  of,
  map,
  BehaviorSubject,
  Subject,
  concatMap,
} from 'rxjs';
import { GamesService } from '../../data-access/games.service';
import { GameModel } from '../../data-access/game.model';
import { GamesTableComponent } from '../../ui/games-table/games-table.component';
import { ConfirmModalComponent } from '../../../../shared/components/confirm-modal/confirm-modal.component';

interface GamesState {
  games: GameModel[];
  loading: boolean;
  error: string | null;
}

interface DeleteState {
  loading: boolean;
  error: string | null;
}

@Component({
  selector: 'app-games-list-page',
  standalone: true,
  imports: [GamesTableComponent],
  template: `
    <div class="container py-4">
      <div class="d-flex justify-content-between align-items-center mb-4">
        <h1>Video Games Catalog {{ games().length }}</h1>
        <button class="btn btn-primary" (click)="onAdd()">Add Game</button>
      </div>

      @if (error()) {
        <div class="alert alert-danger" role="alert">
          {{ error() }}
        </div>
      }

      <app-games-table
        [games]="games()"
        [loading]="loading()"
        (update)="onUpdate($event)"
        (delete)="onDelete($event)"
      />
    </div>
  `,
})
export class GamesListPageComponent {
  private gamesService = inject(GamesService);
  private router = inject(Router);
  private modalService = inject(NgbModal);

  private reload$ = new BehaviorSubject<void>(undefined);
  private deleteAction$ = new Subject<GameModel>();

  private gamesState = toSignal(
    this.reload$.pipe(
      switchMap(() =>
        this.gamesService.getAll().pipe(
          map((games) => ({ games, loading: false, error: null })),
          catchError(() =>
            of({
              games: [],
              loading: false,
              error: 'Failed to load games. Please try again.',
            } as GamesState),
          ),
          startWith({ games: [], loading: true, error: null }),
        ),
      ),
    ),
    {
      initialValue: {
        games: [],
        loading: true,
        error: null,
      } as GamesState,
    },
  );

  private deleteEffect = toSignal(
    this.deleteAction$.pipe(
      concatMap((game) =>
        this.gamesService.delete(game.id).pipe(
          map(() => {
            this.reload$.next();
            return { loading: false, error: null };
          }),
          catchError(() =>
            of({
              loading: false,
              error: 'Failed to delete game. Please try again.',
            } as DeleteState),
          ),
          startWith({ loading: true, error: null }),
        ),
      ),
    ),
    { initialValue: { loading: false, error: null } as DeleteState },
  );

  games = computed(() => this.gamesState().games);
  loading = computed(() => this.gamesState().loading);
  error = computed(() => this.gamesState().error || this.deleteEffect().error);

  onAdd(): void {
    this.router.navigate(['/games', 'add']);
  }

  onUpdate(game: GameModel): void {
    this.router.navigate(['/games', game.id, 'update']);
  }

  onDelete(game: GameModel): void {
    const modalRef = this.modalService.open(ConfirmModalComponent);
    modalRef.componentInstance.title = 'Delete Game';
    modalRef.componentInstance.message = `Are you sure you want to delete "${game.title}"?`;
    modalRef.componentInstance.confirmText = 'Delete';

    modalRef.result.then(
      (confirmed) => {
        if (confirmed) {
          this.deleteAction$.next(game);
        }
      },
      () => {},
    );
  }
}
