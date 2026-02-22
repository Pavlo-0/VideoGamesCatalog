import { Component, computed, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import {
  map,
  of,
  catchError,
  startWith,
  Subject,
  tap,
  concatMap,
  Observable,
} from 'rxjs';
import { GamesService } from '../../data-access/games.service';
import { GameModel, GameFormValue } from '../../data-access/game.model';
import { GameFormComponent } from '../../ui/game-form/game-form.component';
import { LoadingSpinnerComponent } from '../../../../shared/components/loading-spinner/loading-spinner.component';

interface GameState {
  data: GameModel | null;
  loading: boolean;
  error: string | null;
}

interface SaveState {
  loading: boolean;
  error: string | null;
}

@Component({
  selector: 'app-game-update-page',
  standalone: true,
  imports: [GameFormComponent, LoadingSpinnerComponent],
  template: `
    <div class="container py-4">
      <div class="row justify-content-center">
        <div class="col-md-8 col-lg-6">
          <h1 class="mb-4">
            {{ mode() === 'add' ? 'Add New Game' : 'Update Game' }}
          </h1>

          @if (loading()) {
            <app-loading-spinner />
          } @else if (error()) {
            <div class="alert alert-danger" role="alert">
              {{ error() }}
            </div>
          } @else {
            <app-game-form
              [initialData]="game()"
              [mode]="mode()"
              [saving]="loading()"
              (save)="onSave($event)"
              (cancel)="onCancel()"
            />
          }
        </div>
      </div>
    </div>
  `,
})
export class GameUpdatePageComponent {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private gamesService = inject(GamesService);

  private gameState = toSignal(
    (() => {
      const id = this.route.snapshot.paramMap.get('id');

      if (!id) {
        return of({ data: null, loading: false, error: null } as GameState);
      }

      return this.gamesService.getById(id).pipe(
        map(
          (game) => ({ data: game, loading: false, error: null }) as GameState,
        ),
        catchError(() =>
          of({
            data: null,
            loading: false,
            error: 'Failed to load game. Please try again.',
          } as GameState),
        ),
        startWith({ data: null, loading: true, error: null } as GameState),
      );
    })(),
    { initialValue: { data: null, loading: false, error: null } as GameState },
  );

  private saveAction$ = new Subject<GameFormValue>();

  private saveEffect = toSignal(
    this.saveAction$.pipe(
      concatMap((formValue) => {
        const isAdd = this.mode() === 'add';
        const operation$: Observable<void> = isAdd
          ? this.gamesService.add(formValue).pipe(map(() => void 0))
          : this.gamesService
              .update(
                this.game()?.id!,
                formValue,
                this.game()?.rowVersion ?? null,
              )
              .pipe(map(() => void 0));

        return operation$.pipe(
          tap(() => this.router.navigate(['/games'])),
          map(() => ({ loading: false, error: null }) as SaveState),
          catchError(() =>
            of({
              loading: false,
              error: 'Failed to save game. Please try again.',
            } as SaveState),
          ),
          startWith({ loading: true, error: null }),
        );
      }),
    ),
    { initialValue: { loading: false, error: null } as SaveState },
  );

  mode = computed(
    () => (this.gameState().data?.id ? 'update' : 'add') as 'add' | 'update',
  );
  game = computed(() => this.gameState().data);
  loading = computed(
    () => this.gameState().loading || this.saveEffect().loading,
  );
  error = computed(() => this.gameState().error || this.saveEffect().error);

  onSave(formValue: GameFormValue): void {
    this.saveAction$.next(formValue);
  }

  onCancel(): void {
    this.router.navigate(['/games']);
  }
}
